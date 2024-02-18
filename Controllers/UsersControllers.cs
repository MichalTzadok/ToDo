using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace ToDo.Controllers{
public class usersController : ControllerBase
{
      readonly IUserService UserService;

public usersController(IUserService userService){
    this.UserService = userService;


}
      [HttpGet]
       [Authorize(Policy = "Admin")]
       public ActionResult<List<User>> GetAll() =>
               UserService.GetAll();
       [HttpGet("{id}")]
       [Authorize(Policy = "User")]
       public ActionResult<User> GetById(int id)
       {           
        // System.Console.WriteLine(userId);

           var user =UserService.GetById(id);

           if (user == null)
               return NotFound();

           return user;
       }   
        [HttpPost]
        [Route("[action]")]
        [Authorize(Policy = "Admin")]
        public IActionResult Create([FromBody] User User)
        {
          
           UserService.Add(User);
           return CreatedAtAction(nameof(Create), new { id = User.Id }, User);

       
        }  

}}