using ASPNetExercise.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetExercise.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductRepository repo;

        /// <summary>
        /// Constructor for the ProductController will take in a ProductRepository to use for referring to products..
        /// </summary>
        /// <param name="productRepo"></param>
        public ProductController(IProductRepository productRepo)
        {
            repo = productRepo;
        }

        /// <summary>
        /// Display the ViewProduct.cshtml Views page, with all products in the ProductRepository that this Controller is linked to.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(repo.GetAllProducts());
        }

        /// <summary>
        /// View the ViewProduct.cshtml Views page for a singular product based on the Product ID passed in.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult ViewProduct(int id)
        {
            return View(repo.GetProduct(id));
        }

        /// <summary>
        /// View the Update Product page which will display a form allowing the user to update the Product Name/Price.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult UpdateProduct(int id)
        {
            Product prod = repo.GetProduct(id);
            return prod == null ? View("ProductNotFound") : View(prod);           
        }

        /// <summary>
        /// Take in a product, then update the database for the matching ProductID to have the same data as the provided product. 
        /// Then return to the ViewProduct views page, using the routeValue matching the ProductID of the product we just updated.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public IActionResult UpdateProductToDatabase(Product product)
        {
            repo.UpdateProduct(product);
            // returns to the ViewProduct Views page, using the routeValue matching the ProductID of the product we just updated.
            return RedirectToAction("ViewProduct", new { id = product.ProductID });
        }


        public IActionResult InsertProduct()
        {
            return View(repo.AssignCategory());
        }

        public IActionResult InsertProductToDatabase(Product productToInsert)
        {
            repo.InsertProduct(productToInsert);

            return RedirectToAction("Index");
        }

    }
}
