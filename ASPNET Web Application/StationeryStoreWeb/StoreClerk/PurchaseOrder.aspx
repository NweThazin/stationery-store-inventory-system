<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PurchaseOrder.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.PurchaseOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid pg-content">
    <div class="row">
        <div class="col-md-12">
            <h2><i class="fa fa-plus-square"></i> Purchase Stationery</h2>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3"><b>Expected Delivery Date:</b></div>
        <div class="col-md-3">
            <asp:TextBox ID="txtExpectedDeliveryDate" runat="server" TextMode="Date" ValidationGroup="check"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6" style="margin: 10px;">
            <fieldset class="fieldset">
                <legend>Add Item</legend>
                         
                         <div class="form-group row">
                             <label class="col-md-3 col-form-label">Item Number:</label>
                             <div class="col-md-5">
                                 <asp:TextBox ID="txtItemNumber" runat="server" CssClass="form-control" ValidationGroup="check"></asp:TextBox>
                             </div>
                             <div class="col-md-1 pull-left">
                                 <button type="button" class="form-control pull-left" data-toggle="modal" data-target="#myModal">...</button>
                             </div>
                         </div>
                         <!-- Modal -->
                         <div class="modal fade" id="myModal" role="dialog">
                             <div class="modal-dialog modal-sm">
                                 <!-- Modal content-->
                                 <div class="modal-content">
                                     <div class="modal-header">
                                         <button type="button" class="close" data-dismiss="modal">&times;</button>
                                         <h4 class="modal-title">Choose Item</h4>
                                     </div>
                                     <div class="modal-body">
                                         <asp:ListBox ID="lstboxShowItems" runat="server" Height="100%" Width="100%"></asp:ListBox>
                                     </div>
                                     <div class="modal-footer">
                                         <asp:Button ID="btnSelect" runat="server" Text="Select" CssClass="btn btn-md btn-default" OnClick="btnSelect_Click"/>
                                     </div>
                                 </div>
                             </div>
                         </div>
                         <div class="form-group row">
                             <label class="col-md-3 col-form-label">Supplier:</label>
                             <div class="col-md-5">
                                 <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control"></asp:DropDownList>
                             </div>
                         </div>
                         <div class="form-group row">
                             <label class="col-md-3 col-form-label">Description:</label>
                             <div class="col-md-5">
                                 <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" ValidationGroup="check"></asp:TextBox>
                             </div>
                         </div>
                         <div class="form-group row">
                             <label class="col-md-3 col-form-label">Quantity:</label>
                             <div class="col-md-5">
                                <asp:TextBox ID="txtQuantity" onkeypress='return event.charCode >= 48 && event.charCode <= 57' runat="server" CssClass="form-control" ValidationGroup="check" ></asp:TextBox>
                            </div>
                         </div>

                        <div class="form-group row">
                            <div class="col-md-2"></div>
                            <div class="col-md-4 col-md-offset-1">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-md btn-primary" OnClick="btnAdd_Click" ValidationGroup="check"/>
                            </div>
                        </div>
                     </fieldset>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="check" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 maintable">
            <div class="table-responsive">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Height="100%" Width="100%"  ShowFooter="True" CellPadding="3" BorderWidth="0px"  CssClass="table">
                      <Columns>
                          <asp:BoundField HeaderText="Item No" DataField="ItemNo"/>
                          <asp:BoundField HeaderText="Item Description" DataField="itemDescription" />
                          <asp:BoundField HeaderText="Supplier" DataField="supplierName" />
                          <asp:BoundField HeaderText="Quantity" DataField="quantity"/>
                          <asp:BoundField HeaderText="Price" DataField="price"  FooterText="Total Amount"/>
                          <asp:BoundField HeaderText="Amount" DataField="amount"  />
                      </Columns>
                  </asp:GridView>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3"></div>
        <div class="col-md-2 col-md-offset-1">
            <asp:Button ID="btnPurchase" runat="server" Text="Purchase" CssClass="form-control btn btn-warning" OnClick="btnPurchase_Click" OnClientClick="return confirm('Are you sure to purchase all items?')"/>
        </div>
        <div class="col-md-7"></div>
    </div>
    <div class="row">
        <asp:RequiredFieldValidator ID="dateValidation" runat="server" ErrorMessage="Please choose Date" ControlToValidate="txtExpectedDeliveryDate" Display="None" ValidationGroup="check"></asp:RequiredFieldValidator><br/>
        <asp:RequiredFieldValidator ID="validationItemNumber" runat="server" ErrorMessage="Please choose item number" Display="None" ValidationGroup="check" ControlToValidate="txtItemNumber"></asp:RequiredFieldValidator><br/>
        <asp:RequiredFieldValidator ID="validationDescription" runat="server" ErrorMessage="Please enter description" ControlToValidate="txtDescription" Display="None" ValidationGroup="check"></asp:RequiredFieldValidator><br/>
        <asp:RequiredFieldValidator ID="validationQuantity" runat="server" ErrorMessage="Please enter quantity" ControlToValidate="txtQuantity" Display="None" ValidationGroup="check"></asp:RequiredFieldValidator><br/>
        <asp:RangeValidator ID="rangeQuantity" runat="server" ErrorMessage="RangeValidator" ControlToValidate="txtQuantity" Display="None" MaximumValue="2000" MinimumValue="1" Type="Integer" ValidationGroup="check"></asp:RangeValidator>
    </div>

</div>
</asp:Content>
