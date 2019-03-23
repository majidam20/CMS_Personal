<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCPMaster.master" AutoEventWireup="true"
    CodeFile="Albums.aspx.cs" Inherits="ControlPanel_Albums" %>

<%@ Register TagPrefix="cc1" Namespace="BunnyBear" Assembly="msgBox" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntphSideBar" runat="Server">
    <table align="center">
        <tr>
            <td>
                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/ControlPanel/Images/Products.png"
                    Width="60px" PostBackUrl="~/ControlPanel/Products.aspx" />
            </td>
        </tr>
        <tr>
            <td class="titleBar">
                محصولات
            </td>
        </tr>
        <tr>
            <td style="height: 20px">
                <img src="../Images/W_Line.png" alt="" width="180px" height="1px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:ImageButton ID="imgbtnProducts" runat="server" ImageUrl="~/ControlPanel/Images/PhotoGallery.png"
                    Width="60px" PostBackUrl="~/ControlPanel/PhotoGallery.aspx" />
            </td>
        </tr>
        <tr>
            <td class="titleBar">
                گالری تصاویر
            </td>
        </tr>
        <tr>
            <td style="height:20px"><img src="../Images/W_Line.png" alt="" width="180px" height="1px" /></td>
        </tr>
        <tr>
            <td>
                <asp:ImageButton ID="imgbtnTour" runat="server" ImageUrl="~/ControlPanel/Images/VideoGallery.png"
                    Width="60px" PostBackUrl="~/ControlPanel/VideoGallery.aspx" />
            </td>
        </tr>
        <tr>
            <td class="titleBar">
                گالری ویدئو
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphInnerTitle" runat="Server">
    <div style="float: right; color: White;" class="HeaderTop">
        <asp:Label ID="lblHeader" runat="server"></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphMain" runat="Server">

    <asp:Panel ID="pnlAlbums" runat="server">
        <table style="width: 700px;">
            <tr>
                <td colspan="2">
                    <div class="HeaderTop" dir="rtl" style="padding: 10px 10px 10px 0;">
                        برای ویرایش، آلبوم مورد نظر را انتخاب و آن را ویرایش کنید.<br />
                        در صورت حذف یک آلبوم، تمامی فایلهای مربوط به آن حذف خواهد شد.
                    </div>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 5px; width: 60%" align='right'>
                    <asp:DropDownList Style="font-family: Tahoma;" ID="drpAlbumsTypes" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="drpAlbumsTypes_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="0">انتخاب کنید</asp:ListItem>
                        <asp:ListItem Value="Products">محصولات</asp:ListItem>
                        <asp:ListItem Value="Image">گالری تصاویر</asp:ListItem>
                        <asp:ListItem Value="Video">گالری ویدئو</asp:ListItem>
                        <asp:ListItem Value="PageGallery">بنر صفحات</asp:ListItem>
                        <asp:ListItem Value="Article">کاتالوگها</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="padding-top: 5px; text-align: left;" class="HeaderTop" dir="rtl">
                    انواع آلبوم ها:
                </td>
            </tr>
            <tr>
                <td style="padding-top: 5px; width: 60%" align='right'>
                    &nbsp;
                </td>
                <td style="padding-top: 5px; text-align: left;" class="HeaderTop" dir="rtl">
                    آلبوم های ثبت شده از نوع فوق:
                </td>
            </tr>
            <tr>
                <td style="padding-top: 5px;" align='right' colspan="2" dir="rtl">
                    <asp:GridView ID="grdAlbums" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" AllowPaging="True" HorizontalAlign="Center" Width="600px"
                        DataKeyNames="ID" OnRowCreated="grdAlbums_RowCreated" PageSize="8" AllowSorting="True"
                        Font-Names="Tahoma" OnPageIndexChanging="grdAlbums_PageIndexChanging" OnRowCommand="grdAlbums_RowCommand"
                        OnRowDeleting="grdAlbums_RowDeleting" EmptyDataText="آلبومی از نوع انتخاب شده تعریف نشده است">
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
                            <asp:TemplateField HeaderText="عنوان">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("AlbumTitle")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="True" HeaderText="فعال؟" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnVis" runat="server" CausesValidation="False" CommandName="Visible"
                                        CommandArgument='<%# String.Format("{0};{1}", Eval("ID").ToString(), Eval("IsActive").ToString()) %>'
                                        ImageUrl='<%# Boolean.Parse(Eval("IsActive").ToString()) ? "~/ControlPanel/Images/Check.png" : "~/ControlPanel/Images/Cancel.png" %>'
                                        Text="" Width="24px" />
                                </ItemTemplate>
                                <HeaderStyle Width="60px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ویرایش" HeaderStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdt" runat="server" CommandName="Select" CommandArgument='<%# Eval("ID")%>'
                                        CausesValidation="False" Style="display: table-cell;" ImageUrl="~/ControlPanel/Images/Edit.png"
                                        Text="ویرایش" />
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="True" HeaderText="حذف" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDel" runat="server" CausesValidation="False" OnClientClick=<%# string.Format("return confirm(\"آیا از حذف آلبوم '{0}'  مطمئن هستید؟ \\n در صورت حذف آلبوم همه آیتمهای وابسته به آن نیز حذف خواهد شد \");", Eval("AlbumTitle")) %>
                                        CommandName="Delete" Style="display: table-cell;" ImageUrl="~/ControlPanel/Images/Delete.png"
                                        Text="حذف" />
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#555c61" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#555c61" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#888d90" />
                        <HeaderStyle BackColor="#555c61" Font-Bold="True" ForeColor="White" CssClass="textHeader"
                            Font-Size="16px" />
                        <EditRowStyle BackColor="#7C6F57" />
                        <AlternatingRowStyle BackColor="#bec0c1" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                <asp:Panel ID="pnlEdit" runat="server" Visible="False">
                <table align="center" width="100%">
            <tr>
                <td align="center" rowspan="5" style="width:200px">
                    <asp:Image ID="imgThumb" runat="server" Visible="False" />
                </td>
                <td align="center" colspan="2">
                    <img alt="" style="width: 348px; height: 1px; padding: 5px 0 5px 0;" src="Images/line.png" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <telerik:RadComboBox ID="drpLang" runat="server" AutoPostBack="True" 
                        OnSelectedIndexChanged="drpLang_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
                <td class="Header" style="text-align: left; direction: rtl;">
                    انتخاب زبان:
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtTitle" 
                        ErrorMessage="&lt;strong&gt;عنوان را طبق زبان انتخاب شده وارد کنید&lt;/strong&gt;" 
                        Text="&lt;strong&gt;عنوان را طبق زبان انتخاب شده وارد کنید&lt;/strong&gt;" 
                        ValidationGroup="Fields"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtTitle" runat="server" style="font-family: Tahoma" 
                        Width="150px"></asp:TextBox>
                </td>
                <td class="Header" style="text-align: left; direction: rtl;">
                    عنوان:
                </td>
            </tr>
                    <tr>
                        <td align="right">
                            <asp:RequiredFieldValidator ID="reqValThum" runat="server" 
                                ControlToValidate="filUpThumb" 
                                ErrorMessage="&lt;strong&gt;تصویر را انتخاب کنید&lt;/strong&gt;" 
                                Text="&lt;strong&gt;تصویر را انتخاب کنید&lt;/strong&gt;" 
                                ValidationGroup="Fields"></asp:RequiredFieldValidator>
                            <asp:FileUpload ID="filUpThumb" runat="server" />
                        </td>
                        <td class="Header" style="text-align: left; direction: rtl;">
                            تصویر:</td>
                    </tr>
            <tr>
                <td align="right">
                    &nbsp;
                    <asp:Label ID="lblThumbName" runat="server" Text="" Visible="false"></asp:Label>
                </td>
                <td class="Header" style="text-align: left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
                <td align="center" colspan="2">
                    <div style="width: 200px;">
                        <asp:Button ID="btnInsert" runat="server" CssClass="submit-login" 
                            Font-Names="Tahoma" ForeColor="White" OnClick="btnInsert_Click" 
                            Style="float: left;" Text="ارسال" ValidationGroup="Fields" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="submit-login" 
                            Font-Names="Tahoma" ForeColor="White" OnClick="btnCancel_Click" 
                            Style="float: right;" Text="لغو" Visible="false" />
                    </div>
                </td>
            </tr>
            </table>
            </asp:Panel>
            </td></tr>
        </table>
    <cc1:msgBox ID="cliMsgBox" Style="z-index: 103; left: 536px; position: absolute;
        top: 184px" runat="server"></cc1:msgBox>
    </asp:Panel>
</asp:Content>
