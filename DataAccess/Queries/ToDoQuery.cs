using Core.Entities;
using DataAccess.Queries.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Queries
{
    public class ToDoQuery : IToDoQuery
    {
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
            return toDo;
        }

        public async Task<List<ToDo>> GetAll(long tenantId)
        {
            var toDoList = await _context.ToDos.Where(x => x.TenantId == tenantId).ToListAsync();
            return toDoList;
        }
    }
}
