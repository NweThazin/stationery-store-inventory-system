<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="InventoryAdjustment.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.InventoryAdjustment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container-fluid pg-content">
    <div class="row" style ="margin-left: 10px;">
        <div class="col-md-6">
            <h2>Inventory Adjustment</h2>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2"></div> 
        <div class="col-md-6 pg-content">
            <fieldset class="well the-fieldset">
                <legend class="the-legend">Add New Inventory</legend>
                    
                <div class="form-group row">
                    <label class="col-md-3 col-form-label">Item Number:</label>
                    <div class="col-md-5">
                        <asp:TextBox ID="txtItemNumber" runat="server" CssClass="form-control"></asp:TextBox> 
                    </div>
                    <div class="col-md-1 pull-left">
                         <button type="button" class="form-control pull-left" data-toggle="modal" data-target="#myModal">...</button>
                    </div>
                </div>
                
                <!-- Modal -->
                <div class="modal fade" id="myModal" role="dialog">
                    <div class="modal-dialog modal-lg">
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
                        <asp:Button ID="btnSelect" runat="server" Text="Select" CssClass="btn btn-default" OnClick="btnSelect_Click"/>
                   <%--     <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                    </div>
                    </div>
                 </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-3 col-form-label">Adjustment Quantity:</label>
                    <div class="col-md-5">
                        <asp:TextBox ID="txtAdjustment" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group row">
                    <label class="col-md-3 col-form-label"></label>
                    <div class="col-md-5">
                        <asp:RadioButtonList ID="radbtnChoose" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" CellPadding="5" CellSpacing="3">
                            <asp:ListItem Selected="True">Add</asp:ListItem>
                            <asp:ListItem>Remove</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
     
                <div class="form-group row">
                    <label class="col-md-3 col-form-label">Reason:</label>
                    <div class="col-md-5">
                        <asp:TextBox ID="txtReason" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="check" />
                </div>
                <div class="form-group row">
                     <div class="col-md-2"></div>   
                    <div class="col-md-4 col-md-offset-1">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="form-control btn btn-primary" OnClick="btnAdd_Click" ValidationGroup="check"/>
                    </div>
                                            
                </div>
        </fieldset>
      </div>
    <div class="col-lg-4"></div>
   </div>

    <div class="row">
          <div class="table-responsive">
              <asp:GridView ID="inventoryAdjustmentGV" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" OnRowDeleting="inventoryAdjustmentGV_RowDeleting" CssClass="table" ShowFooter="True">
                  <Columns>
                      <asp:BoundField HeaderText="Item Code" DataField="ItemNumber" FooterText="Amount"/>
                      <asp:BoundField HeaderText="Quantity Adjustment" DataField="AdjustmentQty"/>
                      <asp:BoundField HeaderText="Resason" DataField="Reason"/>
                      <asp:CommandField HeaderText="Remove" ShowDeleteButton="true" ControlStyle-CssClass="btn btn-sm">
                      </asp:CommandField>
                  </Columns>
              </asp:GridView>
        </div>
    </div>
            <div class="form-group row">
                <div class="col-md-10">
                    <asp:Label ID="lblShow" runat="server" Text=""></asp:Label>
                </div>
            </div><br/>
    <div class="form-group row">
        <div class="col-md-4"></div>
        <div class="col-md-1 col-md-offset-1">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-control btn btn-warning" OnClick="btnSave_Click" OnClientClick="return confirm('Are you sure to submit all adjustments?')"/>
        </div>
        <div class="col-md-7"></div>
    </div>
            <asp:RangeValidator ID="rangeAdjustment" runat="server" ErrorMessage="Should be number" Display="None" ValidationGroup="check" ControlToValidate="txtAdjustment" MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator><br/>
            <asp:RequiredFieldValidator ID="adjustmentValidation" runat="server" ErrorMessage="Please enter adjustment qty" Display="None" ValidationGroup="check" ControlToValidate="txtAdjustment"></asp:RequiredFieldValidator><br/>
            <asp:RequiredFieldValidator ID="reasonValidation" runat="server" ErrorMessage="Please enter reason" Display="None" ValidationGroup="check" ControlToValidate="txtReason"></asp:RequiredFieldValidator>
            <br />
            <asp:RequiredFieldValidator ID="itemNumberValidation" runat="server" ControlToValidate="txtItemNumber" ErrorMessage="Please choose item" ValidationGroup="check"></asp:RequiredFieldValidator>
    </div>
</asp:Content>
