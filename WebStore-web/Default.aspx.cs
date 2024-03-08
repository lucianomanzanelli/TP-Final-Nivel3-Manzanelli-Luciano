using dominio;
using negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Data;

namespace WebStore_web
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulos { get; set; }
        List<int> lstFavoritos = new List<int> { };

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["busqueda"] != null)
                {

                    ListaArticulos = (List<Articulo>)Session["busqueda"];

                    repRepetidor.DataSource = ListaArticulos;
                    repRepetidor.DataBind();

                    if (!((IEnumerable)repRepetidor.DataSource).Cast<object>().Any())
                    {
                        lblVacio.Text = "No hay articulos que coincidan con la búsqueda.";
                    }

                    Session.Remove("busqueda");
                }
                else
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    ListaArticulos = negocio.listar();

                    repRepetidor.DataSource = ListaArticulos;

                    if (Seguridad.SesionActiva(Session["persona"]))
                    {
                        // Obtener el ID del usuario en sesión
                        Persona persona = (Persona)Session["persona"];
                        int idUsuario = persona.Id;

                        if (idUsuario != 0)
                        {
                            // Obtener los IDs de los artículos favoritos del usuario
                            lstFavoritos = negocio.IDsFavoritos(idUsuario);
                        }

                    }

                    repRepetidor.DataBind();
                }
            }
        }


        protected void btnFavorito_Command(object sender, CommandEventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            if (e.CommandName == "MarcarFavorito")
            {
                int idArticulo = Convert.ToInt32(e.CommandArgument);

                // Obtener el ID de la persona desde la sesión
                Persona persona = (Persona)Session["persona"];
                int idPersona = (int)persona.Id;

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
        }

        protected void repRepetidor_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (!Seguridad.SesionActiva(Session["persona"]))
            {
                return;
            }
            else
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    // Accede al campo "Id" en el DataBinder para obtener el Id del artículo actual
                    int idArticulo = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Id"));

                    // Verifica si el artículo está en la lista de favoritos
                    bool esFavorito = lstFavoritos.Contains(idArticulo);

                    // Encuentra el control que contiene el ícono del favorito
                    HtmlGenericControl iconoFavorito = (HtmlGenericControl)e.Item.FindControl("iconoFavorito");

                    if (esFavorito)
                    {
                        // Cambia el ícono del favorito si es un artículo favorito
                        iconoFavorito.InnerHtml = "<i class='fa-solid fa-star'></i>";
                    }
                    else
                    {
                        // Usa el ícono original pero animado
                        iconoFavorito.InnerHtml = "<i class='fa-regular fa-star fa-beat'></i>";
                    }
                }
            }
        }


    }
}