using MedicalHub.Entities.Consultation;
using MedicalHub.Repository;

namespace MedicalHub.Services.Consultations;

public class ConsultationService : IConsultationService
{
    private readonly  IRepository<Consultation> _repository;
    public ConsultationService(IRepository<Consultation> repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<Consultation>> GetAll()
    {
        return await _repository.GetAll();
    }
    
    public async Task<Consultation?> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }
    
    public async Task Add(Consultation entity)
    {
        await _repository.Add(entity);
    }
    
    public async Task Update(Consultation entity)
    {
        await _repository.Update(entity);
    }
    
    public async Task Delete(Consultation entity)
    {
        await _repository.Delete(entity);
    }
}