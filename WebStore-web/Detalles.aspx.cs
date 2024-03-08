using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebStore_web
{
    public partial class Detalles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";

            if (!string.IsNullOrEmpty(id))
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo articulo = (negocio.listar(id)[0]);

                Session.Add("articulo", articulo);

                lblMarca.Text = articulo.Marca.Descripcion;
                lblNombre.Text = articulo.Nombre;
                lblPrecio.Text = articulo.Precio.ToString("$0.00");
                lblDescripcion.Text = articulo.Descripcion;

                if (!string.IsNullOrEmpty(articulo.ImagenUrl) || !string.IsNullOrWhiteSpace(articulo.ImagenUrl))
                {
                    imgArt.ImageUrl = Utilidades.ObtenerUrlImagen(articulo.ImagenUrl);

                }

                esFavorito();


            }
            else
            {
                Response.Redirect("Default.aspx", false);
            }

        }

        protected bool esFavorito()
        {
            if (Seguridad.SesionActiva(Session["persona"]))
            {

                ArticuloNegocio negocio = new ArticuloNegocio();

                Persona persona = (Persona)Session["persona"];
                int idPersona = (int)persona.Id;

                Articulo articulo = (Articulo)Session["articulo"];
                int idArticulo = (int)articulo.Id;

                lstFavoritos = negocio.IDsFavoritos(idPersona);

                bool esFavorito = lstFavoritos.Contains(idArticulo);
                return esFavorito;
            }
            else
            {
                return false;
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            string urlAnterior = "";

            if (Request.UrlReferrer != null)
            {
                urlAnterior = Request.UrlReferrer.ToString();
            }
            else
            {
                // Si no hay URL anterior, redirige a la raíz del sitio
                urlAnterior = "/";
            }

            // Redirigir a la página anterior
            Response.Redirect(urlAnterior);
        }

        List<int> lstFavoritos = new List<int> { };

        protected void btnFavorito_Command(object sender, CommandEventArgs e)
        {
            if (Seguridad.SesionActiva(Session["persona"]))
            {


                ArticuloNegocio negocio = new ArticuloNegocio();

                // Obtener el ID de la persona desde la sesión
                Persona persona = (Persona)Session["persona"];
                int idPersona = (int)persona.Id;

                // Obtener el ID del articulo desde la sesión
                Articulo articulo = (Articulo)Session["articulo"];
                int idArticulo = (int)articulo.Id;


                if (idPersona != 0)
                {
                    // Obtener los IDs de los artículos favoritos del usuario
                    lstFavoritos = negocio.IDsFavoritos(idPersona);
                }

                // Si ya es favorito, negocio.eliminar
                if (lstFavoritos.Contains(idArticulo))
                {
                    negocio.eliminarFav(idArticulo, idPersona);
                }
                else
                    negocio.agregarFav(idArticulo, idPersona);


                // Recargo la página
                Response.Redirect(Request.RawUrl, false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                lblError.Text = "Debes iniciar sesión.";
            }
        }
    }
}