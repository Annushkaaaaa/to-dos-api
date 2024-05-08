using Application.Dtos;
using Application.Services.Contracts;
using Core.Entities;
using DataAccess.Commands.Contracts;
using DataAccess.Queries.Contracts;
using Serilog;
using Utils;

namespace Application.Services
{
    public class ToDoService : IToDoService
    {
        private const string PROJECT_NAME = "Application";

        private readonly IToDoQuery _toDoQuery;
        private readonly IToDoCommand _toDoCommand;

        public ToDoService(IToDoQuery toDoQuery, IToDoCommand toDoCommand)
        {
            _toDoQuery = toDoQuery;
            _toDoCommand = toDoCommand;
        }
        public async Task<long> AddToDo(string toDoName, long tenantId)
        {
            if (string.IsNullOrEmpty(toDoName))
            {
                throw new ArgumentNullException("Name of toDo cannot be empty or null.");
            }
            var toDo = new ToDo(toDoName, tenantId);
            return await _toDoCommand.Create(toDo);
        }

        public async Task CompleteToDo(List<long> toDoIds, long tenantId)
        {
            foreach (var toDoId in toDoIds)
            {
                await DeleteToDo(toDoId, tenantId);
                Log.Information(LoggerFormatExtensions.FormatMessage(PROJECT_NAME, "Completed a toDo with id {@toDoId} at {now}"), toDoId, DateTime.Now);
            }
        }

        public async Task DeleteToDo(long toDoId, long tenantId)
        {
            var toDo = await _toDoQuery.GetById(toDoId, tenantId);
            if (toDo == null)
            {
                Log.Error(LoggerFormatExtensions.FormatMessage(PROJECT_NAME, "Unable to delete a toDo with id {@toDoId} at {now}. It's not exist"), toDoId, DateTime.Now);
                throw new ArgumentNullException("ToDo doesn't exists!");
            }
            await _toDoCommand.Delete(toDo);
        }

        public async Task<ToDoListDto> GetToDos(long tenantId)
        {
            var toDoList = await _toDoQuery.GetAll(tenantId);
            var toDoListDto = toDoList.Select(toDo => new ToDoDto { Id = toDo.Id, Name = toDo.Name }).ToList();
            return new ToDoListDto() { ToDos = toDoListDto };
        }
    }
}
