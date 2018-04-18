using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;
using ProductMvvm.Controls;
using ProductMvvm.Util;
using ProductMvvm.ViewModels;

namespace ProductMvvm.Model
{
    public class ProductModel
    {
        public Guid ID { get; set; }
        public int ProductId { get; set; }
        public string ModelNumber { get; set; }
        public string ModelName { get; set; }
        public decimal UnitCost { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }

        #region Builder Fields
        private bool checkFileFirst = true;

        private ProductObservableCollection<Product> _Product;
        public ProductModel WhiteCheckFileFirst(bool checkFileFirst)
        {
            this.checkFileFirst = checkFileFirst;
            return this;
        }
        public static implicit operator ProductObservableCollection<Product>(ProductModel instance)
        {
            return instance.Build();
        }

        #endregion

        public ProductModel()
        {
        }

        public bool UpdateByXML()
        {
            var filePath = String.Format("{0}{1}\\ProductModel.xml", AppDomain.CurrentDomain.BaseDirectory,
                DBUtility.FilePath);

            return DBUtility.UpdateByXML(this, filePath);

        }

        public ProductObservableCollection<Product> Build()
        {
            ProductObservableCollection<Product> products = new ProductObservableCollection<Product>();

            var filePath = String.Format("{0}{1}\\ProductModel.xml", AppDomain.CurrentDomain.BaseDirectory,
                DBUtility.FilePath);

            List<ProductModel> list = null;
            if (this.checkFileFirst)
                if (!File.Exists(filePath))
                {
                    list = DBUtility.CreateFile<ProductModel>(DBUtility.MockProductModel());
                }
                else

                    list = DBUtility.DeserializeParamsListOf<ProductModel>(filePath);

            if (list != null)
                foreach (ProductModel sp in list)
                    products.Add(sp.ProductModel2Product());


            return products;
        }

        public ProductModel(Guid id,int productId, string modelNumber, string modelName,
            decimal unitCost, string description, string categoryName)
        {
            ID = id;
            ProductId = productId;
            ModelNumber = modelNumber;
            ModelName = modelName;
            UnitCost = unitCost;
            Description = description;
            CategoryName = categoryName;
        }

        public ProductModel(Product p)
        {
            ID = p._ID;
            ProductId = p._ProductId;
            ModelNumber = p.ModelNumber;
            ModelName = p.ModelName;
            UnitCost = Convert.ToDecimal(p.UnitCost);
            Description = p.Description;
            CategoryName = p.CategoryName;
        }

        public Product ProductModel2Product()
        {
            string unitCost = UnitCost.ToString();
            return new Product(ID,ProductId, ModelNumber, ModelName, unitCost, Description, CategoryName);
        } //ProductModel2Product()



    }
}
