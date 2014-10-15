<%@ Page Title="" Language="C#" MasterPageFile="~/eStore.master" AutoEventWireup="true" CodeBehind="CreditCardInfo.aspx.cs" Inherits="eStoreCase1Website.CreditCardInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <section class="container_12 clearfix">
        <div class="product grid_7 push_2">
            <br />
            <div class="viewDetails">
                <h3>Enter Credit Card Information</h3>
                <h2><img alt="" src="img/pi.png" /></h2>
                <asp:Label ID="Label2" runat="server" Font-Size="Medium" Text="Card Number" Width="140px"></asp:Label>
                <br />
                <asp:TextBox ID="TextBoxCardNumber" pattern="[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]"
                title="Must be 16 digits" required="required" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label3" runat="server" Font-Size="Medium" ForeColor="Black" Text="Card Type" Width="140px"></asp:Label>
                <br />
                <asp:DropDownList ID="DropDownListCard" runat="server">
                    <asp:ListItem Text="Mastercard"></asp:ListItem>
                    <asp:ListItem Text="Visa"></asp:ListItem>
                    <asp:ListItem Text="American Express"></asp:ListItem>
                    <asp:ListItem Text="Discover"></asp:ListItem>
                </asp:DropDownList>
            </div>
         </div>
            <div class="grid_7 push_2" id="actions">
                <asp:Button ID="ButtonOrder" runat="server" class="fancy-button" 
                    Text="Process Order" OnClientClick="copyDate();" onclick="ButtonOrder_Click" />
            </div>
            <center>
           <asp:Label ID="LabelStatus" Width="700px" runat="server" Font-Size="Large"></asp:Label>
           </center>

    
    </section>
</asp:Content>
