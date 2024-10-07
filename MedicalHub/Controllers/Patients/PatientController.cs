using MedicalHub.Controllers.Patients;

using MedicalHub.Entities.Patients;

using MedicalHub.Services.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalHub.Controllers.Patients;

[ApiController]
[Route("api/patient")]
public class PatientController : ControllerBase
{
    private readonly PatientService _patientService;
    
    public PatientController(PatientService patientService)
    {
        _patientService = patientService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllPatients()
    {
        var patients = await _patientService.GetAll();
        return Ok(patients);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterPatient(RegisterPatient registerPatient)
    {
        var patient = Patient.Create(
            registerPatient.FullName,
            registerPatient.IdentificationNumber,
            registerPatient.Email,
            registerPatient.DateOfBirth
        );
        
        await _patientService.Add(patient);
        return Ok(patient.Id);
    }
        
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientById(Guid id)
    {
        var patient = await _patientService.GetById(id);
        if (patient == null) return NotFound();
        return Ok(patient);
    }
    

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(Guid id, UpdatePatient updatePatient)
    {
        var patient = await _patientService.GetById(id);
        if (patient == null) return NotFound();

        patient.Update(updatePatient.FullName, updatePatient.Email, updatePatient.DateOfBirth);
        await _patientService.Update(patient);
        return Ok();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(Guid id)
    {
        var patient = await _patientService.GetById(id);
        if (patient == null) return NotFound();
        
        await _patientService.Delete(patient);
        return Ok();
    }
}