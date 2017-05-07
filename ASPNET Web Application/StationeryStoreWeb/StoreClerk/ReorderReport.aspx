<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ReorderReport.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.ReorderReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid pg-content">
    <div class="row">
        <div class="col-md-12">
            <h2>
                <i class="fa fa-file-text"></i> 
                <asp:Label ID="lblReorderReportTitle" runat="server" Text="Reorder Report as "></asp:Label>
                <asp:Label ID="lblDate" runat="server" DataFormatString="{0:dd-MMM-yyyy}"></asp:Label>
            </h2>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-4 col-xs-offset-2" style="margin-top:15px;">
            <asp:RadioButtonList ID="radbtnChoose" runat="server" AutoPostBack="True" OnSelectedIndexChanged="radbtnChoose_SelectedIndexChanged" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Value="0">Reorder</asp:ListItem>
                <asp:ListItem Value="1">Purchase</asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 maintable">
            <div class="table-responsive">
                            <asp:GridView ID="reorderReportGV" runat="server" CssClass="table" AutoGenerateColumns="False" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" AllowPaging="True" PageSize="10" OnPageIndexChanging="reorderReportGV_PageIndexChanging" OnSelectedIndexChanging="reorderReportGV_SelectedIndexChanging">
                                <Columns>
                                 <%--   <asp:BoundField HeaderText="S/N" DataField="Serialno"/>--%>
                                    <asp:BoundField HeaderText="Item Code" DataField="ItemCode"/>
                                    <asp:BoundField HeaderText="Description" DataField="Description"/>
                                    <asp:BoundField HeaderText="In Stock Qty" DataField="QuantityOnhand"/>
                                    <asp:BoundField HeaderText="Re-order level" DataField="ReorderLevel"/>
                                    <asp:BoundField HeaderText="Re-order Qty" DataField="ReorderQty"/>
                                    <asp:BoundField HeaderText="Purchase Number" DataField="PurchaseID"/>
                                    <asp:CommandField SelectText="Purchase" ShowSelectButton="True" />
                                </Columns>
                        </asp:GridView>
                            <div class="row">
                                <div class="col-md-4"></div>
                                <div class="col-md-4">
<%--                                    <asp:Button ID="btnExportToPDF" runat="server" Text="Export" CssClass="btn btn-primary" OnClick="btnExportToPDF_Click" />--%>
                                </div>
                                <div class="col-md-4"></div>
                            </div>
                </div>
        </div>
    </div>
<%--    <div class="row">
        <div class="col-md-4"></div>
        <div class="col-md-4">
            <asp:Button ID="btnExportToPDF" runat="server" Text="Export" CssClass="btn btn-primary" OnClick="btnExportToPDF_Click" />
        </div>
        <div class="col-md-4"></div>
    </div>--%>
</div>
</asp:Content>
