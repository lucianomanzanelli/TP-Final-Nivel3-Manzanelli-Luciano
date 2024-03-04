<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FormularioArticulo.aspx.cs" Inherits="WebStore_web.FormularioArticulo" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" ID="ScriptManager1" />

    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label">Id</label>
                <asp:TextBox runat="server" ID="txtId" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtCodigo" class="form-label">Código:</label>
                <asp:TextBox runat="server" ID="txtCodigo" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre:</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
            </div>


            <div class="mb-3">
                <label for="txtPrecio" class="form-label">Precio:</label>
                <div class="input-group">
                    <span class="input-group-text" id="basic-addon1">$</span>
                    <asp:TextBox runat="server" ID="txtPrecio" CssClass="form-control" />
                    <span class="input-group-text">.00</span>
                </div>
                <asp:RegularExpressionValidator ID="regexValidator" runat="server"
                    ControlToValidate="txtPrecio"
                    ValidationExpression="^\d+$"
                    ErrorMessage="Ingrese solo números."
                    Display="Dynamic"
                    CssClass="text-danger" />
            </div>


            <div class="mb-3">
                <label for="ddlMarca" class="form-label">Marca:</label>
                <asp:DropDownList ID="ddlMarca" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label for="ddlCategoria" class="form-label">Categoría:</label>
                <asp:DropDownList ID="ddlCategoria" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <asp:Label Text="" ID="lblError" ForeColor="Red" runat="server" />
            </div>

        </div>

        <div class="col-6">
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripción: </label>
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" TextMode="MultiLine" />
            </div>

            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <label for="txtImagenUrl" class="form-label">URL de la imágen:</label>
                        <asp:TextBox runat="server" ID="txtImagenUrl" CssClass="form-control"
                            AutoPostBack="true" OnTextChanged="txtImagenUrl_TextChanged" />
                    </div>
                    <asp:Image ImageUrl="https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png"
                        runat="server" ID="imgArticulo" Width="350px" Height="350px" Style="object-fit: contain;" />
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>

    <div class="row d-flex justify-content-end" style="margin-top: 20px">

        <div class="col-3">
            <%  if (EsModificacion)
                {%>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="col-6">
                        <asp:Button Text="Eliminar" ID="btnEliminar" OnClick="btnEliminar_Click" CssClass="btn btn-warning" runat="server" />
                    </div>
                    <%  if (ConfirmaEliminacion)
                        { %>
                    <div class="mb-3">
                        <asp:CheckBox Text="Confirmar Eliminacion" ID="chkCofirmaEliminar" runat="server" />
                    </div>

                    <div class="mb-3">
                        <asp:Button Text="Eliminar definitivamente" ID="btnConfirmaEliminar" OnClick="btnConfirmaEliminar_Click" CssClass="btn btn-outline-danger" runat="server" />
                    </div>
                    <%  } %>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="col-3"></div>

        <div class="col-3"></div>

        <div class="col-3">
            <asp:Button Text="Aceptar" ID="btnAceptar" CssClass="btn btn-success" OnClick="btnAceptar_Click" runat="server" />
            <a href="AdministrarArticulos.aspx" class="btn btn-primary">Cancelar</a>
        </div>
        <%}

            else
            { %>
        <div class="d-flex justify-content-end">
            <div class="mb-3">
                <asp:Button Text="Aceptar" ID="btnAceptar2" CssClass="btn btn-success" OnClick="btnAceptar_Click" runat="server" />
                <a href="AdministrarArticulos.aspx" class="btn btn-primary">Cancelar</a>
            </div>
        </div>

        <%  } %>

        </div>
</asp:Content>
