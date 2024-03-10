<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="WebStore_web.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-2">
            </div>

            <div class="col-md-8">
                <div class="card" style="margin-top: 60px;">
                    <div class="card-body">
                        <div class="px-4 py-3">
                            <div class="mb-3 needs-validation was-validated" novalidate="">
                                <label class="form-label">Email</label>
                                <asp:TextBox runat="server" ID="txtEmail" required="" onkeydown="Login(event)" placeholder="email@ejemplo.com" CssClass="form-control" />
                                <asp:RegularExpressionValidator ErrorMessage="Escribe un email válido." ControlToValidate="txtEmail" runat="server"
                                    ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                    Display="Dynamic"
                                    CssClass="invalid-feedback" />
                            </div>

                            <div class="mb-3">
                                <label class="form-label">Contraseña</label>
                                <asp:TextBox runat="server" ID="txtPassword"  placeholder="******" CssClass="form-control" TextMode="Password" />
                                <asp:Label Text="" ID="lblError" runat="server" ForeColor="Red" />
                                <asp:RegularExpressionValidator ErrorMessage="No puedes escribir espacios." ControlToValidate="txtPassword" runat="server"
                                    ValidationExpression="^[^\s]+$"
                                    Display="Dynamic"
                                    CssClass="invalid-feedback" />
                            </div>

                            <asp:Button Text="Registrarme" runat="server" ID="btnRegistro" OnClick="btnRegistro_Click" CssClass="btn btn-success" />
                            <a href="Default.aspx" class="btn btn-primary">Volver</a>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-2">
            </div>
        </div>
    </div>



</asp:Content>
