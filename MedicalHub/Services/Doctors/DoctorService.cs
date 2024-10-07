using MedicalHub.Entities.Doctors;
using MedicalHub.Infrastructure;
using MedicalHub.Repository;

namespace MedicalHub.Services.Doctors;

public class DoctorService : IDoctorService
{
    private readonly IRepository<Doctor?> _repository;
    public DoctorService(IRepository<Doctor?> repository)
    {
        _repository = repository;
        
    }
    public async Task<IEnumerable<Doctor?>> GetAll()
    {
        return await _repository.GetAll();
    }
    
    public async Task<Doctor?> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }
    
    public async Task Add(Doctor? entity)
    {
        await _repository.Add(entity);
    }
    
    public async Task Update(Doctor? entity)
    {
        await _repository.Update(entity);
    }
    
    public async Task Delete(Doctor? entity)
    {
        await _repository.Delete(entity);
    }


    public async Task<Doctor?> GetByIdentification(string identificationNumber)
    {
        return await _repository.GetByIdentification(identificationNumber);
    }
}