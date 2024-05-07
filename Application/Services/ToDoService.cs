using Application.Dtos;
using Application.Services.Contracts;
using Core.Entities;
using DataAccess.Commands.Contracts;
using DataAccess.Queries.Contracts;

namespace Application.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoQuery _toDoQuery;
        private readonly IToDoCommand _toDoCommand;

        public ToDoService(IToDoQuery toDoQuery, IToDoCommand toDoCommand)
        {
            _toDoQuery = toDoQuery;
            _toDoCommand = toDoCommand;
        }
        public async Task<long> AddToDo(string toDoName)
        {
            var toDo = new ToDo(toDoName);
            return await _toDoCommand.Create(toDo);
        }

        public async Task CompleteToDo(List<long> toDoIds)
        {
            foreach (var toDoId in toDoIds)
            {
                await DeleteToDo(toDoId);
            }
        }

        public async Task DeleteToDo(long toDoId)
        {
            var toDo = await _toDoQuery.GetById(toDoId);
            if (toDo == null)
            {
                throw new Exception("ToDo doesn't exists!");
            }
            await _toDoCommand.Delete(toDo);
        }

        public async Task<ToDoListDto> GetToDos()
        {
            var toDoList = await _toDoQuery.GetAll();
            var toDoListDto = toDoList.Select(toDo => new ToDoDto { Id = toDo.Id, Name = toDo.Name }).ToList();
            return new ToDoListDto() { ToDos = toDoListDto };
        }
    }
}
