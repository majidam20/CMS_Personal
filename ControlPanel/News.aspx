﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCPMaster.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="ControlPanel_News" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="BunnyBear" Assembly="msgBox" %>
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
            <telerik:AjaxSetting AjaxControlID="pnlNews">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pnlNews" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" Runat="server" 
            Skin="Black">
    </telerik:RadAjaxLoadingPanel>
    <asp:Panel ID="pnlNews" runat="server">
        <table style="width: 700px;">
        <tr>
            <td colspan="2">
                <div class="HeaderTop" style="padding:10px 10px 10px 0; direction:rtl">
                    خبر جدیدی را درج و یا خبری را ویرایش کنید.</div>
            </td>
        </tr>
        <tr>
            <td colspan="2" dir="rtl">
                <asp:GridView ID="grdNews" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" AllowPaging="True" HorizontalAlign="Center"
                        Width="100%" DataKeyNames="ID" OnRowCreated="grdNews_RowCreated" 
                    PageSize="15" AllowSorting="True" 
                    OnSelectedIndexChanged="grdNews_SelectedIndexChanged" 
                    OnPageIndexChanging="grdNews_PageIndexChanging" 
                    OnRowCommand="grdNews_RowCommand">
                        <RowStyle BackColor="White" CssClass="text" Font-Size="13px" />
                        <Columns>
                            <asp:TemplateField HeaderText="رديف" ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <center>
                                        <asp:Literal ID="litRowNumberNormal" runat="server"></asp:Literal>
                                    </center>
                                </ItemTemplate>
                                <ItemStyle Width="20px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="عنوان خبر">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:HyperLink ID="hypLinkView" runat="server" CssClass="pageViewLink" NavigateUrl='<%# String.Format("~/News/NewsView.aspx?nid={0}&t={1}", Eval("ID"), Eval("Title")) %>'><asp:Label ID="lblName" runat="server" Text='<%# Eval("Title") %>'></asp:Label></asp:HyperLink> <a style="color:white" href='../' target="_blank"></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="تاریخ ویرایش" HeaderStyle-Width="120px">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%#Business.UI.CultureResource.DateByCulture(DateTime.Parse(Eval("SendDate").ToString()), "fa-IR") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="120px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="True" HeaderText="ویرایش" HeaderStyle-Width="30px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                        ImageUrl="~/ControlPanel/Images/edit.png" Text="ویرایش"  />
                                </ItemTemplate>
                                <HeaderStyle Width="30px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="True" HeaderText="حذف" HeaderStyle-Width="30px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDel" runat="server" CausesValidation="False" OnClientClick=<%# String.Format("return confirm(\"آیا از حذف خبر '{0}'  مطمئن هستید؟ \");", Eval("Title")) %>
                                        CommandName="DelNews" CommandArgument='<%#String.Format("{0},{1}", Eval("ID").ToString(), Eval("Title").ToString()) %>' Style="display: table-cell;" ImageUrl="~/ControlPanel/Images/Delete.png"
                                        Text="حذف" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    <FooterStyle BackColor="#555c61" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#555c61" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#888d90" />
                    <HeaderStyle BackColor="#555c61" Font-Bold="True" ForeColor="White" CssClass="textHeader" Font-Size="16px" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="#bec0c1" />
                    </asp:GridView>
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
                <td class="Header" style="text-align: left; direction: rtl;">
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
                    <telerik:RadEditor runat="server" ID="radEdtText" SkinID="DefaultSetOfTools" Width="650px"
                        EnableResize="false" AllowScripts="True">
                        <Content>
                        </Content>
                        <ImageManager ViewPaths="~/images" UploadPaths="~/images/Uploads/Images" DeletePaths="~/images/Uploads/Images" />
                        <FlashManager ViewPaths="~/images" UploadPaths="~/images/Uploads/FLV" DeletePaths="~/images/Uploads/FLV" />
                        <MediaManager ViewPaths="~/images" UploadPaths="~/images/Uploads/Media" DeletePaths="~/images/Uploads/Media" />
                        <DocumentManager ViewPaths="~/images" UploadPaths="~/images/Uploads/Documents" DeletePaths="~/images/Uploads/Documents" MaxUploadFileSize="2048000" />
                    </telerik:RadEditor>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img alt="" style="width: 348px; height: 1px; padding: 5px 0 5px 0;" src="Images/line.png" />
                </td>
            </tr>
            <tr>
                <td align="right" style="padding-right: 10px">
                    <asp:Label ID="lblDate" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
                <td class="Header" style="text-align: left; direction: rtl;">
                    تاریخ ویرایش:
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
                    <div style="width: 200px">
                        <asp:Button ID="btnSave" runat="server" CssClass="submit-login" Font-Names="Tahoma" style="float:left;"
                            ForeColor="White" OnClick="btnSave_Click" Text="ذخیره" ValidationGroup="EditFields" />
                        <asp:Button Style="float: right;" ID="btnCancel" runat="server" CssClass="submit-login"
                            Font-Names="Tahoma" Visible="false" ForeColor="White" Text="خبر جدید" ValidationGroup="Fields"
                            OnClick="btnCancel_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc1:msgBox ID="cliMsgBox" Style="z-index: 103; left: 536px; position: absolute;
        top: 184px" runat="server"></cc1:msgBox>
</asp:Content>

