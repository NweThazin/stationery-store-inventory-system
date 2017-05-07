<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.Master" AutoEventWireup="true" CodeBehind="CheckCollectionPoint.aspx.cs" Inherits="ADProjectSA43_Team1.Employee.CheckCollectionPoint" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container-fluid pg-content">
    <div class="row">
        <div class="col-md-6">
            <h2><i class="fa fa-map-marker"></i> Collection Points</h2>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <b>Current Collection Point : </b><asp:Label ID="collectionpoint" runat="server" Text=""></asp:Label>
        </div>
    </div>
     <div class="row">
        <div class="col-md-12 maintable">
            <div class="table-responsive">
                <asp:GridView ID="collectionlistGV"   runat="server" CssClass="table" AutoGenerateColumns="False" GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" AllowPaging="True">
                    <Columns>
                        <asp:BoundField HeaderText="CollectionPoint" DataField="Name"/>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButtonOrder" runat="server"  OnClick="LinkButtonOrder_Click">Change </asp:LinkButton>
                                <asp:HiddenField ID="HiddenField1" Value='<%#Eval("CollectionPointID") %>'  runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns> 
                </asp:GridView>
            </div>
        </div>
    </div>
</div>
</asp:Content>
