using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class UserManager
    {
        public UserManager()
        {
            Users = new List<User>();
        }

        public List<User> Users { get; }

        public bool AddUser(User user)
        {
            if (Users.Any(o => o.ID == user.ID)) return false;

            Users.Add(user);
            return true;
        }

        public bool RemoveUser(User user)
        {
            if (!Users.Any(o => o.ID == user.ID)) return false;

            Users.Remove(user);
            return true;
        }

        public void Dispose()
        {
            var users = Users.ToArray();
            foreach (var user in users)
            {
                user.Dispose();
            }
        }

    }
}
