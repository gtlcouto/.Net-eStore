<%@ Page Title="" Language="C#" MasterPageFile="~/eStore.master" AutoEventWireup="true" CodeBehind="Register2.aspx.cs" Inherits="eStoreCase1Website.Register2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="container_12 clearfix">
        <div id="main-content">

        </div>
    </section>
   <asp:ScriptManager ID="ScriptManager1" runat="server">
   </asp:ScriptManager>
    <a href="#x" class="overlay" id="register_popup"></a>
    <div class="popup">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Wizard ID="Wizard1" runat="server" DisplaySideBar="false" 
                    onfinishbuttonclick="Wizard1_FinishButtonClick">
                <HeaderTemplate>
                    <ul id="wizHeader">
                        <asp:Repeater ID="SideBarList" runat="server">
                         <ItemTemplate>
                                <li>
                                    <a class="<%# GetClassForWizardStep(Container.DataItem) %>" title="<%#Eval("Name") %>"><%#Eval("Name") %></a>
                                <li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </HeaderTemplate>
                    <NavigationButtonStyle CssClass="fancy-button" />
                    <WizardSteps>
                        <asp:WizardStep ID="WizardStep1" runat="server" Title="Personal" >
                            <table style="width: 100%; text-align: center;">
                                <tr>
                                    <td colspan="2" style="text-align: center; padding: 20px;">
                                        <asp:Label ID="Label1" runat="server" Text="Personal Information"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:Label ID="Label2" runat="server" Text="First Name:"></asp:Label>
                                    </td>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:TextBox ID="TextBoxFirst" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:Label ID="Label3" runat="server" Text="Last Name:"></asp:Label>
                                    </td>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:TextBox ID="TextBoxLast" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:Label ID="Label4" runat="server" Text="Age:"></asp:Label>
                                    </td>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:TextBox ID="TextBoxAge"  runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                    <tr>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:Label ID="Label5" runat="server" Text="Credit Card:"></asp:Label>
                                    </td>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:DropDownList ID="DropDownListCredit" runat="server">
                                        <asp:ListItem>Master Card</asp:ListItem>
                                        <asp:ListItem>Visa</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <a class="close" href="#close"></a>
                        </asp:WizardStep>
                        <asp:WizardStep ID="WizardStep2" runat="server" Title="Address" >
                        <table style="width: 100%; text-align: center;">
                                <tr>
                                    <td colspan="2" style="text-align: center; padding: 20px;">
                                        <asp:Label ID="Label6" runat="server" Text="Address Information"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:Label ID="Label7" runat="server" Text="# and Street:"></asp:Label>
                                    </td>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:TextBox ID="TextBoxAddress" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:Label ID="Label8" runat="server" Text="City:"></asp:Label>
                                    </td>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:TextBox ID="TextBoxCity" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:Label ID="Label9" runat="server" Text="Region:"></asp:Label>
                                    </td>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:TextBox ID="TextBoxRegion" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:Label ID="Label10" runat="server" Text="Country:"></asp:Label>
                                    </td>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:DropDownList ID="DropDownListCountry" runat="server">
                                        <asp:ListItem>Canada</asp:ListItem>
                                        <asp:ListItem>United States</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:Label ID="Label11" runat="server" Text="Postal Code:"></asp:Label>
                                       
                                    </td>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:TextBox ID="TextBoxPostal" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <a class="close" href="#close"></a>
                        </asp:WizardStep>
                        <asp:WizardStep ID="WizardStep3" runat="server" Title="Account" >
                        <table style="width: 100%; text-align: center;">
                                <tr>
                                    <td colspan="2" style="text-align: center; padding: 20px;">
                                        <asp:Label ID="Label12" runat="server" Text="Account Information"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:Label ID="Label13" runat="server" Text="User Name:"></asp:Label>
                                    </td>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:TextBox ID="TextBoxUser" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:Label ID="Label14" runat="server" Text="Email Address:"></asp:Label>
                                    </td>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:Label ID="Label15" runat="server" Text="Password:"></asp:Label>
                                    </td>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:TextBox ID="TextBoxPassword" runat="server" ViewStateMode="Disabled" TextMode="Password" ControlToCompare="TextBoxPassword"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:Label ID="Label16" runat="server" Text="Confirm Password:"></asp:Label>
                                    </td>
                                    <td style="text-align: left; padding: 10px; padding: 10px;">
                                        <asp:TextBox ID="TextBoxConfirmPw" runat="server" ViewStateMode="Disabled" TextMode="Password"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="CompareValidator" Text="Pass does not Match!" ControlToValidate="TextBoxConfirmPw" ControlToCompare="TextBoxPassword"></asp:CompareValidator>
               
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center; padding: 20px;">
                                        <asp:Label ID="LabelStatus" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <a class="close" href="#close"></a>
                        </asp:WizardStep>
                    </WizardSteps>
                </asp:Wizard>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
