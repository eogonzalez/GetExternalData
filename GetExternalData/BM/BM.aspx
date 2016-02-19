<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BM.aspx.cs" Inherits="GetExternalData.BM.BM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <asp:Panel ID="pnlMetaData" CssClass="panel panel-primary" runat="server">

        <div class="panel-heading">
            Obtener MetaData de Indicadores
            <span class="badge">Cantidad de Registros:
                <asp:Label ID="lbl_cantidad_metadata" runat="server"></asp:Label>
            </span>
        </div>

        <div class="panel-body form-horizontal">

            <div class="form-group">
                <asp:Label ID="Label4" CssClass="control-label col-xs-2" runat="server" Text="Seleccione Pais:"></asp:Label>
                <div class="col-xs-4">
                    <asp:DropDownList ID="ddl_pais" type="text" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label5" CssClass="control-label col-xs-2" runat="server" Text="Seleccione Tema:"></asp:Label>
                <div class="col-xs-4">
                    <asp:DropDownList ID="ddl_tema"  CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_tema_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label6" CssClass="control-label col-xs-2" runat="server" Text="Seleccione Indicador:"></asp:Label>
                <div class="col-xs-4">
                    <asp:DropDownList ID="ddl_indicador" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
            </div>


            <div class="form-group">
                <asp:Label ID="Label3" CssClass="control-label col-xs-2" runat="server" Text="Cantidad de años:"></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtAnios" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lbl" CssClass="control-label col-xs-2" runat="server" Text="Del anio: "></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox ID="txt_anio_inicial" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <asp:Label ID="lbl2" CssClass="control-label col-xs-1" runat="server" Text=" al: "></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox ID="txt_anio_final" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="form-group col-xs-2">
                <asp:Button ID="btn_get_metadata_bm" CssClass="btn btn-primary " runat="server" Text="Obtener Metadata" OnClick="btn_get_metadata_bm_Click" />
            </div>

        </div>
        <br />
        <div class="panel-body form-vertical ">
            <span>Progreso</span>
            <div class="progress progress-striped">
                <div class="progress-bar progress-bar-success" role="progressbar"
                    aria-valuenow="60" aria-valuemin="0" aria-valuemax="100"
                    id="bar_get_metadata_bm" runat="server">
                </div>
            </div>


            <br />
            <span>Bitacora:</span>
            <div>
                <asp:TextBox ID="txt_log_metadata" type="text" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>

        <%--<div class="panel-footer">
    </div>--%>
    </asp:Panel>


</asp:Content>
