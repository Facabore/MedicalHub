using System.Runtime.InteropServices.JavaScript;
using MedicalHub.Entities.Helpers;

namespace MedicalHub.Controllers.Doctors;

public record RegisterDoctor(string FullName, string IdentificationNumber, string Email);