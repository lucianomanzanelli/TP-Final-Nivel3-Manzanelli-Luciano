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
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (!Seguridad.SesionActiva(Session["persona"]))
                    {
                        return;
                    }
                    else
                    {
                        Persona user = (Persona)Session["persona"];
                        txtEmail.Text = user.Email;
                        txtNombre.Text = user.Nombre;
                        txtApellido.Text = user.Apellido;
                        if (user.ImagenPerfil != null)
                        {
                            imgPerfil.ImageUrl = "~/Images/" + user.ImagenPerfil;
                        }
                        else
                            imgPerfil.ImageUrl = "https://www.bunko.pet/__export/1624827469365/sites/debate/img/2021/06/27/basset-hound_crop1624827361677.jpg_554688468.jpg";
                    }
                }



            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }
    }
}