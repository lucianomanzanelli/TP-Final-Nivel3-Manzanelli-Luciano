using dominio;
using negocio;
using System;
using System.Collections.Generic;

namespace WebStore_web
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["busqueda"] != null)
                {
                    ListaArticulos = (List<Articulo>)Session["busqueda"];

                    repRepetidor.DataSource = ListaArticulos;
                    repRepetidor.DataBind();

                    Session.Remove("busqueda");
                }
                else
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    ListaArticulos = negocio.listar();

                    repRepetidor.DataSource = ListaArticulos;
                    repRepetidor.DataBind();

                }

            }

        }

        public string ObtenerUrlImagen(object imagenUrl)
        {
            string url = imagenUrl as string;

            // Verificar si la URL es válida
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
            {
                return url; // La URL es válida
            }
            else
            {
                // La URL no es válida, usar una URL predeterminada
                return "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png";
            }
        }
        public string ImagenDeRespaldo()
        {
            return "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png";
        }

    }
}