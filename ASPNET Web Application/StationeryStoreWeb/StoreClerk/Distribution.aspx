<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Distribution.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.Distribution" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid pg-content">
    <div class="row">
       <div class="col-md-12">
            <div class="row">
                <div class="col-md-6">
                    <h2><i class="glyphicon glyphicon-list"></i> Disbursement List </h2>
                </div>
            </div>

            <br/>        
            
            <div class="row">
                <div class="table-responsive">
                  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" CssClass="table" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px">
                    <ColumnS>
                        <asp:BoundField DataField="Name" HeaderText="Location" SortExpression="Name" />
                        <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee" SortExpression="EmployeeName" />
                        <asp:HyperLinkField DataNavigateUrlFields="CollectionPointID" DataNavigateUrlFormatString="DestinationPage.aspx?collectionPointID={0}" Text="Detail" />
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="getCollectionPointList" TypeName="BusinessLayer.CollectionPointBL"></asp:ObjectDataSource>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
