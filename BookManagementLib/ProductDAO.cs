﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManagementLib.DataAccess;

namespace BookManagementLib
{
    class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Product> GetProductList()
        {
            var Products = new List<Product>();
            try
            {
                using var context = new BookManagementDBContext();
                Products = context.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Products;
        }

        public Product GetProductByID(string ProductID)
        {
            Product Product = null;
            try
            {
                using var context = new BookManagementDBContext();
                Product = context.Products.SingleOrDefault(m => m.ProductId == ProductID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Product;
        }

        public Product GetProductByName(string ProductName)
        {
            Product Product = null;
            try
            {
                using var context = new BookManagementDBContext();
                Product = context.Products.SingleOrDefault(m => m.ProductName == ProductName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Product;
        }

        //public Product GetProductByUnitPrice(int UnitPrice)
        //{
        //    Product Product = null;
        //    try
        //    {
        //        using var context = new BookManagementDBContext();
        //        Product = context.Products.SingleOrDefault(m => m.UnitPrice == UnitPrice);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return Product;
        //}

        //public Product GetProductByUnitInStock(int UnitsInStock)
        //{
        //    Product Product = null;
        //    try
        //    {
        //        using var context = new BookManagementDBContext();
        //        Product = context.Products.SingleOrDefault(m => m.UnitsInStock == UnitsInStock);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return Product;
        //}

        public void Insert(Product Product)
        {
            try
            {
                Product product = GetProductByID(Product.ProductId);
                if (product == null)
                {
                    using var context = new BookManagementDBContext();
                    context.Products.Add(Product);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Product is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Product Product)
        {
            try
            {
                Product product = GetProductByID(Product.ProductId);
                if (product != null)
                {
                    using var context = new BookManagementDBContext();
                    context.Products.Update(Product);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Product does not exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(string ProductID)
        {
            try
            {
                Product product = GetProductByID(ProductID);
                if (product != null)
                {
                    using var context = new BookManagementDBContext();
                    context.Products.Remove(product);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The Product does not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string IdGenerate()
        {
            string newid = null;
            try
            {
                using var context = new BookManagementDBContext();
                Product product = context.Products.ToList().OrderByDescending(p => p.ProductId).FirstOrDefault();
                if (product == null)
                {
                    newid = "P1";
                }
                else
                {
                    newid = product.CategoryId.Substring(0, 1) + (Int32.Parse(product.CategoryId.Substring(1)) + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return newid;
        }
    }
}