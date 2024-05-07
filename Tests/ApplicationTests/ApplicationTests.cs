using Application.Dtos;
using Application.Services;
using Core.Entities;
using DataAccess.Commands.Contracts;
using DataAccess.Queries.Contracts;
using Moq;


namespace Tests.ApplicationTests
{
    public class ApplicationTests
    {
        private readonly List<ToDo> _toDoList = new List<ToDo>()
        {
            new ToDo { Id = 1, Name = "First todo" },
            new ToDo { Id = 2, Name = "Second todo" },
            new ToDo { Id = 3, Name = "Third todo" }
        };

        [Fact]
        public async Task GetListOfToDos_ReturnsCorrectToDoListAsync()
        {
            var toDoQueryMock = new Mock<IToDoQuery>();
            toDoQueryMock.Setup(x => x.GetAll()).ReturnsAsync(_toDoList);

            var expectedResult = new ToDoListDto()
            {
                ToDos = new List<ToDoDto>()
                {
                    new ToDoDto { Id = 1, Name = "First todo" },
                    new ToDoDto { Id = 2, Name = "Second todo" },
                    new ToDoDto { Id = 3, Name = "Third todo" }
                }
            };

            var toDoService = new ToDoService(toDoQueryMock.Object, new Mock<IToDoCommand>().Object);
            var getAllToDosResult = await toDoService.GetToDos();

            // Method to compare two collections

            Assert.Collection(getAllToDosResult.ToDos,
                expectedResult.ToDos.Select(expected =>
                    new Action<ToDoDto>(actual =>
                    {
                        Assert.Equal(expected.Id, actual.Id);
                        Assert.Equal(expected.Name, actual.Name);
                    })).ToArray());
        }

        [Fact]
        public async Task GetEmptyListOfToDos_ReturnsCorrectEmptyToDoList()
        {
            var toDoQueryMock = new Mock<IToDoQuery>();
            toDoQueryMock.Setup(x => x.GetAll()).ReturnsAsync(new List<ToDo>());

            var expectedResult = new ToDoListDto()
            {
                ToDos = new List<ToDoDto>()
                {
                }
            };

            var toDoService = new ToDoService(toDoQueryMock.Object, new Mock<IToDoCommand>().Object);
            var getAllToDosResult = await toDoService.GetToDos();

            // Method to compare two collections

            Assert.Collection(getAllToDosResult.ToDos,
                expectedResult.ToDos.Select(expected =>
                    new Action<ToDoDto>(actual =>
                    {
                        Assert.Equal(expected.Id, actual.Id);
                        Assert.Equal(expected.Name, actual.Name);
                    })).ToArray());
        }

        [Fact]
        public async Task AddToDoItem_ItemIsAddedSuccessfullyAsync()
        {
            var toDoToAdd = new ToDo { Id = 1, Name = "Test adding todo" };

            var toDoCommandMock = new Mock<IToDoCommand>();
            toDoCommandMock.Setup(x => x.Create(It.IsAny<ToDo>())).ReturnsAsync(toDoToAdd.Id);

            var toDoService = new ToDoService(new Mock<IToDoQuery>().Object, toDoCommandMock.Object);
            var createToDosResult = await toDoService.AddToDo(toDoToAdd.Name);

            Assert.Equal(toDoToAdd.Id, createToDosResult);
        }
    }
}