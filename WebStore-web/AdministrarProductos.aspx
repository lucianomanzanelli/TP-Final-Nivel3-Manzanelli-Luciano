<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AdministrarProductos.aspx.cs" Inherits="WebStore_web.AdministrarProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <asp:Label Text="Filtrar:" runat="server" />
                <asp:TextBox runat="server" AutoPostBack="true" ID="txtFiltro" OnTextChanged="txtFiltro_TextChanged" CssClass="form-control" />
            </div>
        </div>
        <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
            <div class="mb-3">
                <asp:CheckBox Text="Filtro Avanzado" runat="server"
                    CssClass="" ID="chkAvanzado"
                    AutoPostBack="true"
                    OnCheckedChanged="chkAvanzado_CheckedChanged" />
            </div>
        </div>
    </div>

    <% if (FiltroAvanzado)
        { %>
        <div class="row">
    <div class="col-3">
        <div class="mb-3">
            <asp:Label Text="Campo:" runat="server" />
            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCampo" AutoPostBack="true" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                <asp:ListItem Text="Producto" />
                <asp:ListItem Text="Precio" />
                <asp:ListItem Text="Marca" />
                <asp:ListItem Text="Categoría" />
            </asp:DropDownList>
        </div>
    </div>

    <% if (Marca()) { %>
        <div class="col-3">
            <div class="mb-3">
                <asp:Label Text="Marca:" runat="server" />
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlMarca" />
            </div>
        </div>
        <div class="col-3">
            <div class="mb-3">
            </div>
        </div>
    <% } %>

    <% else if (Categoria()) { %>
        <div class="col-3">
            <div class="mb-3">
                <asp:Label Text="Categoria:" runat="server" />
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCategoria" />
            </div>
        </div>
        <div class="col-3">
            <div class="mb-3">
            </div>
        </div>
    <% } else { %>
        <div class="col-3">
            <div class="mb-3">
                <asp:Label Text="Criterio:" runat="server" />
                <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-3">
            <div class="mb-3">
                <asp:Label Text="Filtro:" runat="server" />
                <asp:TextBox runat="server" ID="txtFiltroAvanzado" CssClass="form-control" />
            </div>
        </div>
        <div class="col-3">
            <div class="mb-3">
            </div>
        </div>
    <% } %>

    <div class="row">
        <div class="col-3">
            <div class="mb-3">
                <asp:Button Text="Limpiar" CssClass="btn btn-secondary" ID="btnLimpiar" OnClick="btnLimpiar_Click" runat="server" />
                <asp:Button Text="Buscar" CssClass="btn btn-success" ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" />
            </div>
        </div>
    </div>
</div>



   <%  } %>



    <asp:GridView ID="dgvArticulos" DataKeyNames="Id"
        OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged"
        OnPageIndexChanging="dgvArticulos_PageIndexChanging"
        AllowPaging="true" PageSize="6"
        AutoGenerateColumns="false" CssClass="mt-4 table table-striped" runat="server">
        <Columns>
            <asp:BoundField HeaderText="Código" DataField="Codigo" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
            <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
            <asp:BoundField HeaderText="Categoria" DataField="Categoria.Descripcion" />
            <asp:BoundField HeaderText="Precio" DataField="Precio" DataFormatString="{0:C}" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍" />
        </Columns>
    </asp:GridView>

    <a href="FormularioArticulo.aspx" class="btn btn-primary">Agregar artículo</a>


</asp:Content>
