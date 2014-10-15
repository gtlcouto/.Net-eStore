<%@ Page Title="" Language="C#" MasterPageFile="~/eStore.master" AutoEventWireup="true" CodeBehind="Login2.aspx.cs" Inherits="eStoreCase1Website.Login2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="container_12 clearfix">
        <div id="main-content">

        </div>
    </section>
   <asp:ScriptManager ID="ScriptManager1" runat="server">
   </asp:ScriptManager>
    <a href="#x" class="overlay" id="login_popup"></a>
    <div class="popup" style="width: auto;">
    <h2>
        Enter your Username:
    </h2>
    <p>
    <asp:TextBox ID="TextBoxUsername" required="required" class="fancy-button" runat="server">
    </asp:TextBox>
    </p>
    <h2>
        Enter your Password:
    </h2>
    <p>
    <asp:TextBox ID="TextBoxPassword" required="required" class="fancy-button" runat="server" TextMode="Password">
    </asp:TextBox>
    </p>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <asp:Button ID="ButtonLogin" class="fancy-button" runat="server" style="text-align: center" Text="Login" onclick="ButtonLogin_Click"/>
        <a class="close" href="#close"></a>
    <asp:Label ID="LabelStatus" runat="server" Text=""></asp:Label>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>
