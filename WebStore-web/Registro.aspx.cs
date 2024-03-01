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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            Persona user = new Persona();
            PersonaNegocio negocio = new PersonaNegocio();
            user.Email = txtEmail.Text;
            user.Pass = txtPassword.Text;

            user.Id = negocio.insertarNuevo(user);
            Session.Add("persona", user);

            EmailService email = new EmailService();
            email.armarCorreo(user.Email, "Bienvenid@!", "Tu clave es: " + txtPassword.Text);
            email.enviarEmail();

            Response.Redirect("Default.aspx", false);

        }
    }
}