<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaisesUN.aspx.cs" Inherits="GetExternalData.COMTRADE.PaisesUN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <br />
    <asp:Panel ID="pnlGetPaises" CssClass="panel panel-primary" runat="server">

        <div class="panel-heading">Obtener Paises
            <span class="badge">
                Cantidad de Paises: 
                <asp:Label ID="lbl_cant_paises" runat="server"></asp:Label>
            </span>
        </div>

        <div class="panel-body form-horizontal">
            <div>
                <asp:Button ID="btn_get_paises_un" CssClass="btn btn-primary" runat="server" Text="Obtener Paises" OnClick="btn_get_paises_un_Click"/>
            </div>
            <br />
            <span>Progreso</span>
            <div class="progress progress-striped">
                <div class="progress-bar progress-bar-success" role="progressbar"
                    aria-valuenow="60" aria-valuemin="0" aria-valuemax="100"
                    id="bar_get_paises_un" runat="server">
                </div>
            </div>
            <br />
            <span>Bitacora:</span>
            <div>
                <asp:TextBox ID="txt_log_paises_un" type="text" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
