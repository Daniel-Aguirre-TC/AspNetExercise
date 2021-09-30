using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace ASPNetExercise.Models
{
    public class ProductRepository : IProductRepository
    {

        // Connection to the database.
        private readonly IDbConnection _connection;

        /// <summary>
        /// Constructor for the Product Repository will store the provided IDbConnection to use internally
        /// </summary>
        /// <param name="connection"></param>
        public ProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Return a product that holds all categories from the database.
        /// </summary>
        /// <returns></returns>
        public Product AssignCategory()
        {
            return new Product() { Categories = GetCategories() };
        }

        /// <summary>
        /// Delete the product with the matching ProductID from the Database.
        /// </summary>
        /// <param name="product"></param>
        public void DeleteProduct(Product product)
        {
            _connection.Execute("DELETE * FROM REVIEWS WHERE ProductID = @id;",
                new { id = product.ProductID });
            _connection.Execute("DELETE * FROM Sales WHERE ProductID = @id;",
                new { id = product.ProductID });
            _connection.Execute("DELETE * FROM Products WHERE ProductID = @id;",
                new { id = product.ProductID });
        }

        /// <summary>
        /// Return all Products from the Products table of the bestbuy database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM PRODUCTS;");
        }

        /// <summary>
        /// Return all categories found in the database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> GetCategories()
        {
            return _connection.Query<Category>("SELECT * FROM categories;");
        }

        /// <summary>
        /// Return the Product that matches the provided id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProduct(int id)
        {
            return _connection.QuerySingle<Product>("SELECT * FROM PRODUCTS WHERE PRODUCTID = @id",
                new { id = id });
        }

        /// <summary>
        /// Insert the provided product into the database.
        /// </summary>
        /// <param name="productToInsert"></param>
        public void InsertProduct(Product productToInsert)
        {
            _connection.Execute("INSERT INTO products (NAME, PRICE, CATEGORYID) VALUES (@name, @price, @categoryID);",
                new { productToInsert.Name, productToInsert.Price, productToInsert.CategoryID });
        }

        /// <summary>
        /// Takes in the provided product and updates the product in Database with a matching ProductID to have the same data as the one being passed in.
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(Product product)
        {
            _connection.Execute("UPDATE products SET Name = @name, Price = @price, WHERE ProductID = @id",
                new { name = product.Name, price = product.Price, id = product.ProductID });
        }



    }
}
