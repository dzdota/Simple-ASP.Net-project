<%@ Page Title="Table" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Task._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div >
        <div style="width:70%; margin-left: 150px;">
            <asp:Label ID="CompanyFilter" runat="server" Text="Company Filter" BorderColor="White"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" ID="CompanyNameList" OnSelectedIndexChanged="CompanyNameList_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:DataGrid id="datagrid" runat="server" 
                AllowSorting="True"
                AutoGenerateColumns="true" 
                OnSortCommand="datagrid_SortCommand">
                <HeaderStyle 
                    HorizontalAlign="Center" 
                    ForeColor="#FFFFFF" 
                    BackColor="#111" 
                    Font-Bold=true
                    CssClass="TableHeader" />
                <Columns>
                </Columns>
            </asp:DataGrid>
        </div>
        <div style="
    height: 100%;width: 150px;position: fixed;z-index: 1;
    top: 0;left: 0;background-color: #111;color: #fff; padding-top: 60px;">
            <asp:CheckBoxList ID="CategoryList"  runat="server" OnSelectedIndexChanged="CategoryList_SelectedIndexChanged" AutoPostBack="True"></asp:CheckBoxList>
        </div>
    </div>
</asp:Content>
