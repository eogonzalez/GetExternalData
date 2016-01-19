<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BitacoraDetUN.aspx.cs" Inherits="GetExternalData.COMTRADE.BitacoraDetUN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <style type="text/css">
        .ColumnaOculta {
            display: none;
        }
    </style>

    <br />
    <%--Panel de bitacora de Encabezado--%>
    <asp:Panel ID="pnlBitacoraDet" CssClass="panel panel-primary" runat="server">

        <div class="panel-heading">Detalle de las Consultas Realizadas
            <span class="badge">
                Cantidad de bloques(partidas):
                <asp:Label ID="lbl_cantidad" runat="server"></asp:Label>
            </span>
        </div>
        <br />

        <h4>
            <span class="label label-success">
                <asp:Label ID="lbl_enc" runat="server"></asp:Label>
            </span>
        </h4>

        <div class="btn-group pull-right" role="group">
            <asp:LinkButton ID="lkb_regresar" runat="server" CssClass="btn btn-primary" OnClick="lkb_regresar_Click"> 
                <i aria-hidden="true" class="glyphicon glyphicon-share-alt"></i>
                Regresar 
            </asp:LinkButton>
        </div>

        <div>
            <asp:GridView ID="gvDetBitacora" runat="server"
                CssClass="table table-hover table-striped"
                GridLines="None"
                EmptyDataText="No se encontraron registros."
                AutoGenerateColumns="false" AllowPaging="true"
                OnPageIndexChanging="gvDetBitacora_PageIndexChanging">
                <PagerSettings Mode="Numeric"
                    Position="Bottom"
                    PageButtonCount="10"/>

                <Columns>
                    <asp:BoundField DataField="codigo_inciso" HeaderText="Codigo Partida SAC" />
                    <asp:BoundField DataField="hora_inicio" HeaderText="Hora de Inicio de Carga" />
                    <asp:BoundField DataField="hora_fin" HeaderText="Hora de Finalizacion de Carga" />
                    <asp:BoundField DataField="tiempo_total" HeaderText="Tiempo Total" />
                    <asp:BoundField DataField="estado" HeaderText="Estado de Carga" />
                </Columns>

            </asp:GridView>
        </div>

    </asp:Panel>
</asp:Content>
