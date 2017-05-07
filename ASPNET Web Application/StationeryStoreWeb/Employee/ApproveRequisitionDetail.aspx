<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.Master" AutoEventWireup="true" CodeBehind="ApproveRequisitionDetail.aspx.cs" Inherits="ADProjectSA43_Team1.Employee.ApproveRequisitionDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid pg-content">
    <div class="row">
        <div class="col-md-6">
            <h2>
                <i class="fa fa-file-text"></i> 
                Requisition Detail
            </h2>
        </div>
    </div>
    <%-- Show the requisition item lists by requisition ID --%>
    <div class="row">
        <div class="col-md-12 maintable">
            <div class="table-responsive">
                <asp:GridView ID="approveRequistionDetailGV" runat="server" CssClass="table" AutoGenerateColumns="False" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" AllowPaging="True" OnPageIndexChanging="approveRequistionDetailGV_PageIndexChanging">
                    <Columns>
                    <asp:BoundField HeaderText="Item Description" DataField="Description"/>
                    <asp:BoundField HeaderText="Quantity" DataField="RequestedQty"/>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="col-md-4"></div>
            <div class="col-md-4">  <%--Ask Client ALert BoX--%>
                <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-md btn-primary" OnClick="btnApprove_Click" OnClientClick="return confirm('Are you sure to approve this requistion ? ');"/>
                <button type="button" class="btn btn-md btn-warning" data-toggle="modal" data-target="#myModal">Reject</button>
            </div>
            <div class="col-md-4">
                
            </div>
        </div>
    </div>

                <!-- Modal -->
                <div class="modal fade" id="myModal" role="dialog">
                    <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Reject Reason</h4>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox ID="txtReason" runat="server" Height="100%" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnReject" runat="server" Text="Confirm Reject" CssClass="btn btn-danger" OnClick="btnReject_Click" OnClientClick="return confirm('Are you sure to reject this requistion ? ');"/></div>
                    </div>
                    </div>
                 </div>

        </div>
</asp:Content>
