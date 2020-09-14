using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MileStone4.DataAcces_Layer;

namespace MileStone4.Business_Layer
{
    [Serializable]
    public class User : aUser
    {
        public User() : base() { }

        public User(String username, String userpass) : base()
        {
            this.Email = username;
            this.Password = userpass;
            this.MyBoard = new List<int>();
            this.BoardCurrentId = Constants.NullID;
        }

        public User(String username, String userpass, List<int> bord) : base()
        {
            this.Email = username;
            this.Password = userpass;
            this.MyBoard = bord;
            if (bord.Count != 0)
                this.BoardCurrentId = bord[0];
        }

        // ---------------------- methods ----------------------
        public override void Registration(String email, String password)
        {
            email = email.Split('@')[0]; // turn email to user name
            this.Email = email;
            this.Password = password;
            Hashtable userTemp = new Hashtable();
            userTemp[email] = password; // save to struct object
            UserStruct usersave = new UserStruct(userTemp, this.MyBoard);
            UserDAL.saveUser(usersave); //save to DB
        }

        public override Boolean CheckEmailUnuiqeness(String Email)
        {
            if (UserDAL.isEmailExist(Email))
                return false;
            return true;
        }

        private static UserStruct? IsPasswordMatch(String Email, String Password)
        {
            UserStruct? tempUser = UserDAL.isPasswordExist(Email, Password);
            if (tempUser != null)
                return tempUser;
            return null;
        }
    }
}