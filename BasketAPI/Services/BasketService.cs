using b3.Core.IRepositories;
using b3.Core.IServices;
using b3.Core.Models;
using b3.DTOs;
using BasketAPI.DTOs;

namespace b3.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _repository;
        private readonly ILogger<BasketService> _logger;
        private readonly IBasketItemService _BasketItemService;
        public BasketService(IBasketRepository repository, ILogger<BasketService> logger, IBasketItemService BasketItemService)
        {
            _repository = repository;
            _logger = logger;
            _BasketItemService = BasketItemService;
        }

        public BasketDTO Find(string CustomerId)
        {
            var basketData = _repository.Find(CustomerId);
            var basketItemData = _BasketItemService.FindByCustomer(CustomerId);
            BasketDTO basketDTO = new BasketDTO();
            basketDTO.CustomerId = CustomerId;
            basketDTO.BasketItems = basketItemData;
            return basketDTO;
        }

        public InsertBasketDTO Insert(InsertBasketDTO basketDTO)
        {
            Basket basket = new Basket();
            basket.CustomerId = basketDTO.CustomerId;
            foreach (var item in basketDTO.BasketItems)
            {
                BasketItem basketItem = new BasketItem();
                basketItem.Quantity = item.Quantity;
                basketItem.CustomerBasketId = item.CustomerBasketId;
                basketItem.ProductId = item.ProductId;
                basket.Items.Add(basketItem);
            }
            if (_repository.Find(basket.CustomerId) != null)
            {
                foreach (var item in basket.Items)
                {
                    BasketItemInsertDataDTO data = new BasketItemInsertDataDTO();
                    data.ProductId = item.ProductId;
                    data.Quantity = item.Quantity;
                    data.CustomerBasketId = item.CustomerBasketId;
                    _BasketItemService.Insert(data);
                }
            }
            else
            {
                _repository.Insert(basket);
            }


             return basketDTO;
        }
    }
}
