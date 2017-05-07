<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdjustmentVouchersList.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.AdjustmentVouchersList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid pg-content">
    <div class="row">
        <div class="col-md-12">
            <h2><i class="fa fa-file-text"></i> Adjustment Stock Vouchers</h2>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 maintable">
            <div class="table-responsive">
                <asp:GridView ID="adjustmentVoucherGV" runat="server" CssClass="table" AutoGenerateColumns="False" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" AllowPaging="True" OnSelectedIndexChanging="adjustmentVoucherGV_SelectedIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="Vouncher Number" DataField="AdjID"/>
                        <asp:BoundField HeaderText="Issue Date" DataField="Date" DataFormatString="{0:dd/MM/yyyy}"/>
                        <asp:BoundField HeaderText="Status" DataField="Status" />
                        <asp:CommandField SelectText="Detail" ShowSelectButton="True" />
                    </Columns>
                 </asp:GridView>
            </div>
        </div>
    </div>
</div>
</asp:Content>
