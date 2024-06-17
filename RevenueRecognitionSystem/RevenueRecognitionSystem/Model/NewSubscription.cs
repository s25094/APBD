namespace RevenueRecognitionSystem.Model;

public class NewSubscription : NewSale
{

    public  string SubscriptionName { get; set; }
    public  string RenewalPeriod { get; set; }
    public  int QuantityOfRenewalPeriod { get; set; }


    public bool validateDate()
    {
        if (RenewalPeriod.Equals('m') && (QuantityOfRenewalPeriod < 1 || QuantityOfRenewalPeriod > 24) )
        {
            return true;
        }
        
        if (RenewalPeriod.Equals('y') && (QuantityOfRenewalPeriod > 2 || QuantityOfRenewalPeriod < 1) )
        {
            return true;
        }
        return false;
    }

    public string validateContract()
    {
        if (findClient() == null)
        {
            return "No customer was found";
        }

        if (validateDate())
        {
            return "Subscription max duration is 2 years";
        }

        if (checkSubscription())
        {
            return "Customer already has active subscription/contract";
        }

        if (findSoftware() == null)
        {
            return "There is no software for this id number";
        }

        return "ok";
    }
    
}