using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Task.Models;

namespace Task
{
    public partial class _Default : Page
    {
        private enum enuSortOrder : int
        {
            soAscending = 1,
            soDescending = -1
        }
        // strings to use for the sort expressions and column title
        // separate arrays are used to support the sort expression and titles
        // being different
        static readonly String[] sortExpressions =
                                    new String[] { "Name", "CompanyName", "Size", "ReleaseData", "URL", "Vendor"};
        static readonly String[] columnTitle =
                                    new String[] { "Name", "CompanyName", "Size", "ReleaseData", "URL", "Vendor" };
        // the names of the variables placed in the viewstate
        static readonly String VS_CURRENT_SORT_EXPRESSION = "currentSortExpression";
        static readonly String VS_CURRENT_SORT_ORDER = "currentSortOrder";

        protected void Page_Load(object sender, EventArgs e)
        {
            enuSortOrder defaultSortOrder;
            string defaultSortExpression;
            if (!Page.IsPostBack)
            {
                // sort by title, ascending as the default
                defaultSortExpression = sortExpressions[0];
                defaultSortOrder = enuSortOrder.soAscending;

                // bind data to the DataGrid
                this.ViewState.Add(VS_CURRENT_SORT_EXPRESSION, defaultSortExpression);
                this.ViewState.Add(VS_CURRENT_SORT_ORDER, defaultSortOrder);
                BindData(defaultSortExpression, defaultSortOrder);
            }
        }
        private void BindData(String sortExpression,
                          enuSortOrder sortOrder)
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
            int nocorrectdatatobottom = 1;
            #region sortdata
            switch (sortExpression)
            {
                case "Name":
                    data.Sort((a, b) => (int)sortOrder * a.Name.CompareTo(b.Name));
                    break;
                case "CompanyName":
                    data.Sort((a, b) => a.CompanyName == null ? nocorrectdatatobottom :
                    b.CompanyName == null ? -nocorrectdatatobottom :
                    (int)sortOrder * a.CompanyName.CompareTo(b.CompanyName));
                    break;
                case "Version":
                    data.Sort((a, b) => a.Version == null ? nocorrectdatatobottom :
                    b.Version == null ? -nocorrectdatatobottom :
                    (int)sortOrder * a.Version.CompareTo(b.Version));
                    break;
                case "Size":
                    data.Sort((a, b) => (int)sortOrder * a.Size.CompareTo(b.Size));
                    break;
                case "ReleaseData":
                    data.Sort((a, b) => a.ReleaseData == null ? nocorrectdatatobottom :
                    b.ReleaseData == null ? -nocorrectdatatobottom :
                    (int)sortOrder * a.ReleaseData.CompareTo(b.ReleaseData));
                    break;
                case "URL":
                    data.Sort((a, b) => a.URL == null ? nocorrectdatatobottom :
                    b.URL == null ? -nocorrectdatatobottom :
                    (int)sortOrder * a.URL.CompareTo(b.URL));
                    break;
                case "Vendor":
                    data.Sort((a, b) => a.Vendor == null ? nocorrectdatatobottom :
                    b.Vendor == null ? -nocorrectdatatobottom :
                    (int)sortOrder * a.Vendor.CompareTo(b.Vendor));
                    break;
            }
            #endregion
            datagrid.DataSource = data;
            datagrid.DataBind();
        }

        protected void datagrid_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            String newSortExpression = null;
            String currentSortExpression = null;
            enuSortOrder currentSortOrder;

            // get the current sort expression and order from the viewstate
            currentSortExpression =
                              (String)(this.ViewState[VS_CURRENT_SORT_EXPRESSION]);
            currentSortOrder =
                              (enuSortOrder)(this.ViewState[VS_CURRENT_SORT_ORDER]);

            // check to see if this is a new column or the sort order
            // of the current column needs to be changed.
            newSortExpression = e.SortExpression;
            if (newSortExpression == currentSortExpression)
            {

                // sort column is the same so change the sort order
                if (currentSortOrder == enuSortOrder.soAscending)
                {
                    currentSortOrder = enuSortOrder.soDescending;
                }
                else
                {
                    currentSortOrder = enuSortOrder.soAscending;
                }
            }
            else
            {
                // sort column is different so set the new column with ascending
                // sort order
                currentSortExpression = newSortExpression;
                currentSortOrder = enuSortOrder.soAscending;
            }

            // update the view state with the new sort information
            this.ViewState.Add(VS_CURRENT_SORT_EXPRESSION, currentSortExpression);
            this.ViewState.Add(VS_CURRENT_SORT_ORDER, currentSortOrder);

            // rebind the data in the datagrid
            BindData(currentSortExpression,
                     currentSortOrder);
        }
    }
}