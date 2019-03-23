<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCPMaster.master" AutoEventWireup="true" CodeFile="CustomersMap.aspx.cs" Inherits="ControlPanel_CustomersMap" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="BunnyBear" Assembly="msgBox" %>
<%@ Register Assembly="Artem.GoogleMap" Namespace="Artem.Web.UI.Controls" TagPrefix="artem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="styles.css" rel="stylesheet" type="text/css" />
<!--[if lt IE 7]> <style type="text/css">@import "ie6.css";</style><![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntphSideBar" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphInnerTitle" Runat="Server">
     <div style="float: right; color:White;" class="HeaderTop">
        <asp:Label ID="lblHeader" runat="server"></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphMain" Runat="Server">
    <telerik:RadAjaxManager ID="RadAjaxManage" runat="server" SkinID="Office2007">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="pnlMap">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlMap" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" Runat="server" 
            Skin="Black">
    </telerik:RadAjaxLoadingPanel>
    <asp:Panel ID="pnlMap" runat="server">
        <table style="width: 700px;">
        <tr>
            <td colspan="2">
                <div class="HeaderTop" style="padding:10px 10px 10px 0; direction:rtl">
                    در نقشه زیر میتوانید نقاط جدیدی را درج و یا نقطه ای را ویرایش کنید.<br /> برای 
                    ویرایش یک نقطه کافیست آن را انتخاب کنید.</div>
            </td>
        </tr>
        <tr>
            <td colspan="2" dir="rtl">
                <artem:GoogleMap ID="gMap" runat="server" Latitude="35.739441" Longitude="51.787083"
                    DefaultMapView="Hybrid" Zoom="12" ShowScaleControl="true" Height="300px" Width="100%"
                    BorderStyle="Solid" ZoomPanType="Large3D" EnableScrollWheelZoom="True" 
                    EnableDoubleClickZoom="True">
                    <MarkerEvents OnClick="gMap_Markers_OnClick" />
                </artem:GoogleMap>
            </td>
        </tr>
        </table>
            <table>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Panel ID="pnlCulture" runat="server" Visible="false">
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2" align="center">
                                        <img alt="" style="width: 348px; height: 1px; padding: 5px 0 5px 0;" src="Images/line.png" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="drpLang" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpLang_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </td>
                                    <td class="Header" style="text-align: left; direction: rtl; width: 140px">
                                        زبان:
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            <tr>
                <td colspan="2">
                    <img alt="" style="width: 348px; height: 1px; padding: 5px 0 5px 0;" src="Images/line.png" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTitle"
                        ErrorMessage="&lt;strong&gt;عنوان را وارد کنید&lt;/strong&gt;" Text="&lt;strong&gt;عنوان را وارد کنید&lt;/strong&gt;"
                        ValidationGroup="EditFields"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td class="Header" style="text-align: left; direction: rtl; width: 140px;">
                    عنوان:
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img alt="" style="width: 348px; height: 1px; padding: 5px 0 5px 0;" src="Images/line.png" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radEdtText"
                        ErrorMessage="<strong>متن را وارد کنید</strong>" Text="<strong>متن را وارد کنید</strong>"
                        ValidationGroup="EditFields"></asp:RequiredFieldValidator>
                </td>
                <td class="Header" style="text-align: left; direction: rtl;">
                    متن:
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:TextBox ID="txtText" runat="server" Height="100px" TextMode="MultiLine" 
                        Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img alt="" style="width: 348px; height: 1px; padding: 5px 0 5px 0;" src="Images/line.png" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <%--<asp:Button CssClass="submit-login" runat="server" ForeColor="White" Font-Names="Tahoma"
                                ID="btnSend" Text="ارسال" OnClick="btnSend_Click" style="float:left;" 
                                ValidationGroup="EditFields"/>--%>
                    <div style="width: 100%">
                        <asp:Button ID="btnSave" runat="server" CssClass="submit-login" Font-Names="Tahoma"
                            Style="margin:10px; float:right;" ForeColor="White" OnClick="btnSave_Click" Text="ذخیره" ValidationGroup="EditFields" />
                        <asp:Button Style="margin:10px; float:right;" ID="btnCancel" runat="server" CssClass="submit-login"
                            Width="75px" Font-Names="Tahoma" Visible="false" ForeColor="White" Text="موقعیت جدید"
                            ValidationGroup="Fields" OnClick="btnCancel_Click" />
                        <asp:Button Style="margin:10px; float:right;" ID="btnDelete" runat="server" CssClass="submit-login"
                            Width="75px" Font-Names="Tahoma" Visible="false" ForeColor="White" Text="حذف موقعیت"
                            ValidationGroup="Fields" OnClick="btnDelete_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc1:msgBox ID="cliMsgBox" Style="z-index: 103; left: 536px; position: absolute;
        top: 184px" runat="server"></cc1:msgBox>
</asp:Content>

