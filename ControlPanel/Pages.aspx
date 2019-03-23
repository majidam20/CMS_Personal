<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCPMaster.master" AutoEventWireup="true" CodeFile="Pages.aspx.cs" Inherits="ControlPanel_Pages" %>
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
    <table style="width: 700px;">
        <tr>
            <td colspan="2">
                <div class="HeaderTop" style="padding:10px 10px 10px 0;">
                    صفحه مورد نظر را برای ویرایش انتخاب کنید</div>
            </td>
        </tr>
        <tr>
            <td colspan="2" dir="rtl">
                <asp:GridView ID="grdPages" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" AllowPaging="True" HorizontalAlign="Center"
                        Width="100%" DataKeyNames="ID" OnRowCreated="grdPages_RowCreated" 
                    PageSize="15" AllowSorting="True" 
                    onselectedindexchanged="grdPages_SelectedIndexChanged" 
                    onpageindexchanging="grdPages_PageIndexChanging">
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
                            <asp:TemplateField HeaderText="نام صفحه">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <a style="color:white" href='../<%# Eval("PageName") %>' target="_blank"><asp:Label ID="lblName" runat="server" Text='<%# Eval("PageName") %>'></asp:Label></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="عنوان فارسی">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblFaTitle" runat="server" Text='<%# Eval("Header") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="تاریخ ویرایش" HeaderStyle-Width="120px">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Persia.Calendar.ConvertToPersian(DateTime.Parse(Eval("EditDate").ToString())).Simple %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="True" HeaderText="ویرایش" HeaderStyle-Width="30px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                                        CommandName="Select" ImageUrl="~/ControlPanel/Images/edit.png" Text="ویرایش" Width="24px" />
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
    <asp:Panel ID="pnlEdit" runat="server" Width="100%" Visible="false">
        <table>
            <tr>
                <td align="right">
                    <telerik:RadComboBox ID="drpLang" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpLang_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
                <td class="Header" style="text-align: left; direction: rtl;">
                    زبان:
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
                    <asp:Button ID="btnSave" runat="server" CssClass="submit-login" Font-Names="Tahoma"
                        ForeColor="White" OnClick="btnSave_Click" Text="ذخیره" ValidationGroup="EditFields" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc1:msgBox ID="cliMsgBox" Style="z-index: 103; left: 536px; position: absolute;
        top: 184px" runat="server"></cc1:msgBox>
</asp:Content>

