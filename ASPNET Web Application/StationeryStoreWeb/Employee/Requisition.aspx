<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.Master" AutoEventWireup="true" CodeBehind="Requisition.aspx.cs" Inherits="ADProjectSA43_Team1.Employee.CurrentRequisition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pg-content">
        <div class="row">
            <div class="col-md-12">
                <h2><i class="fa fa-file"></i> Requisition History</h2>
            </div>
         </div>
        <div class="row">
            <div class="col-md-12" style="margin-top:10px;">
                <div class="col-md-3"><b>Start Date:</b></div>
                <div class="col-md-3"><b>End Date:</b></div>
                <div class="col-md-3"></div>
                <div class="col-md-3"></div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12" style="margin-top:10px;">
                <div class="col-md-3">
                     <asp:TextBox ID="txtFromDate" TextMode="Date"  runat="server" ValidationGroup="check"></asp:TextBox>
                </div>
                <div class="col-md-3">
                   <asp:TextBox ID="txtToDate" runat="server" TextMode="Date" ValidationGroup="check"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-xs btn-primary" OnClick="btnSearch_Click" ValidationGroup="check"/>
                </div>
                <div class="col-md-3"></div>
            </div>
        </div>
        
            <div class="row">
                  <div class="col-md-12" style ="margin-left: 10px;">
                      <div class="col-md-3">
                          <asp:ValidationSummary ID="validationSummary" runat="server" ForeColor="Red" ValidationGroup="check" /><br/>
                          <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                      </div>
                  </div>  
            </div>
            <br/>

        <div class="row">
            <div class="col-md-12 maintable">
                <div class="table-responsive">
                    <asp:GridView ID="requisitionGV" runat="server" CssClass="table" AutoGenerateColumns="False" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" AllowPaging="True" OnSelectedIndexChanging="requisitionGV_SelectedIndexChanging" OnPageIndexChanging="requisitionGV_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("FullName") %>'></asp:Label>
                                    <asp:HiddenField ID="hdnReqNumber" runat="server" value='<%#Eval("ReqID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Issue Date" DataField="OrderDate" DataFormatString="{0:dd/MM/yyyy}"/>
                            <asp:BoundField HeaderText="Status" DataField="Status"/>
                            <asp:CommandField ShowSelectButton="true" SelectText="Details"/>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
            
        <asp:RequiredFieldValidator ID="FromDateValidator" runat="server" ErrorMessage="Please enter start date" ControlToValidate="txtFromDate" Display="None" ValidationGroup="check"></asp:RequiredFieldValidator><br/>
        <asp:RequiredFieldValidator ID="ToDateValidator" runat="server" ErrorMessage="Please enter end date" ControlToValidate="txtToDate" Display="None" ValidationGroup="check"></asp:RequiredFieldValidator><br/>
        <asp:CompareValidator ID="compareEarlier" runat="server" ControlToCompare="txtFromDate" CultureInvariantValues="true" Display="None" EnableClientScript="true" ControlToValidate="txtToDate" ErrorMessage="Start date must be earlier than end date" Type="Date" Operator="GreaterThanEqual" ValidationGroup="check"></asp:CompareValidator>
<%--        <asp:CompareValidator ID="compareSameDate" runat="server" ControlToCompare="txtFromDate" CultureInvariantValues="true" Display="None" EnableClientScript="true" ControlToValidate="txtToDate" ErrorMessage="Start date cannot be same as end date" Type="Date" Operator="NotEqual" ValidationGroup="check"></asp:CompareValidator>--%>
    &nbsp;
    </div>
        
</asp:Content>
