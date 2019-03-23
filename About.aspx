<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="About.aspx.cs" Inherits="About" %>

<%@ Register Assembly="Artem.GoogleMap" Namespace="Artem.Web.UI.Controls" TagPrefix="artem" %>
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
        <td>
        <div style="float: <%# CultureResource.ResourceM.GetString("Align")%>; width: 95%">
        <asp:Literal ID="ltrText" runat="server"></asp:Literal>
    </div>
        </td>
        </tr>
        <tr>
            <td class="LargFrameFooter" style='float: <%# CultureResource.ResourceM.GetString("AlignN")%>;'>
                <div class="Text" style='float: <%# CultureResource.ResourceM.GetString("AlignN")%>;
                    padding-top: 15px; text-align: <%# CultureResource.ResourceM.GetString("AlignN")%>'
                    dir='<%# CultureResource.ResourceM.GetString("Direction")%>'>
                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="SideContent">
    
    <div id="googlee" style="padding: 10px 0px 0px 0px; width: 215px; height: 300px;">
        <artem:GoogleMap ID="gMap" runat="server" Latitude="35.705741" Longitude="51.382928"
            DefaultMapView="Hybrid" Zoom="12" ShowScaleControl="False" Height="100%" Width="100%"
            BorderStyle="Solid" ZoomPanType="Large3D" EnableScrollWheelZoom="True" 
            EnableDoubleClickZoom="True" ShowMapTypeControl="False">
            <Markers>
                <artem:GoogleMarker Address="" AutoPan="True" Bouncy="False" Clickable="True" DragCrossMove="False"
                    Draggable="False" IconAnchor="8,16" IconSize="16,16" InfoWindowAnchor="0,0" Latitude="35.735339"
                    Longitude="51.382928" OpenInfoBehaviour="Click" ShadowSize="16,16">
                </artem:GoogleMarker>
            </Markers>
        </artem:GoogleMap>
    </div>
</asp:Content>
