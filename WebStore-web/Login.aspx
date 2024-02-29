<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebStore_web.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-6">
    <div class="mb-3">
        <label class="form-label">User</label>
        <asp:TextBox runat="server" ID="txtEmail" placeholder="Email" required="" CssClass="form-control" />
    </div>
    <div class="mb-3">
        <label class="form-label">Password</label>
        <asp:TextBox runat="server" ID="txtPassword" placeholder="******" required="" CssClass="form-control" TextMode="Password" />
        <asp:Label Text="" ID="lblError" runat="server" />
    </div>
    <asp:Button Text="Ingresar" runat="server" ID="btnIngresar" OnClick="btnIngresar_Click" CssClass="btn btn-primary" />
    <a href="/">Cancelar</a>
</div>
</asp:Content>
