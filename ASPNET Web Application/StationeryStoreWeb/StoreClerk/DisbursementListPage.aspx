<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DisbursementListPage.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.DisbursementListPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid pg-content">
    
    <div class="row">
        <div class="col-md-6">
            <h2><i class="glyphicon glyphicon-list"></i>Disbursement List for 
                <asp:Label ID="DepartmentName" runat="server" Text="departmentName"> Department</asp:Label></h2>
            <h4>Collection Point:<asp:label ID="collectionPoint" runat="server" Text="collectionPoint"></asp:label>
            </h4>
        </div>
    </div>
    
              <div class="row">
                <div class="table-responsive">
                  <asp:GridView ID="disbursementGV" runat="server" AutoGenerateColumns="False"   CssClass="table" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" AllowPaging="True" OnPageIndexChanging="disbursementGV_PageIndexChanging">
                    <ColumnS>
                         
                        <asp:BoundField DataField="ItemNumber" HeaderText="ItemNumber" SortExpression="ItemNumber" ReadOnly="True"/>
                        <asp:BoundField DataField="ItemDescription" HeaderText="Description" SortExpression="ItemDescription" ReadOnly="True"/>
                         <asp:BoundField DataField="Qty"  HeaderText="Quantity" SortExpression="Qty" />
                    </Columns>
                </asp:GridView>
                </div>
            </div>

            <div class="row">
            <div class="col-md-4"></div>
                <div class="col-md-2">
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-md btn-primary" Text="Back" OnClick="Button1_Click"  /></div>
                <div class="col-md-2">
                    <asp:Button ID="btnDownload" runat="server" CssClass="btn btn-md btn-primary" Text="Download" OnClick="btnDownload_Click" /></div>
            <div class="col-md-4"></div>
        </div>         
</div>
</asp:Content>