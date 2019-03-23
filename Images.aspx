<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Images.aspx.cs" Inherits="Images" %>

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
    <table style="width: 100%;" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" style="background-color: #FFFFFF" align="center" valign="top">
                <div>
                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style='height: 400px; text-align: <%# CultureResource.ResourceM.GetString("Align")%>'
                                dir='<%# CultureResource.ResourceM.GetString("Direction")%>' class="Text" valign="top">
                                <asp:Literal ID="ltrText" runat="server"></asp:Literal><br />
                                <asp:ListView ID="lstPhoto" runat="server" GroupItemCount="3">
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
                                        <td class="MidFrame" valign="top" style="padding-bottom: 4px" align="center">
                                            <div style="width: 160px;height: 110px; margin-bottom:5px;">
                                            <a title='<%# Eval("Title")%>' class="title" href='<%# string.Format("images/Uploads/PhotoGallery/{0}/{1}", Eval("AlbumID"), Eval("FileName")) %>'
                                                rel="lightbox">
                                                <asp:Image ID="Image1" CssClass="pic_frame" runat="server" ImageUrl='<%# String.Format("~/ImageReSizer.ashx?aid={0}&if={1}&w={2}&h={3}&t={4}", Eval("AlbumID"), Eval("FileName"), 150, 115, "i") %>' />
                                            </a>
                                             </div><br /> <br /> <br /> <br /> 
                                            <a class="title" target="_blank" title='<%= CultureResource.ResourceM.GetString("ShowDetail")%>'
                                                href='<%# string.Format("images/Uploads/PhotoGallery/{0}/{1}", Eval("AlbumID"), Eval("FileName")) %>'>
                                                <asp:Label ForeColor="Orange" ID="Label3" colspan="2" runat="server" Text='<%# Eval("Title") %>'></asp:Label></a>
                                               
                                        </td>
                                    </ItemTemplate>
                                </asp:ListView>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div class="Text" style='float: <%# CultureResource.ResourceM.GetString("AlignN")%>;
                                    width: 30%; padding-top: 0px; text-align: <%# CultureResource.ResourceM.GetString("AlignN")%>'
                                    dir='<%# CultureResource.ResourceM.GetString("Direction")%>'>
                                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                                </div>
                                <div align="center" dir="ltr" style='width: 60%'>
                                    <asp:DataPager runat="server" ID="prodPager" PageSize="9" QueryStringField="pageNo"
                                        PagedControlID="lstPhoto">
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
