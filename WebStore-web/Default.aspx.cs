using dominio;
using negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
                    repRepetidor.DataBind();

                }

            }

        }

    }
}