<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UN.aspx.cs" Inherits="GetExternalData.COMTRADE.UN" %>
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
                <asp:Button ID="btnGetCommodity" CssClass="btn btn-primary" runat="server" Text="Obtener Productos" OnClick="btnGetCommodity_Click"/>
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
