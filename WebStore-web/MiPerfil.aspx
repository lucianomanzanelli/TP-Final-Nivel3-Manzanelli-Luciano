<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="WebStore_web.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function previewImage(input) {
            var imgPerfil = document.getElementById('<%= imgPerfil.ClientID %>');
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    imgPerfil.src = e.target.result;
                };

                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Mi Perfil</h3>

    <div class="row">
        <div class="col-md-4">
            <div class="mb-3 needs-validation was-validated" novalidate="">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" required="" MaxLength="100" />
                <asp:RequiredFieldValidator ControlToValidate="txtEmail" runat="server" />
                <asp:RegularExpressionValidator ErrorMessage="Escribe un email válido." ControlToValidate="txtEmail" runat="server"
                    ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                    Display="Dynamic"
                    CssClass="invalid-feedback" />
            </div>
            <div class="mb-3 needs-validation was-validated" novalidate="">
                <label class="form-label">Nombre</label>
                <asp:TextBox runat="server" ID="txtNombre" ClientIDMode="Static" CssClass="form-control" required="" MaxLength="50" />
                <asp:RequiredFieldValidator ControlToValidate="txtNombre" runat="server" />
                <asp:RegularExpressionValidator ErrorMessage="Escribe solo tu nombre." ControlToValidate="txtNombre" runat="server"
                    ValidationExpression="^[a-zA-ZñÑ]+(?: [a-zA-ZñÑ]+)*"
                    Display="Dynamic"
                    CssClass="invalid-feedback" />
            </div>
            <div class="mb-3 needs-validation was-validated" novalidate="">
                <label class="form-label">Apellido</label>
                <asp:TextBox runat="server" ID="txtApellido" ClientIDMode="Static" CssClass="form-control" required="" MaxLength="50" />
                <asp:RequiredFieldValidator ControlToValidate="txtApellido" runat="server" />
                <asp:RegularExpressionValidator ErrorMessage="Escribe solo tu apellido." ControlToValidate="txtApellido" runat="server"
                    ValidationExpression="^[a-zA-ZñÑ]+(?: [a-zA-ZñÑ]+)*"
                    Display="Dynamic"
                    CssClass="invalid-feedback" />
            </div>
        </div>

        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label">Imágen de perfil</label>
                <input type="file" class="form-control" id="txtImagen" runat="server" onchange="previewImage(this);" />
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
                <asp:Label Text="" ID="lblGuardar" ForeColor="Green" runat="server" />
                <asp:Button Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_Click"
                    CssClass="btn btn-success" runat="server" />
                <a href="Default.aspx" class="btn btn-primary">Volver</a>
            </div>
        </div>

    </div>

    <%-- Modal de confirmacion --%>

    <div class="modal fade" id="myModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Mensaje de Confirmación</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    Modificado con éxito!
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
