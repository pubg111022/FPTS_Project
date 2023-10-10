using ProductAPI.Core.Models;

namespace ProductAPI.Core.Database.InMemory
{
    public class ProductMemory
    {
        public  Dictionary<string, Products> Memory { get; set; }

        public ProductMemory()
        {
            Memory = new Dictionary<string, Products>();
        }

    }
}
