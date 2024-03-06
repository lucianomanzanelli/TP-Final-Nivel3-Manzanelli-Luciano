<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Detalles.aspx.cs" Inherits="WebStore_web.Detalles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="py-5">
        <div class="container px-4 px-lg-5 my-5">
            <div class="row gx-4 gx-lg-5 align-items-center">

                <div class="col-md-6">
                    <asp:Image CssClass="card-img-top mb-5 mb-md-0" Width="350px" Height="350px" 
                        Style="object-fit: contain;" ID="imgArt" 
                        ImageUrl="https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png" 
                        runat="server" />
                </div>

                <div class="col-md-6">

                    <div class="small mb-1">
                    <asp:Label ID="lblMarca" runat="server" Text="Marca" CssClass="small mb-1" ></asp:Label>
                    </div>

                    <%--<h1 class="display-5 fw-bolder"><%#Eval("Nombre") %></h1>--%>
                    <h1> <asp:Label ID="lblNombre" Text="Nombre" runat="server" CssClass="display-5 fw-bolder" /> </h1>

                    <div class="fs-5 mb-5">
                        <%--<span><%#Eval("Precio", "{0:C}") %></span>--%>
                        <span> <asp:Label ID="lblPrecio" Text="Precio" runat="server"/></span>

                    </div>
                    <%--<p class="lead"><%#Eval("Descripcion")%></p>--%>
                    <p class="lead"> <asp:Label runat="server" Text="Descripcion" ID="lblDescripcion" /> </p>

                    <div class="d-flex">
                        <a href="Default.aspx" class="btn btn-primary w-50 text-center me-3">
                            <i class="fas fa-undo-alt"></i>Volver
                        </a>

                        <%--<button class="btn btn-success btn-agregar-carrito flex-shrink-0 w-50 " type="button" data-idproducto="">
                            <i class="fas fa-cart-plus"></i>Agregar al carrito
                        </button>--%>

                    </div>

                </div>

            </div>
        </div>
    </section>

</asp:Content>
