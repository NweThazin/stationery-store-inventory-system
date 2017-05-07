<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RetrievalPage.aspx.cs" Inherits="ADProjectSA43_Team1.StoreClerk.RetrievalPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid pg-content">
        <div class="row">
            <div class="col-md-12">
                <h2><i class="fa fa-file"></i> Orders List</h2>
            </div>
         </div>
        <div class="row">
            <div class="col-xs-4 col-xs-offset-2" style="margin-top:15px;">
                <asp:RadioButtonList ID="radbtnReterival" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
                    <asp:ListItem Selected="True">Unfullfill Orders</asp:ListItem>
                    <asp:ListItem>New Orders</asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:Label ID="lblListName" runat="server" Text="Label"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 maintable">
                 <div class="table-responsive">
                     <asp:GridView ID="GVRetrieval" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="GVRetrieval_SelectedIndexChanging"  GridLines="Horizontal" Height="100%" Width="100%" CellPadding="3" BorderWidth="0px" CssClass="table">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="Ckb" runat="server" />
                                    <%--<asp:HiddenField ID="hdfd" runat="server" Value='<%# Eval("ItemNumber") %>' />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Bin #" DataField="Bin"/>
                            <asp:BoundField HeaderText="Item No." DataField="ItemNumber"/>
                            <asp:BoundField HeaderText="Description" DataField="Description"/>
                            <asp:BoundField HeaderText="UOM" DataField="UOM"/>
                            <asp:BoundField HeaderText="In Stock Quantity" DataField="InStock"/>
                            <asp:BoundField HeaderText="Requested Quantity" DataField="Requested"/>
                            <asp:BoundField DataField="Unfulfilled" HeaderText="Unfulfilled Quantity" />
                            <asp:BoundField DataField="ToRetrieve" HeaderText="ToRetrieve Quantity"/>
                            <asp:CommandField SelectText="Allocate" ShowSelectButton="true" ButtonType="Link"/>
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="LblValidate" runat="server"></asp:Label>
                 </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="btn btn-md btn-primary" OnClick="btnConfirm_Click"/>
            </div>
            <div class="col-md-4"></div>
        </div>
    </div>
</asp:Content>
