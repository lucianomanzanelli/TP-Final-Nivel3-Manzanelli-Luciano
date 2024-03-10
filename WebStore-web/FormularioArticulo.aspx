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
            <div class="mb-3 needs-validation was-validated" novalidate="">
                <label for="txtCodigo" class="form-label">Código:</label>
                <asp:TextBox runat="server" ID="txtCodigo" CssClass="form-control" required="" MaxLength="50" />
                <asp:RequiredFieldValidator ErrorMessage="Debes agregar un código para identificarlo."
                    CssClass="invalid-feedback" ControlToValidate="txtCodigo" runat="server" />
            </div>
            <div class="mb-3 needs-validation was-validated" novalidate="">
                <label for="txtNombre" class="form-label">Nombre:</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" required="" MaxLength="50" />
                <asp:RequiredFieldValidator ErrorMessage="Debes agregar el nombre del producto."
                    CssClass="invalid-feedback" ControlToValidate="txtNombre" runat="server" />
            </div>


            <div class="mb-3 needs-validation was-validated" novalidate="">
                <label for="txtPrecio" class="form-label">Precio:</label>
                <div class="input-group">
                    <span class="input-group-text" id="basic-addon1">$</span>
                    <asp:TextBox runat="server" ID="txtPrecio" CssClass="form-control" required="" />
                    <span class="input-group-text">.00</span>
                </div>
                <asp:RegularExpressionValidator runat="server"
                    ControlToValidate="txtPrecio"
                    ValidationExpression="^\d+$"
                    ErrorMessage="Ingrese solo números."
                    Display="Dynamic"
                    CssClass="invalid-feedback" />
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
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" TextMode="MultiLine" MaxLength="150" />
            </div>

            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <label for="txtImagenUrl" class="form-label">URL de la imágen:</label>
                        <asp:TextBox runat="server" ID="txtImagenUrl" CssClass="form-control"
                            AutoPostBack="true" OnTextChanged="txtImagenUrl_TextChanged" MaxLength="1000" />
                        <%--<asp:RegularExpressionValidator runat="server"
                            ControlToValidate="txtImagenUrl"
                            ValidationExpression="[-a-zA-Z0-9@:%_\+.~#?&//=]{2,256}\.[a-z]{2,4}\b(\/[-a-zA-Z0-9@:%_\+.~#?&//=]*)?"
                            ErrorMessage="Ingresa una url válida."
                            Display="Dynamic"
                            CssClass="invalid-feedback" />--%>
                    </div>
                    <asp:Image runat="server" ID="imgArticulo" CssClass="img-fluid" Style="height: 18rem; object-fit: cover" />
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="row d-flex justify-content-end" style="margin-top: 20px">

                <div class="col-3">
                    <%  if (EsModificacion)
                        { %>

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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
