using ToDo.Models;
namespace ToDo.Services;

public static class TaskService
{
    private static List<task> tasks;

    static TaskService()
    {
        tasks = new List<task>
        {
            new task { Id = 1, Name = "homework", IsDone = false},
            new task { Id = 2, Name = "shopping", IsDone = false},
            new task { Id = 3, Name = "friends", IsDone = true}
        };
    }

    public static List<task> GetAll() => tasks;

    public static task GetById(int id) 
    {
        return tasks.FirstOrDefault(p => p.Id == id);
    }

    public static int Add(task newTask)
    {
        if (tasks.Count == 0)

            {
                newTask.Id = 1;
            }
            else
            {
        newTask.Id =  tasks.Max(p => p.Id) + 1;

            }

        tasks.Add(newTask);

        return newTask.Id;
    }
  
    public static bool Update(int id, task newTask)
    {
        if (id != newTask.Id)
            return false;

        var existingTask = GetById(id);
        if (existingTask == null )
            return false;

        var index = tasks.IndexOf(existingTask);
        if (index == -1 )
            return false;

        tasks[index] = newTask;

        return true;
    }  

      
    public static bool Delete(int id)
    {
        var existingTask = GetById(id);
        if (existingTask == null )
            return false;

        var index = tasks.IndexOf(existingTask);
        if (index == -1 )
            return false;

        tasks.RemoveAt(index);
        return true;
    }  



}