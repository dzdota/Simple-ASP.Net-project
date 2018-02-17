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
    public class ProductwithCategory
    {
        public string Name;
        public string CompanyName;
        public string Version;
        public long Size;
        public DateTime ReleaseData;
        public string URL;
        public string Vendor;
        public string Categories;
    }

    public partial class _Default : Page
    {
        private enum enumSortOrder : int
        {
            soAscending = 1,
            soDescending = -1
        }
        // strings to use for the sort expressions and column title
        // separate arrays are used to support the sort expression and titles
        // being different
        static readonly string[] sortExpressions =
                                    new String[] { "Name", "CompanyName", "Size", "ReleaseData", "URL", "Vendor"};
        static readonly string[] columnTitle =
                                    new String[] { "Name", "CompanyName", "Size", "ReleaseData", "URL", "Vendor" };
        // the names of the variables placed in the viewstate
        static readonly string VS_CURRENT_SORT_EXPRESSION = "currentSortExpression";
        static readonly string VS_CURRENT_SORT_ORDER = "currentSortOrder";

        static readonly string ALL = "All";

        protected void Page_Load(object sender, EventArgs e)
        {
            enumSortOrder defaultSortOrder;
            string defaultSortExpression;
            if (!Page.IsPostBack)
            {
                // sort by title, ascending as the default
                defaultSortExpression = sortExpressions[0];
                defaultSortOrder = enumSortOrder.soAscending;

                // bind data to the DataGrid
                this.ViewState.Add(VS_CURRENT_SORT_EXPRESSION, defaultSortExpression);
                this.ViewState.Add(VS_CURRENT_SORT_ORDER, defaultSortOrder);
                BindData(defaultSortExpression, defaultSortOrder, ALL);

                using (var context = new ProductContext())
                {
                    var CompanyNames = (from d in context.Drivers
                                        select d.CompanyName).Distinct().ToList();
                    CompanyNames.Sort();
                    CompanyNames.Insert(0, ALL);
                    foreach (var name in CompanyNames)
                    {
                        CompanyNameList.Items.Add(name);
                    }
                    var MergeCategories = (from p in context.Drivers
                                      select p.ProductCategory).ToList();
                    List<string> Categories = new List<string>();
                    foreach (string mc in MergeCategories)
                    {
                        string[] SplitCategories = mc.Split(new string[] { ", " }, StringSplitOptions.None);
                        foreach (string sc in SplitCategories)
                            Categories.Add(sc);
                    }
                    Categories = Categories.Distinct().ToList();
                    Categories.Sort();
                    foreach (string c in Categories)
                    {
                        CategoryList.Items.Add(c);
                    }
                }
            }
        }
        private void BindData(String sortExpression,
                          enumSortOrder sortOrder, string Company)
        {
            var context = new ProductContext();


            var data = (from p in context.Products
                        from d in context.Drivers.Where(x => x.ProductName == p.ProductName).DefaultIfEmpty()
                        from r in context.ReleaseDates.Where(x => x.ProductId == p.ProductId).DefaultIfEmpty()
                        select new ProductwithCategory
                        {
                            Name = p.ProductName,
                            CompanyName = d != null ? d.CompanyName : default(string),
                            Version = d != null ? d.Version : default(string),
                            Size = d != null ? d.Size : default(long),
                            ReleaseData = r != null ? r.RealeseOn : default(DateTime),
                            URL = p.Url,
                            Vendor = p.VendorContact,
                            Categories = d != null ? d.ProductCategory : default(string)
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

            #region FiterByCategory

            string[] Categories = GetCategories();

            List<ProductwithCategory> FilterData = new List<ProductwithCategory>();
            if (Categories.Length == 0)
                FilterData = new List<ProductwithCategory>(data);
            else
            {
                foreach (ProductwithCategory element in data)
                {
                    foreach (string category in Categories)
                    {
                        if (element.Categories != null && element.Categories.IndexOf(category) != -1)
                        {
                            FilterData.Add(element);
                            break;
                        }
                    }
                }
            }

            #endregion
            if (Company != ALL)
                FilterData = (from d in FilterData
                              where d.CompanyName == Company
                             select d).ToList();

            datagrid.DataSource = (from d in FilterData
                                   select new
                                   {
                                       Name = d.Name,
                                       CompanyName = d.CompanyName,
                                       Version = d.Version,
                                       Size = d.Size,
                                       ReleaseData = d.ReleaseData,
                                       URL = d.URL,
                                       Vendor = d.Vendor,
                                   }).ToList();
            datagrid.DataBind();
        }

        protected void datagrid_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            String newSortExpression = null;
            String currentSortExpression = null;
            enumSortOrder currentSortOrder;

            // get the current sort expression and order from the viewstate
            currentSortExpression =
                              (String)(this.ViewState[VS_CURRENT_SORT_EXPRESSION]);
            currentSortOrder =
                              (enumSortOrder)(this.ViewState[VS_CURRENT_SORT_ORDER]);

            // check to see if this is a new column or the sort order
            // of the current column needs to be changed.
            newSortExpression = e.SortExpression;
            if (newSortExpression == currentSortExpression)
            {

                // sort column is the same so change the sort order
                if (currentSortOrder == enumSortOrder.soAscending)
                {
                    currentSortOrder = enumSortOrder.soDescending;
                }
                else
                {
                    currentSortOrder = enumSortOrder.soAscending;
                }
            }
            else
            {
                // sort column is different so set the new column with ascending
                // sort order
                currentSortExpression = newSortExpression;
                currentSortOrder = enumSortOrder.soAscending;
            }

            // update the view state with the new sort information
            this.ViewState.Add(VS_CURRENT_SORT_EXPRESSION, currentSortExpression);
            this.ViewState.Add(VS_CURRENT_SORT_ORDER, currentSortOrder);

            // rebind the data in the datagrid
            BindData(currentSortExpression,
                     currentSortOrder, CompanyNameList.SelectedValue);
        }

        protected void CompanyNameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData(this.ViewState[VS_CURRENT_SORT_EXPRESSION] as string,
                     (enumSortOrder)this.ViewState[VS_CURRENT_SORT_ORDER], CompanyNameList.SelectedValue);
        }

        protected void CategoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData(this.ViewState[VS_CURRENT_SORT_EXPRESSION] as string,
                     (enumSortOrder)this.ViewState[VS_CURRENT_SORT_ORDER], CompanyNameList.SelectedValue);
        }

        private string[] GetCategories()
        {
            List<string> Categories = new List<string>();
            foreach (ListItem item in CategoryList.Items)
            {
                if (item.Selected)
                {
                    Categories.Add(item.Text);
                }
            }
            return Categories.ToArray();
        }
    }
}