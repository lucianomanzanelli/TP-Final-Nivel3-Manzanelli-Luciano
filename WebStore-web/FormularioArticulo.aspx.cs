using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;

namespace WebStore_web
{
    public partial class FormularioArticulo : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; }
        public bool EsModificacion { get; set; } 

        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            ConfirmaEliminacion = false;
            EsModificacion = false;

            try
            {
                if (!IsPostBack)
                {
                    MarcaNegocio marcaNegocio = new MarcaNegocio();
                    List<Marca> lstMarcas = marcaNegocio.listar();
                    CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                    List<Categoria> lstCategorias = categoriaNegocio.listar();

                    ddlMarca.DataSource = lstMarcas;
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataBind();

                    ddlCategoria.DataSource = lstCategorias;
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();

                }

                //Para modificar:
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";

                if (id != "")
                {
                    EsModificacion = true;
                }

                if (id != "" && !IsPostBack)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    Articulo articulo = (negocio.listar(id)[0]);

                    Session.Add("articulo", articulo);

                    txtId.Text = id;
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtImagenUrl.Text = articulo.ImagenUrl;
                    txtPrecio.Text = articulo.Precio.ToString("0");

                    ddlMarca.SelectedValue = articulo.Marca.Id.ToString();
                    ddlCategoria.SelectedValue = articulo.Categoria.Id.ToString();
                }

                cargarImagen();

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                {
                    lblError.ForeColor = Color.Red;
                    lblError.Text = "Completa los campos faltantes.";
                    return;
                }

                Articulo nuevo = new Articulo();
                ArticuloNegocio negocio = new ArticuloNegocio();

                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.ImagenUrl = txtImagenUrl.Text;
                nuevo.Precio = decimal.Parse(txtPrecio.Text);

                nuevo.Marca = new Marca();
                nuevo.Marca.Id = int.Parse(ddlMarca.SelectedValue);
                nuevo.Categoria = new Categoria();
                nuevo.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);

                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(txtId.Text);
                    negocio.modificar(nuevo);
                }
                else
                {
                    negocio.agregar(nuevo);
                }

                Response.Redirect("AdministrarArticulos.aspx", false);

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }


        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            cargarImagen();
        }

        protected void cargarImagen()
        {
            if (string.IsNullOrEmpty(txtImagenUrl.Text) || string.IsNullOrWhiteSpace(txtImagenUrl.Text))
            {
                imgArticulo.ImageUrl = Utilidades.ImagenDeRespaldo();
            }
            else
                imgArticulo.ImageUrl = txtImagenUrl.Text;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }

        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkCofirmaEliminar.Checked)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.eliminar(int.Parse(txtId.Text));
                    Response.Redirect("AdministrarArticulos.aspx", false);
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