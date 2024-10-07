using MedicalHub.Abstractions;
using MedicalHub.Entities.Helpers;

namespace MedicalHub.Entities.Patients;

public class Patient : Entity
{
    private Patient(Guid id, string fullName, string identificationNumber, string email,
        DateTime dateOfBirth) : base(id)
    {
        FullName = fullName;
        IdentificationNumber = identificationNumber;
        Email = email;
        DateOfBirth = dateOfBirth;
    }
    public string FullName { get; set; }
    public string IdentificationNumber { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public static Patient Create(string fullName, string identificationNumber, string email, DateTime dateOfBirth)
    {
        var patient = new Patient(
            Guid.NewGuid(),
            fullName,
            identificationNumber,
            email,
            dateOfBirth
        );

        return patient;
    }
    
    public void Update( string fullName, string email, DateTime dateOfBirth)
    {
        fullName = fullName;
        email = email;
        dateOfBirth = dateOfBirth;
    }
}