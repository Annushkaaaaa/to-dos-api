using Application.Dtos;

namespace Application.Services.Contracts
{
    public interface IToDoService
    {
        public Task<long> AddToDo(string toDoName, long tenantId);
        public Task CompleteToDo(List<long> toDoIds, long tenantId);
        public Task DeleteToDo(long toDoId, long tenantId);
        public Task SoftDeleteToDo(long toDoId, long tenantId);
        public Task<ToDoListDto> GetToDos(long tenantId);
    }
}
