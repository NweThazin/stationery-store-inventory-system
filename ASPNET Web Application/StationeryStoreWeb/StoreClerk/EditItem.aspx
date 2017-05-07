<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditItem.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.EditItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid pg-content">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-8 pg-content">
                <div class="row" style ="margin-left: 20px;">
                    <div class="col-md-8">
                       <h1><i class="fa fa-pencil"></i> Edit Item</h1>
                    </div>                    
                </div>
                <br />
                <div class="form-group row">
                    <label class="col-md-4 col-form-label">Item Number:</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtItemNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-4 col-form-label">Category:</label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                        </asp:DropDownList>
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
                         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="check" ForeColor="Red" />
                     </div>
                    </div>
                <div class="form-group row">
                    <div class="col-md-4"></div>
                    <div class="col-md-4 col-md-offset-1">
                        <asp:Button ID="btnEdit" runat="server" Text="Update" CssClass="form-control btn btn-primary" OnClick="btnEdit_Click" ValidationGroup="check"/>
                    </div>
                    <div class="col-md-4"></div>                            
                </div>
            </div>
            <div class="auto-style2"></div>
            <asp:RequiredFieldValidator ID="ReorderLevelValidation" runat="server" ErrorMessage="Please enter Reorder Level" ControlToValidate="txtReorderlevel" EnableTheming="True" ForeColor="Red" ValidationGroup="check" Display="None"></asp:RequiredFieldValidator><br/>
            <asp:RangeValidator ID="reorderLevelRange" runat="server" ErrorMessage="Please enter number which is greater than zero" ControlToValidate="txtReorderlevel" Display="None" ForeColor="Red" ValidationGroup="check" MaximumValue="1500" MinimumValue="1" Type="Integer"></asp:RangeValidator><br/>
            <asp:RequiredFieldValidator ID="ReorderQtyValidation" runat="server" ErrorMessage="Please enter Reorder Qty" ControlToValidate="txtReorderQty" ForeColor="Red" ValidationGroup="check" Display="None"></asp:RequiredFieldValidator><br/>
            <asp:RangeValidator ID="reorderQtyRange" runat="server" ErrorMessage="Please enter number which is greater than zero" ControlToValidate="txtReorderQty" Display="None" ForeColor="Red" ValidationGroup="check" MaximumValue="100000" MinimumValue="1" Type="Integer"></asp:RangeValidator><br/>
            <asp:RequiredFieldValidator ID="UomValidation" runat="server" ErrorMessage="Please enter Unit of Measure" ControlToValidate="txtUOM" ForeColor="Red" ValidationGroup="check" Display="None"></asp:RequiredFieldValidator><br/>
            <asp:RequiredFieldValidator ID="DescriptionValidation" runat="server" ErrorMessage="Please enter Description" ControlToValidate="txtDescription" ForeColor="Red" ValidationGroup="check" Display="None"></asp:RequiredFieldValidator><br/>
            <asp:RequiredFieldValidator ID="BinValidation" runat="server" ErrorMessage="Please enter Bin" ControlToValidate="txtBin" ForeColor="Red" ValidationGroup="check" Display="None"></asp:RequiredFieldValidator>
        </div>
    </div>
</asp:Content>
