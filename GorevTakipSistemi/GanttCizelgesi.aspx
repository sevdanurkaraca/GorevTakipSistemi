<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GanttCizelgesi.aspx.cs" Inherits="GorevTakipSistemi.GanttCizelgesi" %>

<%@ Register Assembly="DevExpress.Web.ASPxGantt.v20.2, Version=20.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGantt" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Gantt Çizelgesi</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row gutter">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel panel-red">
                <div class="panel-heading">
                    <h4>Gantt Çizelgesi</h4>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <dx:ASPxGantt ID="ASPxGantt1" runat="server" Height="700px" Width="100%" Theme="Office365" KeyFieldName="ID" ParentFieldName="ParentID" TasksDataSourceID="SqlDataSource1" DependenciesDataSourceID="SqlDataSource2" ResourceAssignmentsDataSourceID="SqlDataSource4" ResourcesDataSourceID="SqlDataSource3">    
                        <SettingsValidation AutoUpdateParentTasks="true" />
                <SettingsTaskList Width="35%">
                    <Columns>
                        <dx:GanttDataColumn FieldName="Title" Caption="Görev Başlığı" />
                        <dx:GanttDataColumn FieldName="StartDate" Caption="Başlangıç Tarihi" Width="90" DisplayFormat="dd\/MM\/yyyy" />
                        <dx:GanttDataColumn FieldName="EndDate" Caption="Bitiş Tarihi" Width="90" DisplayFormat="dd\/MM\/yyyy" />
                    </Columns>
                </SettingsTaskList>
                <Mappings>
                    <Task Key="Key" Color="Color" ParentKey="ParentKey" Title="Title" Progress="Progress" End="EndDate" Start="StartDate" />
                    <Resource Name="Name" Key="Key" Color="Color" />
                    <Dependency Key="Key" PredecessorKey="PredecessorKey" SuccessorKey="SuccessorKey" DependencyType="Type" />
                    <ResourceAssignment Key="Key" ResourceKey="ResourceKey" TaskKey="TaskKey" />
                </Mappings>
                        </dx:ASPxGantt>
                       
                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:DB_GorevTakipSistemiConnectionString %>" DeleteCommand="DELETE FROM [ResourceAssignments] WHERE [Key] = @Key" InsertCommand="INSERT INTO [ResourceAssignments] ([TaskKey], [ResourceKey]) VALUES (@TaskKey, @ResourceKey)" SelectCommand="SELECT * FROM [ResourceAssignments]" UpdateCommand="UPDATE [ResourceAssignments] SET [TaskKey] = @TaskKey, [ResourceKey] = @ResourceKey WHERE [Key] = @Key">
                            <DeleteParameters>
                                <asp:Parameter Name="Key" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="TaskKey" Type="Int32" />
                                <asp:Parameter Name="ResourceKey" Type="Int32" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="TaskKey" Type="Int32" />
                                <asp:Parameter Name="ResourceKey" Type="Int32" />
                                <asp:Parameter Name="Key" Type="Int32" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DB_GorevTakipSistemiConnectionString %>" DeleteCommand="DELETE FROM [Resources] WHERE [Key] = @Key" InsertCommand="INSERT INTO [Resources] ([Name], [Color]) VALUES (@Name, @Color)" SelectCommand="SELECT * FROM [Resources]" UpdateCommand="UPDATE [Resources] SET [Name] = @Name, [Color] = @Color WHERE [Key] = @Key">
                            <DeleteParameters>
                                <asp:Parameter Name="Key" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="Name" Type="String" />
                                <asp:Parameter Name="Color" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="Name" Type="String" />
                                <asp:Parameter Name="Color" Type="String" />
                                <asp:Parameter Name="Key" Type="Int32" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DB_GorevTakipSistemiConnectionString %>" DeleteCommand="DELETE FROM [Dependencies] WHERE [Key] = @Key" InsertCommand="INSERT INTO [Dependencies] ([PredecessorKey], [SuccessorKey], [Type]) VALUES (@PredecessorKey, @SuccessorKey, @Type)" SelectCommand="SELECT * FROM [Dependencies]" UpdateCommand="UPDATE [Dependencies] SET [PredecessorKey] = @PredecessorKey, [SuccessorKey] = @SuccessorKey, [Type] = @Type WHERE [Key] = @Key">
                            <DeleteParameters>
                                <asp:Parameter Name="Key" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="PredecessorKey" Type="Int32" />
                                <asp:Parameter Name="SuccessorKey" Type="Int32" />
                                <asp:Parameter Name="Type" Type="Int32" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="PredecessorKey" Type="Int32" />
                                <asp:Parameter Name="SuccessorKey" Type="Int32" />
                                <asp:Parameter Name="Type" Type="Int32" />
                                <asp:Parameter Name="Key" Type="Int32" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DB_GorevTakipSistemiConnectionString %>" DeleteCommand="DELETE FROM [Tasks] WHERE [Key] = @Key" InsertCommand="INSERT INTO [Tasks] ([ParentKey], [Title], [StartDate], [EndDate], [Progress], [Color]) VALUES (@ParentKey, @Title, @StartDate, @EndDate, @Progress, @Color)" SelectCommand="SELECT * FROM [Tasks]" UpdateCommand="UPDATE [Tasks] SET [ParentKey] = @ParentKey, [Title] = @Title, [StartDate] = @StartDate, [EndDate] = @EndDate, [Progress] = @Progress, [Color] = @Color WHERE [Key] = @Key">
                            <DeleteParameters>
                                <asp:Parameter Name="Key" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="ParentKey" Type="Int32" />
                                <asp:Parameter Name="Title" Type="String" />
                                <asp:Parameter Name="StartDate" Type="DateTime" />
                                <asp:Parameter Name="EndDate" Type="DateTime" />
                                <asp:Parameter Name="Progress" Type="Int32" />
                                <asp:Parameter Name="Color" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="ParentKey" Type="Int32" />
                                <asp:Parameter Name="Title" Type="String" />
                                <asp:Parameter Name="StartDate" Type="DateTime" />
                                <asp:Parameter Name="EndDate" Type="DateTime" />
                                <asp:Parameter Name="Progress" Type="Int32" />
                                <asp:Parameter Name="Color" Type="String" />
                                <asp:Parameter Name="Key" Type="Int32" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
