<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CEPALStat.aspx.cs" Inherits="GetExternalData.CEPALStat.CEPALStat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlGetData" CssClass="panel panel-primary" runat="server">

    <div class="panel-heading">Obtener Temas</div>

    <div class="panel-body form-horizontal">
        <div>
            <asp:Button ID="btnGet" CssClass="btn btn-primary" runat="server" Text="GetTheme" OnClick="btnGet_Click" />
        </div>

        <span>Progreso</span>
        <div class="progress progress-striped">
            <div class="progress-bar progress-bar-success" role="progressbar" 
                aria-valuenow="60" aria-valuemin="0" aria-valuemax="100"
                id="barCepal" runat="server"></div>
        </div>
    </div>

    <%--<div class="panel-footer">
    </div>--%>

    </asp:Panel>
</asp:Content>
