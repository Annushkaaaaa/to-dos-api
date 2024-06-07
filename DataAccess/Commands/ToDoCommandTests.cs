using Core.Entities;
using DataAccess;
using DataAccess.Commands;
using Microsoft.EntityFrameworkCore;
using Moq;
using NodaTime;
using Xunit;

namespace Tests.DataAccessTests
{
    public class ToDoCommandTests
    {
        private const long DEFAULT_TENANT_ID = 1;

        private readonly List<ToDo> _toDoList = new List<ToDo>()
        {
            new ToDo 
            { 
                Id = 1, 
                Name = "First todo", 
                TenantId = DEFAULT_TENANT_ID 
            },
            new ToDo 
            { 
                Id = 2, 
                Name = "Second todo", 
                TenantId = DEFAULT_TENANT_ID 
            },
            new ToDo 
            { 
                Id = 3, 
                Name = "Third todo", 
                TenantId = DEFAULT_TENANT_ID 
            }
        };

        [Fact]
        public async Task AddToDoItem_ItemIsAddedSuccessfullyAsync()
        {
            var toDoToAdd = new ToDo 
            { 
                Id = 1, 
                Name = "Test adding todo" 
            };

            var dbContextMock = new Mock<AppDbContext>();
            var dbSetMock = new Mock<DbSet<ToDo>>();

            dbContextMock.Setup(x => x.ToDos).Returns(dbSetMock.Object);
            var clockMock = new Mock<IClock>();
            clockMock.Setup(x => x.GetCurrentInstant()).Returns(Instant.MaxValue);
            var command = new ToDoCommand(dbContextMock.Object, clockMock.Object);

            await command.Create(toDoToAdd);

            dbSetMock.Verify(x => x.AddAsync(toDoToAdd, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteToDo_DeletesToDoItemSuccessfullyAsync()
        {
            var toDoToDelete = new ToDo 
            { 
                Id = 1, 
                Name = "Test deleteing todo" 
            };

            var dbContextMock = new Mock<AppDbContext>();
            var dbSetMock = new Mock<DbSet<ToDo>>();

            dbContextMock.Setup(x => x.ToDos).Returns(dbSetMock.Object);
            var clockMock = new Mock<IClock>();
            clockMock.Setup(x => x.GetCurrentInstant()).Returns(Instant.MaxValue);
            var command = new ToDoCommand(dbContextMock.Object, clockMock.Object);

            await command.Delete(toDoToDelete);

            dbSetMock.Verify(x => x.Remove(toDoToDelete), Times.Once);
        }
    }
}