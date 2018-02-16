using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Task.Models;

namespace Task
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData("ProductName");
        }
        private void BindData(string orderBy)
        {
            var context = new ProductContext();
            var data = (from p in context.Products
                        from d in context.Drivers.Where(x => x.ProductName == p.ProductName).DefaultIfEmpty()
                        from r in context.ReleaseDates.Where(x => x.ProductId == p.ProductId).DefaultIfEmpty()
                        select new
                        {
                            Name = p.ProductName,
                            CompanyName = d != null ? d.CompanyName : default(string),
                            Version = d != null ? d.Version : default(string),
                            Size = d != null ? d.Size : default(long),
                            ReleaseData = r != null ? r.RealeseOn : default(DateTime),
                            URL = p.Url,
                            Vendor = p.VendorContact
                        }).ToList();
            datagrid.DataSource = data;
            datagrid.DataBind();
        }
    }
}