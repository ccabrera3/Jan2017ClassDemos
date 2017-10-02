<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="BootStrapTabs.aspx.cs" Inherits="SamplePages_BootStrapTabs" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Bootstrap Tabs</h1>

    <div class="row">
        <div class="col-md-12">
            <!--Nav tabs-->
            <ul class="nav nav-tabs">
                <li class="active"><a href="#home" data-toggle="tab">Home</a></li>
                <li ><a href="#artist" data-toggle="tab">Artists</a></li>
                <li ><a href="#albums" data-toggle="tab">Albums</a></li>
            </ul>

            <!--Tab panes one for each tab-->
            <div class="tab-content">
                <div role="tabpanel" class="tab-pane active" id="home">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         
                        <ContentTemplate>  
                            
                            <p>This is an example of using css tabs. Each tab is
                                created in the nav tab area. It is assigned a href
                                value to identify the tab pane.
                            </p>
                            <p>
                                Although only one pane will be viewed at a time
                                all panes are loaded with their data. You have
                                access to all controls on all pane at all times.
                            </p>
                         
                        </ContentTemplate>
                    </asp:UpdatePanel>   
                </div>
                 <div role="tabpanel" class="tab-pane" id="artist">
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:ListView ID="ArtistList" runat="server" DataSourceID="ArtistListODS">
                                 <EmptyDataTemplate>
                                     <table runat="server" style="">
                                         <tr>
                                             <td>No data was returned.</td>
                                         </tr>
                                     </table>
                                 </EmptyDataTemplate>
                     
                                 <ItemTemplate>
                                     <tr style="">
                                         <td>
                                             <asp:Label Text='<%# Eval("ArtistId") %>' runat="server" ID="ArtistIdLabel" /></td>
                                         <td>
                                             <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" /></td>
                                     </tr>
                                 </ItemTemplate>
                                 <LayoutTemplate>
                                     <table runat="server">
                                         <tr runat="server">
                                             <td runat="server">
                                                 <table runat="server" id="itemPlaceholderContainer" style="" border="0">
                                                     <tr runat="server" style="">
                                                         <th runat="server">ArtistId</th>
                                                         <th runat="server">Name</th>
                                                     </tr>
                                                     <tr runat="server" id="itemPlaceholder"></tr>
                                                 </table>
                                             </td>
                                         </tr>
                                         <tr runat="server">
                                             <td runat="server" style="">
                                                 <asp:DataPager runat="server" ID="DataPager2">
                                                     <Fields>
                                                         <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                         <asp:NumericPagerField></asp:NumericPagerField>
                                                         <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                     </Fields>
                                                 </asp:DataPager>
                                             </td>
                                         </tr>
                                     </table>
                                 </LayoutTemplate>
                   
                             </asp:ListView>
                            <asp:ObjectDataSource ID="ArtistListODS" runat="server" 
                             OldValuesParameterFormatString="original_{0}" 
                             SelectMethod="Artist_ListAll" 
                             TypeName="ChinookSystem.BLL.ArtistController">
                            </asp:ObjectDataSource>
                        </ContentTemplate>
                    </asp:UpdatePanel> 
                </div> 
                 <div role="tabpanel" class="tab-pane" id="albums">
                      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:ListView ID="AlbumList" runat="server" DataSourceID="AlbumListODS" >
                                <EmptyDataTemplate>
                                     <table runat="server" style="">
                                         <tr>
                                             <td>No data was returned.</td>
                                         </tr>
                                     </table>
                                 </EmptyDataTemplate>
                                 <ItemTemplate>
                                     <tr style="">
                                         <td>
                                             <asp:Label Text='<%# Eval("Artist") %>' runat="server" ID="ArtistLabel" /></td>
                                         <td>
                                             <asp:GridView ID="Albums" runat="server" AutoGenerateColumns="true"
                                                  DataSource='<%# Eval("Albums") %>'></asp:GridView>
                                             </td>
                                     </tr>
                                 </ItemTemplate>
                                 <LayoutTemplate>
                                     <table runat="server">
                                         <tr runat="server">
                                             <td runat="server">
                                                 <table runat="server" id="itemPlaceholderContainer" style="" border="0">
                                                     <tr runat="server" style="">
                                                         <th runat="server">Artist</th>
                                                         <th runat="server">Albums</th>
                                                     </tr>
                                                     <tr runat="server" id="itemPlaceholder"></tr>
                                                 </table>
                                             </td>
                                         </tr>
                                         <tr runat="server">
                                             <td runat="server" style="">
                                                 <asp:DataPager runat="server" ID="DataPager1">
                                                     <Fields>
                                                         <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                         <asp:NumericPagerField></asp:NumericPagerField>
                                                         <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                     </Fields>
                                                 </asp:DataPager>
                                             </td>
                                         </tr>
                                     </table>
                                 </LayoutTemplate>
                            </asp:ListView>
                            <asp:ObjectDataSource ID="AlbumListODS" runat="server" 
                                 OldValuesParameterFormatString="original_{0}" 
                                 SelectMethod="ArtistAlbumReleases_List" 
                                 TypeName="ChinookSystem.BLL.AlbumController"
                                >
                            </asp:ObjectDataSource>
                        </ContentTemplate>
                    </asp:UpdatePanel>   
                </div>
           </div>
        </div>
    </div>
</asp:Content>

