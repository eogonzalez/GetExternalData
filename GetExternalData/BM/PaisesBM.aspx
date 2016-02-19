<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaisesBM.aspx.cs" Inherits="GetExternalData.BM.PaisesBM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <br />
    <asp:Panel ID="pnlGetPaises" CssClass="panel panel-primary" runat="server">

        <div class="panel-heading">Obtener Paises
            <span class="badge">Cantidad de Paises
                <asp:Label ID="lbl_cant_paises" runat="server"></asp:Label>
            </span>
        </div>

        <div class="panel-body form-horizontal">
            <br />

            <div class="form-group col-xs-2">
                <asp:Button ID="btn_get_paises_bm" CssClass="btn btn-primary" runat="server" Text="Obtener Paises" OnClick="btn_get_paises_bm_Click"/>
            </div>
        </div>

        <div class="panel-body form-vertical">
            
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

    </asp:Panel>

</asp:Content>
