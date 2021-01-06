<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="KullanicilarEkrani.aspx.cs" Inherits="GorevTakipSistemi.KullanicilarEkrani" %>

<%@ Register Assembly="DevExpress.Web.v20.2, Version=20.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Kullanıcılar</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row gutter">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="text-center">
                        <button type="button" class="btn btn-success" data-toggle="modal" data-target="#myModalDefault">Kullanıcı Ekle</button>
                    </div>
                    <div class="modal fade" id="myModalDefault" tabindex="-1" role="dialog" aria-labelledby="myModalDefault">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                                    <h4 class="modal-title" id="myModalDefault">Kullanıcı Ekle</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                        <label for="recipient-name" class="control-label">Kullanıcı Adı:</label>
                                        <dx:ASPxTextBox ID="txtKullAd" runat="server" Theme="MaterialCompact" Width="100%"></dx:ASPxTextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="recipient-name" class="control-label">Parola:</label>
                                        <dx:ASPxTextBox ID="txtParola" Password="true" runat="server" Theme="MaterialCompact" Width="100%"></dx:ASPxTextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="recipient-name" class="control-label">Ad Soyad:</label>
                                        <dx:ASPxTextBox ID="txtAdsoyad" runat="server" Theme="MaterialCompact" Width="100%"></dx:ASPxTextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="recipient-name" class="control-label">Email:</label>
                                        <dx:ASPxTextBox ID="txtEmail" runat="server" Theme="MaterialCompact" Width="100%"></dx:ASPxTextBox>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-light-grey" data-dismiss="modal">Kapat</button>
                                    <button type="button" class="btn btn-info" id="btnKaydet" runat="server" onserverclick="btnKaydet_ServerClick">Kaydet</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row gutter">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-red">
                <div class="panel-heading">
                    <h4>Kullanıcı Listesi</h4>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" Theme="Office365" Width="100%" AutoGenerateColumns="false"
                            KeyFieldName="ID" OnContextMenuItemClick="grid_ContextMenuItemClick"
                            OnRowDeleting="grid_RowDeleting" OnRowUpdating="grid_RowUpdating">

                            <Settings AutoFilterCondition="Contains" />
                            <SettingsBehavior AllowFocusedRow="true" EnableCustomizationWindow="true" EnableRowHotTrack="true" ConfirmDelete="true" AllowSelectByRowClick="true" />
                            <%--<SettingsPopup>
                                <FilterControl AutoUpdatePosition="False"></FilterControl>
                            </SettingsPopup>--%>
                            <SettingsText ConfirmDelete="Silmek istediğinize emin misiniz?" />
                            <SettingsContextMenu Enabled="true"></SettingsContextMenu>
                            <SettingsDataSecurity AllowDelete="true" AllowEdit="true" AllowInsert="false" />
                            <SettingsContextMenu Enabled="true"></SettingsContextMenu>
                            <ClientSideEvents ContextMenu="OnContextMenu" ContextMenuItemClick="OnContextMenuItemClick" />

                            <Columns>
                                <dx:GridViewDataTextColumn Caption="ID" FieldName="ID" Visible="false" Width="1px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Kullanıcı Adı" FieldName="KullaniciAdi" VisibleIndex="1">
                                    <PropertiesTextEdit>
                                        <ClientSideEvents Validation="function(s, e){ e.isValid = (s.GetText() != '' )}" />
                                        <ValidationSettings Display="Dynamic" ErrorText="Kullanıcı Adı boş geçilemez"></ValidationSettings>
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Parola" FieldName="Parola" Visible="false">
                                    <PropertiesTextEdit Password="true" NullText="Boş bırakıldığında parola değişmez">
                                    </PropertiesTextEdit>
                                    <EditFormSettings Visible="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Adı Soyadı" FieldName="AdSoyad" VisibleIndex="3">
                                    <PropertiesTextEdit>
                                        <ClientSideEvents Validation="function(s, e){ e.isValid = (s.GetText() != '' )}" />
                                        <ValidationSettings Display="Dynamic" ErrorText="Ad Soyad boş geçilemez"></ValidationSettings>
                                    </PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Email" FieldName="Email" VisibleIndex="4">
                                </dx:GridViewDataTextColumn>
                            </Columns>

                            <Styles>
                                <AlternatingRow Enabled="True"></AlternatingRow>
                                <FocusedRow BackColor="LightBlue" ForeColor="Black"></FocusedRow>
                                <Header BackColor="#4796CE" ForeColor="White"></Header>
                            </Styles>
                        </dx:ASPxGridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
