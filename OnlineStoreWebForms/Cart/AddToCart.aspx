<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="AddToCart.aspx.cs" Inherits="OnlineStore.ShoppingCart.AddToCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--NOTE: Tilde (~) sign does NOT work at beginning of href path of link tag of css file. Tilde sign must NOT be there.--%>
    <link href="/Content/AddToCart.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="outerContainer">
        <div>
            <table id="addToCartTable">
                <tr>
                    <td class="addToCartStatusTd">
                        <a target="_blank" href="paris.jpg">
                            <img src="paris.jpg" alt="Paris"/>
                        </a>
                    </td>
                    <td class="cartSubTotalTd">
                        <span class="subTotalTitle">Cart Subtotal</span>
                        <asp:Label ID="cartItemsCountLabel" runat="server" CssClass="cartItemsCountLabel"></asp:Label>
                        <asp:Label ID="subTotalLabel" runat="server" CssClass="subTotalLabel"></asp:Label>
                    </td>
                    <td class="cartButtonTd">
                        <asp:Button ID="cartButton" runat="server" Text="Cart" OnClick="cartButton_Click" CssClass="cartButton" />
                    </td>
                    <td class="checkoutButtonTd">
                        <asp:Button ID="checkoutButton" runat="server" Text="Checkout" OnClick="checkoutButton_Click" CssClass="checkoutButton" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
