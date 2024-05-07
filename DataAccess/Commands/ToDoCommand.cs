using Core.Entities;
using DataAccess.Commands.Contracts;

namespace DataAccess.Commands
{
    public class ToDoCommand : IToDoCommand
    {
        private readonly AppDbContext _context;
        public ToDoCommand(AppDbContext context)
        {
            _context = context;
        }
        public async Task<long> Create(ToDo toDo)
        {
            await _context.ToDos.AddAsync(toDo);
            await _context.SaveChangesAsync();
            return toDo.Id;
        }

        public async Task Delete(ToDo toDo)
        {
            _context.ToDos.Remove(toDo);
            await _context.SaveChangesAsync();
        }
    }
}
