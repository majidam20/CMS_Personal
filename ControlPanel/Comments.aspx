<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCPMaster.master" AutoEventWireup="true" CodeFile="Comments.aspx.cs" Inherits="ControlPanel_Comments"%>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="BunnyBear" Assembly="msgBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
            <td>
                <div class="HeaderTop" style="padding:10px 10px 10px 0;">
                    صفحه مورد نظر را برای ویرایش انتخاب کنید</div>
            </td>
        </tr>
        <tr>
            <td dir="rtl" class="Header">
                نمایش نظرهای زبان: &nbsp;
                <asp:DropDownList ID="drpLang" runat="server" Font-Names="Tahoma" 
                    AutoPostBack="True" onselectedindexchanged="drpLang_SelectedIndexChanged">
                    <asp:ListItem Value="fa-IR">فارسی</asp:ListItem>
                    <asp:ListItem Value="en-US">انگلیسی</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td dir="rtl">
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
            </telerik:RadWindowManager>
                <asp:UpdatePanel ID="updpnlComnt" runat="server">
                <ContentTemplate>
                    <asp:UpdateProgress ID="updprg" AssociatedUpdatePanelID="updpnlComnt" runat="server">
                        <ProgressTemplate>
                        <div align="center">
                            <img src="../images/Loding.gif" />
                        </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:GridView ID="grdComments" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" AllowPaging="True" HorizontalAlign="Center"
                        Width="600px" DataKeyNames="ID" OnRowCreated="grdComments_RowCreated" 
                        PageSize="15" AllowSorting="True" 
                    onrowcommand="grdComments_RowCommand" onrowdeleting="grdComments_RowDeleting">
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
                            <asp:TemplateField HeaderText="Visible" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblVisible" runat="server" Text='<%# Eval("Visible") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="دسته">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPlaceNum" runat="server" Text='<%# Business.Comments.Comments._PlaceNum2Name(Convert.ToInt32(Eval("PlaceNum").ToString())) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="محل نمایش نظر">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPlaceID" runat="server" Text='<%# Business.Comments.Comments._PlaceID2Name(Convert.ToInt32(Eval("PlaceNum").ToString()), Convert.ToInt32(Eval("PlaceID").ToString()))%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="تاریخ درج">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Persia.Calendar.ConvertToPersian(DateTime.Parse(Eval("SentDate").ToString())).Simple %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="نمایش نظر">
                                <ItemTemplate>
                                <div style="text-align:center;">
                                    <button class="submit-login" style="color:White; font-family:Tahoma; display:table-cell" id="btnShow" onclick="<%# string.Format("radalert({0} , {1}, {2},\'{3}\'); return false;", _MakeContactShow(Eval("Comment").ToString(), Eval("SenderName").ToString(), Eval("e_Mail").ToString(), DateTime.Parse(Eval("SentDate").ToString())), "360", "50", Eval("SenderName"))%>">نمایش</button>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="True" HeaderText="حذف">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDel" runat="server" CausesValidation="False" OnClientClick=<%# string.Format("return confirm(\"آیا از حذف نظر ردیف '{0}'  مطمئن هستید؟ \");", (grdComments.Rows.Count+1).ToString()) %>
                                        CommandName="Delete" ImageUrl="~/ControlPanel/Images/Delete.png" Text="ویرایش" Width="24px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="True" HeaderText="نمایش؟">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnVis" runat="server" CausesValidation="False" CommandName="Visible" CommandArgument='<%# grdComments.Rows.Count %>' ImageUrl='<%# Boolean.Parse(Eval("Visible").ToString()) ? "~/ControlPanel/Images/Check.png" : "~/ControlPanel/Images/Cancel.png" %>' Text="" Width="24px" />
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
                    <cc1:msgBox ID="cliMsgBox" Style="z-index: 103; left: 536px; position: absolute;
                        top: 184px" runat="server"></cc1:msgBox>
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        </table>
        <%--<table class="ShowTable">
        <tr><td dir="rtl" align="right" class="HeaderTop">
            <div style="float:right">{0} گفته:</div>
        </td></tr>
        <tr>
            <td class="ShowRTL">
                {2}
            </td>
        </tr>
        <tr>
            <td dir="rtl" align="left" class="text" style="border-top-width:1px; border-top-style:solid; border-top-color:Orange; font-size:11px">
                <div style="float:left">ارسال در: {3}</div>
                <div style="float:right">ایمیل: {1}</div>
            </td>
        </tr>
        </table>--%>
</asp:Content>

