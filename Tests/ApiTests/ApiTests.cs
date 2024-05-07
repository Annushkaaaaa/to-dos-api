using Moq;
using Api.Controllers;
using Api.Requests;
using Application.Dtos;
using Application.Services.Contracts;

namespace Tests.ApiTests
{
   
    public class ToDoControllerTests
    {
        [Fact]
        public async Task GetAllToDos_ReturnsCorrectToDoList()
        {
            var expectedToDoList = new ToDoListDto
            {
                ToDos = new List<ToDoDto>
            {
                new ToDoDto { Id = 1, Name = "First todo" },
                new ToDoDto { Id = 2, Name = "Second todo" },
                new ToDoDto { Id = 3, Name = "Third todo" }
            }
            };

            var toDoServiceMock = new Mock<IToDoService>();
            toDoServiceMock.Setup(x => x.GetToDos()).ReturnsAsync(expectedToDoList);

            var controller = new ToDoController(toDoServiceMock.Object);

            var actionResult = await controller.GetAllToDos();

            Assert.NotNull(actionResult);
            Assert.Equal(expectedToDoList.ToDos.Count, actionResult.ToDos.Count);
            for (int i = 0; i < expectedToDoList.ToDos.Count; i++)
            {
                Assert.Equal(expectedToDoList.ToDos[i].Id, actionResult.ToDos[i].Id);
                Assert.Equal(expectedToDoList.ToDos[i].Name, actionResult.ToDos[i].Name);
            }
        }

        [Fact]
        public async Task GetAllToDos_ReturnsEmptyToDoList()
        {
            var expectedToDoList = new ToDoListDto
            {
                ToDos = new List<ToDoDto>()
            };

            var toDoServiceMock = new Mock<IToDoService>();
            toDoServiceMock.Setup(x => x.GetToDos()).ReturnsAsync(expectedToDoList);

            var controller = new ToDoController(toDoServiceMock.Object);

            var actionResult = await controller.GetAllToDos();

            Assert.NotNull(actionResult);
            Assert.Empty(actionResult.ToDos);
        }

        [Fact]
        public async Task AddToDo_ReturnsAddedToDoId()
        {
            var addToDoRequest = new AddToDoRequest { Name = "New ToDo" };
            var expectedId = 1;

            var toDoServiceMock = new Mock<IToDoService>();
            toDoServiceMock.Setup(x => x.AddToDo(addToDoRequest.Name)).ReturnsAsync(expectedId);

            var controller = new ToDoController(toDoServiceMock.Object);

            var actionResult = await controller.AddToDo(addToDoRequest);

            Assert.Equal(expectedId, actionResult);
        }
    }

}
