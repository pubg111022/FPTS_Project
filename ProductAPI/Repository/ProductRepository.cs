using Microsoft.CodeAnalysis;
using Oracle.ManagedDataAccess.Client;
using ProductAPI.Core.Database;
using ProductAPI.Core.Database.InMemory;
using ProductAPI.Core.IRepositories;
using ProductAPI.Core.Models;
using System;
using System.Data;
using System.Text;
using System.Xml.Linq;

namespace ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductRepository> _logger;
        private readonly ProductDbContext _db;
        private readonly ProductMemory  _inMem;

        public ProductRepository(IConfiguration configuration, ILogger<ProductRepository> logger,ProductDbContext db, ProductMemory inMem)
        {
            _configuration = configuration;
            _logger = logger;
            _db = db;
            _inMem = inMem;
        }


        public Products Insert(Products products)
        {
            using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("OracleConnection")))
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand("sp_InsertProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("p_name", OracleDbType.Int64).Value = products.Id;
                    command.Parameters.Add("p_name", OracleDbType.Varchar2).Value = products.Name;
                    command.Parameters.Add("p_price", OracleDbType.BinaryFloat).Value = products.Price;
                    command.Parameters.Add("p_availablequantity", OracleDbType.BinaryFloat).Value = products.AvailableQuantity;
                    try
                    {
                        command.ExecuteNonQuery();
                        _inMem.Memory.Add(products.Id, products);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        throw;

                    }
                }
            }
            return products;
        }
        public List<Products> GetProducts()
        {
            //var connectionString = _configuration.GetConnectionString("OracleConnection");
            //List<Products> ListProducts = new List<Products>();
            //using (OracleConnection connection = new OracleConnection(connectionString))
            //{
            //    connection.Open();

            //    using (OracleCommand command = connection.CreateCommand())
            //    {
            //        command.CommandText = "sp_GetAllProducts";
            //        command.CommandType = System.Data.CommandType.StoredProcedure;

            //        // Add output parameter for the cursor
            //        OracleParameter cursorParam = new OracleParameter("p_cursor", OracleDbType.RefCursor);
            //        cursorParam.Direction = System.Data.ParameterDirection.Output;
            //        command.Parameters.Add(cursorParam);

            //        using (OracleDataReader reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                Products product = new Products
            //                {
            //                    Id = Convert.ToInt32(reader["Id"]),
            //                    Name = reader["Name"].ToString(),
            //                    Price = float.Parse(reader["Price"].ToString()),
            //                    AvailableQuantity = float.Parse(reader["AvailableQuantity"].ToString())
            //                };
            //                ListProducts.Add(product);
            //            }
            //        } 
            //    }
            //}
            //return ListProducts;
            List<Products> products = new List<Products>();
            try
            {
                products = _inMem.Memory.Values.ToList();
                

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;

            }
            return products;
        }

        public Products UpdateAvailableQuantity(string ProductId, float AvailableQuantity)
        {
            Products p = new Products();
            using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("OracleConnection")))
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand("UpdateAvailableQuantity", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("p_id", OracleDbType.Varchar2).Value = ProductId;
                    command.Parameters.Add("p_new_quantity", OracleDbType.Varchar2).Value = AvailableQuantity;

                    try
                    {
                        command.ExecuteNonQuery();
                        _inMem.Memory.TryGetValue(ProductId, out p);
                        p.AvailableQuantity = AvailableQuantity;
                        p.Id = ProductId;
                        p.Price = _inMem.Memory.Where(x => x.Key.Equals(ProductId)).FirstOrDefault().Value.Price;
                        p.Name = _inMem.Memory.Where(x => x.Key.Equals(ProductId)).FirstOrDefault().Value.Name;

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        throw;

                    }
                }
            }
            return p;
        }

        public Products UpdateName(string ProductId, string Name)
        {
            var oldP = _inMem.Memory.Where(x => x.Key.Equals(ProductId)).FirstOrDefault();
            Products p = new Products();
            p.Id = ProductId;
            p.Name = Name;
            p.AvailableQuantity = oldP.Value.AvailableQuantity;
            p.Price = oldP.Value.Price;
            using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("OracleConnection")))
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand("sp_UpdateProductName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("p_id", OracleDbType.Varchar2).Value = ProductId;
                    command.Parameters.Add("p_name", OracleDbType.Varchar2).Value = Name;

                    try
                    {
                        command.ExecuteNonQuery();
                        _inMem.Memory.Remove(p.Id);
                        _inMem.Memory.Add(p.Id, p);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        throw;
                    }
                }
            }
            return p;
        }

        public Products UpdatePrice(string ProductId, float Price)
        {
            var oldP = _inMem.Memory.Where(x => x.Key.Equals(ProductId)).FirstOrDefault();
            Products p = new Products();
            p.Id = ProductId;
            p.Price = Price;
            p.AvailableQuantity = oldP.Value.AvailableQuantity;
            p.Name = oldP.Value.Name;
            using (OracleConnection connection = new OracleConnection(_configuration.GetConnectionString("OracleConnection")))
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand("sp_UpdateProductPrice", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("p_id", OracleDbType.Varchar2).Value = ProductId;
                    command.Parameters.Add("p_price", OracleDbType.Varchar2).Value = Price;

                    try
                    {
                        command.ExecuteNonQuery();
                        _inMem.Memory.Remove(p.Id);
                        _inMem.Memory.Add(p.Id, p);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        throw;
                    }
                }
            }
            return p;
        }
    }
}
