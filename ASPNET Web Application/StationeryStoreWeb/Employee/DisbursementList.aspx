<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.Master" AutoEventWireup="true" CodeBehind="DisbursementList.aspx.cs" Inherits="ADProjectSA43_Team1.Employee.DisbursementList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid pg-content">
    <div class="row">
        <div class="col-md-6">
            <h2><i class="glyphicon glyphicon-list"></i>Disbursement List </h2>
        </div>
    </div>

            <div class="row">
                <div class="table-responsive">
                  <asp:GridView ID="disbursementGV" runat="server" AutoGenerateColumns="False"   CssClass="table" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" OnRowEditing="disbursementGV_RowEditing" OnRowCancelingEdit="disbursementGV_RowCancelingEdit"  OnRowUpdating="disbursementGV_RowUpdating" AllowPaging="True" OnPageIndexChanging="disbursementGV_PageIndexChanging">
                    <ColumnS>
                         
                        <asp:BoundField DataField="ItemNumber" HeaderText="ItemNumber" SortExpression="ItemNumber" ReadOnly="True"/>
                        <asp:BoundField DataField="ItemDescription" HeaderText="Description" SortExpression="ItemDescription" ReadOnly="True"/>
                         <asp:BoundField DataField="Qty"  HeaderText="Quantity" SortExpression="Qty" />
                         <asp:TemplateField  HeaderText="">
                            <ItemTemplate>
                                
                                  <asp:HiddenField ID="HiddenField1" value='<%#Eval("ItemID") %>'  runat="server" />
                                  <asp:HiddenField ID="HiddenField2" value='<%#Eval("DisbursementID") %>'  runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                   
                        <asp:CommandField ShowEditButton="true"   />
                    </Columns>
                </asp:GridView>
              
                </div>
            </div>

        <div class="row">
            <div class="col-md-12">
                <div class="col-md-4"></div>
                <div class="col-md-4">  <%--Ask Client ALert BoX--%>
                    <asp:Button ID="Confirm" runat="server" Text="Confirm" CssClass="btn btn-md btn-primary" OnClick="Confirm_Click" />
                    <%--<button type="button" class="btn btn-md btn-warning" data-toggle="modal" data-target="#myModal">Reject--%>
                </div>
                <div class="col-md-4">
                
                </div>
            </div>
        </div>
    </div>
</asp:Content>
