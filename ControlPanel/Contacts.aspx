<%@ Page Title="" Language="C#" MasterPageFile="~/AdminCPMaster.master" AutoEventWireup="true" CodeFile="Contacts.aspx.cs" Inherits="ControlPanel_Contacts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cntphSideBar" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphInnerTitle" Runat="Server">
     <div style="float: right; color:White;" class="HeaderTop">
        <asp:Label ID="lblHeader" runat="server"></asp:Label>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="HeaderTop" style="padding: 10px 10px 10px 0;" dir="rtl">
        تمامی تماسهای مربوط به صفحه تماس با ما به ایمیل شما به آدرس:
        <br />
        <asp:Label runat="server" ID="lblMailAddress" ForeColor="Red" Text=""></asp:Label>
        <br />
        ارسال می شود.
    </div>
</asp:Content>
