<%@ Page Title="Table" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Task._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DataGrid id="datagrid" runat="server" 
        AllowSorting="True"
        AutoGenerateColumns="true" 
        OnSortCommand="datagrid_SortCommand">
        <HeaderStyle 
            HorizontalAlign="Center" 
            ForeColor="#FFFFFF" 
            BackColor="#000080" 
            Font-Bold=true
            CssClass="TableHeader" />
        <Columns>
           <asp:BoundColumn DataField="Name" 
               HeaderText="Name"  
               SortExpression="Name"></asp:BoundColumn>
           <asp:BoundColumn DataField="CompanyName" 
               HeaderText="Company Name"  
               SortExpression="CompanyName">
           </asp:BoundColumn>
           <asp:BoundColumn DataField="Version" 
               HeaderText="Version"  
               SortExpression="Version">
           </asp:BoundColumn>
           <asp:BoundColumn DataField="Size" 
               HeaderText="Size"  
               SortExpression="Size">
           </asp:BoundColumn>
           <asp:BoundColumn DataField="ReleaseData" 
               HeaderText="Release data"  
               SortExpression="ReleaseData">
           </asp:BoundColumn>
           <asp:BoundColumn DataField="URL" 
               HeaderText="URL"  
               SortExpression="URL">
           </asp:BoundColumn>
           <asp:BoundColumn DataField="Vendor" 
               HeaderText="Vendor"  
               SortExpression="Vendor">
           </asp:BoundColumn>
        </Columns>
    </asp:DataGrid>
</asp:Content>
