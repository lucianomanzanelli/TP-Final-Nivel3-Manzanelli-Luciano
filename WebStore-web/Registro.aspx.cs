using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebStore_web
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
               
                lblError.Text = "Completa los campos faltantes.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "setTimeout(function() { document.getElementById('" + lblError.ClientID + "').innerHTML = ''; }, 3000);", true);
                return;
            }

            Persona user = new Persona();
            PersonaNegocio negocio = new PersonaNegocio();
            user.Email = txtEmail.Text;
            user.Pass = txtPassword.Text;

            if (negocio.validarEmail(txtEmail.Text))
            {
                
                lblError.Text = "El email ya está registrado.";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "setTimeout(function() { document.getElementById('" + lblError.ClientID + "').innerHTML = ''; }, 3000);", true);

            }
            else
            {
                user.Id = negocio.insertarNuevo(user);
                Session.Add("persona", user);

                EmailService email = new EmailService();
                email.armarCorreo(user.Email, "Bienvenid@!", "Tu clave es: " + txtPassword.Text);
                email.enviarEmail();

                Response.Redirect("Default.aspx", false);
            }



        }
    }
}