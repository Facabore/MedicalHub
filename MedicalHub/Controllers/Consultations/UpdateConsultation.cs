using MedicalHub.Entities.Consultation;

namespace MedicalHub.Controllers.Consultations;

public record UpdateConsultation(DateTime ConsultationDate, string Reason, string Diagnosis, Status status, string result, string pendingOrders);