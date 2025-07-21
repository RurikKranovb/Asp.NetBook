using DependencyInjection.Infrastructure;

namespace DependencyInjection.Models
{
    public class ProductTotalizer
    {
        public ProductTotalizer(IRepository repository) => Repository = repository;
        public IRepository Repository { get; set; }

        public decimal Total => Repository.Products.Sum(p => p.Price);
    }
}
