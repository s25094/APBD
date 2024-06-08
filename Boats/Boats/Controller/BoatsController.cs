using Boats.Context;
using Boats.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Boats.Controller;



[Route("api/")]
[ApiController]
public class BoatsController : ControllerBase
{
    private BoatsDbContext _dbContext = new BoatsDbContext();
    public BoatsController()
    {
        
    }

    [HttpGet("clients/{idClient:int}")]
    public async Task<IActionResult> GetClientsReservations(int idClient)
    {
        var reservations = await _dbContext.Clients
            .Include(client => client.Reservations)
            .Select(client => new
            {
                IdClient = client.IdClient,
                Reservations = client.Reservations.Select(r => new
                {
                    IdReservation = r.IdReservation
                }).ToList()
            }).FirstOrDefaultAsync(c => c.IdClient == idClient);

        return Ok(reservations);
    }

    [HttpPost("newreservation")]
    public async Task<IActionResult> AddNewReservation(NewReservation newReservation)
    {
        if (_dbContext.Reservations.Any(c => c.Client.IdClient == newReservation.IdClient && c.Fulfilled == 1))
        {
            return NotFound("Customer already has active reservation");
        }

        int BoatStandard = await _dbContext.BoatStandards.Where(b => b.IdBoatStandard == newReservation.IdBoatStandard)
            .Select(b => b.Level).FirstOrDefaultAsync();
        
        ICollection<Sailboat> sailboats =
            await _dbContext.Sailboats.Where(s => s.BoatStandard.Level >= BoatStandard && ! _dbContext.Sailboat_Reservations
                    .Any(r => r.Sailboat ==s.IdSailboat))
                .OrderBy(s => s.BoatStandard.Level).ToListAsync();

       
        var reservation = new Reservation
        {
            Client = await _dbContext.Clients.FirstOrDefaultAsync(c => c.IdClient== newReservation.IdClient),
            DateFrom = newReservation.DateFrom,
            DateTo = newReservation.DateTo,
            BoatStandard = await _dbContext.BoatStandards.FirstOrDefaultAsync(c => c.IdBoatStandard== newReservation.IdBoatStandard),
            Capacity = 20,
            NumOfBoats = newReservation.NumOfBoats,
            Fulfilled = 0,
            Price = 0,
            CancelReason = ""
        };
        await _dbContext.Reservations.AddAsync(reservation);
        await _dbContext.SaveChangesAsync();
        if (sailboats.Count < newReservation.NumOfBoats)
        {
            reservation.CancelReason = "Not enough boats";
            _dbContext.Reservations.Update(reservation);
            await _dbContext.SaveChangesAsync();
            return NotFound("There is not enough free boats!");
        }

        float price = 0.0f;
        foreach (Sailboat s in sailboats)
        {
            var sailboat_reservation = new Sailboat_Reservation
            {
                Sailboat = s.IdSailboat,
                Reservation = reservation.IdReservation,
            };
            price += s.Price;
            await _dbContext.Sailboat_Reservations.AddAsync(sailboat_reservation);
        }


        reservation.Price = price;
        reservation.Fulfilled = 1;
        _dbContext.Update(reservation);
        await _dbContext.SaveChangesAsync();
        return Ok(reservation.IdReservation);
    }
}