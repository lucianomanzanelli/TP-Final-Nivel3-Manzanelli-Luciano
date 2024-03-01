using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class PersonaNegocio
    {
        public bool Login(Persona persona)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select id, email, pass, admin, urlimagenPerfil," +
                    " nombre, apellido from users where email = @email and pass = @pass");
                datos.setearParametro("@email", persona.Email);
                datos.setearParametro("@pass", persona.Pass);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    persona.Id = (int)datos.Lector["id"];
                    persona.Admin = (bool)datos.Lector["admin"];
                    if (!(datos.Lector["urlimagenPerfil"] is DBNull))
                        persona.ImagenPerfil = datos.Lector["urlimagenperfil"].ToString();
                    if (!(datos.Lector["nombre"] is DBNull))
                        persona.Nombre = datos.Lector["nombre"].ToString();
                    if (!(datos.Lector["apellido"] is DBNull))
                        persona.Apellido = datos.Lector["apellido"].ToString();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        public void Actualizar(Persona persona)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if (!string.IsNullOrEmpty(persona.NuevaPass))
                {
                    datos.setearConsulta("update users set urlImagenPerfil = @imagen, nombre = @nombre, " +
                    "apellido = @apellido, pass = @pass where id = @id");
                    datos.setearParametro("@pass", persona.NuevaPass);
                }
                else
                {
                    datos.setearConsulta("update users set urlImagenPerfil = @imagen, nombre = @nombre, " +
                    "apellido = @apellido where id = @id");
                }
                
                datos.setearParametro("@imagen", persona.ImagenPerfil != null ? persona.ImagenPerfil : (object)DBNull.Value);
                datos.setearParametro("@nombre", persona.Nombre);
                datos.setearParametro("@apellido", persona.Apellido);
                datos.setearParametro("@id", persona.Id);
                
                datos.ejecutarAccion();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public int insertarNuevo(Persona nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into USERS(email, pass, admin) output inserted.id values(@email, @pass, 0)");
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@pass", nuevo.Pass);

                return datos.ejecutarAccionScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

    }
}
