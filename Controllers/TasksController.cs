using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Services;
namespace ToDo.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<task>> Get()
    {
        return TaskService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<task> Get(int id)
    {
        var Task = TaskService.GetById(id);
        if (Task == null)
            return NotFound();
        return Task;
    }

    [HttpPost]
    public ActionResult Post(task newTask)
    {
        var newId = TaskService.Add(newTask);

        return CreatedAtAction("Post", 
            new {id = newId}, TaskService.GetById(newId));
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id,task newPizza)
    {
        var result = TaskService.Update(id, newPizza);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
}
