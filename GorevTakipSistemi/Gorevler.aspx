<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Gorevler.aspx.cs" Inherits="GorevTakipSistemi.Gorevler" %>

<%@ Register Assembly="DevExpress.Web.v20.2, Version=20.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Görevler</title>

    <%--<script type="text/javascript">
        function onSelectedIndexChanged() {
            grid.PerformCallback();
        }
    </script>--%>

    <script type="text/javascript">
        function onSelectedIndexChanged() {
            grid.PerformCallback();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class=" row gutter">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="form-group">
                <label for="recipient-name" class="control-label">Durum:</label>
                <dx:ASPxComboBox ID="cmbDurum" runat="server" ValueType="System.Int32" Theme="Office365">
                    <Items>
                        <dx:ListEditItem Selected="true" Text="Hepsi" Value="-1" />
                        <dx:ListEditItem Text="Başlandı" Value="0" />
                        <dx:ListEditItem Text="Tamamlandı" Value="1" />
                        <dx:ListEditItem Text="Beklemede" Value="2" />
                        <dx:ListEditItem Text="Tamamlanmadı" Value="3" />
                    </Items>
                    <ClientSideEvents SelectedIndexChanged="onSelectedIndexChanged" />
                </dx:ASPxComboBox>
            </div>
        </div>
    </div>

    <div class="row gutter">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-red">
                <div class="panel-heading">
                    <h4>Görev Listesi</h4>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" Theme="Office365" Width="100%" AutoGenerateColumns="false"
                            KeyFieldName="ID" OnContextMenuInitialize="grid_ContextMenuInitialize" OnContextMenuItemClick="grid_ContextMenuItemClick"
                            OnRowDeleting="grid_RowDeleting" OnHtmlDataCellPrepared="grid_HtmlDataCellPrepared" OnCustomCallback="grid_CustomCallback">
                            <Settings AutoFilterCondition="Contains" />
                            <SettingsBehavior AllowFocusedRow="true" EnableCustomizationWindow="true" EnableRowHotTrack="true" ConfirmDelete="true" AllowSelectByRowClick="true" />

                            <%--<SettingsPopup>
                                <FilterControl AutoUpdatePosition="False"></FilterControl>
                            </SettingsPopup> --%>

                            <SettingsText ConfirmDelete="Silmek istediğinize emin misiniz?" />
                            <SettingsContextMenu Enabled="true"></SettingsContextMenu>
                            <SettingsDataSecurity AllowDelete="true" AllowEdit="false" AllowInsert="false" />
                            <SettingsContextMenu Enabled="true"></SettingsContextMenu>
                            <ClientSideEvents ContextMenu="OnContextMenu" ContextMenuItemClick="OnContextMenuItemClick" />

                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Görev ID" FieldName="ID" VisibleIndex="0" Width="1px">
                                    <DataItemTemplate>
                                        <asp:LinkButton ID="btnTamamla" runat="server" CssClass="btn btn-xs btn-success" data-toggle="tooltip" data-placement="right"
                                            ForeColor="Black"
                                            title="Görev durumunu değiştirmek için tıklayınız" OnClick="btnTamamla_Click" Text='<%# Eval("ID") %>'>
                                        </asp:LinkButton>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Görev Başlığı" FieldName="GorevBaslik" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Görev Detayı" FieldName="GorevDetay" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn Width="1px" Caption="Başlangıç Tarihi" FieldName="BaslangicTarihi" VisibleIndex="3">
                                    <PropertiesDateEdit DisplayFormatString="d"></PropertiesDateEdit>
                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn Width="2px" Caption="Bitiş Tarihi" FieldName="BitisTarihi" VisibleIndex="4">
                                    <PropertiesDateEdit DisplayFormatString="d"></PropertiesDateEdit>
                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn Caption="Oluşturan" FieldName="Olusturan" VisibleIndex="5">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Görevli" FieldName="Gorevli" VisibleIndex="6">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Yapılan İş" FieldName="YapilanIs" VisibleIndex="7">
                                    <%--<EditFormSettings />--%>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Width="1px" Caption="Dosya" FieldName="Dosya" VisibleIndex="8">
                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                    <DataItemTemplate>
                                        <asp:LinkButton ID="btnIndir" runat="server" CssClass="btn btn-xs btn-warning" data-toggle="tooltip" data-placement="left"
                                            ForeColor="Black"
                                            title="Dosyaları indirmek için tıklayınız" OnClick="btnIndir_Click" Text='<%# Eval("Dosya") %>'>
                                        </asp:LinkButton>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Grup Guid" FieldName="GrupGuid" Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Durum" FieldName="DurumBilgisi" VisibleIndex="8">
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
