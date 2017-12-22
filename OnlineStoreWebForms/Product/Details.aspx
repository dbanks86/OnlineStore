<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="OnlineStore.ProductController.Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--NOTE: Tilde (~) sign does NOT work at beginning of href path of link tag of css file. Tilde sign must NOT be there.--%>
    <link href="/Content/Details.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="detailsPageContainer">
         <div id="rightColumn">
            <div id="addToCartSectionContainer">
                <div id="addToCartSection">
                    <div id="quantity">
                        <asp:HiddenField ID="productIdHiddenField" runat="server" />
                        <span>Qty:</span>
                        <asp:DropDownList ID="ddlQuantity" runat="server" CssClass="quantityDropDown"></asp:DropDownList>
                    </div>
                    <div id="addToCartButtonContainer">
                        <div id="addToCartButton">
                            <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" OnClick="btnAddToCart_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="leftColumn">
        </div>
        <div id="centerColumn">
            <div id="titleDiv">
                <h1 id="productTitleHeader">
                    <span id="productTitleSpan" runat="server"></span>
                </h1>
            </div>
            <div id="priceDiv">
                <span>Price:</span>
                <asp:Label ID="priceLabel" runat="server" CssClass="priceLabel"></asp:Label>
            </div>
            <div id="stockCount">
                <asp:Label ID="stockLabel" runat="server"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
