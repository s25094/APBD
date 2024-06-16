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
            await _dbContent.UpfrontContracts.FirstOrDefaultAsync(c => c.ContractId.Equals(payment.ContractID));

        if (contract == null)
        {
            return NotFound("Contract not found");
        }
        
        var price =  _dbContent.UpfrontContracts.FirstOrDefault(c =>
            c.ContractId.Equals(payment.ContractID)).Price;

        var paid = _dbContent.Payments
            .Where(p => p.UpfrontContract.ContractId.Equals(payment.ContractID))
            .Select(s => s.paymentAmount).Sum();

        if (paid == null)
        {
            paid = 0;
        }

        if (price - paid < payment.Amount)
        {
            return NotFound("It's to much - aborted");
        }
        
        if (contract.EndDate < DateTime.Now)
        {
            return NotFound("Contract expired");
        }
        

        var newPayment = new Payment
        {
            UpfrontContract = contract,
            paymentAmount = payment.Amount
        };
        
        await _dbContent.Payments.AddAsync(newPayment);

        if (payment.Amount + paid == price)
        {
            contract.isPaid = 1;
            contract.isSigned = 1;
            contract.Status = "Active";
        }

        await _dbContent.SaveChangesAsync();
        return Ok("Payment added");
    }


    [HttpPost("/newcontract/upfront")]
    public async Task<IActionResult> AddUpFrontContract(Contract contract)
    {
        var contractValidation = contract.validateContract();
        
        if (!contractValidation.Equals("ok"))
        {
            return NotFound(contractValidation);
        }

        var clientID = await _dbContent.Clients.FirstOrDefaultAsync(c => c.ClientId == contract.IdClient);
        var softwareID =  _dbContent.Softwares.FirstOrDefaultAsync(s => s.SoftwareId == contract.SoftwareId).Result;
        var discountsID =  await _dbContent.Discounts.Where(d => contract.discountsID.Contains(d.DiscountId)).ToListAsync();
        var newContract = new UpfrontContract
        {
            isSubscription = contract.isSubscription,
            Client = clientID, 
            isSigned = 0, 
            Software = softwareID,
            Updates = contract.Updates, 
            Price = softwareID.FullPrice, 
            Status = "New", 
            isPaid = 0, StartDate = contract.StartDate, EndDate = contract.EndDate,
            Discounts = discountsID
        };
        
        Console.WriteLine(softwareID.FullPrice);
        //newContract.updatePrice();
        
        await _dbContent.UpfrontContracts.AddAsync(newContract);
        newContract.updatePrice();
        await _dbContent.SaveChangesAsync();
        return Ok(newContract.ContractId);
    }
    
    [HttpPost("/newclient/individual")]
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
    
    
    [HttpPost("/newclient/company")]
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
    
    [HttpPost("/delete/individuals")]
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
    
    [HttpPost("/update/individuals")]
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
    
    [HttpPost("/update/companies")]
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