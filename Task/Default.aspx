<%@ Page Title="Table" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Task._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>This place from table</h1>
    <asp:DataGrid id="datagrid" runat="server" AllowSorting="True"
                  AutoGenerateColumns="true">
        <%-- <Columns>
           <asp:BoundColumn DataField="ProductName" 
               HeaderText="Name"  
               SortExpression="ProductName"></asp:BoundColumn>
           <asp:BoundColumn DataField="CompanyName" 
               HeaderText="CompanyName"  
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
           <asp:BoundColumn DataField="Release data" 
               HeaderText="Release data"  
               SortExpression="Release data">
           </asp:BoundColumn>
           <asp:BoundColumn DataField="URL" 
               HeaderText="URL"  
               SortExpression="URL">
           </asp:BoundColumn>
           <asp:BoundColumn DataField="Vendor" 
               HeaderText="Vendor"  
               SortExpression="Vendor">
           </asp:BoundColumn>
        </Columns>--%>
    </asp:DataGrid>
    <h1>This place from table end</h1>
</asp:Content>
