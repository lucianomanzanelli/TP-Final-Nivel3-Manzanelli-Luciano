using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select id, descripcion from categorias");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria categoria = new Categoria();
                    categoria.Id = (int)datos.Lector["id"];
                    categoria.Descripcion = Convert.ToString(datos.Lector["descripcion"]);

                    lista.Add(categoria);
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
