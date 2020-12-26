<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GorevOlustur.aspx.cs" Inherits="GorevTakipSistemi.GorevOlustur" %>

<%@ Register Assembly="DevExpress.Web.v20.2, Version=20.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Görev Oluştur</title>

    <script type="text/javascript">
        var seperator = "; ";

        function updateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            deDropDown.SetText(getSelectedItemsText(selectedItems));
        }

        function getSelectedItemsText(selectedItems) {
            var texts = [];
            for (var i = 0; i < selectedItems.length; i++) {
                texts.push(selectedItems[i].text);
            }
            return texts.join(seperator);
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row gutter">
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="form-group">
                        <label for="userName">Başlangıç Tarihi</label>
                        <dx:ASPxDateEdit ID="deStartDate" runat="server" Theme="Office365" Width="100%"></dx:ASPxDateEdit>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="form-group has-success">
                        <label for="userName">Bitiş Tarihi</label>
                        <dx:ASPxDateEdit ID="deEndDate" runat="server" Width="100%" Theme="Office365"></dx:ASPxDateEdit>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row gutter">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel">
                <div class="panel-heading">
                    <h4>Görev Bilgileri</h4>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <div class="row gutter">
                            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                <label for="username">Görevli </label>
                                <dx:ASPxDropDownEdit ID="deDropDown" runat="server" Width="100%" Theme="Office365" ClientInstanceName="deDropDown">
                                    <DropDownWindowTemplate>
                                        <dx:ASPxListBox ID="listBox" runat="server" ClientInstanceName="checkListBox" SelectionMode="CheckColumn" Height="200" Width="100%" ValueField="ID" TextField="AdSoyad" ValueType="System.Int32" Theme="Office365">
                                            <ClientSideEvents SelectedIndexChanged="updateText" />
                                        </dx:ASPxListBox>
                                    </DropDownWindowTemplate>
                                </dx:ASPxDropDownEdit>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row gutter">
                            <div class="col-lg-5 col-md-5 col-sm-8 col-xs-12">
                                <label for="username">Görev Başlığı </label>
                                <dx:ASPxTextBox ID="txtBaslik" runat="server" Width="100%" Theme="Office365"></dx:ASPxTextBox>
                            </div>
                            <div class="col-lg-4 col-md-5 col-sm-8 col-xs-12">
                                <label for="username">Dosya Seçiniz </label>
                                <asp:FileUpload ID="fuDosya" runat="server" AllowMultiple="true" />
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="row gutter">
                            <div class="col-lg-5 col-md-3 col-sm-6 col-xs-12">
                                <label for="username">Görev Detayı </label>
                                <dx:ASPxMemo ID="meDetay" runat="server" Width="100%" Theme="Office365" Rows="4"></dx:ASPxMemo>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row gutter">
        <div class="col-md-12">
            <div class="panel no-border">
                <div class="btn-group">
                    <dx:ASPxButton ID="btnKaydet" runat="server" Theme="Office365" Text="Kaydet" OnClick="btnKaydet_Click"></dx:ASPxButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
