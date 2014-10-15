<%@ Page Title="" Language="C#" MasterPageFile="~/eStore.master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="eStoreCase1Website.OrderDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="container_12 clearfix">
        <div class="product grid_7 push_2"  id="main-content">
            <div class="viewDetails">
                <asp:Label ID="LabelStatus" Width="700px" runat="server" Font-Size="XX-Large"></asp:Label>
                <asp:Table ID="tableItems" runat="server" BorderStyle="Groove" BorderColor="#CC0000" BorderWidth="10px">
                </asp:Table>
            </div>
        </div>
    </section>
</asp:Content>
