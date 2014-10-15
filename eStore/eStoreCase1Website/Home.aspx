<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="eStoreCase1Website.Home" %>
<!DOCTYPE HTML>
<html lang="en">
<head id="Head1" runat="server">
    <link rel="stylesheet" href="css/reset.css" />
    <link rel="stylesheet" href="css/960.css" />
    <link rel="stylesheet" href="css/common.css" />
    <link rel="stylesheet" href="css/popupwindow.css" />
    <link rel="stylesheet" href="css/fancybutton.css" />
    <title>Sample Webform using 960 layout CSS</title>
</head>
<body>
   <div id="hfwrap">
    <div id="lnwrap">
        <header id="main-header" class="container_12">
            <h1 id="logo" class="grid_2 suffix">
                <img src="img/pi.png" /></h1>
            <nav id="main-nav" class="grid_8 suffix_2">
                <h1>
                    Main page navigation</h1>
                <ul>
                    <li class="current"><a href="Home.aspx">Home</a></li>
                    <li><a href="#login_popup">Login</a></li>
                    <li><a href="register.aspx#register_popup">Register</a></li>
                    <li><a href="#">Link 4</a></li>
                </ul>
            </nav>
        </header>
    </div> <!--#lnwrap-->
   </div> <!-- #hfwrap -->

    <section class="container_12 clearfix">
        <div id="main-content">

        </div>
    </section>

    <!-- popup and overlay -->
   <form id="form1" runat="server" style="margin: 0px;">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
   </asp:ScriptManager>
    <a href="#x" class="overlay" id="login_popup"></a>
    <div class="popup">
    <h2>
        Enter your Username:
    </h2>
    <p>
    <asp:TextBox ID="TextBoxUsername" class="fancy-button" runat="server">
    </asp:TextBox>
    </p>
    <h2>
        Enter your Password:
    </h2>
    <p>
    <asp:TextBox ID="TextBoxPassword" class="fancy-button" runat="server" style="text-align: center" TextMode="Password">
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
    


    </form>
    <div id="footerwrap">
        <div id="fsubwrap1">
            <footer id="main-footer" class="container_12 clearfix">
                <section id="fsub1" class="footersub grid_4">
                    <h1>
                        address</h1>
                    <p>
                        12345 Somewhere Street, Someplace, Somearea</p>
                </section>
                <section id="fsub2" class="footersub grid_4">
                    <h1>
                        contact us</h1>
                    <p>
                        Phone: (555) 555-5555</p>
                    <p>
                        customerhelp@yoursite.com</p>
                </section>
                <section id="fsub4" class="footersub grid_4">
                    <h1>
                        connect with us</h1>
                    <p class="icon">
                        <a href="#">
                            <img src="img/twitter_32.png" width="32" height="32" alt="twitter icon" /></a></p>
                    <p class="icon">
                        <a href="#">
                            <img src="img/facebook_32.png" width="32" height="32" alt="facebook icon" /></a></p>
                    <p class="icon">
                        <a href="#">
                            <img src="img/flickr_32.png" width="32" height="32" alt="flickr icon" /></a></p>
                    <p class="icon">
                        <a href="#">
                            <img src="img/rss_32.png" width="32" height="32" alt="rss icon" /></a></p>
                </section>
            </footer>
        </div>
        <!--fsubwrap1 -->
        <div id="fsubwrap2">
            <section id="copyright-info">
                <small>©2012 Info3067 Case 1</small>
            </section>
        </div>
        <!--fsubwrap2 -->
    </div>
    <!--footerwrap-->
</body>
</html>
