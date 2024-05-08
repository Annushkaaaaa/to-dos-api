using Core.Entities;

namespace DataAccess.Queries.Contracts;

public interface IToDoQuery
{
    public Task<ToDo> GetById(long id, long tenantId);
    public Task<List<ToDo>> GetAll(long tenantId);
}