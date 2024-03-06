using dominio;
using negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebStore_web
{
    public partial class Favoritos : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Seguridad.SesionActiva(Session["persona"]))
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    Persona persona = (Persona)Session["persona"];
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    ListaArticulos = negocio.listarFavs(persona.Id.ToString());

                    repRepetidor.DataSource = ListaArticulos;
                    repRepetidor.DataBind();
                }


            }
        }
    }
}