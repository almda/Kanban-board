using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone4.DataAcces_Layer
{
    [Serializable]
    struct UserStruct
    {
        Hashtable UserDetails;
        public List<int> MyBord;


        public UserStruct(Hashtable usersave, List<int> bord)
        {
            this.UserDetails = usersave;
            this.MyBord = new List<int>();
            this.MyBord = bord;
        }

        public UserStruct(Hashtable usersave)
        {
            this.UserDetails = usersave;
            this.MyBord = new List<int>();
        }

        /*
        public Hashtable save()
        {
            Hashtable UserDetails = new Hashtable();
            String username = useremail.Split('@')[0];
            UserDetails[username] = userpass;
            return UserDetails;
        }
        */

        // get

        public String getUserName()
        {
            foreach (String item in UserDetails.Keys)
                return item;
            return "";
        }

        public String getPassword()
        {
            foreach (String item in UserDetails.Values)
                return item;
            return "";
        }

        public List<int> getBoard() { return MyBord; }

        public Hashtable getUser() { return UserDetails; }
    }
}
