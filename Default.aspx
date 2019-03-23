<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Import Namespace="Business.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="Scripts/jquery-1.5.1.min.js"></script>
    <script type="text/javascript" src="Scripts/coin-slider/coin-slider.min.js"></script>
    <script type="text/javascript" src="Scripts/coin-slider/coin-slider.js"></script>
    <link rel="stylesheet" href="Scripts/coin-slider/coin-slider-styles.css" type="text/css" />
    <style type="text/css">
        .content
        {
            color: #757575;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="Server">
    <h1 class="Title" style='float: <%# CultureResource.ResourceM.GetString("Align")%>;
        text-align: <%# CultureResource.ResourceM.GetString("Align")%>' dir='<%# CultureResource.ResourceM.GetString("Direction")%>'>
        <asp:Literal ID="ltrHeader" runat="server"></asp:Literal>
    </h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table style="width: 100%; height: 510px;" cellpadding="0" cellspacing="0">
        <tr id="nimeh_bala">
           <%-- <td style="background-image: url('Images/LeftTop.jpg'); background-repeat: no-repeat;
                background-position: right">
                &nbsp;
            </td>--%>
            <td style="padding:10px; width: 100%; height: 222px; background-position: 5px 10px; background-repeat: no-repeat" valign="top">
                <div id="d1" class="mainframe">
                    <div id="RouterBanner" style="text-align: center;">
                        <asp:Literal ID="ltrGallery" runat="server"></asp:Literal>
                    </div>
                </div>
                <script type="text/javascript">
                    $(document).ready(function () {
                        $('#RouterBanner').coinslider
                           ({ width: 480,
                               height: 205,
                               delay: 10000,
                               links: false,
                               hoverPause: false,
                               navigation: true
                           });
                    });
                </script>
            </td>
            
        </tr>
        <tr id="nimeh_paeen">
            <td colspan="3" style="background-color: #FFFFFF" align="center" class="content"
                valign="top">
                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style='height: 600px; text-align: <%# CultureResource.ResourceM.GetString("Align")%>'
                            dir='<%# CultureResource.ResourceM.GetString("Direction")%>' class="Text" valign="top">
                            <asp:Literal ID="ltrText" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td style='float: <%# CultureResource.ResourceM.GetString("Align")%>;'>
                            <div class="Text" style='float: <%# CultureResource.ResourceM.GetString("AlignN")%>;
                                padding-top: 15px; text-align: <%# CultureResource.ResourceM.GetString("AlignN")%>'
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
