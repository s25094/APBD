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

    public double CheckIfProductExists(Order order)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM Product WHERE IdProduct = @IdProduct";
        cmd.Parameters.AddWithValue("@IdProduct", order.IdProduct);

        var dr = cmd.ExecuteReader();
        if (!dr.Read()) return 0.0;
        
        double price = Convert.ToSingle(dr["Price"]);
        return price;
    }
    
    
    public bool CheckIfWarehouseExists(int IdWarehouse)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT count(*) FROM Warehouse WHERE IdWarehouse = @IdWarehouse";
        cmd.Parameters.AddWithValue("@IdWarehouse", IdWarehouse);
        
        Int32 count = (Int32) cmd .ExecuteScalar();
      
        return count > 0 ;
    }
    
    public int CheckIfOrderExists(Order order)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "select * from [order] where IdProduct = @IdProduct and Amount = @Amount and FulfilledAt is NULL and CreatedAt < @CreatedAt";
        cmd.Parameters.AddWithValue("@IdProduct", order.IdProduct);
        cmd.Parameters.AddWithValue("@Amount", order.Amount);
        cmd.Parameters.AddWithValue("@CreatedAt", order.CreatedAt);

        
        var dr = cmd.ExecuteReader();
        if (!dr.Read()) return 0;
        
        var orderId = (int)dr["IdOrder"];
      
        return orderId ;
    }
    
    public int IsOrderFinalized(int OrderID)
    {
        if (OrderID == 0)
        {
            return 0;
        }

        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "select count(*) from product_warehouse where IdOrder = @IdOrder";
        cmd.Parameters.AddWithValue("@IdOrder", OrderID);
        
        Int32 count = (Int32) cmd .ExecuteScalar();
      
        return count > 0 ?  0 : OrderID ;
    }

    public void UpdateOrder(int OrderId)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "UPDATE [order] SET FulfilledAt=@FulfilledAt WHERE IdOrder = @IdOrder";
        cmd.Parameters.AddWithValue("@FulfilledAt", DateTime.Now);
        cmd.Parameters.AddWithValue("@IdOrder", OrderId);
        
        var affectedCount = cmd.ExecuteNonQuery();
    }

    public int CreateOrder(Order order)
    {
        double Price = CheckIfProductExists(order) * order.Amount;
        var OrderId = IsOrderFinalized(CheckIfOrderExists(order));
        
        if (Price > 0.0 && OrderId > 0 && CheckIfWarehouseExists(order.IdWarehouse))
        {
            UpdateOrder(OrderId);
            using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
            con.Open();
        
            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText =
                "INSERT INTO Product_Warehouse(IdProduct, IdWarehouse, Amount, CreatedAt, IdOrder, Price) output INSERTED.IdProductWarehouse VALUES(@IdProduct, @IdWarehouse, @Amount, @CreatedAt, @IdOrder,@Price)";
            cmd.Parameters.AddWithValue("@IdProduct", order.IdProduct);
            cmd.Parameters.AddWithValue("@IdWarehouse", order.IdWarehouse);
            cmd.Parameters.AddWithValue("@Amount", order.Amount);
            cmd.Parameters.AddWithValue("@CreatedAt", order.CreatedAt);
            cmd.Parameters.AddWithValue("@IdOrder", OrderId);
            cmd.Parameters.AddWithValue("@Price", Price);
        
            int modified =(int)cmd.ExecuteScalar();
            return modified;
        }
        return 0;
    }
}