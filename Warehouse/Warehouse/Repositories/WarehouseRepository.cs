using System.Data.Common;
using System.Data.SqlClient;
using Warehouse.Model;

namespace Warehouse.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private IConfiguration _configuration;

    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<double> CheckIfProductExists(Order order)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM Product WHERE IdProduct = @IdProduct";
        cmd.Parameters.AddWithValue("@IdProduct", order.IdProduct);

        var dr = await cmd.ExecuteReaderAsync();
        if (! await dr.ReadAsync()) return 0.0;
        
        double price = Convert.ToSingle(dr["Price"]);
        return price;
    }
    
    
    public async Task<bool> CheckIfWarehouseExists(int IdWarehouse)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT count(*) FROM Warehouse WHERE IdWarehouse = @IdWarehouse";
        cmd.Parameters.AddWithValue("@IdWarehouse", IdWarehouse);
        
        Int32 count = (Int32) await cmd .ExecuteScalarAsync();
      
        return count > 0 ;
    }
    
    public async Task<int> CheckIfOrderExists(Order order)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "select * from [order] where IdProduct = @IdProduct and Amount = @Amount and FulfilledAt is NULL and CreatedAt < @CreatedAt";
        cmd.Parameters.AddWithValue("@IdProduct", order.IdProduct);
        cmd.Parameters.AddWithValue("@Amount", order.Amount);
        cmd.Parameters.AddWithValue("@CreatedAt", order.CreatedAt);

        
        var dr = await cmd.ExecuteReaderAsync();
        if (! await dr.ReadAsync()) return 0;
        
        var orderId = (int)dr["IdOrder"];
      
        return orderId ;
    }
    
    public async Task<int> IsOrderFinalized(int OrderID)
    {
        if (OrderID == 0)
        {
            return 0;
        }

        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "select count(*) from product_warehouse where IdOrder = @IdOrder";
        cmd.Parameters.AddWithValue("@IdOrder", OrderID);
        
        Int32 count = (Int32) await cmd .ExecuteScalarAsync();
      
        return count > 0 ?  0 : OrderID ;
    }

    public async Task UpdateOrder(int OrderId)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await con.OpenAsync();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "UPDATE [order] SET FulfilledAt=@FulfilledAt WHERE IdOrder = @IdOrder";
        cmd.Parameters.AddWithValue("@FulfilledAt", DateTime.Now);
        cmd.Parameters.AddWithValue("@IdOrder", OrderId);
        
        var affectedCount = await cmd.ExecuteNonQueryAsync();
    }

    public async Task<int> CreateOrder(Order order)
    {
        double Price = await CheckIfProductExists(order) * order.Amount;
        var OrderId = await IsOrderFinalized(await CheckIfOrderExists(order));
        
        if (Price > 0.0 && OrderId > 0 && await CheckIfWarehouseExists(order.IdWarehouse))
        {
            await UpdateOrder(OrderId);
            using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
            using var cmd = new SqlCommand();
            cmd.Connection = con;
            
            await con.OpenAsync();
           
            cmd.CommandText =
                "INSERT INTO Product_Warehouse(IdProduct, IdWarehouse, Amount, CreatedAt, IdOrder, Price) output INSERTED.IdProductWarehouse VALUES(@IdProduct, @IdWarehouse, @Amount, @CreatedAt, @IdOrder,@Price)";
            cmd.Parameters.AddWithValue("@IdProduct", order.IdProduct);
            cmd.Parameters.AddWithValue("@IdWarehouse", order.IdWarehouse);
            cmd.Parameters.AddWithValue("@Amount", order.Amount);
            cmd.Parameters.AddWithValue("@CreatedAt", order.CreatedAt);
            cmd.Parameters.AddWithValue("@IdOrder", OrderId);
            cmd.Parameters.AddWithValue("@Price", Price);
        
            
            DbTransaction tran = await con.BeginTransactionAsync();
            cmd.Transaction = (SqlTransaction)tran;
            
            int modified = await cmd.ExecuteNonQueryAsync();
            await tran.CommitAsync();
            return modified;
        }
        return 0;
    }
}