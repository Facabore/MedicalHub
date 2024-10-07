using MedicalHub.Entities.Patients;
using MedicalHub.Repository;

namespace MedicalHub.Services.Patients;

public class PatientService : IPatientService
{
    private readonly IRepository<Patient> _repository;
    public PatientService(IRepository<Patient> repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<Patient>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Patient?> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task Add(Patient entity)
    {
        await _repository.Add(entity);
    }

    public async Task Update(Patient entity)
    {
        await _repository.Update(entity);
    }

    public async Task Delete(Patient entity)
    {
        await _repository.Delete(entity);
    }
    public async Task<Patient?> GetByIdentification(string identificationNumber)
    {
        return await _repository.GetByIdentification(identificationNumber);
    }
}