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
                        runat="server" />
                </div>

                <div class="col-md-6">

                    <div class="small mb-1">
                        <asp:Label ID="lblMarca" runat="server" Text="Marca" CssClass="small mb-1"></asp:Label>
                    </div>


                    <h1>
                        <asp:Label ID="lblNombre" Text="Nombre" runat="server" CssClass="display-5 fw-bolder" />
                    </h1>

                    <div class="fs-5 mb-3">
                        <span>
                            <asp:Label ID="lblPrecio" Text="Precio" runat="server" />
                        </span>
                    </div>

                    <p class="lead">
                        <asp:Label runat="server" Text="Descripcion" ID="lblDescripcion" />
                    </p>

                    <div class="d-flex">
                        
                        <asp:LinkButton Text="" runat="server" ID="btnVolver"
                            CssClass="btn btn-primary w-50 text-center me-2"
                            PostBackUrl="~/Default.aspx">
                            <i class="fas fa-undo-alt"></i> Volver
                        </asp:LinkButton>


                        <asp:LinkButton Text="" runat="server" ID="btnFavorito"
                            CssClass="btn btn-outline-warning flex-shrink-0"
                            OnCommand="btnFavorito_Command">

                            <%if (esFavorito())
                                {  %>
                            <i class="fa-solid fa-circle-minus"></i> Quitar de favoritos
                                
                            <%  }
                                else
                                {  %>
                            <i class='fa-regular fa-star'></i> Agregar a favoritos
            <%  }%>
                        </asp:LinkButton>

                    </div>

                    <div class="d-flex">
                        <div class="col-md-6 w-50 me-2"></div>
                        <div class="col-md-6 ">
                            <asp:Label Text="" ID="lblError" ForeColor="Red" runat="server" />
                        </div>

                    </div>

                </div>

            </div>
        </div>
    </section>

</asp:Content>
