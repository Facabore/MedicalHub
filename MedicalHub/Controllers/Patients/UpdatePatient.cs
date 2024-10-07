namespace MedicalHub.Controllers.Patients;

public record UpdatePatient(string FullName, string IdentificationNumber, string Email, DateTime DateOfBirth);