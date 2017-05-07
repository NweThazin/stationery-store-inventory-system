<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AddItem.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.AddItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-8 pg-content">
                <div class="row" style ="margin-left: 20px;">
                    <div class="col-md-8">
                       <h1><i class="fa fa-plus-square"></i> Add New Item</h1>
                    </div>                    
                </div>
                <br />
                <div class="form-group row">
                    <label class="col-md-4 col-form-label">Item Number:</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtItemNumber" runat="server" CssClass="form-control" OnTextChanged="txtItemNumber_TextChanged"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-4 col-form-label">Category:</label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-4 col-form-label">Reorder Level:</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtReorderlevel" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-4 col-form-label">Reorder Quantity:</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtReorderQty" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-4 col-form-label">Unit of Measure:</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtUOM" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-4 col-form-label">Description:</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-4 col-form-label">Bin:</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtBin" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                     <div class="col-md-6">
                         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                     </div>
                    </div>
                <div class="form-group row">
                    <div class="col-md-4"></div>
                    <div class="col-md-4 col-md-offset-1">
                        <asp:Button ID="btnAddItem" runat="server" Text="Add" CssClass="form-control btn btn-primary" OnClick="btnAddItem_Click"/>
                    </div>
                    <div class="col-md-4"></div>
                </div>
            </div>
            <div class="col-md-2"></div>
            <%-- Validation --%>
            <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtReorderlevel" forecolor="Red" ErrorMessage="Please input a whole number for Reorder Level" /><br/>
            <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtReorderQty" forecolor="Red" ErrorMessage="Please input a whole number for Reorder Quantity" /><br/>
            <asp:RequiredFieldValidator ID="validationItemNum" runat="server" ErrorMessage="Please enter Item Number" ControlToValidate="txtItemNumber" Display="None"></asp:RequiredFieldValidator><br/>
            <asp:RequiredFieldValidator ID="validationReorderLevel" runat="server" ErrorMessage="Please enter Reorder Level" ControlToValidate="txtReorderlevel" Display="None"></asp:RequiredFieldValidator><br/>
            <asp:RequiredFieldValidator ID="validationReorderQty" runat="server" ErrorMessage="Please enter Reorder Qty" ControlToValidate="txtReorderQty" Display="None"></asp:RequiredFieldValidator><br/>
            <asp:RequiredFieldValidator ID="validationUOM" runat="server" ErrorMessage="Please enter UOM" ControlToValidate="txtUOM" Display="None"></asp:RequiredFieldValidator><br/>
            <asp:RequiredFieldValidator ID="validationDescription" runat="server" ErrorMessage="Please enter description" ControlToValidate="txtDescription" Display="None"></asp:RequiredFieldValidator><br/>
            <asp:RequiredFieldValidator ID="validationBin" runat="server" ErrorMessage="Please enter bin" ControlToValidate="txtBin" Display="None"></asp:RequiredFieldValidator>
        </div>
    </div>
</asp:Content>
