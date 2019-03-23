<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Products.aspx.cs" Inherits="Products" %>

<%@ Import Namespace="Business.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="Scripts/lightBox/js/prototype.js"></script>
    <script type="text/javascript" src="Scripts/lightBox/js/scriptaculous.js?load=effects,builder"></script>
    <script type="text/javascript" src="Scripts/lightBox/js/lightbox.js"></script>
    <link href="Scripts/lightBox/css/lightbox.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="Server">
    <h1 class="Title" style='float: <%# CultureResource.ResourceM.GetString("Align")%>;
        text-align: <%# CultureResource.ResourceM.GetString("Align")%>' dir='<%# CultureResource.ResourceM.GetString("Direction")%>'>
        <asp:Literal ID="ltrHeader" runat="server"></asp:Literal>
    </h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table style="width: 100%;height:100%" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" style="background-color: #FFFFFF" align="center" class="content"
                valign="top">
                <table style="width: 100%;height:100%;" cellpadding="0" cellspacing="0" dir='<%# CultureResource.ResourceM.GetString("DirectionN")%>'>
                    <tr>
                        <td style='height: 100%; text-align: <%# CultureResource.ResourceM.GetString("Align")%>;'
                            dir='<%# CultureResource.ResourceM.GetString("Direction")%>' class="Text" valign="top">
                            <asp:Literal ID="ltrText" runat="server"></asp:Literal><br />
                         <hr/>
                            <h1 class="Title" style='text-align: center; padding: 0px; color: #ceb46b'>
                                <asp:Literal ID="ltrCatTitle" runat="server"></asp:Literal>
                            </h1>
                            <asp:ListView ID="lstProducts" runat="server" GroupItemCount="3">
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
                                    <td class="MidFrame"   valign="top"  align="center">
                                        <%--href='<%# string.Format("ImageHandler.ashx?fileName={0}&size=580&path=~/images/Uploads/Products/", Eval("FileName")) %>'--%>
                                        <a title='<%# Eval("Title")%>' style="text-decoration: none;" href='<%# String.Format("images/Uploads/Products/{0}/{1}", Eval("AlbumID"), Eval("FileName")) %>'
                                            rel="lightbox">
                                            <asp:Image CssClass="pic_frame_product" ID="Image1" runat="server" ImageUrl='<%# String.Format("~/ImageReSizer.ashx?aid={0}&if={1}&w={2}&h={3}&t={4}",Eval("AlbumID"), Eval("FileName"), 150, 115, "p") %>' />
                                        </a>
                                     <br />   <br />  <br />   <br />
                                        <a class="title" target="_blank" title='<%= CultureResource.ResourceM.GetString("ShowDetail")%>'
                                            href='<%# string.Format("ProductDetail.aspx?ProdID={0}&ProdTitle={1}", Eval("ID"), Eval("Title")) %>'> 
                                            <asp:Label ForeColor="Orange" ID="Label3" colspan="2" runat="server" Text='<%# Eval("Title") %>'></asp:Label></a>
                                    </td>
                                </ItemTemplate>
                            </asp:ListView>
                        </td>
                    </tr>
                    <tr>
                        <td class="MiddleFrameFooter" align="center">
                            <div class="Text" style='float: <%# CultureResource.ResourceM.GetString("AlignN")%>;
                                width: 40%; padding-top: 0px; text-align: <%# CultureResource.ResourceM.GetString("AlignN")%>'
                                dir='<%# CultureResource.ResourceM.GetString("Direction")%>'>
                                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                            </div>
                            <div align="center" dir="ltr" style='width: 50%'>
                                <asp:DataPager runat="server" ID="DataPager1" PageSize="9" QueryStringField="ProdPageNo"
                                    PagedControlID="lstProducts">
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
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="SideContent">
    <div style="float: <%= CultureResource.ResourceM.GetString("Align") %>; width: 100%;
        height: 100%;">
        <div>
            <h1 class="Title" style='text-align: center; padding: 0px'>
                <%# CultureResource.ResourceM.GetString("ProductCats")%>
            </h1>
            <hr />
        </div>
        <asp:ListView ID="lstProductCats" runat="server" GroupItemCount="1">
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
                        <a title='<%# Eval("AlbumTitle")%>' href='<%# String.Format("Products.aspx?pcid={0}&cn={1}",Eval("Id").ToString(),Eval("AlbumTitle"))%>'>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# String.Format("~/ImageReSizer.ashx?aid={0}&if={1}&w={2}&h={3}&t={4}",Eval("ID"), Eval("Thumb"), 125, 110, "pa") %>' />
                        </a>
                    </div>
                    <br />
                    <div align="center" dir='<%= CultureResource.ResourceM.GetString("Direction") %>'>
                        <asp:Label ForeColor="#666666" ID="Label3" colspan="2" runat="server" Text='<%# Eval("AlbumTitle") %>'></asp:Label>
                    </div>
                </td>
            </ItemTemplate>
            <SelectedItemTemplate>
                <td style="padding-bottom: 4px; border: 1px solid Orange; background-color: White;"
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
        
        <div align="center" dir="ltr" style="width:100%;">
            <asp:DataPager runat="server" ID="prodPager" PageSize="4" QueryStringField="CatPageNo"
                PagedControlID="lstProductCats">
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
