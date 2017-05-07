<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UpdateStockCard.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.UpdateStockCard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid pg-content">
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 pg-content">
            <div class="row" style ="margin-left: 20px;">
                       <h2>Update Stock Card for 
                           <asp:Label ID="lblStock" runat="server" DataFormatString="{0:dd/MM/yyyy}"></asp:Label>
                       </h2>
            </div> <br />

                <div class="form-group row">
                    <label class="col-md-4 col-form-label">Date:</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtCalendar" runat="server" CssClass="form-control" placeholder="dd/MM/yyyy" TextMode="Date" ></asp:TextBox>
                    </div>
                </div>

                <div class="form-group row">
                    <label class="col-md-4 col-form-label">Supplier:</label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group row">
                    <label class="col-md-4 col-form-label">Amount:</label>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-6">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ValidationGroup="check" />
                    </div>
                </div>
                <br/>
                <div class="form-group row">
                    <div class="col-md-4 col-md-offset-1">
                        <asp:Button ID="btnEdit" runat="server" Text="Update" CssClass="btn btn-md btn-primary" OnClick="btnEdit_Click" ValidationGroup="check"/>
                        
                    </div>
                    <div class="col-md-4 col-md-offset-1">
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-md btn-default" OnClick="btnBack_Click"/>
                    </div>
                    <div class="col-md-4"></div>                            
                </div>
            </div>
        </div>
        <asp:RequiredFieldValidator ID="validationDate" runat="server" ErrorMessage="Please choose Date" ControlToValidate="txtCalendar" Display="None" ValidationGroup="check"></asp:RequiredFieldValidator><br/>
        <asp:RequiredFieldValidator ID="validationAmount" runat="server" ErrorMessage="Please enter amount" ControlToValidate="txtAmount" Display="None" ValidationGroup="check"></asp:RequiredFieldValidator><br/>
        <asp:RangeValidator ID="rangeAmount" runat="server" ErrorMessage="Should be number and greater than zero" ControlToValidate="txtAmount" Display="None" ValidationGroup="check" MaximumValue="2000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
</div>
</asp:Content>
