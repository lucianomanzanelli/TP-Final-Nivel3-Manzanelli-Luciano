<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="WebStore_web.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Mi Perfil</h3>

    <div class="row">
        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />

            </div>
            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <asp:TextBox runat="server" ID="txtNombre" ClientIDMode="Static" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label class="form-label">Apellido</label>
                <asp:TextBox runat="server" ID="txtApellido" ClientIDMode="Static" CssClass="form-control" />
            </div>
        </div>

        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label">Imágen de perfil</label>
                <input type="file" class="form-control" id="txtImagen" runat="server" />
            </div>


            <div class="mb-3">
                <asp:Image ID="imgPerfil" runat="server" CssClass="img-fluid mb-4" Width="75%"
                    ImageUrl="https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg" />
            </div>
        </div>

        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label">Contraseña actual</label>
                <asp:TextBox runat="server" ID="txtPass" placeholder="******" TextMode="Password" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label class="form-label">Contraseña nueva</label>
                <asp:TextBox runat="server" ID="txtUpdPass" placeholder="******" TextMode="Password" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label class="form-label">Confirmar contraseña nueva</label>
                <asp:TextBox runat="server" ID="txtConfPass" placeholder="******" TextMode="Password" CssClass="form-control" />
            </div>
        </div>

        <div class="d-flex justify-content-end">
            <div class="mb-3">
                <asp:Button Text="Guardar" ID="btnGuardar" OnClientClick="return validar()" OnClick="btnGuardar_Click"
                    CssClass="btn btn-success" runat="server" />
                <a href="Default.aspx" class="btn btn-primary">Volver</a>
            </div>
        </div>


    </div>


</asp:Content>
