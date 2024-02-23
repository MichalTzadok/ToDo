using ToDo.Services;
using ToDo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.Interfaces;
using Microsoft.AspNetCore.Authorization;



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

          var getUser=UserService.GetAll()?.FirstOrDefault(u=>u.Name==User.Name&&u.Password==User.Password);

          var claims=new List<Claim>();
          if(getUser==null){
                      return Unauthorized();
              
          }
          else
              if(getUser.IsAdmin)
                      {claims.Add(new Claim("type", "Admin"));
                      claims.Add(new Claim("id", getUser.Id.ToString()));
                      }
              else{
              claims.Add(new Claim("type", "User"));
              claims.Add(new Claim("id", getUser.Id.ToString()));

        }
                     var token = TokenService.GetToken(claims);
           return new OkObjectResult(TokenService.WriteToken(token));
        }

          
            
       
     }



}
