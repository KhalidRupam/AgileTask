using AgileTask.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace AgileTask.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        public ProductRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        public async Task<List<Product>> getAllProducts()
        {
            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionString))
                {

                    var results = await _dbConnection.QueryAsync<Product>("[dbo].[SP_GetAllProduct]", commandType: CommandType.StoredProcedure);


                    return results.ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public async Task<List<Product>> insertProduct(Product product)
        {
            await manageProduct(product, 1);
            return await getAllProducts();
        }

        public async Task<List<Product>> updateProduct(Product product)
        {
            await manageProduct(product, 2);
            return await getAllProducts();
        }

        public async Task<List<Product>> deleteProduct(Product product)
        {
            await manageProduct(product, 3);
            return await getAllProducts();
        }

        public async Task manageProduct(Product product, int type)
        {
            try
            {
                using (IDbConnection _dbConnection = new SqlConnection(_connectionString))
                {
                    var cartId = Guid.NewGuid().ToString();

                    DynamicParameters parameters = new();
                    parameters.Add("@TransactionType", type, DbType.Int32);
                    parameters.Add("@Id", product.Id, DbType.Int32);
                    parameters.Add("@TenantId", 10, DbType.Int32);
                    parameters.Add("@Name", product.Name, DbType.String);
                    parameters.Add("@Description", product.Description, DbType.String);
                    parameters.Add("@IsAvailable", product.IsAvailable, DbType.Boolean);

                    var results = await _dbConnection.ExecuteAsync("[dbo].[SP_ManageProduct]", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
