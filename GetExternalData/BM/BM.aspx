<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BM.aspx.cs" Inherits="GetExternalData.BM.BM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <asp:Panel ID="pnlGetIndicators" CssClass="panel panel-primary" runat="server">

        <div class="panel-heading">Obtener Indicadores</div>

        <div class="panel-body form-vertical">
            <div>
                <asp:Button ID="btnGet" CssClass="btn btn-primary" runat="server" Text="Obtener temas" OnClick="btnGet_Click" />
            </div>
            <br />
            <span>Progreso</span>
            <div class="progress progress-striped">
                <div class="progress-bar progress-bar-success" role="progressbar"
                    aria-valuenow="60" aria-valuemin="0" aria-valuemax="100"
                    id="barBMInicators" runat="server">
                </div>
            </div>
            <br />
            <span>Bitacora:</span>
            <div>
                <asp:TextBox ID="txt_log_Indicators" type="text" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>

    <%--<div class="panel-footer">
    </div>--%>

    </asp:Panel>

    <br />
    <asp:Panel ID="Panel1" CssClass="panel panel-primary" runat="server">

        <div class="panel-heading">Obtener Paises</div>

        <div class="panel-body form-vertical">
            <div>
                <asp:Button ID="btn_get_paises_bm" CssClass="btn btn-primary" runat="server" Text="Obtener Paises" OnClick="btn_get_paises_bm_Click"/>
            </div>
            <br />
            <span>Progreso</span>
            <div class="progress progress-striped">
                <div class="progress-bar progress-bar-success" role="progressbar"
                    aria-valuenow="60" aria-valuemin="0" aria-valuemax="100"
                    id="bar_get_paises_bm" runat="server">
                </div>
            </div>
            <br />
            <span>Bitacora:</span>
            <div>
                <asp:TextBox ID="txt_log_paises_bm" type="text" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>

    <%--<div class="panel-footer">
    </div>--%>

    </asp:Panel>


</asp:Content>
