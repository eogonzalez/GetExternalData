<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CommoditiesUN.aspx.cs" Inherits="GetExternalData.COMTRADE.CommoditiesUN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

            <br />
    <asp:Panel ID="pnlGetCommodity" CssClass="panel panel-primary" runat="server">

        <div class="panel-heading">Obtener Productos
            <span class="badge">
                Cantidad de Registros del SAC:
                <asp:Label id="lbl_cant_commodities" runat="server"></asp:Label>
            </span>
        </div>

        <div class="panel-body form-horizontal">

            <br />
            <div class="form-group">
                <asp:Label ID="Label4" CssClass="control-label col-xs-2" runat="server" Text="Version Sistema Armonizado:"></asp:Label>
                <div class="col-xs-4">
                    <asp:DropDownList ID="ddl_hs" CssClass="form-control" runat="server">
                        <asp:ListItem Value="H0">H0 - HS1988</asp:ListItem>
                        <asp:ListItem Value="H1">H1 - HS1996</asp:ListItem>
                        <asp:ListItem Value="H2">H2 - HS2002</asp:ListItem>
                        <asp:ListItem Value="H3">H3 - HS2007</asp:ListItem>
                        <asp:ListItem Value="H4">H4 - HS2012</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group col-xs-2">
                <asp:Button ID="btnGetCommodity" CssClass="btn btn-primary" runat="server" Text="Obtener Productos" OnClick="btnGetCommodity_Click" />
            </div>

        </div>

        <div class="panel-body form-vertical">
            <br />
            <span>Progreso</span>
            <div class="form-group">
                <div class="progress progress-striped">
                    <div class="progress-bar progress-bar-success" role="progressbar"
                        aria-valuenow="60" aria-valuemin="0" aria-valuemax="100"
                        id="bar_UN_Commodity" runat="server">
                    </div>
                </div>
            </div>

            <br />
            <span>Bitacora:</span>
            <div>
                <asp:TextBox ID="txt_log_Commodity" type="text" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
