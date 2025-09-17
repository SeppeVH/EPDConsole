using Chipsoft.Assignments.DAL.Exceptions;
using Chipsoft.Assignments.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chipsoft.Assignments.DAL.Repositories;

public abstract class Repository<T, TK>(DbContext context) : IRepository<T, TK> where T : class
{
    public T Read(TK id)
    {
        return context.Set<T>().Find(id) ?? throw new NotFoundException($"{typeof(T).Name} Entity not found {id}");
    }

    public IEnumerable<T> ReadAll()
    {
        return context.Set<T>();
    }

    public void Create(T entity)
    {
        context.Set<T>().Add(entity);
        context.SaveChanges();
    }

    public void Update(T entity)
    {
        context.Update(entity);
        context.SaveChanges();
    }

    public void Delete(T entity)
    {
        context.Remove(entity);
        context.SaveChanges();
    }
}