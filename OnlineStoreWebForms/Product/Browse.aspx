<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Browse.aspx.cs" Inherits="OnlineStore.ProductController.Browse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--NOTE: Tilde (~) sign does NOT work at beginning of href path of link tag of css file. Tilde sign must NOT be there.--%>
    <link href="/Content/Browse.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="rptProducts" runat="server" OnItemDataBound="rptProducts_ItemDataBound">
        <HeaderTemplate>
            <table class="productsTable">
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <div class="productContainer">
                        <div class="productContentContainer">
                            <div class="productContent">
                                <div class="productContentHeadingContainer">
                                    <h4 class="productContentHeading">
                                        <asp:LinkButton ID="productRow" runat="server" Text='<%# Eval("Name") %>' OnClick="Product_Click"></asp:LinkButton>
                                    </h4>
                                </div>
                                <div class="productContentPrimaryInfo">
                                    <div class="productContentRow">
                                        <div class="productPriceContainer">
                                            <span class="dollarSign">$</span><%# Eval("Price") %>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
