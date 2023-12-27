using ToDo.Models;
namespace ToDo.Interfaces;
using System.Collections.Generic;



public  interface ITaskService
{

    List<task> GetAll() ;

     task GetById(int id) ;

     void Add(task newTask);
  
     void Update(task newTask);
     void Delete(int id);
}