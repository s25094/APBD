using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Trips.Context;
using Trips.Models;

namespace Trips.Controllers;

[Route("api/")]
[ApiController]
public class TripController : ControllerBase
{
    TripsContext _dbContext = new TripsContext();
    public TripController()
    {
    }
    
    [HttpGet("trips")]
    public async Task<IActionResult> GetTrips()
    {

        var trips = await _dbContext.Trips
                .Select(c => new
                {
                    Name = c.Name, 
                    Description = c.Description,
                    DateFrom = c.DateFrom,
                    DateTo = c.DateTo,
                    MaxPeople = c.MaxPeople,
                  Countries = c.IdCountries.Select(a => new { Name = _dbContext.Countries
                      .Where(b=> b.IdCountry == a.IdCountry).Select( b=> b.Name).First()}),
                  Clients = c.ClientTrips.Select(a => new { FirstName = _dbContext.Clients
                      .Where(b=> b.IdClient == a.IdClient).Select( b=> b.FirstName).First(),
                      LastName = _dbContext.Clients
                          .Where(b=> b.IdClient == a.IdClient).Select( b=> b.LastName).First()
                  })
                }).OrderBy(c => c.DateFrom).ToListAsync()
            ;
        return Ok(trips);
    }

    [HttpPost("trips/{idTrip:int}/clients")]
    public async Task<IActionResult> Create(ClientPost clientPost)
    {
        var clientId = 0;
        if (_dbContext.Trips.Any(t => t.IdTrip == clientPost.IdTrip))
        {

            if (!_dbContext.Clients.Any(c => c.Pesel == clientPost.Pesel))
            {
                var newClient = new Client
                {
                    FirstName = clientPost.FirstName,
                    LastName = clientPost.LastName,
                    Email = clientPost.Email,
                    Telephone = clientPost.Telephone,
                    Pesel = clientPost.Pesel,
                };
                await _dbContext.Clients.AddAsync(newClient);
                //await _dbContext.SaveChangesAsync();
                clientId = newClient.IdClient;
            }

            else
            {
                clientId = await _dbContext.Clients.Where(c => c.Pesel == clientPost.Pesel).Select(c => c.IdClient)
                    .FirstAsync();
            }

            try
            {
                var newClientTrip = new ClientTrip
                {
                    IdClient = clientId,
                    IdTrip = clientPost.IdTrip,
                    RegisteredAt = DateTime.Now,
                    PaymentDate = clientPost.PaymentDate
                };
                await _dbContext.ClientTrips.AddAsync(newClientTrip);
                await _dbContext.SaveChangesAsync();
                return Ok("Klient dodany do wycieczki");
            }
            catch (Exception e)
            {
                return Ok("Klient przypisany jest juz na tę wycieczkę.");
            }
        }

        return Ok("Wycieczka nie istnieje");
    }
    
    [HttpDelete("clients/{idClient:int}")]
    public async Task<IActionResult> Delete(int idClient)
    {
      
        var toDelete = new Client
        {
            IdClient = idClient
        };

        _dbContext.Clients.Attach(toDelete);

        var entry = _dbContext.Entry(toDelete);
        entry.State = EntityState.Deleted;
        
        try
        {
            await _dbContext.SaveChangesAsync();
            return Ok(toDelete);
        }
        catch (Exception e)
        {
            return Ok("Klient jest przypisany do wycieczki");
        }
        
    }
}