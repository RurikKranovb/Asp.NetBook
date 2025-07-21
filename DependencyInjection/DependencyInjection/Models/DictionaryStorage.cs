namespace DependencyInjection.Models
{
    public class DictionaryStorage : IModelStorage
    {
        private Dictionary<string, Product> _items = new();


        public IEnumerable<Product> Items => _items.Values;

        public Product this[string key]
        {
            get => _items[key];
            set => _items[key] = value;
        }

        public bool ContainsKey(string key) => _items.ContainsKey(key);

        public void RemoveItem(string key) => _items.Remove(key);
    }
}
