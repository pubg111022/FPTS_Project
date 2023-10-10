using b2.Core.IRepository;
using b2.Core.IServices;
using b2.Core.Models;
using b2.DTOS;
using Customer_OrderAPI.DTOS;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace b2.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderItemService _OrderItemService;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository repository, ILogger<OrderService> logger, IOrderItemService orderItemService)
        {
            _repository = repository;
            _logger = logger;
            _OrderItemService = orderItemService;
        }
        public OrdersDTO Delete(OrdersDTO order)
        {
            List<OrderItem> list = new List<OrderItem>();
            foreach (var item in getOrderItem(order.Id)) {
                OrderItem r = new OrderItem();
                r.Id = item.Id;
                r.Quantity  = item.Quantity;
                r.OrderId = item.OrderId;  
                r.ProductId = item.ProductId;
                r.ProductName = item.ProductName;
                list.Add(r);
            }
            Order orders = new Order();
            orders.Id = order.Id;
            orders.AdditionalAddress = order.AdditionalAddress;
            orders.District = order.District;
            orders.City = order.City;
            orders.OrderDate = order.OrderDate;
            orders.Street = order.Street;
            orders.CustomerId = order.CustomerId;
            orders.OrderItems = list;
            _repository.Delete(orders);
            return order;
        }

        public OrdersDTO FindById(string id)
        {
            var item = _repository.FindById(id);
            OrdersDTO orders = new OrdersDTO();
            orders.Id = item.Id;
            orders.AdditionalAddress = item.AdditionalAddress;
            orders.District = item.District;
            orders.City = item.City;
            orders.OrderDate = item.OrderDate;
            orders.Street = item.Street;
            orders.CustomerId = item.CustomerId;
            orders.OrderItems = getOrderItem(item.Id);
            return orders;
        }

        public List<OrdersDTO> GetAll()
        {
            List<OrdersDTO> res = new List<OrdersDTO>();
            var list = _repository.GetAll();

            foreach (var item in list)
            {
                OrdersDTO orders = new OrdersDTO();
                orders.Id = item.Id;
                orders.AdditionalAddress = item.AdditionalAddress;
                orders.District = item.District;
                orders.City = item.City;
                orders.OrderDate = item.OrderDate;
                orders.Street = item.Street;
                orders.CustomerId = item.CustomerId;
                orders.OrderItems = getOrderItem(item.Id);
                res.Add(orders);
            }
            
            return res;
        }

        public List<Order> GetAllInformation()
        {
            return _repository.GetAllInformation();
        }

        public InsertOrderDTO Insert(InsertOrderDTO order)
        {
            List<OrderItem> list = new List<OrderItem>();
            foreach (var item in getBasket(order.CustomerId))
            {
                OrderItem r = new OrderItem();
                r.Id = item.Id;
                r.Quantity = item.Quantity;
                r.OrderId = order.Id;
                r.ProductId = item.ProductId;
                r.ProductName = item.ProductName;
                list.Add(r);
            }
            Order orders = new Order();
            orders.Id = order.Id;
            orders.AdditionalAddress = order.AdditionalAddress;
            orders.District = order.District;
            orders.City = order.City;
            orders.OrderDate = DateTime.Now;
            orders.Street = order.Street;
            orders.CustomerId = order.CustomerId;
            orders.OrderItems = list;
            if (checkProductQuantity(getBasket(order.CustomerId)))
            {
                try
                {
                    _repository.Insert(orders);
                    foreach (var item in getBasket(order.CustomerId))
                    {
                        UpdateProductQuantityAsync(item);
                    }
                    DeleteBasketItemAsync(orders.CustomerId);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }
        }
        public async void UpdateProductQuantityAsync(BasketItemDTO basket)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {

                    string apiUrl = $"https://localhost:44333/api/Products/updateAvailableQuantity?id={basket.ProductId}&availableQuantity={basket.Quantity}";

                    HttpResponseMessage response = await client.PutAsync(apiUrl, null);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("API response: " + responseData);
                    }
                    else
                    {
                        Console.WriteLine("API request failed with status code: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions gracefully.
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        public List<BasketItemDTO> getBasket(string customerId)
        {
            using var httpClient = new HttpClient();
            string apiUrl = "https://localhost:44382/api/BasketItems/" + customerId;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(apiUrl).Result; // Blocking call

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = response.Content.ReadAsStringAsync().Result; // Blocking call
                    List<BasketItemDTO> ?baskets = JsonConvert.DeserializeObject<List<BasketItemDTO>>(jsonContent);
                    if (baskets != null)
                    {
                        return baskets;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"HTTP Request Error: {e.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return null;

        }
        public async void DeleteBasketItemAsync(string customerId)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string apiUrl = "https://localhost:44382/api/BasketItems/" + customerId;

                try
                {
                    HttpResponseMessage response = await httpClient.DeleteAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("DELETE request was successful.");
                    }
                    else
                    {
                        Console.WriteLine($"DELETE request failed with status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
        public bool checkProductQuantity(ICollection<BasketItemDTO> list)
        {
            using var httpClient = new HttpClient();
            ICollection<BasketItemDTO> checkList = new List<BasketItemDTO>();
            string apiUrl = "https://localhost:44333/api/Products/";
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(apiUrl).Result; // Blocking call

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = response.Content.ReadAsStringAsync().Result; // Blocking call
                    List<ProductsDTO> ?products = JsonConvert.DeserializeObject<List<ProductsDTO>>(jsonContent);
                    if (products != null)
                    {
                        foreach (var item in products)
                        {
                            foreach (var basketitem in list)
                            {
                                if(basketitem.ProductId == item.Id && basketitem.Quantity <= item.AvailableQuantity)
                                {
                                    checkList.Add(basketitem);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"HTTP Request Error: {e.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            if(checkList.Count() == list.Count())
            {
                return true;
            }
            return false;
        }
        public OrdersDTO Update(OrdersDTO order)
        {
            List<OrderItem> list = new List<OrderItem>();
            foreach (var item in getOrderItem(order.Id))
            {
                OrderItem r = new OrderItem();
                r.Id = item.Id;
                r.Quantity = item.Quantity;
                r.OrderId = item.OrderId;
                r.ProductId = item.ProductId;
                r.ProductName = item.ProductName;
                list.Add(r);
            }
            Order orders = new Order();
            orders.Id = order.Id;
            orders.AdditionalAddress = order.AdditionalAddress;
            orders.District = order.District;
            orders.City = order.City;
            orders.OrderDate = order.OrderDate;
            orders.Street = order.Street;
            orders.CustomerId = order.CustomerId;
            orders.OrderItems = list;
            _repository.Update(orders);
            return order;
        }

        public List<OrderItemDTO> getOrderItem(string id)
        {
            List<OrderItemDTO> res = new List<OrderItemDTO>();
            var list = _OrderItemService.GetAll();

            foreach (var item in list)
            {
                if(item.OrderId == id)
                {
                    res.Add(item);
                }
            }
            return res;
        }
    }
}
