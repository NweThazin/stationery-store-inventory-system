<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="DelegateByManager.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.DelegateManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid pg-content">
    <div class="row">
        <div class="col-md-6">
            <h2><i class="fa fa-user"></i> Current Store Manager: <asp:Label ID="lblCurrentManager" runat="server" Text=""></asp:Label></h2>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4" style="margin-top:10px;">
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="lblStartDateLabel" runat="server" Text="Start Date:"></asp:Label> &nbsp;
                    <asp:Label ID="lblStartDate" runat="server" DataFormatString="{0:dd/MMM/yyyy}"></asp:Label><br/>
                    <asp:Label ID="lblEndDateLabel" runat="server" Text="End Date:"></asp:Label>&nbsp;
                    <asp:Label ID="lblEndDate" runat="server" DataFormatString="{0:dd/MMM/yyyy}"></asp:Label> &nbsp;
                </div>
                <div class="col-md-6">
                   <asp:Button ID="btnRemoveManager" runat="server" Text="Remove Manager" OnClick="btnRemoveManager_Click" CssClass="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <h2><i class="fa fa-users"></i>Employees</h2><br/>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 maintable">
            <div class="table-responsive">
                <asp:GridView ID="changeManagerGV" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" CssClass="table" AllowPaging="True" OnPageIndexChanging="changeManagerGV_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:RadioButton ID="radbtnChoose" runat="server" GroupName="rep" AutoPostBack="true" OnCheckedChanged="radbtnChoose_CheckedChanged"/>
                                <asp:HiddenField ID="hdnChoose" runat="server" Value='<%#Eval("EmployeeID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Employee Name" DataField="FullName"/>
                        <asp:BoundField HeaderText="Email" DataField="Email"/>
                        <asp:BoundField HeaderText="Phone" DataField="Phone"/>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="LblValidate" runat="server"></asp:Label>
            </div>
        </div>
        <br/>
         
        <div class="form-group row">
            <div class="col-md-4"></div>
            <div class="col-md-2 col-md-offset-1">
                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal">Assign Manager</button>
            </div>          
        </div>
        <!-- Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Choose Date</h4>
                    </div>
                    <div class="modal-body">
                        Start Date: <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date"></asp:TextBox><br/><br/>
                        End Date: <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date"></asp:TextBox><br/> 
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="check" ForeColor="Red"/><br/>                 
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnChange" runat="server" Text="Change Manager" CssClass="btn btn-warning" OnClick="btnChange_Click" ValidationGroup="check"/>
                    </div>
                    </div>
                 </div>
          
    </div>
    </div>
            <asp:RequiredFieldValidator ID="validatorStartDate" runat="server" ErrorMessage="Please choose start date" ControlToValidate="txtStartDate" Display="None" ValidationGroup="check"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="validatorEndDate" runat="server" ErrorMessage="Please choose end date" ControlToValidate="txtEndDate" Display="None" ValidationGroup="check"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="compareEarlier" runat="server" ControlToCompare="txtStartDate" CultureInvariantValues="true" Display="None" EnableClientScript="true" ControlToValidate="txtEndDate" ErrorMessage="Start date must be earlier than end date" Type="Date" Operator="GreaterThanEqual" ValidationGroup="check"></asp:CompareValidator>
            <asp:CompareValidator ID="compareSameDate" runat="server" ControlToCompare="txtStartDate" CultureInvariantValues="true" Display="None" EnableClientScript="true" ControlToValidate="txtEndDate" ErrorMessage="Start date cannot be same as end date" Type="Date" Operator="NotEqual" ValidationGroup="check"></asp:CompareValidator>
            <asp:CompareValidator runat="server" ID="compareValidatorStartToday" Display="None" ErrorMessage="Start date cannot be earlier than today" ControlToValidate="txtStartDate" Type="date" Operator="GreaterThanEqual" ValidationGroup="check" />
            <asp:CompareValidator runat="server" ID="compareValidatorEndToday" Display="None" ErrorMessage="End date cannot be earlier than today" ControlToValidate="txtEndDate"  Type="date" Operator="GreaterThan" ValidationGroup="check"/>
    </div>
</asp:Content>
