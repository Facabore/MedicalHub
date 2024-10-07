namespace MedicalHub.Controllers.Consultations;

public record InsertConsultation(Guid PatientId, Guid DoctorId, DateTime Date, string Reason);