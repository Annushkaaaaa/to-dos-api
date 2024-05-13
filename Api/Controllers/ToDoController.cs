using Api.Requests;
using Application.Dtos;
using Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

//[Authorize]
[Route("to-dos-api")]
public class ToDoController : Controller
{
    private const long DEFAULT_TENANT_ID = 1;

    private readonly IToDoService _toDoService;
    public ToDoController(IToDoService toDoService)
    {
        _toDoService = toDoService;
    }

    [HttpGet("to-dos")]
    public async Task<ToDoListDto> GetAllToDos()
    {
        return await _toDoService.GetToDos(DEFAULT_TENANT_ID);
    }
    [HttpPost("to-dos")]
    public async Task<long> AddToDo([FromBody] AddToDoRequest addToDoRequest)
    {
        return await _toDoService.AddToDo(addToDoRequest.Name, DEFAULT_TENANT_ID);
    }
    [HttpPost("to-dos/complete")]
    public async Task CompleteToDo([FromBody] CompleteToDoRequest completeToDoRequest)
    {
        await _toDoService.CompleteToDo(completeToDoRequest.ToDoIds, DEFAULT_TENANT_ID);
    }
    [HttpDelete("to-dos")]
    public async Task DeleteToDo([FromQuery] long toDoId)
    {
        await _toDoService.DeleteToDo(toDoId, DEFAULT_TENANT_ID);
    }
    [HttpDelete("to-dos/soft-delete")]
    public async Task SoftDeleteToDo([FromQuery] long toDoId)
    {
        await _toDoService.SoftDeleteToDo(toDoId, DEFAULT_TENANT_ID);
    }
}