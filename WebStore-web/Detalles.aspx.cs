using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebStore_web
{
    public partial class Detalles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";

            if (id != "" && !IsPostBack)
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

            }
            else
            {
                Response.Redirect("Default.aspx", false);
            }
        }
    }
}