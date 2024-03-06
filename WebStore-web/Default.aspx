<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebStore_web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="row row-cols-1 row-cols-md-5 g-4">

        <asp:Repeater ID="repRepetidor" runat="server">
            <ItemTemplate>

                <div class="col">
                    <div class="card text-center">
                        <img id="imgArticulo" src='<%#  negocio.Utilidades.ObtenerUrlImagen(Eval("ImagenUrl")) %>' class="card-img-top square-image" alt="..." onerror='this.onerror = null; this.src="<%#  negocio.Utilidades.ImagenDeRespaldo() %>"'>
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre")?.ToString().Length > 14 ? Eval("Nombre").ToString().Substring(0, 14) + "..." : Eval("Nombre") %></h5>
                            <p class="card-text"><%#Eval("Descripcion")?.ToString().Length > 16 ? Eval("Descripcion").ToString().Substring(0, 16) + "..." : Eval("Descripcion") %></p>
                            <p class="card-text"><%#Eval("Precio", "{0:C}") %></p>
                            <a href="Detalles.aspx?id=<%#Eval("Id") %>" class="btn btn-primary">Ver detalle</a>
                        </div>
                    </div>
                </div>

            </ItemTemplate>
        </asp:Repeater>

    </div>

    <div class="row row-cols-1 row-cols-md-1 g-1" style="margin-top: 60px;">
        <asp:Label Text="" ID="lblVacio" runat="server" ForeColor="Red" CssClass="d-flex justify-content-center" />
    </div>

</asp:Content>
