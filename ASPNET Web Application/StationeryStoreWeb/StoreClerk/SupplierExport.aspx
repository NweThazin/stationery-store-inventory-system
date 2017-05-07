<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SupplierExport.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.SupplierExport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <h2><i class="fa fa-plus-square"></i> Add Supplier</h2>
        </div>
    </div>

    <div class="row" style="margin-top:10px;">
        <div class="col-md-4">
            <asp:FileUpload ID="Upload" runat="server"/>
        </div>
    </div>

    <div class="row" style="margin-top:10px;">
        <div class="col-md-4">
            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-md btn-primary"/>

        </div>
    </div>

    <div class="row">
        <div class="col-md-12 maintable">
            <div class="table-responsive">
                <asp:GridView ID="GridView1" runat="server" CssClass="table" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" AllowPaging="True" PageSize="12" OnPageIndexChanging="GridView1_PageIndexChanging">

                </asp:GridView>
            </div>
        </div>
    </div>
    
    
</div>
</asp:Content>
