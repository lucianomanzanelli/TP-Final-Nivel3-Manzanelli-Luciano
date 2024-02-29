using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select id, descripcion from marcas");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca marca = new Marca();
                    marca.Id = (int)datos.Lector["id"];
                    marca.Descripcion = Convert.ToString(datos.Lector["descripcion"]);

                    lista.Add(marca);
                }

                datos.cerrarConexion();
                return lista;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
