using OnlineStore.Managers;
using OnlineStoreModels;
using OnlineStoreServices.Services;
using System;
using System.IO;
using System.Web.UI.WebControls;

namespace OnlineStore.ProductController
{
    public partial class Browse : System.Web.UI.Page
    {
        Services services = new Services();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Make repeater items have the 'Product' data type
                rptProducts.DataSource = services.ProductService.SearchProducts(Request.QueryString["searchString"]);
                rptProducts.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex, Path.GetFileName(Request.PhysicalPath));
                Server.Transfer(Constants.PAGE_GENERIC_ERROR);
            }
        }

        protected void Product_Click(object sender, EventArgs e)
        {
            PageManager.RedirectToProductDetailsPage(((LinkButton)sender).Attributes["ProductID"]);
        }

        protected void rptProducts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var row = e.Item.FindControl("productRow") as LinkButton;
            if (row != null)
            {
                //Repeater rows will be of type 'Product' since repeater was databinded to query returing 'Product' objects
                var drv = (Product)e.Item.DataItem;
                row.Attributes["ProductID"] = drv.ProductID.ToString();
            }
        }
    }
}