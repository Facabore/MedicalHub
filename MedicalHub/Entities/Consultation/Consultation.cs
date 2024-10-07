using MedicalHub.Abstractions;
using MedicalHub.Entities.Doctors;
using MedicalHub.Entities.Patients;

namespace MedicalHub.Entities.Consultation
{
    public class Consultation : Entity
    {
        public Consultation() : base(Guid.NewGuid())
        {
        } // Required by EF Core}

        private Consultation(Guid id, Guid patientId, Guid doctorId, DateTime consultationsDate,
            string reasonForConsultations, string diagnosis, Status consultationStatus, string result,
            string pendingOrders)
            : base(id)
        {
            this.PatientId = patientId;
            this.DoctorId = doctorId;
            this.ConsultationsDate = consultationsDate;
            this.ReasonForConsultations = reasonForConsultations;
            this.Diagnosis = diagnosis;
            this.Status = consultationStatus;
            this.Result = result;
            this.PendingOrders = pendingOrders;
        }

        private Consultation(Guid id, Guid patientId, Guid doctorId, DateTime consultationsDate,
            string reasonForConsultations)
            : base(id)
        {
            this.PatientId = patientId;
            this.DoctorId = doctorId;
            this.ConsultationsDate = consultationsDate;
            this.ReasonForConsultations = reasonForConsultations;
        }

        public Guid PatientId { get; private set; }
        public Guid DoctorId { get; private set; }
        public DateTime ConsultationsDate { get; private set; }
        public string ReasonForConsultations { get; private set; }
        public string? Diagnosis { get; private set; }
        public Status Status { get; private set; }
        public string? Result { get; private set; }
        public string? PendingOrders { get; private set; }

        public static Consultation Create(Guid patientIdentificationNumber, Guid doctorIdentificationNumber,
            DateTime consultationsDate, string reasonForConsultations)
        {
            var consultation = new Consultation(
                Guid.NewGuid(),
                patientIdentificationNumber,
                doctorIdentificationNumber,
                consultationsDate,
                reasonForConsultations
            );

            return consultation;
        }

        public void Update(DateTime dateTime,string reason,string diagnosis, Status status, string result, string pendingOrders)
        {
            this.ConsultationsDate = dateTime;
            this.ReasonForConsultations = reason;
            this.Diagnosis = diagnosis;
            this.Status = status;
            this.Result = result;
            this.PendingOrders = pendingOrders;
        }
    }
}
