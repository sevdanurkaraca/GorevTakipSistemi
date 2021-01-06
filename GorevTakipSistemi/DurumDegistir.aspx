<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DurumDegistir.aspx.cs" Inherits="GorevTakipSistemi.DurumDegistir" %>

<%@ Register Assembly="DevExpress.Web.v20.2, Version=20.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Görev Durum Değiştirme</title>

    <link href="css/bootstrap.min.css" media="screen" rel="stylesheet"/>
    <link href="css/main.css" rel="stylesheet" media="screen"/>
    <link href="fonts/icomoon/icomoon.css" rel="stylesheet"/>
    <link href="css/alertify/core.css" rel="stylesheet" />
    <link href="css/alertify/default.css" rel="stylesheet" />
    <script src="js/alertify/alertify.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row gutter">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="modalForm">
                                    <span runat="server" style="color: dodgerblue">Görev ID:</span>
                                    <span runat="server" id="txtGorevID"></span>
                                    <span runat="server" style="color: dodgerblue"> / Başlık:</span>
                                    <span runat="server" id="txtBaslik"></span>
                                </h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <label for="recipient-name" class="control-label">Durum:</label>
                                    <dx:ASPxComboBox ID="cmbDurum" runat="server" ValueType="System.Int32" Theme="Office365">
                                        <Items>
                                            <dx:ListEditItem Selected="true" Text="Boşta" Value="-1" />
                                            <dx:ListEditItem Text="Başlandı" Value="0" />
                                            <dx:ListEditItem Text="Tamamlandı" Value="1" />
                                            <dx:ListEditItem Text="Beklemede" Value="2" />
                                            <dx:ListEditItem Text="Tamamlanmadı" Value="3" />
                                        </Items>
                                    </dx:ASPxComboBox>
                                </div>

                                <div class=" form-group">
                                    <label class="control-label">Konuşma Geçmişi:</label>
                                    <asp:Repeater ID="rptMesaj" runat="server">
                                        <HeaderTemplate>
                                            <ul class="chat-list">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <li id="liMesaj" runat="server" class='<%# Eval("Class") %>'>
                                                <div class="chat-img">
                                                    <img alt="Avtar" src="img/thumbs/userg.png">
                                                </div>
                                                <div class="chat-body">
                                                    <div class="chat-message">
                                                        <h5><%# Eval("AdSoyad") %> - <%# Eval("Tarih") %></h5>
                                                        <p><%# Eval("Mesaj") %></p>
                                                    </div>
                                                </div>

                                            </li>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </ul>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>

                                <div class="form-group">
                                    <label for="message-text" class="control-label">Mesaj:</label>
                                    <dx:ASPxMemo ID="meMesaj" runat="server" Theme="Office365" Rows="3" Width="100%"></dx:ASPxMemo>
                                </div>

                                <div class="form-group">
                                    <label for="message-text" class="control-label">Yapılan İş:</label>
                                    <dx:ASPxMemo ID="meYapilanIs" runat="server" Theme="Office365" Rows="3" Width="100%"></dx:ASPxMemo>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" id="btnGeriDon" runat="server" onserverclick="btnGeriDon_ServerClick">Görevlere Dön</button>
                                <button type="button" class="btn btn-info" id="btnDurumDegistir" runat="server" onserverclick="btnDurumDegistir_ServerClick">Durum Değiştir</button>
                                <button type="button" class="btn btn-warning" id="btnMesaj" runat="server" onserverclick="btnMesaj_ServerClick" >Mesaj Kaydet</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="js/jquery.js"></script>
    <%--<script src="js/jquery-ui.min.js"></script>--%>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/custom.js"></script>
  <%-- <script src="js/scrollup/jquery.scrollUp.js"></script>--%>
</body>
</html>
