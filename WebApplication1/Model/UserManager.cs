using System.Collections.Generic;

namespace WebApplication1.Model
{
   class UserManager
    {
        public static List<User> Get()
        {
            return new List<User>
            {
                new User
                {
                    Username = "Bradley",
                    Password = "bradley1",
                    Role = "Admin"
                }
                ,
                    new User
                {
                    Username = "Connor",
                    Password = "connor",
                    Role = "Admin"
                }
                    ,
                new User
                {
                    Username = "Sven",
                    Password = "secret",
                    Role = "user"
                }
                ,
                new User
                {
                    Username = "standarduser",
                    Password = "password",
                    Role = "user"
                }
                ,
                new User
                {
                    Username= "IFC",
                    Password= "ifcunc1",
                    Role = "Admin"
                },

                new User
                {
                    Username= "Athletics",
                    Password= "ath123",
                    Role = "Admin"
                }
            };
        }

        public static string GetRole(string username)
        {
            var user = Get();
            var result = user.Find(n => n.Username.Equals(username));
            return result.Role;
        }

        public static bool IsValid(string username, string password)
        {
            var user = Get();
            var result = user.Find(n => n.Username.Equals(username) && n.Password.Equals(password));

            if (result != null) return true;
            return false;
        }
    }
}
