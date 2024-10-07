using MedicalHub.Entities.Consultation;
using MedicalHub.Services.Consultations;
using MedicalHub.Services.Doctors;
using MedicalHub.Services.Patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalHub.Controllers.Consultations;

[ApiController]
[Route("api/consultation")]
public class ConsultationController : ControllerBase
{
    private readonly ConsultationService _consultationService;
    private readonly DoctorService _doctorService;
    private readonly PatientService _patientService;
    public ConsultationController(ConsultationService consultationService, DoctorService doctorService, PatientService patientService)
    {
        _consultationService = consultationService;
        _doctorService = doctorService;
        _patientService = patientService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllConsultations()
    {
        var consultations = await _consultationService.GetAll();
        return Ok(consultations);
    }

    [HttpPost("insertConsultation")]
    public async Task<IActionResult> InsertConsultation(InsertConsultation insertConsultation)
    {
 
        var patient = await _patientService.GetById(insertConsultation.PatientId);
        if (patient == null) return NotFound("Patient not found");
        var doctor = await _doctorService.GetById(insertConsultation.DoctorId);
        if (doctor == null) return NotFound("Doctor not found");
        
        var consultation = Consultation.Create(
            insertConsultation.PatientId,
            insertConsultation.DoctorId,
            insertConsultation.Date,
            insertConsultation.Reason
        );
        
        await _consultationService.Add(consultation);
        return Ok(consultation.Id);
    }

    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetConsultationById(Guid id)
    {
        var consultation = await _consultationService.GetById(id);
        if (consultation == null) return NotFound();
        return Ok(consultation);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateConsultation(Guid id, UpdateConsultation updateConsultation)
    {
        var consultation = await _consultationService.GetById(id);
        if (consultation == null) return NotFound();

        consultation.Update(updateConsultation.ConsultationDate, updateConsultation.Reason, updateConsultation.Reason, updateConsultation.status, updateConsultation.result, updateConsultation.pendingOrders );
        await _consultationService.Update(consultation);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConsultation(Guid id)
    {
        var consultation = await _consultationService.GetById(id);
        if (consultation == null) return NotFound();
        
        await _consultationService.Delete(consultation);
        return Ok();
    }
    
}