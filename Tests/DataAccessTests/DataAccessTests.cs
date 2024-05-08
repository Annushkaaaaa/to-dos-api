using Core.Entities;
using DataAccess;
using DataAccess.Commands;
using DataAccess.Queries;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;


namespace Tests.DataAccessTests
{
    public class DataAccessTests
    {
        private const long DEFAULT_TENANT_ID = 1;

        private readonly List<ToDo> _toDoList = new List<ToDo>()
        {
            new ToDo { Id = 1, Name = "First todo", TenantId = DEFAULT_TENANT_ID },
            new ToDo { Id = 2, Name = "Second todo", TenantId = DEFAULT_TENANT_ID },
            new ToDo { Id = 3, Name = "Third todo", TenantId = DEFAULT_TENANT_ID }
        };

        [Fact]
        public async Task GetListOfToDos_ReturnsCorrectToDoListAsync()
        {
            var dbContextMock = new Mock<AppDbContext>();

            dbContextMock
                .Setup(x => x.ToDos)
                .ReturnsDbSet(_toDoList);

            var query = new ToDoQuery(dbContextMock.Object);
            var queryResult = await query.GetAll(DEFAULT_TENANT_ID);

            Assert.Equal(_toDoList, queryResult);
        }

        [Fact]
        public void GetEmptyListOfToDos_ReturnsCorrectEmptyToDoList()
        {
            var emptyToDoList = new List<ToDo>();
            var dbContextMock = new Mock<AppDbContext>();

            dbContextMock
                .Setup(x => x.ToDos)
                .ReturnsDbSet(new List<ToDo>());

            var query = new ToDoQuery(dbContextMock.Object);
            var queryResult = query.GetAll(DEFAULT_TENANT_ID).Result;

            Assert.Equal(emptyToDoList, queryResult);
        }

        [Fact]
        public async Task AddToDoItem_ItemIsAddedSuccessfullyAsync()
        {
            var toDoToAdd = new ToDo { Id = 1, Name = "Test adding todo" };

            var dbContextMock = new Mock<AppDbContext>();
            var dbSetMock = new Mock<DbSet<ToDo>>();

            dbContextMock.Setup(x => x.ToDos).Returns(dbSetMock.Object);

            var command = new ToDoCommand(dbContextMock.Object);

            await command.Create(toDoToAdd);

            dbSetMock.Verify(x => x.AddAsync(toDoToAdd, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteToDo_DeletesToDoItemSuccessfullyAsync()
        {
            var toDoToDelete = new ToDo { Id = 1, Name = "Test deleteing todo" };

            var dbContextMock = new Mock<AppDbContext>();
            var dbSetMock = new Mock<DbSet<ToDo>>();

            dbContextMock.Setup(x => x.ToDos).Returns(dbSetMock.Object);

            var command = new ToDoCommand(dbContextMock.Object);

            await command.Delete(toDoToDelete);

            dbSetMock.Verify(x => x.Remove(toDoToDelete), Times.Once);
        }
    }
}