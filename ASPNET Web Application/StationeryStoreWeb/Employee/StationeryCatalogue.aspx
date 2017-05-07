<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.Master" AutoEventWireup="true" CodeBehind="StationeryCatalogue.aspx.cs" Inherits="ADProjectSA43_Team1.Employee.StationeryCatalogue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pg-content">
    <div class="row">
        <div class="col-md-6">
            <h2><i class="fa fa-cubes"></i>Stationery Catalogue</h2>
        </div>
    </div>
     <div class="row">
        <div class="col-xs-6 col-xs-offset-2" style="margin-top:15px;">
            <div class="input-group">
                    <div class="input-group-btn search-panel">
                        <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" runat="server">
                            <span class="caret"></span>
                        </button>
                        <ul class="dropdown-menu" role="menu" runat="server">
                           <%-- <li><asp:LinkButton ID="lnkbtnItemNumber" runat="server" OnClick="lnkbtnItemNumber_Click">Item Number</asp:LinkButton></li>--%>
                            <li><asp:LinkButton ID="lnkbtnDescription" runat="server" OnClick="lnkbtnDescription_Click">Description</asp:LinkButton></li>
                        </ul>
                    </div>
                        <asp:TextBox ID="txtEnterSearch" runat="server" OnClick="lnkbtnItemNumber_Click" CssClass="form-control" placeholder="Choose Description to search.."></asp:TextBox>
                    <span class="input-group-btn">
                        <asp:LinkButton ID="lnkbtnSearch" runat="server" OnClick="lnkbtnSearch_Click" CssClass="btn btn-default" >
                            <span aria-hidden="true" class="glyphicon glyphicon-search"></span>
                        </asp:LinkButton>
                    </span>
                </div>
            </div>
	    </div>
            <br/>
        <div class="row">
            <div class="col-md-12 maintable">
                <div class="table-responsive">
                     <asp:GridView ID="catalogueGV" runat="server" CssClass="table" AutoGenerateColumns="False" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" AllowPaging="True" OnPageIndexChanging="catalogueGV_PageIndexChanging" PageSize="6" >
                        <Columns>
                            <%--<asp:BoundField HeaderText="Item Number" DataField="ItemNumber"/>--%>
                            <asp:BoundField HeaderText="Description" DataField="Description"/>
                            <asp:TemplateField  HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:TextBox ID="qty" runat="server" onkeypress='return event.charCode >= 48 && event.charCode <= 57' ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Unit of Measure" DataField="UnitOfMeasure"/>
                            <asp:TemplateField  HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonOrder" runat="server" OnClick="LinkButtonOrder_Click">Add</asp:LinkButton>
                                    <asp:HiddenField ID="HiddenField1" value='<%#Eval("itemId") %>'  runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns> 
                    </asp:GridView>
                </div>
            </div>
        </div>
    <div class="row">
            <div class="table-responsive">
                <asp:GridView ID="cartView" runat="server" CssClass="table" AutoGenerateColumns="False" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px"  OnRowDeleting="cartView_RowDeleting"  AllowPaging="True" PageSize="6" OnPageIndexChanging="cartView_PageIndexChanging" >
                    <Columns>
                        <asp:BoundField HeaderText="Item Description" DataField="Description"/>
                        <asp:BoundField HeaderText="Quantity" DataField="RequestedQty"/>
                        <asp:TemplateField HeaderText="Remove">
                            <ItemTemplate>
                                <asp:LinkButton ID="Delete" runat="server" OnClick="Delete_Click" OnClientClick="return confirm('Are you sure to delete this item?')">Delete</asp:LinkButton>
                                <asp:HiddenField ID="HiddenField2" value='<%#Eval("itemId") %>'  runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>    
            </div>
    </div>
        <div class="row">
        <div class="col-md-12">
            <div class="col-md-4"></div>
            <div class="col-md-4">  <%--Ask Client ALert BoX--%>
                <asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" CssClass="btn btn-warning" OnClientClick="return confirm('Are you sure to submit all requistion?')"/>&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClientClick="return confirm('Are you sure to cancel all requistion?')" OnClick="btnCancel_Click"/>
            </div>
            <div class="col-md-4">
            </div>
        </div>
    </div>
</div>
</asp:Content>
