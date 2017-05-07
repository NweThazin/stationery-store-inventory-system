<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.Master" AutoEventWireup="true" CodeBehind="ApproveRequisition.aspx.cs" Inherits="ADProjectSA43_Team1.Employee.ApproveRequisition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid pg-content" >
    <div class="row">
        <div class="col-md-6">
            <h2><i class="fa fa-file"></i> Requisitions</h2>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 maintable">
            <div class="table-responsive">
                   <asp:GridView ID="approveRequistionGV" runat="server" CssClass="table" AutoGenerateColumns="False" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" AllowPaging="True">
                    <Columns>
                        <asp:BoundField HeaderText="Employee Name" DataField="FullName"/>
                        <asp:BoundField HeaderText="Date" DataField="OrderDate" DataFormatString="{0:dd/MM/yyyy}"/>
                        <asp:BoundField HeaderText="Total Item" DataField="TotalItem"/>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnDetail" runat="server" OnClick="lnkbtnDetail_Click">Detail</asp:LinkButton>
                                <asp:HiddenField ID="hdnRequisitionID" runat="server" Value='<%#Eval("RequisitionID") %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                   </asp:GridView>
             </div>
        </div>
    </div>
</div>
</asp:Content>
