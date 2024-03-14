<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebStore_web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function soloNumeros(event) {
            // Obtener el código de la tecla presionada
            var key = event.keyCode || event.which;

            // Permitir solo números (0-9), la tecla Backspace, y las teclas de navegación (flechas, inicio, fin)
            if (key < 48 || key > 57) {
                if (key != 8 && key != 37 && key != 39 && key != 36 && key != 35) {
                    event.preventDefault();
                    return false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <% if (EsBusqueda)
        { %>
    <div class="d-flex" style="padding: 12px;">
        <div class="w-25">
            <asp:Label Text="" ID="lblBusqueda" runat="server" CssClass="lead" />
        </div>
        <div class="w-75 row text-center">
            <div class="col-md-1">
                <h5 class="mb-0 mr-2">Filtrar:</h5>
            </div>
            <div class="col-md-3 text-start">
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlPrecio" AutoPostBack="true" ClientIDMode="Static">
                    <asp:ListItem Text="Precio" />
                    <asp:ListItem Text="Menor a mayor" />
                    <asp:ListItem Text="Mayor a menor" />
                </asp:DropDownList>
            </div>
            <div class="col-md-3 text-start">
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlMarca" AutoPostBack="true" ClientIDMode="Static" />
            </div>
            <div class="col-md-4 text-start">
                <div class="row">
                    <div class="col-md-3 text-start">
                        <asp:TextBox runat="server" ID="txtMin" placeholder="Min" CssClass="form-control" Style="width: 60px;" onkeypress="return soloNumeros(event)" />
                        <asp:RegularExpressionValidator runat="server"
                            ControlToValidate="txtMin"
                            ValidationExpression="^\d+$"
                            ErrorMessage="Solo números."
                            Display="Dynamic"
                            CssClass="invalid-feedback" />
                    </div>
                    <div class="col-md-1 text-center">
                        <span>-</span>
                    </div>
                    <div class="col-md-3 text-start">
                        <asp:TextBox runat="server" ID="txtMax" placeholder="Max" CssClass="form-control" Style="width: 60px;" onkeypress="return soloNumeros(event)" />
                        <asp:RegularExpressionValidator runat="server"
                            ControlToValidate="txtMax"
                            ValidationExpression="^\d+$"
                            ErrorMessage="Solo números."
                            Display="Dynamic"
                            CssClass="invalid-feedback" />
                    </div>
                    <div class="col-md-2">
                        <button type="submit" data-test-id="sendButton" class="btn btn-success">></button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <% } %>

    <div class="container">
        <div class="row row-cols-1 row-cols-md-5 g-4">
            <asp:Repeater ID="repRepetidor" runat="server" OnItemDataBound="repRepetidor_ItemDataBound" >
                <ItemTemplate>

                    <div class="col-lg-3 col-md-5 col-sm-7">
                        <div class="card mb-5 shadow-sm text-center">

                            <div class="d-flex align-items-center justify-content-center">
                                <img id="imgArticulo" src='<%#  negocio.Utilidades.ObtenerUrlImagen(Eval("ImagenUrl").ToString()) %>'
                                    class="img-fluid" style="height: 18rem; object-fit: cover"
                                    onerror='this.onerror = null; this.src="<%#  negocio.Utilidades.ImagenDeRespaldo() %>"'>
                            </div>

                            <div class="card-body">
                                <h5 id="lblNombre" class="card-title"><%#Eval("Nombre")?.ToString().Length > 14 ? Eval("Nombre").ToString().Substring(0, 14) + "..." : Eval("Nombre") %></h5>
                                <p class="card-text"><%#Eval("Descripcion")?.ToString().Length > 16 ? Eval("Descripcion").ToString().Substring(0, 16) + "..." : Eval("Descripcion") %></p>
                                <p class="card-text"><%#Eval("Precio", "{0:C}") %></p>

                                <div class="d-flex justify-content-center">
                                    <a href="Detalles.aspx?id=<%#Eval("Id") %>" class="btn btn-primary w-30 text-center me-1">Ver detalle
                                    </a>

                                    <asp:LinkButton Text="" runat="server" ID="btnFavorito"
                                        CssClass="btn btn-outline-warning flex-shrink-0"
                                        CommandName="MarcarFavorito" CommandArgument='<%# Eval("Id") %>'
                                        OnCommand="btnFavorito_Command">
                                        <div id="iconoFavorito" runat="server"><i class='fa-regular fa-star'></i></div>
                                    </asp:LinkButton>

                                </div>
                            </div>
                        </div>
                    </div>

                </ItemTemplate>
            </asp:Repeater>

        </div>

        <div class="row row-cols-1 row-cols-md-1 g-1" style="margin-top: 60px;">
            <asp:Label Text="" ID="lblVacio" runat="server" ForeColor="Red" CssClass="d-flex justify-content-center" />
        </div>

    </div>
</asp:Content>
