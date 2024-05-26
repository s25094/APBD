using System.Net;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Perscription.Context;
using Perscription.Model;


namespace Perscription.Controllers;

[Route("api/")]
[ApiController]
public class PharmacyController : ControllerBase
{
    PharmacyDbContext _dbContext = new PharmacyDbContext();
    public PharmacyController()
    {
    }
    
    [HttpPost("perscription")]
    public async Task<IActionResult> AddNewPerscription(NewPerscription newPerscription)
    {
        if (newPerscription.DueDate < newPerscription.Date)
        { 
            return BadRequest("Due date parameter has to be larger than Date parameter.");
        }

        if (newPerscription.NewDoses.Count >= 10)
        {
            return BadRequest("You can add max 10 medications to one perscription!");
        }

        foreach(var meds in newPerscription.NewDoses){
            if (!_dbContext.Medicaments.Any(m => m.IdMedicament == meds.IdMedicament))
            {
                return BadRequest($"Medicament with id: {meds.IdMedicament} - not found in database!");
            }
        }

        
        if (!_dbContext.Patients.Any(p => p.IdPatient == newPerscription.IdPatient))
            {
                var patient = new Patient
                {
                    IdPatient = newPerscription.IdPatient,
                    BirthDate = newPerscription.BirthDate,
                    FirstName = newPerscription.FirstName,
                    LastName = newPerscription.LastName
                };
                await _dbContext.Patients.AddAsync(patient);
                
            }

        var perscription = new PerscriptionC
        {
            Date = newPerscription.Date,
            DueDate = newPerscription.DueDate,
            Patient = await _dbContext.Patients.Where(p => p.IdPatient == newPerscription.IdPatient).FirstAsync(),
            Doctor = await _dbContext.Doctors.Where(p => p.IdDoctor == newPerscription.IdDoctor).FirstAsync()
        };
        await _dbContext.PerscriptionCs.AddAsync(perscription);
        await _dbContext.SaveChangesAsync();
        foreach (var med in newPerscription.NewDoses)
        {
            var medicament_perscription = new Perscription_Medicament
            {
                Dose = med.Dose,
                Details = med.Details,
                IdMedicament = med.IdMedicament,
                IdPerscription = perscription.IdPerscription
            };
            
            await _dbContext.Perscription_Medicaments.AddAsync(medicament_perscription);
        }
        
        
        await _dbContext.SaveChangesAsync();
        return Ok();
            
    }

    [HttpGet("patients/{idPatient:int}")]
    public async Task<IActionResult> GetPatientDetails(int idPatient)
    {
        var patient = await _dbContext.Patients.Where(p => p.IdPatient == idPatient).Select(c => new
        {
            idPatient = c.IdPatient, 
            FirstName = c.FirstName, 
            LastName = c.LastName, 
            BirthDate = c.BirthDate, 
            Perscriptions = c.PerscriptionCs.Where(d => c.IdPatient == d.Patient.IdPatient).Select(d => new
            {
                Date = d.Date,
                DueDate = d.DueDate,
                IdPerscription = d.IdPerscription,
                Doctor = new
                {
                    IdDoctor = d.Doctor.IdDoctor, 
                    FirstName = d.Doctor.FirstName
                }, 
                Medicaments = d.Perscription_Medicament.Where(pm => pm.IdPerscription == d.IdPerscription)
                    .Select(no => new
                    {
                        IdMedicament = no.IdMedicament,
                        Name = _dbContext.Medicaments.Where(md => md.IdMedicament == no.IdMedicament).Select(
                            newmed => newmed.Name
                            ).First(),
                        Dose = no.Dose,
                        Details = no.Details
                        
                    })
            })
        }).FirstAsync();
        return Ok(patient);
    }


}