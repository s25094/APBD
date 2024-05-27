using System.Net;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;
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

        int patient_id = newPerscription.IdPatient;
        if (!_dbContext.Patients.Any(p => p.IdPatient == newPerscription.IdPatient))
            {
                var patient = new Patient
                {
                    //IdPatient = newPerscription.IdPatient,
                    BirthDate = newPerscription.BirthDate,
                    FirstName = newPerscription.FirstName,
                    LastName = newPerscription.LastName
                };
                await _dbContext.Patients.AddAsync(patient);
                await _dbContext.SaveChangesAsync();
                patient_id = patient.IdPatient;
            }
        
              var perscription = new PerscriptionC
              {
                  Date = newPerscription.Date,
                  DueDate = newPerscription.DueDate,
                  Patient = await _dbContext.Patients.Where(p => p.IdPatient == patient_id).FirstAsync(),
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
        var patient = await _dbContext.Patients
            .Include(persc => persc.PerscriptionCs)
            .Select(patient => new
        {
            IdPatient = patient.IdPatient,
            PatientFirstName = patient.FirstName,
            PatientLastName = patient.LastName,
            BirthDay = patient.BirthDate,
            Perscriptions = patient.PerscriptionCs.Select(p => new
            {
                PerscriptionID = p.IdPerscription,
                Date = p.Date,
                DueDate = p.DueDate,
                Medicaments = p.Perscription_Medicament.Select(m => new
                {
                    IdMedicament = m.IdMedicament,
                    MedicamentName = _dbContext.Medicaments.Select(name => name.Name).FirstOrDefault(),
                    Dose = m.Dose, 
                    Details = m.Details
                    
                }).ToList(), 
                Doctor = new
                {
                    IdDoctor = p.Doctor.IdDoctor,
                    DoctorName = p.Doctor.FirstName + " " + p.Doctor.LastName
                }
            }).ToList()
        }).FirstOrDefaultAsync(p => p.IdPatient == idPatient);
        
        return Ok(patient);
    }


}