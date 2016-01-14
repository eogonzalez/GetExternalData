<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BitacoraUN.aspx.cs" Inherits="GetExternalData.COMTRADE.BitacoraUN" %>
<%--<asp:Content ID="ContentH" ContentPlaceHolderID="head" runat="server">

</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        .ColumnaOculta {
            display: none;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function SelectSingleRadiobutton(rdbtnid) {
            var rdBtn = document.getElementById(rdbtnid);
            var rdBtnList = document.getElementsByTagName("input");
            for (i = 0; i < rdBtnList.length; i++) {
                if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id) {
                    rdBtnList[i].checked = false;
                }
            }
        }
    </script>

    <br />
    <%--Panel de bitacora de Encabezado--%>
    <asp:Panel ID="pnlGetCommodity" CssClass="panel panel-primary" runat="server">

        <div class="panel-heading">Listado de Consultas Realizadas</div>
        <br />

        <div class="btn-group pull-right" role="group">
            <asp:LinkButton ID="lkb_detalles" runat="server" CssClass="btn btn-primary" OnClick="lkb_detalles_Click"> 
                <i aria-hidden="true" class="glyphicon glyphicon-list"></i>
                Ver Detalles 
            </asp:LinkButton>
        </div>

        <div>
            <asp:GridView ID="gvEncBitacora" runat="server"
                CssClass="table table-hover table-striped"
                GridLines="None"
                EmptyDataText="No se encontraron registros."
                AutoGenerateColumns="false" AllowPaging="true"
                OnPageIndexChanging="gvEncBitacora_PageIndexChanging">

                <PagerSettings Mode="Numeric"
                    Position="Bottom"
                    PageButtonCount="10"/>

                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:RadioButton ID="rb_registro" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)" />
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:BoundField DataField="codigo_pais" HeaderText="Codigo Pais" />
                    <asp:BoundField DataField="name" HeaderText="Nombre Pais" />
                    <asp:BoundField DataField="anio_carga" HeaderText="Anio Carga" />
                    <asp:BoundField DataField="codigo_sistem_harmony" HeaderText="Codigo Version SAC" />
                    <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha de Inicio de Carga" />
                    <asp:BoundField DataField="fecha_fin" HeaderText="Fecha de Finalizacion de Carga" />
                    <asp:BoundField DataField="estado" HeaderText="Estado de Carga" />
                </Columns>

            </asp:GridView>
        </div>

    </asp:Panel>

</asp:Content>
