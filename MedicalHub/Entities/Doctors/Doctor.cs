using MedicalHub.Abstractions;
using MedicalHub.Entities.Helpers;

namespace MedicalHub.Entities.Doctors;

public class Doctor : Entity
{
    private Doctor(Guid id, string fullName, string identificationNumber, string email) : base(id)
    {
        FullName = fullName;
        IdentificationNumber = identificationNumber;
        Email = email;
    }
    
    public string FullName { get; set; }
    public string IdentificationNumber { get; set; }
    public string Email { get; set; }
    
    public static Doctor? Create(string fullName, string identificationNumber, string email)
    {
        var doctor = new Doctor(
            Guid.NewGuid(),
            fullName,
            identificationNumber,
            email
        );

        return doctor;
    }
    public void Update(string fullName, string email)
    {
        FullName = fullName;
        Email = email;
    }
}