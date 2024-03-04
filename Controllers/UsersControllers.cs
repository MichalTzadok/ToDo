using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace ToDo.Controllers{
    

    [ApiController]
    [Route("[controller]")]
     [Authorize(Policy = "User")]
public class usersController : ControllerBase
{
      readonly IUserService UserService;
              private readonly int userId;


public usersController(IUserService UserService,IHttpContextAccessor httpContextAccessor){
               this.UserService = UserService;
              this.userId = int.Parse(httpContextAccessor?.HttpContext?.User?.FindFirst("id")?.Value);

}


      [HttpGet]
       [Authorize(Policy = "Admin")]
       public ActionResult<List<User>> GetAll() =>
               UserService.GetAll();
       
       [HttpGet("{id}")]
      [Authorize(Policy = "Admin")]
        public ActionResult<User> GetById(int id)
        {
            var user = UserService.GetById(id);
            if (user == null)
                return NotFound();

            return user;
        }
         [HttpGet]
        [Route("currentUser")]
        public ActionResult<User> getCurrentUser()
        {
            var user = UserService.GetById(userId);
            if (user == null)
                return NotFound();

            return user;
        }
    [HttpPut]    
       public IActionResult updateCurrentUser(User user)
       {
          if(userId!=user.Id)
          return BadRequest();
           UserService.Update(user);

           return NoContent();
       }

         [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]

       public IActionResult Delete(int id)
       {
           var user = UserService.GetById(id);
           if (user is null)
               return NotFound();

           UserService.Delete(id);

           return NoContent();
       }
        [HttpPost]
        [Authorize(Policy = "Admin")]
       public IActionResult Create(User user)
       {
           UserService.Add(user);
           return CreatedAtAction(nameof(Create), new { id = user.Id }, user);

       }

    //     [HttpPost]
    //     [Route("[action]")]
    //     [Authorize(Policy = "Admin")]
    //     public IActionResult Create([FromBody] User User)
    //     {
          
    //        UserService.Add(User);
    //        return CreatedAtAction(nameof(Create), new { id = User.Id }, User);

       
    //     }  

}}