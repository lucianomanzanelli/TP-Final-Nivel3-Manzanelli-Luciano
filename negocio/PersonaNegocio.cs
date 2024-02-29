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
    }
}
