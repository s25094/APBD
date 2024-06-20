using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevenueRecognitionSystem.Context;
using RevenueRecognitionSystem.Model;
using RevenueRecognitionSystem.Services;

namespace RevenueRecognitionSystem.Controller;


[Route("api/revenue-system/")]
[ApiController]
public class RevenueRecognitionController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly ISoftwareOrdeService _softwareOrdeService;
    private readonly IPaymentService _paymentService;
    private IRevenueService _revenueService;
    

    public RevenueRecognitionController(IClientService clientService, ISoftwareOrdeService softwareOrdeService, 
        IPaymentService paymentService, IRevenueService revenueService)
    {
        _clientService = clientService;
        _softwareOrdeService = softwareOrdeService;
        _paymentService = paymentService;
        _revenueService = revenueService;
    }
    
    
    [HttpPost("/new-client/individual")]
    public async Task<IActionResult> AddIndiviudalClient(IndividualRequest individualRequest)
    {

        try
        {
            return Ok(await _clientService.AddNewIndividualClientAsync(individualRequest, new CancellationToken()));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        
    }
    
    
    [HttpPost("/new-client/company")]
    public async Task<IActionResult> AddCompanylClient(CompanyRequest companyRequest)
    {
        try
        {
            return Ok(await _clientService.AddNewCompanyClientAsync(companyRequest, new CancellationToken()));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
    
    

    
    [HttpPost("/delete-client/individual")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        try
        {
            return Ok(await _clientService.DeleteIndividualClientAsync(idClient, new CancellationToken()));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
    
    [HttpPost("/update-client/individual")]
    public async Task<IActionResult> UpdateIndividual(IndividualUpdateRequest individual)
    {
        try
        {
            return Ok(await _clientService.UpdateIndividualClientAsync(individual, new CancellationToken()));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
    
    
    [HttpPost("/update-client/company")]
    public async Task<IActionResult> UpdateCompany(CompanyUpdateRequest company)
    {
        try
        {
            return Ok(await _clientService.UpdateCompanyClientAsync(company, new CancellationToken()));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
    
    
    [HttpPost("/newcontract/upfront")]
    public async Task<IActionResult> AddNewContract(ContractRequest contract)
    {
        try
        {
            return Ok(await _softwareOrdeService.CreateNewContractAsync(contract, new CancellationToken()));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
    
    [HttpPost("/newcontract/subscription")]
    public async Task<IActionResult> AddNewContract(SubscriptionRequest subscription)
    {
        try
        {
            return Ok(await _softwareOrdeService.CreateNewSubscriptionAsync(subscription, new CancellationToken()));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
    
    
    
   [HttpPost("/new-payment/contract")]
    public async Task<IActionResult> AddContractPayment(PaymentRequest payment)
    {
        try
        {
            return Ok(await _paymentService.AddUpfrontPaymentAsync(payment, new CancellationToken()));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
    
    [HttpPost("/new-payment/subscription")]
    public async Task<IActionResult> AddSubscriptionPayment(PaymentRequest subscription)
    {
        try
        {
            return Ok(await _paymentService.AddSubscriptionPaymentAsync(subscription, new CancellationToken()));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
    
    [HttpGet("/revenue/predicted")]
    public async Task<IActionResult> CalculateRevenueForcompany()
    {
        try
        {
            return Ok( await _revenueService.CalculatePredictedRevenue(new CancellationToken()));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
    
    [HttpGet("/revenue/current")]
    public async Task<IActionResult> CalculateCurrentRevenueForcompany()
    {
        try
        {
            return Ok( await _revenueService.CalculateCurrentRevenue(new CancellationToken()));
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
    
    
    
}