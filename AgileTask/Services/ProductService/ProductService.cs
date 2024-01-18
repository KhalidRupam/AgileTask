using AgileTask.Models;
using AgileTask.Repositories.ProductRepository;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace AgileTask.Services.ProductService
{
    public class ProductService : IProductService
    {
        private HttpClient _httpClient;
        private readonly IProductRepository _productRepository;

        private static string ApiUsers => $"services/app/ProductSync";

        public ProductService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IProductRepository productRepository)
        {
            _httpClient = httpClientFactory.CreateClient("HttpClient");
            var token = httpContextAccessor.HttpContext.Session.GetString("token");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }

            _productRepository = productRepository;
        }
        public async Task<List<Product>> getAllProduct()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync(ApiUsers+ "/GetAllproduct");
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    List<Product> itemList = await responseMessage.Content.ReadFromJsonAsync<List<Product>>();
                    return itemList;
                }
                else
                {
                    var list= await _productRepository.getAllProducts();
                    return list;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Product>> insertProduct(Product product)
        {
            try
            {
                product.TenantId = 10;
                HttpResponseMessage  responseMessage=new();
                if (product.Id==0)
                {
                    responseMessage = await _httpClient.PostAsJsonAsync(ApiUsers + "/CreateOrEdit", product);
                }
                else
                {
                    var jsonContent = JsonContent.Create(product);
                    responseMessage = await _httpClient.PatchAsync(ApiUsers + "/CreateOrEdit", jsonContent);
                }
               
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var res = await responseMessage.Content.ReadAsStringAsync();
                    var products= await getAllProduct();
                    List<Product> itemList = products;
                    return itemList;
                }
                else
                {
                    var list = await _productRepository.insertProduct(product);
                    return list;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

       

        Task<List<Product>> IProductService.updateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        Task<List<Product>> IProductService.deleteProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
