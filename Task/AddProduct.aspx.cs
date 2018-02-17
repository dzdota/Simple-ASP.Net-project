using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Task.Models;

namespace Task
{
    public partial class AddProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                using (var context = new ProductContext())
                {
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

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            using (var context = new ProductContext())
            {
                context.Drivers.Add(new Driver()
                {
                    ProductName = TextBoxName.Text,
                    ProductCategory = string.Join(", ", GetCategories().ToArray()),
                    CompanyName = TextBoxCompany.Text
                });

                context.Products.Add(new Product() {
                    ProductName = TextBoxName.Text,
                    ProductId = TextBoxId.Text,
                    Url = TextBoxUrl.Text,
                    VendorContact = TextBoxVendor.Text
                });

                string[] dataelement = TextBoxRelease.Text.Split(' ', '.', '-', ':');
                DateTime ReleaseOn = new DateTime(int.Parse(dataelement[0]), int.Parse(dataelement[1]), int.Parse(dataelement[2]),
                                int.Parse(dataelement[3]), int.Parse(dataelement[4]), int.Parse(dataelement[5]), int.Parse(dataelement[6]));

                context.ReleaseDates.Add(new ProductReleaseDates() {
                    ProductId = TextBoxId.Text,
                    RealeseOn = ReleaseOn
                });
                context.SaveChanges();
            }
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