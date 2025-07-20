using DependencyInjection.Models;

namespace DependencyInjection.Infrastructure
{
    public static class TypeBroker
    {
        private static Type _repoType = typeof(MemoryRepository);
        private static IRepository _testRepo;

        public static IRepository Repository =>
        _testRepo ?? Activator.CreateInstance(_repoType) as IRepository;

        public static void SetRepositoryType<T>() where T : IRepository => _repoType = typeof(T);

        public static void SetTestObject(IRepository repo)
        {
            _testRepo = repo;
        }
    }
}
