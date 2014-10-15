<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="eStoreCase1Website.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ex1 - A password Program</title>
    <style type="text/css">
        .style1
        {
            margin-left: 40px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <h2>
        Enter your Username:
    </h2>
    <p>
    <asp:TextBox ID="TextBoxUsername" runat="server">
    </asp:TextBox>
    </p>
    <h2>
        Enter your Password:
    </h2>
    <p>
    <asp:TextBox ID="TextBoxPassword" runat="server" style="text-align: center" TextMode="Password">
    </asp:TextBox>
    </p>
        <asp:Button ID="Button1" runat="server" style="text-align: center" 
            Text="Click" onclick="Button1_Click" />
     <p>
        <asp:Label ID="LabelStatus" runat="server" Text=""></asp:Label>
    </p>
    </center>
    
    </form>
</body>
</html>
