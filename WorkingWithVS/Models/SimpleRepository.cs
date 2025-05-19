namespace WorkingWithVS.Models
{
    public class SimpleRepository
    {
        private static SimpleRepository _sharedRepository = new();

        private Dictionary<string, Product> _products = new();

        public static SimpleRepository SharedRepository => _sharedRepository;

        public SimpleRepository()
        {
            var initialItems = new[]
            {
                new Product() { Name = "Kayak", Price = 275M },
                new Product() { Name = "Lifejacket", Price = 48.95M },
                new Product() { Name = "Soccer ball", Price = 19.50M },
                new Product() { Name = "Corner Flag", Price = 34.95M },
            };

            foreach (var initialItem in initialItems)
            {
                AddProduct(initialItem);
            }
            _products.Add("Error", null);
        }

        public IEnumerable<Product> Products => _products.Values;
        public void AddProduct(Product initialItem) => _products.Add(initialItem.Name, initialItem);
    }
}
