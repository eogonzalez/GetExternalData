<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndicatorsBM.aspx.cs" Inherits="GetExternalData.BM.IndicatorsBM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var objChkd;

        function HandleOnCheck() {

            var chkLst = document.getElementById('cb_idioma');

            if (objChkd && objChkd.checked) {
                objChkd.checked = false;
                objChkd = event.srcElement;
            }
        }

        function radioMe(e) {
            if (!e) e = window.event;
            var sender = e.target || e.srcElement;

            if (sender.nodeName != 'INPUT') return;
            var checker = sender;
            var chkBox = document.getElementById('<%= cb_idioma.ClientID %>');
            var chks = chkBox.getElementsByTagName('INPUT');
            for (i = 0; i < chks.length; i++) {
                if (chks[i] != checker)
                    chks[i].checked = false;
            }
        }

    </script>

    <br />

    <asp:Panel ID="pnlGetIndicators" CssClass="panel panel-primary" runat="server">

        <div class="panel-heading">
            Obtener Indicadores
            <span class="badge">Cantidad de Indicadores
                <asp:Label ID="lbl_cant_indicadores" runat="server"></asp:Label>
            </span>
        </div>

        <div class="panel-body form-horizontal">
            <br />
            <div class="form-group">
                <asp:Label ID="lbl_ingles" CssClass="control-label col-xs-2" runat="server" Text="Obtener versión de indicadores en:"></asp:Label>
                <div class="col-xs-4">
                    <asp:CheckBoxList ID="cb_idioma" runat="server">
                        <asp:ListItem Text="Ingles" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Español" Value="2"></asp:ListItem>
                    </asp:CheckBoxList>

                </div>
            </div>

            <div class="form-group col-xs-2">
                <asp:Button ID="btnGet" CssClass="btn btn-primary" runat="server" Text="Obtener temas" OnClick="btnGet_Click" />
            </div>

        </div>

        <div class="panel-body form-vertical">

            <br />
            <span>Progreso</span>
            <div class="form-group">
                <div class="progress progress-striped">
                    <div class="progress-bar progress-bar-success" role="progressbar"
                        aria-valuenow="60" aria-valuemin="0" aria-valuemax="100"
                        id="barBMInicators" runat="server">
                    </div>
                </div>
            </div>

            <br />
            <span>Bitacora:</span>
            <div>
                <asp:TextBox ID="txt_log_Indicators" type="text" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>

    </asp:Panel>

</asp:Content>
