using Core.Entities;
using DataAccess.Queries.Contracts;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Utils;

namespace DataAccess.Queries
{
    public class ToDoQuery : IToDoQuery
    {
        private const string PROJECT_NAME = "DataAccess";

        private readonly AppDbContext _context;
        public ToDoQuery(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ToDo> GetById(long id, long tenantId)
        {
            var toDo = await _context.ToDos
                .Where(x => x.Id == id && x.TenantId == tenantId)
                .SingleOrDefaultAsync();
            Log.Information(LoggerFormatExtensions.FormatMessage(PROJECT_NAME, "Got a toDo {@toDo} at {now}"), toDo, DateTime.Now);
            return toDo;
        }

        public async Task<List<ToDo>> GetAll(long tenantId)
        {
            var toDoList = await _context.ToDos.Where(x => x.TenantId == tenantId).ToListAsync();
            Log.Information(LoggerFormatExtensions.FormatMessage(PROJECT_NAME, "Got a toDo list {@toDo} at {now}"), toDoList, DateTime.Now);
            return toDoList;
        }
    }
}
