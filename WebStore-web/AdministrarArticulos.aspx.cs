using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebStore_web
{
    public partial class AdministrarArticulos : System.Web.UI.Page
    {
        public bool FiltroAvanzado { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (!Seguridad.EsAdmin(Session["persona"]))
            {
                Session.Add("error", "Se requieren permisos de admin para acceder");
                Response.Redirect("Error.aspx", false);
                return;
            }

            FiltroAvanzado = chkAvanzado.Checked;
            if (!IsPostBack)
            {
                CargarArticulos();

            }

        }

        public bool Marca()
        {
            if (ddlCampo.SelectedItem.ToString() == "Marca")
            {
                return true;
            }

            return false;
        }
        public void CargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            List<dominio.Marca> lista = negocio.listar();
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataTextField = "Descripcion";
            ddlMarca.DataSource = lista;
            ddlMarca.DataBind();
        }
        public bool Categoria()
        {
            if (ddlCampo.SelectedItem.ToString() == "Categoría")
            {

                return true;
            }
            return false;
        }
        public void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            List<dominio.Categoria> lista = negocio.listar();
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataTextField = "Descripcion";
            ddlCategoria.DataSource = lista;
            ddlCategoria.DataBind();
        }

        protected void CargarArticulos()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Session.Add("listaArticulos", negocio.listar());
            dgvArticulos.DataSource = Session["listaArticulos"];
            dgvArticulos.DataBind();
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];
            List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToLower().Contains(txtFiltro.Text.ToLower()));
            dgvArticulos.DataSource = listaFiltrada;
            dgvArticulos.DataBind();
        }

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = chkAvanzado.Checked;
            txtFiltro.Text = string.Empty;
            txtFiltro.Enabled = !FiltroAvanzado;
            ddlCampo_SelectedIndexChanged(sender, e);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            ddlCampo.SelectedIndex = 0;
            ddlCampo_SelectedIndexChanged(sender, e);
            txtFiltroAvanzado.Text = string.Empty;
            CargarArticulos();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();


                if (ddlCriterio.SelectedItem != null)
                {
                    if (ddlCampo.Text == "Precio" && IsDigitsOnly(txtFiltroAvanzado.Text))
                    {
                        lblError.Text = "";
                        dgvArticulos.DataSource = negocio.filtrar(ddlCampo.SelectedItem.ToString(),
                                                            ddlCriterio.SelectedItem.ToString(),
                                                            txtFiltroAvanzado.Text);
                    }
                    else if (ddlCampo.Text == "Precio" && !IsDigitsOnly(txtFiltroAvanzado.Text))
                    {
                        lblError.Text = "Debes escribir solo numeros.";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "setTimeout(function() { document.getElementById('" + lblError.ClientID + "').innerHTML = ''; }, 3000);", true);

                        dgvArticulos.DataSource = negocio.filtrar(ddlCampo.SelectedItem.ToString(),
                                                                ddlCriterio.SelectedItem.ToString(),
                                                                "");
                    }
                    else
                    {
                        lblError.Text = "";
                        dgvArticulos.DataSource = negocio.filtrar(ddlCampo.SelectedItem.ToString(),
                                                            ddlCriterio.SelectedItem.ToString(),
                                                            txtFiltroAvanzado.Text);
                    }
                    
                }

                else if (ddlCriterio.SelectedItem == null)
                {
                    if (ddlMarca.SelectedItem != null)
                    {
                        dgvArticulos.DataSource = negocio.filtrar(ddlCampo.SelectedItem.ToString(),
                                                                "",
                                                                ddlMarca.SelectedItem.ToString());
                    }
                    else if (ddlCategoria.SelectedItem != null)
                    {
                        dgvArticulos.DataSource = negocio.filtrar(ddlCampo.SelectedItem.ToString(),
                                                            "",
                                                            ddlCategoria.SelectedItem.ToString());
                    }
                    else
                        dgvArticulos.DataSource = negocio.filtrar(ddlCampo.SelectedItem.ToString(),
                                                                "",
                                                                txtFiltroAvanzado.Text);
                }


                dgvArticulos.DataBind();

                if (dgvArticulos.Rows.Count == 0)
                {
                    lblError.Text = "No existen articulos para esos filtros :(";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "setTimeout(function() { document.getElementById('" + lblError.ClientID + "').innerHTML = ''; }, 3000);", true);

                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);

            }
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            ddlMarca.Items.Clear();
            ddlCategoria.Items.Clear();

            if (ddlCampo.SelectedItem.ToString() == "Producto")
            {
                ddlCriterio.Items.Add("Contiene");
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
            }
            else if (ddlCampo.SelectedItem.ToString() == "Precio")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Menor a");
                ddlCriterio.Items.Add("Mayor a");
            }

            if (ddlCampo.SelectedItem.ToString() == "Marca")
            {
                CargarMarcas();
            }
            if (ddlCampo.SelectedItem.ToString() == "Categoría")
            {
                CargarCategorias();
            }

        }

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulos.PageIndex = e.NewPageIndex;
            dgvArticulos.DataBind();
            btnBuscar_Click(sender, e);
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvArticulos.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioArticulo.aspx?id=" + id);
        }


    }
}