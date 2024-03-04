namespace ToDo.Services;
using ToDo.Models;
using ToDo.Interfaces;
using System.Text.Json;


public  class UserService : IUserService
{
List<User> users;                                
private string fileName = "User.json";
public UserService(IWebHostEnvironment webHost)
       {

           this.fileName = Path.Combine(webHost.ContentRootPath,"Data", "Users.json");

           using (var jsonFile = File.OpenText(fileName))
           {
           users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
               new JsonSerializerOptions
               {
                   PropertyNameCaseInsensitive = true
});
       }
}

       private void saveToFile()
       {
           File.WriteAllText(fileName, JsonSerializer.Serialize(users));
       }
    

   public  List<User> GetAll() => users;

   
   

   

    //    public  User GetById(int id) 
    //    {
    //        return users.FirstOrDefault(p => p.Id == id);
    //    }
        public User? GetById(int id) => users.FirstOrDefault(p => p.Id == id);
    // public void Add(User User)
    //    {
    //        User.Id = users.Count()+1;
    //        users.Add(User);
    //        saveToFile();
    //    }

public void Delete(int id)
       {
           var user = GetById(id);
           if (user is null)
               return;

           users.Remove(user);
           saveToFile();
       }
       public void Update(User User)
       {
           var index = users.FindIndex(p => p.Id == User.Id);
           if (index == -1)
               return;

           users[index].Name = User.Name;
            users[index].Password = User.Password;

           saveToFile();
       }
       public void Add( User user)
       {
           user.Id = users.Max(u=>u.Id)+1;
           users.Add(user);
           saveToFile();
       }
    //    public void AdminUpdate(User User)
    //    {
    //        var index = users.FindIndex(p => p.Id == User.Id);
    //        if (index == -1)
    //            return;

    //        users[index].Name = User.Name;
    //         users[index].Password = User.Password;

    //        saveToFile();
    //    }

}

