<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="StockCard.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.StockCard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid pg-content">
    <div class="row">
        <div class="col-md-6">
            <h2>
                <i class="fa fa-cube"></i> 
                Stock Card for <asp:Label ID="lblItemCode" runat="server"></asp:Label> - <asp:Label ID="lblItemDescription" runat="server"></asp:Label>
            </h2>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-5">
                    <div class="row">
                        <div class="col-md-2"><b>Bin #: </b></div>
                        <div class="col-md-2"><asp:Label ID="lblBin" runat="server"></asp:Label></div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"><b>In Stock Qty: </b></div>
                        <div class="col-md-2"><asp:Label ID="lblInStock" runat="server"></asp:Label></div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"><b>UOM: </b></div>
                        <div class="col-md-2"><asp:Label ID="lblUOM" runat="server"></asp:Label></div>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="row">
                        <div class="col-md-4"><b>1<sup>st</sup> Supplier: </b></div>
                        <div class="col-md-4"><asp:Label ID="lblSupplier1" runat="server"></asp:Label></div>
                    </div>
                    <div class="row">
                        <div class="col-md-4"><b>2<sup>nd</sup> Supplier: </b></div>
                        <div class="col-md-4"><asp:Label ID="lblSupplier2" runat="server"></asp:Label></div>
                    </div>
                    <div class="row">
                        <div class="col-md-4"><b>3<sup>rd</sup> Supplier: </b></div>
                        <div class="col-md-4"><asp:Label ID="lblSupplier3" runat="server"></asp:Label></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row" style="margin-top:5px;">
        <div class="col-md-6">
            <asp:Button ID="btnUpdate" runat="server" Text="Update Stock Card" CssClass="btn btn-md btn-primary" OnClick="btnUpdate_Click"/>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 maintable">
            <div class="table-responsive">
                <asp:GridView ID="stockCardGV" runat="server" CssClass="table" AutoGenerateColumns="False" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" OnPageIndexChanging="stockCardGV_PageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:dd/MM/yyyy}"/>
                        <asp:BoundField HeaderText="Department/Supplier" DataField="Name"/>
                        <asp:BoundField HeaderText="Quantity" DataField="Quantity"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</div>
</asp:Content>
