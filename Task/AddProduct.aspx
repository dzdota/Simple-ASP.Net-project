<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="Task.AddProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="grid">
        <div class="box Name">
            <asp:Label ID="LabelName" runat="server" Text="Product Name" Width="100px"></asp:Label>
            <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
        </div>
        <div class="box Id">
            <asp:Label ID="LabelId" runat="server" Text="Product Id" Width="100px"></asp:Label>
            <asp:TextBox ID="TextBoxId" runat="server"></asp:TextBox>
        </div>
        <div class="box Company">
            <asp:Label ID="LabelCompany" runat="server" Text="Company Name" Width="100px"></asp:Label>
            <asp:TextBox ID="TextBoxCompany" runat="server"></asp:TextBox>
        </div>
        <div class="box Url">
            <asp:Label ID="LabelUrl" runat="server" Text="Url" Width="100px"></asp:Label>
            <asp:TextBox ID="TextBoxUrl" runat="server"></asp:TextBox>
        </div>
        <div class="box Vendor">
            <asp:Label ID="LabelVendor" runat="server" Text="Vendor" Width="100px"></asp:Label>
            <asp:TextBox ID="TextBoxVendor" runat="server"></asp:TextBox>
        </div>
        <div class="box Release">
            <asp:Label ID="LabelRelease" runat="server" Text="Release Data" Width="100px"></asp:Label>
            <asp:TextBox ID="TextBoxRelease" runat="server"></asp:TextBox>
        </div>
        <div class="box Category">
            <asp:CheckBoxList ID="CategoryList"  runat="server">

            </asp:CheckBoxList>
        </div>
        <div class="box buttom">
            <asp:Button ID="ButtonAdd" runat="server" Text="Add" OnClick="ButtonAdd_Click" Width="100px" />
        </div>
    </div>

</asp:Content>
