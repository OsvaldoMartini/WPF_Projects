﻿using System;
using System.Collections.Generic;
using System.IO;
using Products_SQL_Linq.Controls;
using Products_SQL_Linq.Util;
using Products_SQL_Linq.ViewModels;

namespace Products_SQL_Linq.Model
{
    public class ProductModel
    {
        public Guid Guid { get; set; }
        public int ProductId { get; set; }
        public string ModelNumber { get; set; }
        public string ModelName { get; set; }
        public decimal UnitCost { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }

        #region Builder Fields
        private bool checkFileFirst = true;

        private ProductObservableCollection<ProductVM> _Product;
        public ProductModel WhitCheckFileFirst(bool checkFileFirst)
        {
            this.checkFileFirst = checkFileFirst;
            return this;
        }

        public ProductModel WhitProductId(int p)
        {
            this.ProductId = p;
            return this;
        }

        public static implicit operator ProductObservableCollection<ProductVM>(ProductModel instance)
        {
            return instance.Build();
        }

        #endregion

        public ProductModel()
        {
        }
        
        public bool DeleteByXML()
        {
            var filePath = String.Format("{0}{1}\\ProductModel.xml", AppDomain.CurrentDomain.BaseDirectory,
                DBUtility.FilePath);

            return DBUtility.DeleteByXML(this.ProductId, filePath);
        }

        public bool AddByXML()
        {
            var filePath = String.Format("{0}{1}\\ProductModel.xml", AppDomain.CurrentDomain.BaseDirectory,
                DBUtility.FilePath);

            return DBUtility.AddByXML(this, filePath);

        }

        public bool UpdateByXML()
        {
            var filePath = String.Format("{0}{1}\\ProductModel.xml", AppDomain.CurrentDomain.BaseDirectory,
                DBUtility.FilePath);

            return DBUtility.UpdateByXML(this, filePath);

        }

        public ProductObservableCollection<ProductVM> Build()
        {
            ProductObservableCollection<ProductVM> products = new ProductObservableCollection<ProductVM>();

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
            Guid = id;
            ProductId = productId;
            ModelNumber = modelNumber;
            ModelName = modelName;
            UnitCost = unitCost;
            Description = description;
            CategoryName = categoryName;
        }

        public ProductModel(ProductVM p)
        {
            Guid = p._Guid;
            ProductId = p._ProductId;
            ModelNumber = p.ModelNumber;
            ModelName = p.ModelName;
            UnitCost = Convert.ToDecimal(p.UnitCost);
            Description = p.Description;
            CategoryName = p.CategoryName;
        }

        public ProductVM ProductModel2Product()
        {
            string unitCost = UnitCost.ToString();
            return new ProductVM(Guid,ProductId, ModelNumber, ModelName, unitCost, Description, CategoryName);
        } //ProductModel2Product()



    }
}
