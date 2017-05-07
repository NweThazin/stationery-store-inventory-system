<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdjustmentVouchersDetail.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.AdjustmentVouchersDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pg-content">

        <div class="row">
                <div class="col-md-2"><b>Adjustment Stock Voucher:</b></div>
                <div class="col-md-2"><asp:Label ID="lblAdjustmentCode" runat="server"></asp:Label></div>
        </div>
        <div class="row">
                <div class="col-md-2"><b>Date Issue:</b></div>
                <div class="col-md-2"><asp:Label ID="lblDateIssue" runat="server" DataFormatString="{0:dd/MM/yyyy}"></asp:Label></div>
        </div>
        <div class="row">
            <div class="col-md-12 maintable">
                <div class="table-responsive">
                    <asp:GridView ID="adjustDetailGV" runat="server" CssClass="table" AutoGenerateColumns="False" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px">
                        <Columns>
                            <asp:BoundField HeaderText="Item Code" DataField="ItemNumber"/>
                            <asp:BoundField HeaderText="Item Description" DataField="ItemDescription"/>
                            <asp:BoundField HeaderText="Quantity Adjusted" DataField="QuantityAdjusted"/>
                            <asp:BoundField HeaderText="Reason" DataField="Reason"/>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4"></div>
                <div class="col-md-2">
                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-md btn-primary" OnClick="btnApprove_Click"  OnClientClick="return confirm('Are you sure to approve?')"/></div>
                <div class="col-md-2">
                    <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-md btn-warning" OnClick="btnReject_Click" OnClientClick="return confirm('Are you sure to reject?')"/></div>
            <div class="col-md-4"></div>
        </div>
        </div>
</asp:Content>
