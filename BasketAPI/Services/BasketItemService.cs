using b3.Core.IRepositories;
using b3.Core.IServices;
using b3.Core.Models;
using b3.DTOs;
using BasketAPI.DTOs;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace b3.Services
{
    public class BasketItemService : IBasketItemService
    {
        private readonly IBasketItemRepository _repository;
        private readonly ILogger<BasketItemService> _logger;
        public BasketItemService(IBasketItemRepository repository, ILogger<BasketItemService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public List<BasketItemDTO> FindByCustomer(string CustomerId)
        {
            var data = _repository.FindByCustomer(CustomerId);
            List<BasketItemDTO> res = new List<BasketItemDTO>();
            foreach (var item in data)
            {
                BasketItemDTO basketItemDTO = new BasketItemDTO();
                basketItemDTO.Id = item.Id;
                basketItemDTO.Quantity = item.Quantity;
                basketItemDTO.Status = item.Status;
                basketItemDTO.CustomerBasketId = item.CustomerBasketId;
                basketItemDTO.ProductId = item.ProductId;
                basketItemDTO.ProductName = item.ProductName;
                res.Add(basketItemDTO);
            }
            return res;
        }
        public BasketItem checkExistItem(string CustomerId, string productId) {

            var item = _repository.FindByCustomer(CustomerId).Where(x=>x.ProductId.Equals(productId)).FirstOrDefault();
            
            
            return item;

        }
        public BasketItemInsertDataDTO Insert(BasketItemInsertDataDTO item)
        {
            using var httpClient = new HttpClient();

            string apiUrl = "https://localhost:44333/api/Products/";
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(apiUrl).Result; // Blocking call

                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = response.Content.ReadAsStringAsync().Result; // Blocking call
                    List<ProductDTO> products = JsonConvert.DeserializeObject<List<ProductDTO>>(jsonContent);
                    foreach (var i in products)
                    {
                        if (item.ProductId == i.Id)
                        {
                            BasketItem basketItem = new BasketItem();
                            basketItem.Status = 0;
                            basketItem.Quantity = item.Quantity;
                            basketItem.CustomerBasketId = item.CustomerBasketId;
                            basketItem.ProductId = item.ProductId;
                            basketItem.ProductName = i.Name;
                            if(item.Quantity <= i.AvailableQuantity)
                            {
                                if (checkExistItem(item.CustomerBasketId, item.ProductId) != null)
                                {
                                    var data = checkExistItem(item.CustomerBasketId, item.ProductId);
                                    basketItem.Id = data.Id;
                                    basketItem.Quantity = item.Quantity + data.Quantity;
                                    _repository.Update(basketItem,basketItem.CustomerBasketId,basketItem.ProductId);
                                }
                                else
                                {
                                    _repository.Insert(basketItem);
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
            
            BasketItemInsertDataDTO dto = new BasketItemInsertDataDTO();
            dto.Quantity = item.Quantity;
            dto.ProductId = item.ProductId;
            dto.CustomerBasketId = item.CustomerBasketId;
            return dto; 
        }

        public BasketItemDTO Update(BasketItemDTO b, string CustomerId, string ProductId)
        {
            BasketItem baskeitemt = new BasketItem();
            baskeitemt.Id = b.Id;
            baskeitemt.Quantity = b.Quantity;
            baskeitemt.Status = b.Status;
            baskeitemt.CustomerBasketId = b.CustomerBasketId;
            baskeitemt.ProductId = b.ProductId;
            baskeitemt.ProductName = b.ProductName;
            _repository.Update(baskeitemt, CustomerId, ProductId);
            return b;
        }
        public List<BasketItemDTO> DeleteByBasketCustomerId(string Id)
        {
            var item = _repository.DeleteByBasketCustomerId(Id);
            List<BasketItemDTO> rs = new List<BasketItemDTO>();
            foreach (var b in item)
            {
                BasketItemDTO baskeitem = new BasketItemDTO();
                baskeitem.Id = b.Id;
                baskeitem.Quantity = b.Quantity;
                baskeitem.Status = b.Status;
                baskeitem.CustomerBasketId = b.CustomerBasketId;
                baskeitem.ProductId = b.ProductId;
                baskeitem.ProductName = b.ProductName;
                rs.Add(baskeitem);
            }
            return rs;
        }
    }
}
