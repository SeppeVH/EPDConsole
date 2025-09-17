namespace Chipsoft.Assignments.DAL.Repositories.Interfaces;

public interface IRepository<T, TK>
{
    T Read(TK id);
    IEnumerable<T> ReadAll();
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}