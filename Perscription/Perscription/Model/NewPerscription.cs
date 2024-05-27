using System.ComponentModel.DataAnnotations;

namespace Perscription.Model;

public class NewPerscription
{
    
    private int max_doses = 10;
    

    public void checkDoses()
    {
        if (this.NewDoses.Count > 10)
        {
            throw new Exception($"You can add only {max_doses} at one perscripiton.");
        }
    }

    public void checkDates()
    {
        if (this.DueDate < this.Date)
        {
            throw new Exception($"Due date has to be greater or equal than Date parameter.");
        }
    }

    public void validateData()
    {
        checkDates();
        checkDoses();
    }
    
    
    public int IdPatient { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    

    public DateTime? BirthDate { get; set; }
    
    public ICollection<NewDose> NewDoses { get; set; }
    
    
    [Required]
    public DateTime? Date { get; set; }
    
    [Required]
    public DateTime? DueDate { get; set; }
    
    public int IdDoctor { get; set; }

    
    
}