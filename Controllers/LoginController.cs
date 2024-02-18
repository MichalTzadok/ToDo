using ToDo.Services;
using ToDo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.Interfaces;


namespace ToDo.Controllers{

    [ApiController]
    [Route("[controller]")]
    public class loginController : ControllerBase
    {
      readonly IUserService UserService;
       public loginController(IUserService UserService) 
       { 
         this.UserService=UserService;
         }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<String> Login([FromBody] User User)
        {
          System.Console.WriteLine("kjmj.,mlpkhjjui"+User.Password);

          var getUser=UserService.GetAll()?.FirstOrDefault(u=>u.Name==User.Name&&u.Password==User.Password);
                    System.Console.WriteLine(getUser.Password);

          var claims=new List<Claim>();
          if(getUser==null){
                      return Unauthorized();
              
          }
          else
              if(getUser.IsAdmin)
                      claims.Add(new Claim("type", "Admin"));
              else
                  claims.Add(new Claim("type", "User"));
          var token = ToDoTokenService.GetToken(claims);
                    System.Console.WriteLine(token);

           return new OkObjectResult(ToDoTokenService.WriteToken(token));
        }

          
           
            
    //     [HttpPost]
    //     [Route("[action]")]
    //     [Authorize(Policy = "Admin")]
    //     public IActionResult GenerateBadge([FromBody] Agent Agent)
    //     {
    //         var claims = new List<Claim>
    //         {
    //             new Claim("type", "Agent"),
    //             new Claim("UserName", Agent.Name),
    //             new Claim("ClearanceLevel", Agent.ClearanceLevel.ToString()),
    //         };

    //         var token = FbiTokenService.GetToken(claims);

    //         return new OkObjectResult(FbiTokenService.WriteToken(token));
    //     }
     }



}
