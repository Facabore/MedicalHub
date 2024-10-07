using MedicalHub.Entities.Doctors;
using MedicalHub.Services.Doctors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalHub.Controllers.Doctors;

[ApiController]
[Route("api/doctors")]
public class DoctorController : ControllerBase
{
    private readonly DoctorService _doctorService;
    
    public DoctorController(DoctorService doctorService)
    {
        _doctorService = doctorService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllPatients()
    {
        var patients = await _doctorService.GetAll();
        return Ok(patients);
    }
    [HttpPost("register")]
    public async Task<IActionResult> RegisterDoctor(RegisterDoctor registerDoctor)
    {
        var doctor = Doctor.Create(
            registerDoctor.FullName,
            registerDoctor.IdentificationNumber,
            registerDoctor.Email
        );
        
        await _doctorService.Add(doctor);
        return Ok(doctor.Id);
    }
        
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientById(Guid id)
    {
        var patient = await _doctorService.GetById(id);
        if (patient == null) return NotFound();
        return Ok(patient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(Guid id, UpdateDoctor updateDoctor)
    {
        var patient = await _doctorService.GetById(id);
        if (patient == null) return NotFound();

        patient.Update(updateDoctor.FullName, updateDoctor.Email);
        await _doctorService.Update(patient);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(Guid id)
    {
        var patient = await _doctorService.GetById(id);
        if (patient == null) return NotFound();
        
        await _doctorService.Delete(patient);
        return NoContent();
    }
}