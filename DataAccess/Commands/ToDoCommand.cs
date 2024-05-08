using Core.Entities;
using DataAccess.Commands.Contracts;
using Serilog;
using Utils;

namespace DataAccess.Commands
{
    public class ToDoCommand : IToDoCommand
    {
        private const string PROJECT_NAME = "DataAccess";
        private readonly AppDbContext _context;
        public ToDoCommand(AppDbContext context)
        {
            _context = context;
        }
        public async Task<long> Create(ToDo toDo)
        {
            await _context.ToDos.AddAsync(toDo);
            await _context.SaveChangesAsync();
            Log.Information(LoggerFormatExtensions.FormatMessage(PROJECT_NAME, "Created a toDo {@toDo} at {now}"), toDo, DateTime.Now);
            return toDo.Id;
        }

        public async Task Delete(ToDo toDo)
        {
            _context.ToDos.Remove(toDo);
            Log.Information(LoggerFormatExtensions.FormatMessage(PROJECT_NAME, "Created a toDo {@toDo} at {now}"), toDo, DateTime.Now);
            await _context.SaveChangesAsync();
        }
    }
}
