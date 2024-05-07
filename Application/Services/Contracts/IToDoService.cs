using Application.Dtos;

namespace Application.Services.Contracts
{
    public interface IToDoService
    {
        public Task<long> AddToDo(string toDoName);
        public Task CompleteToDo(List<long> toDoIds);
        public Task DeleteToDo(long toDoId);
        public Task<ToDoListDto> GetToDos();
    }
}
