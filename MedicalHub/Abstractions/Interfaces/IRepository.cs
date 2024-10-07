namespace MedicalHub.Repository;

public interface IRepository<T>
{
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task<T?> GetById(Guid id);
    Task<T?> GetByIdentification(string identificationNumber);
    Task<IEnumerable<T>> GetAll();
}