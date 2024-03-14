using dominio;
using Microsoft.Win32;
using negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebStore_web
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            imgAvatar.ImageUrl = "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png";

            if (!(Page is Login || Page is Default || Page is Error || Page is Registro 
                || Page is Favoritos || Page is Detalles))
            {
                if (!Seguridad.SesionActiva(Session["persona"]))
                {
                    Response.Redirect("Login.aspx");
                }

            }

            if (Seguridad.SesionActiva(Session["persona"]))
            {
                Persona user = (Persona)Session["persona"];

                if (user.ImagenPerfil != null)
                {
                    imgAvatar.ImageUrl = "~/Images/" + user.ImagenPerfil;
                }
                else
                {
                    imgAvatar.ImageUrl = "https://www.bunko.pet/__export/1624827469365/sites/debate/img/2021/06/27/basset-hound_crop1624827361677.jpg_554688468.jpg";
                }
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx", false);

        }

        public List<Articulo> ListaArticulos { get; set; }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            ListaArticulos = negocio.listar();

            string textoBusqueda = txtBuscar.Value.ToLower();


            List<Articulo> listaFiltrada = ListaArticulos.FindAll(x =>
                                            NormalizarTexto(x.Nombre).ToLower().Contains(NormalizarTexto(textoBusqueda)) ||
                                            NormalizarTexto(x.Descripcion).ToLower().Contains(NormalizarTexto(textoBusqueda)));

            string txtBusqueda = txtBuscar.Value;
            Session.Add("txtBusqueda", txtBusqueda);
            Session.Add("busqueda", listaFiltrada);
            Response.Redirect("/", false);
        }

        //tiene en cuenta en la busqueda las palabras con tilde
        string NormalizarTexto(string texto)
        {
            string txtNormalizado = new string(texto.Normalize(NormalizationForm.FormD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray());
            return txtNormalizado;
        }

    }
}