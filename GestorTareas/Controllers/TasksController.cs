using Microsoft.AspNetCore.Mvc;
public class TasksController : ControllerBase
{
    private readonly TaskManagerContext _context;
    public TasksController(TaskManagerContext context)
    {
        _context = context;
    }
}