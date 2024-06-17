using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Context;
using RevenueRecognitionSystem.Model;

namespace RevenueRecognitionSystem.Controller;


[Route("api/")]
[ApiController]
public class RevenueRecognitionController : ControllerBase
{
    private RevenueRecognitionContext _dbContent = new RevenueRecognitionContext();
    public RevenueRecognitionController(){}

    
    [HttpPost("/newpayment")]
    public async Task<IActionResult> AddPayment(NewPayment payment)
    {
        var contract =
            await _dbContent.SoftwareOrders.FirstOrDefaultAsync(c => c.OrderId.Equals(payment.ContractID));

        if (contract == null)
        {
            return NotFound("Contract not found");
        }

        var price =  contract.Price;
        
        
        var paid = _dbContent.Payments
            .Where(p => p.SoftwareOrder.OrderId.Equals(payment.ContractID))
            .Select(s => s.paymentAmount).Sum();
        

        if (price - paid < payment.Amount)
        {
            return NotFound("It's to much - aborted");
        }

        var Upfront = await _dbContent.UpfrontContracts.FirstOrDefaultAsync(u => u.OrderId.Equals(payment.ContractID));
        if (Upfront != null)
        {
            if (Upfront.EndDate < DateTime.Now)
            {
                return NotFound("Contract Expired");
            }
        }


        var newPayment = new Payment
        {
            SoftwareOrder = contract,
            paymentAmount = payment.Amount
        };
        
        await _dbContent.Payments.AddAsync(newPayment);

        if (payment.Amount + paid == price)
        {
            contract.isPaid = 1;
            contract.Status = "Active";

            if (Upfront != null)
            {
                Upfront.isSigned = 1;
            }
        }

        await _dbContent.SaveChangesAsync();
        
        return Ok("Payment added");
    }


    [HttpPost("/newcontract/subscription")]
    public async Task<IActionResult> AddNewSubscription(NewSubscription newSubscription)
    {
        var subscriptionValidation = newSubscription.validateContract();
        if (!subscriptionValidation.Equals("ok"))
        {
            return NotFound(subscriptionValidation);
        }
        var clientID = await _dbContent.Clients.FirstOrDefaultAsync(c => c.ClientId == newSubscription.IdClient);
        var softwareID =  await _dbContent.Softwares.FirstOrDefaultAsync(s => s.SoftwareId == newSubscription.SoftwareId);
        var subscription = new Subscription
        {
            Client = clientID, 
            Software = softwareID,
            Price = softwareID.FullPrice, 
            Status = "New", 
            isPaid = 0,
            RenewalPeriod = newSubscription.RenewalPeriod,
            QuantityOfRenewalPeriod = newSubscription.QuantityOfRenewalPeriod,
            SubscriptionName = newSubscription.SubscriptionName
        };
        
        
        await _dbContent.Subscriptions.AddAsync(subscription);
        subscription.CalculateSubscription();
        await _dbContent.SaveChangesAsync();
        return Ok(subscription.OrderId);
    }


    [HttpPost("/newcontract/upfront")]
    public async Task<IActionResult> AddNewContract(NewContract contract)
    {
        var contractValidation = contract.validateContract();
        
        if (!contractValidation.Equals("ok"))
        {
            return NotFound(contractValidation);
        }

        var clientID = await _dbContent.Clients.FirstOrDefaultAsync(c => c.ClientId == contract.IdClient);
        var softwareID =  await _dbContent.Softwares.FirstOrDefaultAsync(s => s.SoftwareId == contract.SoftwareId);
        var newContract = new UpfrontContract
        {
            Client = clientID, 
            isSigned = 0, 
            Software = softwareID,
            Updates = contract.Updates, 
            Price = softwareID.FullPrice, 
            Status = "New", 
            isPaid = 0, StartDate = contract.StartDate, EndDate = contract.EndDate
        };
        
        Console.WriteLine(softwareID.FullPrice);
        
        await _dbContent.UpfrontContracts.AddAsync(newContract);
        newContract.AddUpdates();
        await _dbContent.SaveChangesAsync();
        return Ok(newContract.OrderId);
    }
    
    
    
    [HttpPost("/new/individual")]
    public async Task<IActionResult> AddIndiviudal(Individual individual)
    {
        var newIndividual = new Individual
        {
            FirstName = individual.FirstName,
            LastName = individual.LastName,
            PESEL = individual.PESEL,
            
        };
        newIndividual.setClient(individual.Email, individual.Phone, individual.Address);

        await _dbContent.Individuals.AddAsync(newIndividual);
        await _dbContent.SaveChangesAsync();
        return Ok(newIndividual.PESEL);
    }
    
    
    [HttpPost("/new/company")]
    public async Task<IActionResult> AddCompany(Company company)
    {
        var newCompany = new Company
        {
            CompanyName = company.CompanyName,
            KRS = company.KRS,
        };
        newCompany.setClient(company.Email, company.Phone, company.Address);

        await _dbContent.Companies.AddAsync(newCompany);
        await _dbContent.SaveChangesAsync();
        return Ok(newCompany.KRS);
    }
    
    [HttpPost("/delete/individual")]
    public async Task<IActionResult> DeleteIndividual(string pesel)
    {
        var individual = _dbContent.Individuals
            .FirstOrDefaultAsync(i => i.PESEL.Equals(pesel))
            .Result;
        if (individual != null)
        {
            individual.FirstName = "";
            individual.LastName = "";
            individual.Address = "";
            individual.setClient("", "", "");
        }

        await _dbContent.SaveChangesAsync();
        return Ok("record deleted");
    }
    
    [HttpPost("/update/individual")]
    public async Task<IActionResult> UpdateIndividual(Individual individual)
    {
        var updatedindividual = _dbContent.Individuals
            .FirstOrDefaultAsync(i => i.PESEL.Equals(individual.PESEL))
            .Result;
        if (updatedindividual != null)
        {
            updatedindividual.FirstName = individual.FirstName;
            updatedindividual.LastName = individual.LastName;
            updatedindividual.Address = individual.Address;
            updatedindividual.setClient(individual.Email, individual.Phone, individual.Address);
        }

        await _dbContent.SaveChangesAsync();
        return Ok(updatedindividual);
    }
    
    [HttpPost("/update/company")]
    public async Task<IActionResult> UpdateCompany(Company company)
    {
        var updatedCompany = _dbContent.Companies
            .FirstOrDefaultAsync(i => i.KRS.Equals(company.KRS))
            .Result;
        if (updatedCompany != null)
        {
            updatedCompany.CompanyName = company.CompanyName;
            updatedCompany.setClient(company.Email, company.Phone, company.Address);
        }

        await _dbContent.SaveChangesAsync();
        return Ok(updatedCompany);
    }

}