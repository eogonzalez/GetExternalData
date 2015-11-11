<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CEPALStat.aspx.cs" Inherits="GetExternalData.CEPALStat.CEPALStat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />

    <asp:Panel ID="pnlGetData" CssClass="panel panel-primary" runat="server">

        <div class="panel-heading">Obtener Temas</div>

        <div class="panel-body form-vertical">
            <div>
                <asp:Button ID="btnGet" CssClass="btn btn-primary" runat="server" Text="Obtener temas" OnClick="btnGet_Click" />
            </div>
            <br />
            <span>Progreso</span>
            <div class="progress progress-striped">
                <div class="progress-bar progress-bar-success" role="progressbar"
                    aria-valuenow="60" aria-valuemin="0" aria-valuemax="100"
                    id="barCepal" runat="server">
                </div>
            </div>
            <br />
            <span>Bitacora:</span>
            <div>
                <asp:TextBox ID="txt_log_theme" type="text" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>

    <%--<div class="panel-footer">
    </div>--%>

    </asp:Panel>

    <br />
    <asp:Panel ID="pnlGetDimensiones"   CssClass="panel panel-primary" runat="server">
        <div class="panel-heading">Obtener Dimensiones</div>
        <div class="panel-body form-horizontal">
            <div>
                <asp:Button ID="btnGetDim" CssClass="btn btn-primary" runat="server" Text="Obtener Dimensiones" OnClick="btnGetDim_Click" />
            </div>
            <div>
                <asp:Label ID="Label5" CssClass="control-label col-xs-4" runat="server" Text="IdIndicador:"></asp:Label>
                <div class="col-xs-8">
                    <asp:TextBox ID="txtIdIndicador" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <br />
            <span>Progreso</span>
            <div class="progress progress-striped">
                <div class="progress-bar progress-bar-success" role="progressbar"
                    aria-valuenow="60" aria-valuemin="0" aria-valuemax="100"
                    id="barCepalDim" runat="server">
                </div>
            </div>
            <br />
            <span>Bitacora:</span>
            <div>
                <asp:TextBox ID="txt_log_dim" type="text" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
    </asp:Panel>

    <br />
    <asp:Panel ID="pnlGetMetaData"   CssClass="panel panel-primary" runat="server">
        <div class="panel-heading">Obtener Meta Data</div>
        <div class="panel-body form-horizontal">
            <div>
                <asp:Button ID="BtnGetMetaData" CssClass="btn btn-primary" runat="server" Text="Obtener Metadata" OnClick="BtnGetMetaData_Click"/>
            </div>
            <div>
                <asp:Label ID="Label1" CssClass="control-label col-xs-4" runat="server" Text="IdIndicador:"></asp:Label>
                <div class="col-xs-8">
                    <asp:TextBox ID="txtMetaDataIndicator" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <br />
            <span>Progreso</span>
            <div class="progress progress-striped">
                <div class="progress-bar progress-bar-success" role="progressbar"
                    aria-valuenow="60" aria-valuemin="0" aria-valuemax="100"
                    id="barCepalMeta" runat="server">
                </div>
            </div>
            <br />
            <span>Bitacora:</span>
            <div>
                <asp:TextBox ID="txt_log_metadata" type="text" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
