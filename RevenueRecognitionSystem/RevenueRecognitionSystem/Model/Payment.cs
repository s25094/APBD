using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevenueRecognitionSystem.Model;

public class Payment
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PaymentId { get; set; }

    [Column(TypeName="money")]
    [Required] public decimal paymentAmount { get; set; }
    
    [ForeignKey("ContractId")]
    public UpfrontContract UpfrontContract { get; set; }
}