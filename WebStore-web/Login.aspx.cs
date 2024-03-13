using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace WebStore_web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Seguridad.SesionActiva(Session["persona"]))
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Persona persona = new Persona();
            PersonaNegocio negocio = new PersonaNegocio();

            try
            {
                persona.Email = txtEmail.Text;
                persona.Pass = txtPassword.Text;

                if (!negocio.validarEmail(txtEmail.Text))
                {
                    lblError.Text = "El email no está registrado.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(),
                        "HideLabel", "setTimeout(function() { document.getElementById('" +
                        lblError.ClientID + "').innerHTML = ''; }, 3000);", true);
                }
                else
                {
                    if (negocio.Login(persona))
                    {
                        Session.Add("persona", persona);
                        Response.Redirect("Default.aspx", false);
                    }
                    else
                    {
                        lblError.Text = "Email o contraseña incorrectos.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(),
                            "HideLabel", "setTimeout(function() { document.getElementById('" +
                            lblError.ClientID + "').innerHTML = ''; }, 3000);", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}