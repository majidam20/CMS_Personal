<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Videos.aspx.cs" Inherits="Products" %>

<%@ Import Namespace="Business.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="Scripts/lightBox/js/prototype.js"></script>
    <script type="text/javascript" src="Scripts/lightBox/js/scriptaculous.js?load=effects,builder"></script>
    <script type="text/javascript" src="Scripts/lightBox/js/lightbox.js"></script>
    <link href="Scripts/lightBox/css/lightbox.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="Server">
    <h1 class="Title" style='text-align: <%# CultureResource.ResourceM.GetString("Align")%>'
        dir='<%# CultureResource.ResourceM.GetString("Direction")%>'>
        <asp:Literal ID="ltrHeader" runat="server"></asp:Literal>
    </h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table style="width: 100%;" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" style="background-color: #FFFFFF" align="center" class="content"
                valign="top">
                <table style="width: 100%;" cellpadding="0" cellspacing="0" dir='<%# CultureResource.ResourceM.GetString("DirectionN")%>'>
                    <tr>
                        <td style='height: 600px; text-align: <%# CultureResource.ResourceM.GetString("Align")%>;'
                            dir='<%# CultureResource.ResourceM.GetString("Direction")%>' class="Text" valign="top">
                            <br />
                            <div style="width: 95%">
                                <asp:Panel ID="pnlPlayer" runat="server" Visible="false">
                                    <script type="text/javascript">
                                        playerFile = "http://www.mcmediaplayer.com/public/mcmp_0.8.swf";
                                        fpFileURL = '<%= strVideo.ToString() %>';
                                        fpPreviewImageURL = '<%= strImage.ToString() %>';
                                        defaultBufferLength = "8";
                                    </script>
                                    <script type="text/javascript" src="http://www.mcmediaplayer.com/public/mcmp_0.8.js"></script>
                                </asp:Panel>
                                <asp:Label ID="lblVidPM" runat="server" ForeColor="Red" Text='<%# CultureResource.ResourceM.GetString("NoVideo") %>'></asp:Label>
                            </div>
                            <br />
                            <asp:Literal ID="ltrText" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="Text" style='float: <%# CultureResource.ResourceM.GetString("AlignN")%>;
                                padding-top: 0px; text-align: <%# CultureResource.ResourceM.GetString("AlignN")%>'
                                dir='<%# CultureResource.ResourceM.GetString("Direction")%>'>
                                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="SideContent">
    <div style="float: <%= CultureResource.ResourceM.GetString("Align") %>; width: 100%;">
        <asp:ListView ID="lstVideos" runat="server" GroupItemCount="1">
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
                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("ID")%>' Visible="false"></asp:Label>
                    <div>
                        <a title='<%# Eval("Title")%>' href='<%# String.Format("Videos.aspx?vid={0}&vn={1}",Eval("Id").ToString(),Eval("Title"))%>'>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# String.Format("~/ImageReSizer.ashx?aid={0}&if={1}&w={2}&h={3}&t={4}",Eval("AlbumID"), Eval("ImageFileName"), 144, 105, "v") %>' />
                        </a>
                    </div>
                    <div align="center" dir='<%= CultureResource.ResourceM.GetString("Direction") %>'>
                        <asp:Label ForeColor="#666666" ID="Label3" colspan="2" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                        -
                        <asp:Label ForeColor="#FF6600" ID="Label1" colspan="2" runat="server" Text='<%# Eval("VideoLengthTime") %>'></asp:Label>
                    </div>
                </td>
            </ItemTemplate>
            <ItemSeparatorTemplate>
                <hr />
            </ItemSeparatorTemplate>
        </asp:ListView>
    </div>
    <div align="center" dir="ltr" style="padding-top: 5px;">
        <asp:DataPager runat="server" ID="prodPager" PageSize="4" QueryStringField="pageNo"
            PagedControlID="lstVideos">
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
</asp:Content>
