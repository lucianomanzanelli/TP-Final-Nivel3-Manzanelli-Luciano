<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebStore_web.Login" %>

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
                            <div class="mb-3">
                                <label for="exampleDropdownFormEmail1" class="form-label">Email</label>
                                <asp:TextBox runat="server" ID="txtEmail" placeholder="email@ejemplo.com" CssClass="form-control" />
                            </div>

                            <div class="mb-3">
                                <label for="exampleDropdownFormPassword1" class="form-label">Contraseña</label>
                                <asp:TextBox runat="server" ID="txtPassword" placeholder="******" CssClass="form-control" TextMode="Password" />
                                <asp:Label Text="" ID="lblError" runat="server" ForeColor="Red" />
                            </div>

                            <div class="mb-3">
                                <div class="form-check">
                                    <input type="checkbox" class="form-check-input" id="dropdownCheck">
                                    <label class="form-check-label" for="dropdownCheck">
                                        Recordarme
                                    </label>
                                </div>
                            </div>

                            <asp:Button Text="Ingresar" runat="server" ID="btnIngresar" OnClick="btnIngresar_Click" CssClass="btn btn-primary" />
                            <a href="/">Cancelar</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
            </div>
        </div>
    </div>



</asp:Content>
