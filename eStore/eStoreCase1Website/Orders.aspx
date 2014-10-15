<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="eStoreCase1Website.Orders" %>

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
        var ext = 0.0;
        var firstName;

        function cur(num) {
            num = num.toString().replace(/\$|\,/g, '');
            if (isNaN(num))
                num = "0";
            sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            cents = num % 100;
            num = Math.floor(num / 100).toString();
            if (cents < 10)
                cents = "0" + cents;
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' +
                         num.substring(num.length - (4 * i + 3));
            return (((sign) ? '' : '-') + '$' + num + '.' + cents);
        }

        $(document).ready(function () {

            $('td').click(function () { // click on a table cell in notes
                if ($(this).attr('id') == 'OrderNo') {  // if we have more than 1 table, we want the right one
                    var id = '#jqPopup'; // empty table
                    //Get the window height and width
                    var winH = $(window).height();
                    var winW = $(window).width();
                    //Set the popup window to center
                    $(id).css('top', winH / 2 - $(id).height() / 2);
                    $(id).css('left', winW / 2 - $(id).width() / 2);
                    $(id).show();

                    if (oldRow != null) { // reset old row to previous state
                        oldRow.removeAttr("style");
                        oldRow.removeAttr("class");
                        $(oldRow).toggleClass("itemSelected");
                        $(oldRow).toggleClass(oldClass);
                    }

                    oldRow = $(this).parents("tr");
                    oldClass = oldRow.attr("class");
                    oldRow.removeAttr("style");
                    oldRow.removeAttr("class");
                    oldRow.toggleClass("itemSelected");

                    orderId = $(this).text();
                    orderDate = oldRow[0].cells[1].innerHTML;


                    cellContents = $(this).html();
                    noteNo = parseInt(cellContents.substr(cellContents.indexOf(" ") + 1, cellContents.length - (cellContents.indexOf(" ") + 1)));  // note number

                    if (jQuery.browser.msie) {   // Internet Explorer
                        xmlDocument = new ActiveXObject("Microsoft.XMLDOM");
                        xmlDocument.async = "false";
                        xmlDocument.loadXML(sXML);
                        collOrders = xmlDocument.selectNodes("//SingleOrderDetails");              // all note nodes IE
                    } else {
                        var parser = new DOMParser();
                        xmlDocument = parser.parseFromString(sXML, "text/xml");
                        collOrders = xmlDocument.getElementsByTagName("SingleOrderDetails");       // all note nodes FF

                    }

                    popupBuilder();
                }
            }); // td click

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
            tot = 0.0;
            /* Row */
            var tr = $("<tr>");
            var td = $("<td colspan='3'>").text("Order # " + noteNo);
            td.addClass("tblItemBigHead");
            td.appendTo(tr);       // add cell to table
            var td = $("<td colspan='3'>").text("Date: " + orderDate);
            td.addClass("tblItemBigHead");
            td.appendTo(tr);
            tr.appendTo(tbl);      // add row to table
            /* Row */
            tr = $("<tr>");
            td = $("<td>").text("Product Name");
            td.addClass("tblItemHead");
            td.css({ 'width': '35%', 'text-align': 'center', 'font-weight': 'bold' });
            td.appendTo(tr);       // add cell to table
            td = $("<td>").text("Sell Price");
            td.addClass("tblItemHead");
            td.css({ 'width': '15%', 'text-align': 'center' });
            td.appendTo(tr);       // add cell to table
            td = $("<td>").text("QtyS");
            td.addClass("tblItemHead");
            td.css({ 'width': '10%', 'text-align': 'center' });
            td.appendTo(tr);       // add cell to table
            td = $("<td>").text("QtyO");
            td.addClass("tblItemHead");
            td.css({ 'width': '10%', 'text-align': 'center' });
            td.appendTo(tr);       // add cell to table
            td = $("<td>").text("QtyB");
            td.addClass("tblItemHead");
            td.css({ 'width': '10%', 'text-align': 'center' });
            td.appendTo(tr);       // add cell to table
            tr.appendTo(tbl);      // add row to table
            td = $("<td>").text("Extended");
            td.addClass("tblItemHead");
            td.css({ 'width': '20%', 'text-align': 'center' });
            td.appendTo(tr);       // add cell to table
            tr.appendTo(tbl);      // add row to table

            for (i = 0; i < collOrders.length; i++) {
                // select correct note  
                tr = $("<tr>").addClass("itemData");

                if (jQuery.browser.msie) {   // Internet Explorer

                    if (noteNo == collOrders.item(i).selectSingleNode("OrderID").text) {
                        td = $("<td>").css({ 'width': '35%', 'text-align': 'left' });
                        td.text(collOrders.item(i).selectSingleNode("prodnam").text);
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '15%', 'text-align': 'left' });
                        td.text(collOrders.item(i).selectSingleNode("SellingPrice").text);
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '10%', 'text-align': 'left' });
                        td.text(collOrders.item(i).selectSingleNode("qtysold").text);
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '10%', 'text-align': 'left' });
                        td.text(collOrders.item(i).selectSingleNode("qtyordered").text);
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '10%', 'text-align': 'left' });
                        td.text(collOrders.item(i).selectSingleNode("qtybackordered").text);
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'text-align': 'center' });
                        td.text(cur(ext));
                        td.appendTo(tr);       // add cell to tabl
                    }
                } else {  // firefox or chrome
                    if (noteNo == collOrders[i].childNodes[3].firstChild.textContent) { // id
                        tr = $("<tr>");
                        td = $("<td>").css({ 'width': '35%', 'text-align': 'left' });
                        td.text(collOrders[i].childNodes[0].firstChild.textContent);
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '15%', 'text-align': 'center' });
                        td.text(cur(collOrders[i].childNodes[1].firstChild.textContent));
                        ext = 0.0;
                        ext = parseFloat(collOrders[i].childNodes[1].firstChild.textContent);
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '10%', 'text-align': 'center' });
                        td.text(collOrders[i].childNodes[4].firstChild.textContent);
                        ext = ext * collOrders[i].childNodes[5].firstChild.textContent;
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '10%', 'text-align': 'center' });
                        td.text(collOrders[i].childNodes[5].firstChild.textContent);
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'width': '10%', 'text-align': 'center' });
                        td.text(collOrders[i].childNodes[6].firstChild.textContent);
                        td.appendTo(tr);       // add cell to table
                        td = $("<td>").css({ 'text-align': 'right' });
                        td.text(cur(ext));
                        td.appendTo(tr);       // add cell to tabl
                        tr.appendTo(tbl);      // add row to table
                        tot = tot + ext;
                    }  // end if
                } // end if


            }   // end for
            /* Row */
            tr = $("<tr>");
            td = $("<td colspan='5'>");
            td.text("   ");
            td.appendTo(tr);
            td = $("<td>");
            td = $("<td>").css({ 'text-align': 'right' });
            td.text("-----------");
            td.addClass("DisItem");
            td.appendTo(tr);       // add cell to table
            tr.appendTo(tbl);      // add row to table

            tr = $("<tr>");
            td = $("<td colspan='5'>");
            td.text("Total: ");
            td.appendTo(tr);
            td = $("<td>");
            td = $("<td>").css({ 'text-align': 'right' });
            td.text(cur(tot));
            td.addClass("DisItem");
            td.appendTo(tr);       // add cell to table
            tr.appendTo(tbl);      // add row to table

            tr = $("<tr>");
            td = $("<td colspan='5'>");
            td.text("Tax: ");
            td.appendTo(tr);
            td = $("<td>");
            td = $("<td>").css({ 'text-align': 'right' });
            td.text(cur(tot * 0.13));
            td.addClass("DisItem");
            td.appendTo(tr);       // add cell to table
            tr.appendTo(tbl);      // add row to table

            tr = $("<tr>");
            td = $("<td colspan='3'>");
            td.text("   ");
            td.appendTo(tr);
            td = $("<td colspan='2'>");
            td.text("Ord Total: ");
            td.addClass("tblItemHead");
            td.appendTo(tr);
            td = $("<td>");
            td = $("<td>").css({ 'text-align': 'right' });
            td.text(cur(tot * 1.13));
            td.addClass("tblItemHead");
            td.appendTo(tr);       // add cell to table
            tr.appendTo(tbl);      // add row to table

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
                    <li><a href="Profile.aspx">Profile</a></li>
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
    <div style="height:200px; overflow-y: scroll;">
    <center>
            <div class="viewOrders">
               <asp:Table ID="TableMain" runat="server" BorderStyle="Groove" BorderColor="#CC0000" BorderWidth="10px">
                </asp:Table>
            </div>
    </center>
    </div>
    <center>
    <asp:Label ID="LabelStatus" runat="server" Text="Your orders, choose one"></asp:Label>
    </center>
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


