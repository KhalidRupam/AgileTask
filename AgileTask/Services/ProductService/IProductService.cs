using AgileTask.Models;

namespace AgileTask.Services.ProductService
{
    public interface IProductService
    {
        public Task<List<Product>> getAllProduct();
        public Task<List<Product>> insertProduct(Product product);
        public Task<List<Product>> updateProduct(Product product);
        public Task<List<Product>> deleteProduct(Product product);
    }
}
