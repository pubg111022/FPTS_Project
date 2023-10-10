using b3.Core.Models;

namespace b3.Core.IRepositories
{
    public interface IBasketRepository
    {
        public Basket Insert(Basket basket);
        public Basket Find(string CustomerId);
    }
}
