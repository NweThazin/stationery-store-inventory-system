<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="InventoryPageEdit.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.InventoryPageEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid pg-content">
    <div class="row">
        <div class="col-md-6">
            <h2><i class="fa fa-cubes"></i> Stationery Inventory</h2>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-6 col-xs-offset-2" style="margin-top:5px;">
            <div class="input-group">
                <div class="input-group-btn search-panel">
                    <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" runat="server">
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu" role="menu" runat="server">
                        <li><asp:LinkButton ID="lnkbtnItemNumber" runat="server" OnClick="lnkbtnItemNumber_Click">Item Number</asp:LinkButton></li>
                        <li><asp:LinkButton ID="lnkbtnDescription" runat="server" OnClick="lnkbtnDescription_Click">Description</asp:LinkButton></li>
                    </ul>
                </div>
                <asp:TextBox ID="txtEnterSearch" runat="server" CssClass="form-control" placeholder="Choose Item Number or Description to search.."></asp:TextBox>
                    <span class="input-group-btn">
                        <asp:LinkButton ID="lnkbtnSearch" runat="server" CssClass="btn btn-default" OnClick="lnkbtnSearch_Click">
                        <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                        </asp:LinkButton>
                    </span>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 maintable">
            <div class="table-responsive">
                <asp:GridView ID="inventoryEditGV" runat="server" CssClass="table" AutoGenerateColumns="False" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" AllowPaging="True" OnRowEditing="inventoryEditGV_RowEditing" OnPageIndexChanging="inventoryEditGV_PageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="Item Number" DataField="ItemNumber"/>
                        <asp:BoundField HeaderText="Category" DataField="CategoryName"/>
                        <asp:BoundField HeaderText="Description" DataField="Description"/>
                        <asp:BoundField HeaderText="In Stock" DataField="InStockQty"/>
                        <asp:BoundField HeaderText="Reorder Level" DataField="ReorderLevel"/>
                        <asp:BoundField HeaderText="Reorder Quantity" DataField="ReorderQty"/>
                        <asp:BoundField HeaderText="UOM" DataField="UnitOfMeasure"/>
                        <asp:CommandField ShowEditButton="true"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    

</div>
</asp:Content>
