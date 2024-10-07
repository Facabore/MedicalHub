using MedicalHub.Entities.Helpers;
namespace MedicalHub.Controllers.Patients;

public record RegisterPatient(string FullName, string IdentificationNumber, string Email, DateTime DateOfBirth);