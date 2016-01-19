<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UN.aspx.cs" Inherits="GetExternalData.COMTRADE.UN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <asp:Panel ID="pnlMetaData" CssClass="panel panel-primary" runat="server">

        <div class="panel-heading">Obtener MetaData
            <span class="badge">
                Cantidad de Registros:
                <asp:Label ID="lbl_cant_metadata" runat="server"></asp:Label>
            </span>
        </div>

        <div class="panel-body form-horizontal">
            
            <div class="form-group">
                <asp:Label ID="Label6" CssClass="control-label col-xs-2" runat="server" Text="Seleccione Pais:"></asp:Label>
                <div class="col-xs-4">
                    <asp:DropDownList ID="ddl_paisMetaData" CssClass="form-control" runat="server">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label5" CssClass="control-label col-xs-2" runat="server" Text="Seleccione Version Sistema Armonizado:"></asp:Label>
                <div class="col-xs-4">
                    <asp:DropDownList ID="ddl_hsMetaData" CssClass="form-control" runat="server">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="Label3" CssClass="control-label col-xs-2" runat="server" Text="Año:"></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox ID="txtAnio" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>

            <%--<div class="form-group">
                <asp:Label ID  ="lbl" CssClass="control-label col-xs-2" runat="server" Text="Del anio: "></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox ID="txt_anio_inicial" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <asp:Label ID="lbl2" CssClass="control-label col-xs-1" runat="server" Text=" al: "></asp:Label>
                <div class="col-xs-4">
                    <asp:TextBox ID="txt_anio_final" type="text" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>--%>

            <div class="form-group col-xs-2">
                <asp:Button ID="btn_get_metadata_un" CssClass="btn btn-primary " runat="server" Text="Obtener Metadata" OnClick="btn_get_metadata_un_Click"/>
            </div>

       </div>
        <br />
        <div class="panel-body form-vertical ">
            <span>Progreso</span>
            <div class="progress progress-striped">
                <div class="progress-bar progress-bar-success" role="progressbar"
                    aria-valuenow="60" aria-valuemin="0" aria-valuemax="100"
                    id="bar_get_metadata_un" runat="server">
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
