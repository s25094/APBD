using System.Data.SqlClient;
using Animals.Model;

namespace Animals.Repositories;

public class AnimalsRepository : IAnimalsRepository
{
       private IConfiguration _configuration;
    
    public AnimalsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IEnumerable<Animal> GetAnimals()
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT IdStudent, FirstName, LastName, Email, Address, IndexNumber FROM Student ORDER BY LastName, FirstName";
        
        var dr = cmd.ExecuteReader();
        var animals = new List<Animal>();
        while (dr.Read())
        {
            var grade = new Animal
            {
                IdAnimal = (int)dr["IdStudent"],
                Name = dr["FirstName"].ToString(),
                Description = dr["LastName"].ToString(),
                Category = dr["Email"].ToString(),
                Area = dr["Address"].ToString(),
            };
            animals.Add(grade);
        }
        
        return animals;
    }

    public Animal GetAnimal(int idAnimal)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT IdStudent, FirstName, LastName, Email, Address, IndexNumber FROM Student WHERE IdStudent = @IdStudent";
        cmd.Parameters.AddWithValue("@IdStudent", idAnimal);
        
        var dr = cmd.ExecuteReader();
        
        if (!dr.Read()) return null;
        
        var animal = new Animal
        {
            IdAnimal = (int)dr["IdStudent"],
            Name = dr["FirstName"].ToString(),
            Description = dr["LastName"].ToString(),
            Category = dr["Email"].ToString(),
            Area = dr["Address"].ToString()
        };
        
        return animal;
    }
    
    public int CreateAnimal(Animal animal)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "INSERT INTO Student(FirstName, LastName, Email, Address, IndexNumber) VALUES(@FirstName, @LastName, @Email, @Address, @IndexNumber)";
        cmd.Parameters.AddWithValue("@IdStudent", animal.IdAnimal);
        cmd.Parameters.AddWithValue("@FirstName", animal.Name);
        cmd.Parameters.AddWithValue("@LastName", animal.Description);
        cmd.Parameters.AddWithValue("@Email", animal.Category);
        cmd.Parameters.AddWithValue("@Address", animal.Area);
        
        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }

    
    public int DeleteAnimal(int id)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "DELETE FROM Student WHERE IdStudent = @IdStudent";
        cmd.Parameters.AddWithValue("@IdStudent", id);
        
        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }

    public int UpdateAnimal(Animal animal)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();
        
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "UPDATE Student SET FirstName=@FirstName, LastName=@LastName, Email=@Email, Address=@Address, IndexNumber=@IndexNumber WHERE IdStudent = @IdStudent";
        cmd.Parameters.AddWithValue("@IdStudent", animal.IdAnimal);
        cmd.Parameters.AddWithValue("@FirstName", animal.Name);
        cmd.Parameters.AddWithValue("@LastName", animal.Description);
        cmd.Parameters.AddWithValue("@Email", animal.Category);
        cmd.Parameters.AddWithValue("@Address", animal.Area);
        
        var affectedCount = cmd.ExecuteNonQuery();
        return affectedCount;
    }
    
}