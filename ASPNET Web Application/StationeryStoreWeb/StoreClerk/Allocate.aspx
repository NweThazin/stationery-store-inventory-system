<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Allocate.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.Allocate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pg-content">
        <div class="row">
            <div class="col-md-12">
                <h2><i class="fa fa-exchange"></i> Breakdown by Department</h2>
            </div>
         </div>
        <div class="row">
            <div class="col-md-12 maintable">
                 <div class="table-responsive">
                     <asp:GridView ID="GVAllocate" runat="server" AutoGenerateColumns="False"  GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" CssClass="table">
                        <Columns>
                            <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" />
                            <asp:BoundField DataField="Requested" HeaderText="Requested Quantity" />
                            <asp:BoundField DataField="Unfulfilled" HeaderText="Unfulfilled Quantity" />
                            <asp:TemplateField HeaderText="Allocate">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtToRetrieve" runat="server" Text='<%# Eval("ToRetrieve") %>' onkeypress='return event.charCode >= 48 && event.charCode <= 57'></asp:TextBox>
                                    <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                 </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <asp:Button runat="server" Text="Submit" ID="btnSubmit" OnClick="btnSubmit_Click" CssClass="btn btn-md btn-primary" />
                <asp:Button ID="btnCancel" runat="server" OnClick="Button1_Click" Text="Cancel" CssClass="btn btn-md btn-warning"/>
            </div>
            <div class="col-md-4"></div>
        </div>
    </div>
</asp:Content>
