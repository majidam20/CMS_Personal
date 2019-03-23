<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Catalogs.aspx.cs" Inherits="Catalogs" %>

<%@ Import Namespace="Business.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="Scripts/lightBox/js/prototype.js"></script>
    <script type="text/javascript" src="Scripts/lightBox/js/scriptaculous.js?load=effects,builder"></script>
    <script type="text/javascript" src="Scripts/lightBox/js/lightbox.js"></script>
    <link href="Scripts/lightBox/css/lightbox.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="Server">
    <h1 class="Title" style='text-align: center; padding: 0px'>
        <%# CultureResource.ResourceM.GetString("ArticlesCat")%>
        <asp:Literal ID="ltrHeader" runat="server"></asp:Literal>
    </h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table style="width: 100%;" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" style="background-color: #FFFFFF" align="center" class="content"
                valign="top">
                <div>
                    <table style="width: 100%;" cellpadding="0" cellspacing="0" class="LargFrame" dir='<%# CultureResource.ResourceM.GetString("DirectionN")%>'>
                        
                        <tr>
                            <td style='height: 500px; text-align: <%# CultureResource.ResourceM.GetString("Align")%>;'
                                dir='<%# CultureResource.ResourceM.GetString("Direction")%>' class="Text" valign="top">
                                <asp:Literal ID="ltrText" runat="server"></asp:Literal><br />
                                <hr />
                                <h1 class="Title" style='text-align: center; padding: 0px; color: #757575'>
                                    <asp:Literal ID="ltrCatTitle" runat="server"></asp:Literal>
                                </h1>
                                <asp:ListView ID="lstCatalogs" runat="server" GroupItemCount="1">
                                    <LayoutTemplate>
                                        <table cellspacing="5" runat="server" id="tblProducts" width="100%" dir='<%= CultureResource.ResourceM.GetString("Direction") %>'
                                            style="padding-top: 8px;">
                                            <tr runat="server" id="groupPlaceholder">
                                            </tr>
                                        </table>
                                    </LayoutTemplate>
                                    <GroupTemplate>
                                        <tr runat="server" id="productRow" width="100%">
                                            <td runat="server" id="itemPlaceholder" width="100%">
                                            </td>
                                        </tr>
                                    </GroupTemplate>
                                    <ItemTemplate>
                                        <td  valign="top" style="padding-bottom: 4px" align="<%# CultureResource.ResourceM.GetString("Align")%>"
                                            dir='<%# CultureResource.ResourceM.GetString("Direction")%>'>
                                            <a class="title" target="_blank" title='<%= CultureResource.ResourceM.GetString("ShowDetail")%>'
                                                style='float: <%= CultureResource.ResourceM.GetString("Align") %>; width: 70%'
                                                href='<%# string.Format("CatalogDetail.aspx?ArtID={0}&t={1}", Eval("ID"), Eval("Title")) %>'>
                                                <asp:Label ForeColor="Orange" ID="Label3" runat="server" Text='<%# Eval("Title") %>'></asp:Label></a>
                                            <div style='float: <%= CultureResource.ResourceM.GetString("AlignN") %>; width: 20%'>
                                                <asp:Label ForeColor="#757575" ID="lbCatDate" runat="server" Text='<%# CultureResource.DateByCulture(DateTime.Parse(Eval("EditDate").ToString()), Session["Cult"].ToString()) %>'></asp:Label>
                                            </div>
                                        </td>
                                    </ItemTemplate>
                                </asp:ListView>
                            </td>
                           
                        </tr>
                        <tr>
                            <td class="MiddleFrameFooter" align="center" style="padding-top:118px">
                                <div class="Text" style='float: <%# CultureResource.ResourceM.GetString("AlignN")%>;
                                    width: 40%; padding-top: 0px; text-align: <%# CultureResource.ResourceM.GetString("AlignN")%>'
                                    dir='<%# CultureResource.ResourceM.GetString("Direction")%>'>
                                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                                </div>
                                <div align="center" dir="ltr" style='width: 50%'>
                                    <asp:DataPager runat="server" ID="DataPager1" PageSize="9" QueryStringField="ProdPageNo"
                                        PagedControlID="lstCatalogs">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Image" FirstPageImageUrl="~/images/first.png"
                                                ShowFirstPageButton="true" PreviousPageImageUrl="~/images/prev.png" ShowLastPageButton="false"
                                                ShowNextPageButton="false" FirstPageText='<%= CultureResource.ResourceM.GetString("_First") %>'
                                                PreviousPageText='<%= CultureResource.ResourceM.GetString("_Previous") %>' />
                                            <asp:NumericPagerField ButtonCount="5" NumericButtonCssClass="title" NextPreviousButtonCssClass="title" />
                                            <asp:NextPreviousPagerField ButtonType="Image" ShowLastPageButton="true" ShowNextPageButton="true"
                                                LastPageText='<%= CultureResource.ResourceM.GetString("_Last") %>' NextPageText='<%= CultureResource.ResourceM.GetString("_Next") %>'
                                                ShowPreviousPageButton="false" LastPageImageUrl="~/images/last.png" NextPageImageUrl="~/images/next.png" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="SideContent">
    <div style="float: <%= CultureResource.ResourceM.GetString("Align") %>; width: 100%;
        height: 100%;">
        <div>
            <h1 class="Title" style='text-align: center; padding: 0px'>
                <%# CultureResource.ResourceM.GetString("ArticlesCat")%>
            </h1>
            <hr />
        </div>
        <asp:ListView ID="lstCatalogsCats" runat="server" GroupItemCount="1">
            <LayoutTemplate>
                <table cellspacing="5" runat="server" id="tblProducts" width="100%" dir='<%= CultureResource.ResourceM.GetString("Direction") %>'
                    style="padding-top: 8px;">
                    <tr runat="server" id="groupPlaceholder">
                    </tr>
                </table>
            </LayoutTemplate>
            <GroupTemplate>
                <tr runat="server" id="productRow" width="100%">
                    <td runat="server" id="itemPlaceholder" width="100%">
                    </td>
                </tr>
            </GroupTemplate>
            <ItemTemplate>
                <td valign="top" style="padding-bottom: 4px" align="center">
                    <div>
                        <a title='<%# Eval("AlbumTitle")%>' href='<%# String.Format("Catalogs.aspx?acid={0}&cn={1}",Eval("Id").ToString(),Eval("AlbumTitle"))%>'>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# String.Format("~/ImageReSizer.ashx?aid={0}&if={1}&w={2}&h={3}&t={4}",Eval("ID"), Eval("Thumb"), 144, 105, "pa") %>' />
                        </a>
                    
                    </div>
                    <div align="center" dir='<%= CultureResource.ResourceM.GetString("Direction") %>'>
                        <asp:Label ForeColor="#666666" ID="Label3" colspan="2" runat="server" Text='<%# Eval("AlbumTitle") %>'></asp:Label>
                    </div>
                </td>
            </ItemTemplate>
            <SelectedItemTemplate>
                <td valign="top" style="padding-bottom: 4px; border: 1px solid Orange; background-color: White;"
                    align="center">
                    <div>
                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# String.Format("~/ImageReSizer.ashx?aid={0}&if={1}&w={2}&h={3}&t={4}",Eval("ID"), Eval("Thumb"), 200, 200, "pa") %>' />
                    </div>
                    <div align="center" dir='<%= CultureResource.ResourceM.GetString("Direction") %>'>
                        <asp:Label ForeColor="#666666" ID="Label3" colspan="2" runat="server" Text='<%# Eval("AlbumTitle") %>'></asp:Label>
                    </div>
                </td>
            </SelectedItemTemplate>
            <ItemSeparatorTemplate>
                <hr />
            </ItemSeparatorTemplate>
        </asp:ListView>
       
                    <div align="center" dir="ltr" style="padding-top: 5px; width:100%">
                        <asp:DataPager runat="server" ID="prodPager" PageSize="4" QueryStringField="CatPageNo"
                            PagedControlID="lstCatalogsCats">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Image" FirstPageImageUrl="~/images/first.png"
                                    ShowFirstPageButton="true" PreviousPageImageUrl="~/images/prev.png" ShowLastPageButton="false"
                                    ShowNextPageButton="false" FirstPageText='<%= CultureResource.ResourceM.GetString("_First") %>'
                                    PreviousPageText='<%= CultureResource.ResourceM.GetString("_Previous") %>' />
                                <asp:NumericPagerField ButtonCount="5" NumericButtonCssClass="title" NextPreviousButtonCssClass="title" />
                                <asp:NextPreviousPagerField ButtonType="Image" ShowLastPageButton="true" ShowNextPageButton="true"
                                    LastPageText='<%= CultureResource.ResourceM.GetString("_Last") %>' NextPageText='<%= CultureResource.ResourceM.GetString("_Next") %>'
                                    ShowPreviousPageButton="false" LastPageImageUrl="~/images/last.png" NextPageImageUrl="~/images/next.png" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                
    </div>
</asp:Content>
