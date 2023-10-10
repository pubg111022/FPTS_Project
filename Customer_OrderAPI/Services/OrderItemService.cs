using b2.Core.IRepository;
using b2.Core.IServices;
using b2.Core.Models;
using b2.DTOS;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace b2.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _repository;
        private readonly ILogger<OrderItemService> _logger;

        public OrderItemService(IOrderItemRepository repository, ILogger<OrderItemService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public OrderItemDTO Delete(OrderItemDTO orderItem)
        {
            OrderItem o = new OrderItem();
            o.OrderId = orderItem.OrderId;
            o.Quantity = orderItem.Quantity;
            o.ProductId = orderItem.ProductId;
            o.Id = orderItem.Id;
            o.ProductName = orderItem.ProductName;
            _repository.Delete(o);
            return orderItem;
        }

        public OrderItemDTO FindById(string id)
        {
            OrderItemDTO res = new OrderItemDTO();
            var item = _repository.FindById(id);
            res.Quantity = item.Quantity;
            res.Id = item.Id;
            res.ProductId = item.ProductId;
            res.ProductName = item.ProductName;
            res.Products = GetProductById(item.ProductId);

            return res;
        }

        public List<OrderItemDTO> GetAll()
        {
            List<OrderItemDTO> res = new List<OrderItemDTO>();
            foreach (var item in _repository.GetAll())
            {
                OrderItemDTO i = new OrderItemDTO();
                i.Id = item.Id;
                i.Quantity = item.Quantity;
                i.ProductName = item.ProductName;
                i.Products = GetProductById(item.ProductId);
                i.OrderId = item.OrderId;
                i.ProductId = item.ProductId;
                res.Add(i);
            }
            return res;
        }

        public OrderItemDTO Insert(OrderItemDTO orderItem)
        {
            ProductsDTO products = new ProductsDTO();
            products = orderItem.Products;
            OrderItem res = new OrderItem();
            res.OrderId = orderItem.OrderId;
            res.Quantity = orderItem.Quantity;
            res.ProductId = orderItem.ProductId;
            res.ProductName= orderItem.ProductName;
            res.Id= orderItem.Id;
            _repository.Insert(res);
            return orderItem;
        }

        public OrderItemDTO Update(OrderItemDTO orderItem)
        {
            OrderItem o = new OrderItem();
            o.OrderId = orderItem.OrderId;
            o.Quantity = orderItem.Quantity;
            o.ProductId = orderItem.ProductId;
            o.Id = orderItem.Id;
            o.ProductName = orderItem.ProductName;
            _repository.Update(o);
            return orderItem;
        }


        public ProductsDTO GetProductById(string productId)
        {
            using var httpClient = new HttpClient();

            string apiUrl = "https://localhost:44333/api/Products/";

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(apiUrl).Result; // Blocking call

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = response.Content.ReadAsStringAsync().Result; // Blocking call
                    List< ProductsDTO> products = JsonConvert.DeserializeObject<List<ProductsDTO>>(jsonContent);
                    foreach (var item in products)
                    {
                        if(item.Id == productId)
                        {
                            return item;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"HTTP Request Error: {e.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
            return null;
        }
    }
}
