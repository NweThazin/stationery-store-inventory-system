<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.Master" AutoEventWireup="true" CodeBehind="ChangeRepresentative.aspx.cs" Inherits="ADProjectSA43_Team1.Employee.ChangeRepresentative" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid pg-content">
    <div class="row">
        <div class="col-md-6">
            <h2><i class="fa fa-users"></i> Change Representative</h2>
        </div>
    </div>
    
    <div class="row">
        <div class="col-md-12" style="margin-top:10px;">
            <b> Current Representatvie of <asp:Label ID="lblDepartmentName" runat="server" Text=""></asp:Label>:</b>&nbsp;&nbsp;
            <asp:Label ID="lblCurrentRepresentative" runat="server" Text=""></asp:Label>
        </div>
    </div><br/>

    <div class="row">
        <div class="col-md-12 maintable">
            <div class="table-responsive">
                <asp:GridView ID="changeRepGV" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" CssClass="table" AllowPaging="True" OnPageIndexChanging="changeRepGV_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:RadioButton ID="radbtnChoose" runat="server" OnCheckedChanged="radbtnChoose_CheckedChanged" GroupName="rep" AutoPostBack="true"/>
                                <asp:HiddenField ID="hdnChoose" runat="server" Value='<%#Eval("EmployeeID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Employee Name" DataField="FullName"/>
                        <asp:BoundField HeaderText="Email" DataField="Email"/>
                        <asp:BoundField HeaderText="Phone Number" DataField="Phone"/>
                    </Columns>
                </asp:GridView>
            </div>
                <asp:Label ID="LblValidate" runat="server"></asp:Label>
            <br/>
                <div class="form-group row">
                    <div class="col-md-4"></div>
                    <div class="col-md-4">
                        <asp:Button ID="btnChange" runat="server" Text="Change Representative" CssClass="btn btn-md btn-primary" OnClick="btnChange_Click" OnClientClick="return confirm('Are you sure to assign this employee as representative ? ');"/>
                    </div>
                    <div class="col-md-4"></div>                     
                </div>
        </div>
    </div>

</div>
</asp:Content>
