<%@ Page Title="" Language="C#" MasterPageFile="~/eStore.master" AutoEventWireup="true" CodeBehind="Shop.aspx.cs" Inherits="eStoreCase1Website.Shop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/catalogue.css" />
    <script type="text/javascript">
        function copyProdInfoToPopup(pcd) {
            window.document.getElementById('<%=LabelStatus.ClientID %>').innerHTML = "";
            window.document.getElementById('<%=qty.ClientID %>').value = "0";
            window.document.getElementById("detailsGraphic").src =
                window.document.getElementById("Graphic" + pcd).src;
            window.document.getElementById("detailsProdName").innerText =
                window.document.getElementById("Name" + pcd).innerText;
            window.document.getElementById("detailsDescr").innerText =
                window.document.getElementById("Descr" + pcd).dataset.description;
            window.document.getElementById("ContentPlaceHolder1_detailsProdcd").defaultValue = pcd;
            window.document.getElementById("ContentPlaceHolder1_detailsPrice").innerText =
                window.document.getElementById("Price" + pcd).innerText;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="container_12 clearfix">
        <div id="main-content" data-mnuitem="shpMnuItem" class="catalogue">
            <section id="catalogue">

                    <h2>Our current Products</h2>
                 <div style="height:400px; overflow-y: scroll;">
                    <ul>
                        <asp:PlaceHolder ID="plcLi" runat="server" />
                    </ul>                
                </div>            
            </section>        
        </div>

    </section>

   <asp:ScriptManager ID="ScriptManager1" runat="server">
   </asp:ScriptManager>

    <a href="#x" class="overlay" id="details_popup"></a>
    <div class="popup" style="width: 400px;">
        <section id="details">
            <p id="detailsProdName" style="font-size: x-large; font-family: Verdana;"></p>
        <div class="stuff">
            <div class="img">
                <img alt="" id="detailsGraphic" />
            </div>
            <p id="detailsDescr" style="font-family: Verdana; font-size:smaller;"></p>
        </div>
        <div class="info">
            <a class="popTitle" href="#"></a>
            <div class="stuff">
            QTY
            <asp:TextBox runat="server" ID="qty" Value="0" style="width: 40px; text-align: right" Text="0" />
            </div>
            <span>Your Price: </span><asp:Label class="st" ID="detailsPrice" runat="server"></asp:Label>
            <asp:HiddenField ID="detailsProdcd" runat="server" />
        </div>
        <div class="buttons" style="padding: 30px;">
        
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <table>
        <tr>
            <td style="text-align: left; padding: 10px; padding: 10px;">
    <asp:Button ID="ButtonAddtoCart" class="fancy-button" runat="server" 
                    style="text-align: center" Text="Add to Cart" onclick="ButtonAddtoCart_Click"/>
            </td>
            <td style="text-align: left; padding: 10px; padding: 10px;">
    <asp:Button ID="ButtonViewCart" class="fancy-button" runat="server" 
                    style="text-align: center" Text="View Cart" onclick="ButtonViewCart_Click"/>
            </td>
        </tr>
    </table>
    <asp:Label ID="LabelStatus" runat="server" Text=""></asp:Label>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
     <a class="close" href="#close"></a>
    </section>
    </div>
</asp:Content>
