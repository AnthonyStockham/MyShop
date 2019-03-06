using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemiry;


namespace MySHop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        public ProductManagerController()
        {
            context = new ProductRepository();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            //show the create product page
            Product product = new Product();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            //when the user insers a new product save it to the database
            if (!ModelState.IsValid)
            {
                //the product is not in the correct form
                return View(product);
            }
            else
            {
                //save the new product
                //save  it to the list of products
                context.Insert(product);
                //commit changes to the db
                context.Commit();
                return RedirectToAction("index");
            }
        }

        public ActionResult ViewProduct(string productID)
        {
            //retrieve the product details
            Product selectedProduct = context.Find(productID);
            if (selectedProduct==null)
            {
                return HttpNotFound();
            }
            else
            {
                //the product has been found
                //send the product to the view
                return View(selectedProduct);
            }
            
        }

        [HttpPost]
        public ActionResult ViewProduct(Product product)
        {
            //save the results of the update
            //retrieve the product details
            Product selectedProduct = context.Find(product.ProductID);
            if (selectedProduct == null)
            {
                return HttpNotFound();
            }
            else
            {
                //the product has been found.  manually update the product
                selectedProduct.ProductID = product.ProductID;
                selectedProduct.ProductName = product.ProductName;
                selectedProduct.Description = product.Description;
                selectedProduct.Category = product.Category;
                selectedProduct.Price = product.Price;
                selectedProduct.Image = product.Image;

                //update the product in the product list
                context.Update(product);

                //commit the changes
                context.Commit();
                
                return RedirectToAction("index");
            }

            
        }
    }
}