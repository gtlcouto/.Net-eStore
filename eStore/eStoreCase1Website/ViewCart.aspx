<%@ Page Title="" Language="C#" MasterPageFile="~/eStore.master" AutoEventWireup="true" CodeBehind="ViewCart.aspx.cs" Inherits="eStoreCase1Website.ViewCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="container_12 clearfix">
        <div class="product grid_7 push_2" id="main-content">
            <h3>
                Your Cart Contains       
            </h3>
            <br />
            <div class="viewDetails">
                <asp:Table ID="tableItems" runat="server" BorderStyle="Groove" BorderColor="#CC0000" BorderWidth="10px">
                </asp:Table>
            </div>
            <div class="grid_7 push_2" id="actions">
                <asp:Button ID="ButtonOrder" runat="server" class="fancy-button" 
                    Text="Process Order" OnClientClick="copyDate();" onclick="ButtonOrder_Click" />
            </div>
            <center>
           <asp:Label ID="LabelStatus" Width="700px" runat="server" Font-Size="Large"></asp:Label>
           </center>
        </div>
    </section>
</asp:Content>
