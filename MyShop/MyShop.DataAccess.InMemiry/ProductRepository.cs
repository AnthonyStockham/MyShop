using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemiry
{
    public class ProductRepository
    {
        //build an object repository in the cache
        ObjectCache cache = MemoryCache.Default;

        //define a list of products to hold things in
        List<Product> products = new List<Product>();

        public ProductRepository()
        {
            //retreive the cache of products
            products = cache["products"] as List<Product>;

            //if the cache is empty, create a ne list of products
            if(products == null)
            {
                products = new List<Product>();
            }

        }

        public void Commit()
        {
            //save the current products list to the cache
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            //add the product to the current list of products
            products.Add(p);
        }

        public void Update(Product product)
        {
            //find the product we want to update in the current list of products
            Product ProductToUpdate = products.Find(p => p.ProductID == product.ProductID);
            //if we have found the required product, replace it in the product list
            if(ProductToUpdate!= null)
            {
                ProductToUpdate = product;
            }
            else
            {
                //product was not found in the existing product list
                throw new Exception("Product not found");
            }
        }

        public Product Find(string ID)
        {
            //find a product in the current list of products
            Product product = products.Find(p => p.ProductID == ID);
            if (product != null)
            {
                return (product);
            }
            else
            {
                //product not in current list
                throw new Exception("Prodcut not found");
            }
        }


        public void Delete(string ID)
        {
            //delete product from the current product list
            Product ProductToDelete = products.Find(p => p.ProductID == ID);
            //if the product exists remove it form the list
            if (ProductToDelete != null)
            {
                products.Remove(ProductToDelete);
            }
            else
            {
                //the product does not exist
                throw new Exception("Product not found");
            }
        }

        public IQueryable<Product> Collection()
            {
            return products.AsQueryable();
    }

