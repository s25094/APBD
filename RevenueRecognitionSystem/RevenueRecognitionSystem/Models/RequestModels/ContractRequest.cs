namespace RevenueRecognitionSystem.Model;

public class ContractRequest
{
    public int IdClient { get; set; }
    public int SoftwareId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Updates { get; set; }
}