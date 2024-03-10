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

                if (negocio.Login(persona))
                {
                    Session.Add("persona", persona);

                    if (chkRecordarme.Checked)
                    {
                        HttpCookie cookie = new HttpCookie("UsuarioRecordado");

                        // Establecer el valor de la cookie (por ejemplo, el ID de usuario)
                        cookie.Value = persona.Email;

                        // Configurar la expiración de la cookie (por ejemplo, una semana)
                        cookie.Expires = DateTime.Now.AddDays(21);

                        // Agregar la cookie a la respuesta del servidor
                        Response.Cookies.Add(cookie);
                    }
                    
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    lblError.Text = "Email o contraseña incorrectos.";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "setTimeout(function() { document.getElementById('" + lblError.ClientID + "').innerHTML = ''; }, 3000);", true);
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