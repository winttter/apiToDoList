using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using WebApplication3.model;

namespace WebApplication3.Services
{
    public interface ITodoService
    {
        Task<List<TaskListEntity>> GetTodos();
        Task AddTodo(TaskListEntity list);
        Task RemoveTodo(Guid Id);
        Task EditTodo(Guid Id, string Description, bool IsCompleted);
        Task RemoveAllTasks();

    }
    public class TodoService: ITodoService
    {
        //обращение к БД
        private readonly ApplicationDbContext _context;

        public TodoService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddTodo(TaskListEntity list)
        {
            await _context.taskLists.AddAsync(list);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveTodo(Guid Id)
        {
            var taskToDelete = await _context.taskLists.FindAsync(Id);
            _context.taskLists.Remove(taskToDelete);
            await _context.SaveChangesAsync();
        }
        public async Task<List<TaskListEntity>> GetTodos()
        {
            var list = await _context.taskLists.ToListAsync();
            return list;
        }
        public async Task RemoveAllTasks()
        {
            //_context.taskLists.RemoveRange(0, _context.);

            _context.taskLists.RemoveRange(_context.taskLists);
            await _context.SaveChangesAsync();
        }
        public async Task EditTodo(Guid Id, string Description, bool IsCompleted)
        {
            //_context.taskLists.RemoveRange(0, _context.);

            var previousTask = await _context.taskLists.FindAsync(Id);
            if (Description != null)
            {
                previousTask.Description = Description;
            }
            if (IsCompleted != previousTask.IsCompleted)
            {
                previousTask.IsCompleted = IsCompleted;
            }
           

            await _context.SaveChangesAsync();
        }
    }
}
