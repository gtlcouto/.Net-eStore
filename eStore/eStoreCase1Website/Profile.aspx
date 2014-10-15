<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="eStoreCase1Website.Profile" %>

<!DOCTYPE HTML>
<html lang="en">
<head id="Head1" runat="server">
    <link rel="stylesheet" href="css/reset.css" />
    <link rel="stylesheet" href="css/960.css" />
    <link rel="stylesheet" href="css/common.css" />
    <link rel="stylesheet" href="css/popupwindow.css" />
    <link rel="stylesheet" href="css/fancybutton.css" />
    <link rel="stylesheet" href="css/wizard.css" />
    <link rel="stylesheet" href="css/popup.css" />
    <script type="text/javascript" src="scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript">

        // global scoped variables
        var tbl = null;
        var orderDate;
        var orderID;
        var oldRow;
        var oldClass;
        var xml;
        var popupTbl;
        var tot = 0.0;
        var oldElem
        var noteNo = 0;
        var collOrders;
        var myWindow;



        $(document).ready(function () {

            var id = '#jqPopup'; // empty table
            //Get the window height and width
            var winH = $(window).height();
            var winW = $(window).width();
            //Set the popup window to center
            $(id).css('top', winH / 2 - $(id).height() / 2);
            $(id).css('left', winW / 2 - $(id).width() / 2);
            $(id).show();

            if (jQuery.browser.msie) {   // Internet Explorer
                xmlDocument = new ActiveXObject("Microsoft.XMLDOM");
                xmlDocument.async = "false";
                xmlDocument.loadXML(sXML);
                collCustomer = xmlDocument.selectNodes("//AllCustomerInfo");              // all note nodes IE
            } else {
                var parser = new DOMParser();
                xmlDocument = parser.parseFromString(sXML, "text/xml");
                collCustomer = xmlDocument.getElementsByTagName("AllCustomerInfo");       // all note nodes FF
                firstName = xmlDocument.getElementsByTagName("FirstName");
                userName = xmlDocument.getElementsByTagName("UserName");
                lastName = xmlDocument.getElementsByTagName("LastName");

            }

            popupBuilder();


            // click on [X] anchor to close popup
            $('a').click(function (e) {
                //Cancel the link behavior
                e.preventDefault();
                $('#dialog, .popupWindow').hide();
            });

        });

        function popupBuilder() {
            var tbl = $('<table>');
            // delete rows from old table (if it exists)
            $(tbl).find("tr").remove();


            for (i = 0; i < collCustomer.length; i++) {
                // select correct note  
                tr = $("<tr>").addClass("itemData");

                if (jQuery.browser.msie) {
                        td = $("<td>").css({ 'width': '35%', 'text-align': 'left' });
                        td.text(collCustomer.item(i).selectSingleNode("UserName").text);
                        td.appendTo(tr);       // add cell to table
                        tr.appendTo(tbl);       // add cell to tabl
                    
                } else {  // firefox or chrome
                        tr = $("<tr>");
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'left' });
                        td.text("User Name: ");
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'right' });
                        td.text(collCustomer[0].childNodes[0].firstChild.textContent);
                        td.appendTo(tr);       // add cell to table
                        tr.appendTo(tbl);
                        tr = $("<tr>");
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'left' });
                        td.text("First Name: ");
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'right' });
                        td.text(collCustomer[0].childNodes[1].firstChild.textContent);
                        td.appendTo(tr);       // add cell to table
                        tr.appendTo(tbl);
                        tr = $("<tr>");
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'left' });
                        td.text("Last Name: ");
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '400px%', 'text-align': 'right' });
                        td.text(collCustomer[0].childNodes[2].firstChild.textContent);
                        td.appendTo(tr);       // add cell to table
                        tr.appendTo(tbl);
                        tr = $("<tr>");
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'left' });
                        td.text("Email: ");
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'right' });
                        td.text(collCustomer[0].childNodes[3].firstChild.textContent);
                        td.appendTo(tr);       // add cell to table
                        tr.appendTo(tbl);
                        tr = $("<tr>");
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'left' });
                        td.text("Age: ");
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'right' });
                        td.text(collCustomer[0].childNodes[4].firstChild.textContent);
                        td.appendTo(tr);       // add cell to table
                        tr.appendTo(tbl);
                        tr = $("<tr>");
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'left' });
                        td.text("Address: ");
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'right' });
                        td.text(collCustomer[0].childNodes[5].firstChild.textContent);
                        td.appendTo(tr);       // add cell to table
                        tr.appendTo(tbl);
                        tr = $("<tr>");
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'left' });
                        td.text("City: ");
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'right' });
                        td.text(collCustomer[0].childNodes[6].firstChild.textContent);
                        td.appendTo(tr);       // add cell to table
                        tr.appendTo(tbl);      // add row to table
                        tr = $("<tr>");
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'left' });
                        td.text("Region: ");
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'right' });
                        td.text(collCustomer[0].childNodes[8].firstChild.textContent);
                        td.appendTo(tr);       // add cell to table
                        tr.appendTo(tbl);      // add row to table
                        tr = $("<tr>");
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'left' });
                        td.text("Country: ");
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'right' });
                        td.text(collCustomer[0].childNodes[9].firstChild.textContent);
                        td.appendTo(tr);       // add cell to table
                        tr.appendTo(tbl);      // add row to table                        
                        tr = $("<tr>");
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'left' });
                        td.text("Zip: ");
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'right' });
                        td.text(collCustomer[0].childNodes[7].firstChild.textContent);
                        td.appendTo(tr);       // add cell to table
                        tr.appendTo(tbl);      // add row to table
                        tr = $("<tr>");
                        td = $("<td>").css({ 'width': '400px', 'text-align': 'left' });
                        td.text("In case of change or errors please contact our tech support");
                        td.appendTo(tr);       // add cell to table
                        tr.appendTo(tbl);

                } // end if


            }   // end for


            $('#popupTbl').find("tr").remove();      // clean out old stuff
            $('#popupTbl').append($(tbl).html());    // put in new stuff
        }
    </script>
    <title>Tomato Source</title>
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
                    <li><a href="Home2.aspx">Home</a></li>
                    <li><a href="Login2.aspx#login_popup">Login</a></li>
                    <li><a href="Register.aspx#register_popup">Register</a></li>
                    <li><a href="Shop.aspx">Shop</a></li>
                    <li><a href="Orders.aspx">Orders</a></li>
                </ul>
            </nav>
        </header>
    </div> <!--#lnwrap-->
    </div>


    <!-- popup and overlay -->
   <form id="form1" runat="server" style="margin: 0px;">
    <div>

    <section class="container_12 clearfix">
        <div class="product grid_7 push_2" id="Div1">
    <br />
    <br />
    <div style="height:200px;">

    </div>
        </div>
    
    </section>
    </div>
    </form>

<div id="Footer">
    <div id="footerwrap">
        <div id="fsubwrap1">
            <footer id="main-footer" class="container_12 clearfix">
                <section id="fsub1" class="footersub grid_4">
                    <h1>
                        address</h1>
                    <p>
                        22 Tomato Street, Tomato City, Tomato Country</p>
                </section>
                <section id="fsub2" class="footersub grid_4">
                    <h1>
                        contact us</h1>
                    <p>
                        Phone: (519) 555-5555</p>
                    <p>
                        tomatohelpers@tomatosource.com</p>
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
</div>

        <!-- JQUERY POPUP -->
    <div class="container_12 clearfix">
        <div id="jqPopup" class="popupWindow">
            <a href="#" class="close"></a>
            <div style="text-align: center">
                <img alt="" src="img/pi.png" />
            </div>
            <br />
            <table border="1" style="width: 100%">
                <tr>
                    <td>
                        <table id="popupTbl"></table>
                    </td>
                </tr>           
            </table>
        </div> 
     </div>   
</body>
</html>
