<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="InventoryReport.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.InventoryReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid pg-content">
    <div class="row">
        <div class="col-md-12">
            <h2>
                <i class="fa fa-file-text"></i> 
                <asp:Label ID="lblTitle" runat="server" Text="Inventory Status Report"></asp:Label>
                <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label>
            </h2>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 maintable">
            <div class="table-responsive">
                <asp:GridView ID="inventoryReportGV" runat="server" CssClass="table" AutoGenerateColumns="False" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" AllowPaging="True" PageSize="12" OnPageIndexChanging="inventoryReportGV_PageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="Item Code" DataField="ItemNumber"/>
                        <asp:BoundField HeaderText="Description" DataField="Description"/>
                        <asp:BoundField HeaderText="Location" DataField="Bin"/>
                        <asp:BoundField HeaderText="Quantity on hand" DataField="InStockQty"/>
                        <asp:BoundField HeaderText="Re-order level" DataField="ReorderLevel"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4"></div>
        <div class="col-md-4">
            <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btn btn-primary" OnClick="btnExport_Click" />
        </div>
        <div class="col-md-4"></div>
    </div>
</div>
</asp:Content>
