using Core.Entities;

namespace DataAccess.Commands.Contracts;

public interface IToDoCommand
{
    public Task<long> Create(ToDo toDo);
    public Task Delete(ToDo toDo);
}