using Microsoft.AspNetCore.Mvc;
using WebApplication3.model;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoListController : ControllerBase
    {

        private readonly ILogger<TodoListController> _logger;
        private readonly ITodoService _todoService;

        public TodoListController(ILogger<TodoListController> logger, ITodoService todoService)
        {
            _logger = logger;
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _todoService.GetTodos());
        }
        [HttpPost]
        public async Task<IActionResult> post(TaskListEntity list)
        {
            await _todoService.AddTodo(list);
            return Ok();
        }

        [HttpDelete("{Id:GUID}")]
        public async Task<IActionResult> Remove(Guid Id)
        {

            await _todoService.RemoveTodo(Id);
            return Ok();
        }
        [HttpPatch("{Id:GUID}")]
        public async Task<IActionResult> Patch(Guid Id, string Description, bool IsCompleted)
        {
            await _todoService.EditTodo(Id, Description, IsCompleted);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAll(List<TaskListEntity> todoList)
        {
            await _todoService.RemoveAllTasks();
            foreach(TaskListEntity todo in todoList)
            {
                await _todoService.AddTodo(todo);
            }

            return Ok();
        }
    }
}