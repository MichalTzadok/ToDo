namespace ToDo.Services;

using ToDo.Models;
using ToDo.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

public  class TaskService : ITaskService
{
 List<task> tasks;
 private string fileName = "Task.json";
public TaskService(IWebHostEnvironment webHost)
        {
            this.fileName = Path.Combine(webHost.ContentRootPath,"wwwroot", "data", "Task.json");

            using (var jsonFile = File.OpenText(fileName))
            {
            tasks = JsonSerializer.Deserialize<List<task>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
});
        }
 }

        private void saveToFile()
        {
            File.WriteAllText(fileName, JsonSerializer.Serialize(tasks));
        }
    

    public List<task> GetAll() => tasks;

    public  task GetById(int id) 
    {
        return tasks.FirstOrDefault(p => p.Id == id);
    }

     public void Add(task task)
        {
            task.Id = tasks.Count()+1;
            tasks.Add(task);
            saveToFile();
        }

        public void Delete(int id)
        {
            var task = GetById(id);
            if (task is null)
                return;

            tasks.Remove(task);
            saveToFile();
        }

        public void Update(task task)
        {
            var index = tasks.FindIndex(p => p.Id == task.Id);
            if (index == -1)
                return;

            tasks[index] = task;
            saveToFile();
        }

        public int Count => tasks.Count();
    }

public static class TasksUtils
{
    public static void AddTask(this IServiceCollection services)
    {
        services.AddSingleton<ITaskService, TaskService>();
    }
}