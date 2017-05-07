<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.Master" AutoEventWireup="true" CodeBehind="RequisitionDetail.aspx.cs" Inherits="ADProjectSA43_Team1.Employee.CurrentRequisitionDetailedPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid pg-content">
    <div class="row">
        <div class="col-md-6">
            <h2>
                <i class="fa fa-cube"></i> 
                Requisition ID:  <asp:Label ID="lblItemCode" runat="server"></asp:Label>
            </h2>
        </div>
    </div>
    <div class="row" style="margin:10px;">
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-3"><b>Issue Date:</b></div>
                <div class="col-md-3"><asp:Label ID="lblIssueDate" runat="server" DataFormatString="{0:dd/MM/yyyy}"></asp:Label></div>
            </div>
            <div class="row">
                <div class="col-md-3"><b>Approval Status:</b></div>
                <div class="col-md-3"><asp:Label ID="lblStatus" runat="server"></asp:Label></div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 maintable">
            <div class="table-responsive">
                <asp:GridView ID="RequisitionDetailGV" runat="server" CssClass="table" AutoGenerateColumns="False" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px">
                    <Columns>
                      <%--  <asp:BoundField HeaderText="#"/>--%>
                      <%--  <asp:BoundField HeaderText="Item Number" DataField="ItemNumber"/>--%>
                        <asp:BoundField HeaderText="Description" DataField="Description"/>
                        <asp:BoundField HeaderText="Requested Quantity" DataField="RequestedQty"/>
                        <asp:BoundField HeaderText="Received Quantity" DataField="ReceivedQty"/>
                      <%--  <asp:BoundField HeaderText="Status" DataField="Status"/>--%>
                        <asp:BoundField HeaderText="Unit of Measure" DataField="Uom"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4"></div>
        <div class="col-md-4"><asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-default" OnClick="btnBack_Click" /></div>
         <div class="col-md-4"></div>
    </div>
        </div>
</asp:Content>
