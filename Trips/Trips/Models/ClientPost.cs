using System.ComponentModel.DataAnnotations;

namespace Trips.Models;

public partial class ClientPost
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    [EmailAddress]
    public string Email { get; set; } = null!;

    [Phone]
    public string Telephone { get; set; } = null!;
    
    public string Pesel { get; set; } = null!;
    
    public int IdTrip { get; set; }

    public string Name { get; set; } = null!;
    
    public DateTime? PaymentDate { get; set; }
}