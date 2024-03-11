using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
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

                        cargarImagen();
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);

            }
        }

        protected void cargarImagen()
        {
            Persona user = (Persona)Session["persona"];

            if (user.ImagenPerfil != null)
            {
                imgPerfil.ImageUrl = "~/Images/" + user.ImagenPerfil;
            }
            else
                imgPerfil.ImageUrl = "https://www.bunko.pet/__export/1624827469365/sites/debate/img/2021/06/27/basset-hound_crop1624827361677.jpg_554688468.jpg";

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid || string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
                {
                    lblGuardar.ForeColor = Color.Red;
                    lblGuardar.Text = "Completa los datos faltantes!";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "setTimeout(function() { document.getElementById('" + lblGuardar.ClientID + "').innerHTML = ''; }, 3000);", true);
                    return;
                }


                PersonaNegocio negocio = new PersonaNegocio();
                Persona user = (Persona)Session["persona"];

                if (txtImagen.PostedFile.FileName != "")
                {
                    string ruta = Server.MapPath("./Images/");
                    txtImagen.PostedFile.SaveAs(ruta + "perfil-" + user.Id + ".jpg");
                    user.ImagenPerfil = "perfil-" + user.Id + ".jpg";
                }
                user.Nombre = txtNombre.Text;
                user.Apellido = txtApellido.Text;



                if (string.IsNullOrEmpty(txtPass.Text) && string.IsNullOrEmpty(txtUpdPass.Text)
                    && string.IsNullOrEmpty(txtConfPass.Text))
                {
                    lblNuevaClave.Text = "";
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtPass.Text) && user.Pass == txtPass.Text
                        && txtUpdPass.Text == txtConfPass.Text)
                    {
                        user.NuevaPass = txtUpdPass.Text;
                        user.Pass = txtConfPass.Text;

                        EmailService email = new EmailService();
                        email.armarCorreo(user.Email, "Web Store: cambio de contraseña",
                            "La contraseña de tu cuenta de Web Store se actualizó, tu nueva clave es: " + txtUpdPass.Text);
                        email.enviarEmail();

                        lblNuevaClave.ForeColor = Color.Green;
                        lblNuevaClave.Text = "Contraseña actualizada! Se envió a tu email!";
                        Page.ClientScript.RegisterStartupScript(this.GetType(),
                            "HideLabel", "setTimeout(function() { document.getElementById('" +
                            lblNuevaClave.ClientID + "').innerHTML = ''; }, 3000);", true);
                    }
                    else
                    {
                        lblNuevaClave.ForeColor = Color.Red;
                        lblNuevaClave.Text = "Tus contraseñas no coinciden!";
                        Page.ClientScript.RegisterStartupScript(this.GetType(),
                                "HideLabel", "setTimeout(function() { document.getElementById('" +
                                lblNuevaClave.ClientID + "').innerHTML = ''; }, 3000);", true);
                    }
                }
                



                negocio.Actualizar(user);
                user.NuevaPass = null;


                System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)Master.FindControl("imgAvatar");
                if (string.IsNullOrEmpty(img.ImageUrl))
                    img.ImageUrl = "~/Images/" + user.ImagenPerfil;

                lblGuardar.ForeColor = Color.Green;
                lblGuardar.Text = "Datos actualizados correctamente!";
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                    "HideLabel", "setTimeout(function() { document.getElementById('" +
                    lblGuardar.ClientID + "').innerHTML = ''; }, 3000);", true);
                

                cargarImagen();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}