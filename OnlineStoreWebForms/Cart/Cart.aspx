<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="OnlineStore.Account.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--NOTE: Tilde (~) sign does NOT work at beginning of href path of link tag of css file. Tilde sign must NOT be there.--%>
    <link href="/Content/ShoppingCart.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            // Document.ready -> link up remove event handler
            $(".deleteCartItemButton").click(function () {
                // Get the id from the link
                var recordToDelete = $(this).attr("cartItemId");
                if (recordToDelete != '') {
                    // Perform the ajax post
                    $.ajax(
                    {
                        type: "POST",
                        url: "Cart.aspx/RemoveCartItem",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: '{cartItemId: "' + recordToDelete + '" }',
                        success: function (response) {
                            // Successful requests get here

                            // Update the page elements
                            $('.cartItemID' + response.d.CartItemID + ' .cartItemContent').fadeOut(500, function () {
                                $('.cartItemID' + response.d.CartItemID + ' .cartItemRemovedMessage').show();

                                //update checkout information
                                $('#<%= subTotalTd.ClientID %>').text(response.d.SubTotal);
                                $('#<%= lblCartItemCount.ClientID %>').text(response.d.SubTotalLabel);
                                $('#<%= this.Master.FindControl("cartLinkButton").ClientID%>').text(response.d.CartLabel)
                            });
                        },
                        error: function (response) {
                            PageMethods.LogAjaxException(response);
                            console.error(response.status + " " + response.statusText + "</br>" + response.responseText);
                        }
                    });
                }

                //prevent submit of form
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container">
        <div id="leftColumn">
            <asp:Repeater ID="rptCartItems" runat="server" OnItemDataBound="rptCartItems_ItemDataBound">
                <HeaderTemplate>
                    <div id="cartItemsContainer">
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HiddenField ID="productIdHiddenField" runat="server" Value='<%# Eval("CartItemID") %>' />
                    <div class="cartItemRowContainer <%#string.Format("cartItemID{0}", Eval("CartItemID")) %>">
                        <div class="cartItemContentContainer">
                            <div class="cartItemUpdateMessage">
                                <div class="cartItemRemovedMessage invisible">
                                    <asp:LinkButton ID="cartItemRemovedLinkButton" runat="server" Text='<%# Eval("ProductName") %>' OnClick="cartItemRemovedLinkButton_Click"></asp:LinkButton>
                                    removed from shopping cart
                                </div>
                            </div>
                            <div class="cartItemContent">
                                <div class="cartItemImage">
                                    <asp:ImageButton ID="ProductImageButton" runat="server"
                                        AlternateText="ImageButton 1"
                                        ImageAlign="left"
                                        ImageUrl="~/Content/Images/5110DJ6RuKL._SS100_.jpg"
                                        OnClick="ProductImageButton_Click" />
                                </div>
                                <div class="contentNoImageContainer">
                                    <div class="cartItemInfo">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="cartItemProductNameLinkButton" runat="server" OnClick="cartItemProductNameLinkButton_Click">
                                                    <span><%# Eval("ProductName") %></span>
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span id="stockSpan" class="stockSpan" runat="server"></span>
                                                    <asp:Label ID="stockLabel" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <table class="cartItemActionLinksTable">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="deleteCartItemButton" runat="server" Text="Delete" CssClass="deleteCartItemButton" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="saveForLaterCartItemButton" runat="server" Text="Save For Later" CssClass="saveForLaterCartItemButton" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="cartItemProductPrice">
                                        <span><%# string.Format("{0:C}", Eval("Price")) %></span>
                                    </div>
                                    <div class="cartItemQuantity">
                                        <asp:DropDownList ID="quantityDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="quantityDropDownList_SelectedIndexChanged" CssClass="quantityDropDown"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>

        </div>
        <div id="rightColumn">
            <table id="checkout-info">
                <tr>
                    <td class="checkout-field-label">Subtotal
                        <asp:Label ID="lblCartItemCount" runat="server"></asp:Label>
                    </td>
                    <td id="subTotalTd" runat="server" class="checkout-field-value"></td>
                </tr>
                <tr>
                    <td class="checkout-button-td" colspan="2">
                        <asp:Button ID="btnCheckout" runat="server" Text="Checkout" OnClick="btnCheckout_Click" CssClass="checkoutButton" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
