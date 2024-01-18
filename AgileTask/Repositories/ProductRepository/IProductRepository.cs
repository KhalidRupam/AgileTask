using AgileTask.Models;

namespace AgileTask.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        public Task<List<Product>> getAllProducts();

        public Task<List<Product>> insertProduct(Product product);
        public Task<List<Product>> updateProduct(Product product);
        public Task<List<Product>> deleteProduct(Product product);
    }
}
