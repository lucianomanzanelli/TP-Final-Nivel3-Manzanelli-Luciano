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
        public List<Articulo> ListaBusqueda { get; set; }

        List<int> lstFavoritos = new List<int> { };
        public bool EsBusqueda { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["busqueda"] != null)
                {
                    EsBusqueda = true;

                    ListaBusqueda = (List<Articulo>)Session["busqueda"];
                    string txtBusqueda = Session["txtBusqueda"].ToString();

                    Session.Add("filtroBusq", ListaBusqueda);

                    CargarMarcas();
                    CargarFavoritos();

                    repRepetidor.DataSource = ListaBusqueda;
                    repRepetidor.DataBind();

                    if (!((IEnumerable)repRepetidor.DataSource).Cast<object>().Any())
                    {
                        lblVacio.Text = "No hay articulos que coincidan con la búsqueda.";
                    }

                    lblBusqueda.Text = "Tu búsqueda: '" + txtBusqueda + "'";

                    Session.Remove("txtBusqueda");
                    Session.Remove("busqueda");
                }
                else
                {
                    EsBusqueda = false;
                    ListaBusqueda = null;

                    ArticuloNegocio negocio = new ArticuloNegocio();
                    ListaArticulos = negocio.listar();

                    CargarFavoritos();

                    repRepetidor.DataSource = ListaArticulos;
                    repRepetidor.DataBind();
                }
            }
            else if (ddlMarca.SelectedItem == null)
            {
                return;
            }
            else
            {
                EsBusqueda = true;
                List<Articulo> listaFiltrada = (List<Articulo>)Session["filtroBusq"];

                bool filtroAplicado = false;


                if (int.TryParse(txtMin.Text, out int min) && int.TryParse(txtMax.Text, out int max) && min <= max)
                    listaFiltrada = listaFiltrada.Where(a => a.Precio >= min && a.Precio <= max).ToList();


                // Verificar si se ha seleccionado un filtro distinto al precio
                if ((ddlPrecio.Text == "Menor a mayor" || ddlPrecio.Text == "Mayor a menor") && ddlMarca.SelectedItem != null)
                {
                    filtroAplicado = true;
                }

                if (ddlPrecio.Text == "Precio" && ddlMarca.Text == "0")
                {
                    //Carga la lista completa

                }
                else if (ddlPrecio.Text == "Precio" && ddlMarca.SelectedItem != null)
                {
                    listaFiltrada = listaFiltrada.Where(x => x.Marca.Descripcion.ToLower().Contains(ddlMarca.SelectedItem.Text.ToLower())).ToList();
                }
                else
                {
                    if (ddlPrecio.Text == "Menor a mayor")
                    {
                        listaFiltrada = listaFiltrada.OrderBy(a => a.Precio).ToList();
                        if (filtroAplicado && ddlMarca.Text != "0")
                        {
                            listaFiltrada = listaFiltrada.Where(x => x.Marca.Descripcion.ToLower().Contains(ddlMarca.SelectedItem.Text.ToLower())).ToList();
                        }
                    }
                    else if (ddlPrecio.Text == "Mayor a menor")
                    {
                        listaFiltrada = listaFiltrada.OrderByDescending(a => a.Precio).ToList();
                        if (filtroAplicado && ddlMarca.Text != "0")
                        {
                            listaFiltrada = listaFiltrada.Where(x => x.Marca.Descripcion.ToLower().Contains(ddlMarca.SelectedItem.Text.ToLower())).ToList();
                        }
                    }
                    else if (filtroAplicado && ddlMarca.Text != "0")
                    {
                        listaFiltrada = listaFiltrada.Where(x => x.Marca.Descripcion.ToLower().Contains(ddlMarca.SelectedItem.Text.ToLower())).ToList();
                    }
                }

                repRepetidor.DataSource = listaFiltrada;
                repRepetidor.DataBind();
            }
        }

        private void CargarFavoritos()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
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
        }

        public void CargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            List<dominio.Marca> lista = negocio.listar();

            ListItem marcaDefault = new ListItem("Marca", "0");
            ddlMarca.Items.Insert(0, marcaDefault);

            foreach (Marca marca in lista)
            {
                ListItem listItem = new ListItem(marca.Descripcion, marca.Id.ToString());
                ddlMarca.Items.Add(listItem);
            }
        }

        protected void btnFavorito_Command(object sender, CommandEventArgs e)
        {
            if (Seguridad.SesionActiva(Session["persona"]))
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
            else
            {
                Response.Redirect("Login.aspx", false);
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