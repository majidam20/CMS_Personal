<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCPMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .menu{width:33%; height:150px;}
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphInnerTitle" Runat="Server">
    <div style="float: right;" class="HeaderTop">
        <asp:Label ID="lblHeader" runat="server"></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphMain" Runat="Server">
    <table width="500px" align="center">
        <tr>
            <td class="menu">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/ControlPanel/Images/Articles.png"
                                Width="100px" PostBackUrl="~/ControlPanel/Articles.aspx" />
                        </td>
                    </tr>
                    <tr>
                        <td class="titleBar">
                            کاتالوگ ها
                        </td>
                    </tr>
                </table>
            </td>
            <td class="menu">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgbtnAlbums" runat="server" 
                                ImageUrl="~/ControlPanel/Images/Albums.png" Width="100px"
                                PostBackUrl="~/ControlPanel/Albums.aspx" />
                        </td>
                    </tr>
                    <tr>
                        <td class="titleBar">مدیریت آلبومها</td>
                    </tr>
                </table>
            </td>
            <td class="menu">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgbtnPages" runat="server" ImageUrl="~/ControlPanel/Images/Pages.png" Width="100px" PostBackUrl="~/ControlPanel/Pages.aspx" />
                        </td>
                    </tr>
                    <tr>
                        <td class="titleBar">صفحات</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgbtnTour" runat="server" ImageUrl="~/ControlPanel/Images/VideoGallery.png" Width="100px"
                                PostBackUrl="~/ControlPanel/VideoGallery.aspx" />
                        </td>
                    </tr>
                    <tr>
                        <td class="titleBar">
                            گالری ویدئو</td>
                    </tr>
                </table>
            </td>
            <td class="menu">
                <%--<table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgbtnComments" runat="server" ImageUrl="~/ControlPanel/Images/Comments.jpg" PostBackUrl="~/ControlPanel/Comments.aspx" />
                        </td>
                    </tr>
                    <tr>
                        <td class="titleBar">
                            نظرات کاربران
                        </td>
                    </tr>
                </table>--%>
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgbtnProducts" runat="server" ImageUrl="~/ControlPanel/Images/PhotoGallery.png" Width="100px" PostBackUrl="~/ControlPanel/PhotoGallery.aspx" />
                        </td>
                    </tr>
                    <tr>
                        <td class="titleBar">گالری تصاویر</td>
                    </tr>
                </table>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/ControlPanel/Images/Products.png"
                                Width="100px" PostBackUrl="~/ControlPanel/Products.aspx" />
                        </td>
                    </tr>
                    <tr>
                        <td class="titleBar">
                            محصولات</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
            <td class="menu">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgbtnContact" runat="server" ImageUrl="~/ControlPanel/Images/Contacts.jpg" PostBackUrl="~/ControlPanel/Contacts.aspx" />
                        </td>
                    </tr>
                    <tr>
                        <td class="titleBar">
                            تماسهای دریافتی
                         </td>
                    </tr>
                </table>
            </td>
            <td>
                
            </td>
        </tr>
    </table>
</asp:Content>

