using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MileStone4.Business_Layer;
using MileStone4.DataAcces_Layer;

namespace MileStone4.DataAcces_Layer
{
    [Serializable]
    static class PresistenceUser
    {
        private static string UsersFile = "Users.bin"; // DB
        private static List<UserStruct> users = new List<UserStruct>();

        static PresistenceUser()
        {
            load();
        }

        public static void saveUser(UserStruct UserDetails)
        {
            try
            {
                if (!File.Exists(UsersFile)) // exist file check
                {
                    File.Create(UsersFile);
                }
                // open file, and save the user there
                Stream stream = File.OpenWrite(UsersFile);
                stream.Position = stream.Length;
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, UserDetails);
                Console.WriteLine("The registrtion of " + UserDetails.getUserName() + " is Succeeded");
                stream.Close();
                users.Add(UserDetails);
            }
            catch(Exception e)
            {
                MileStone4.DataAcces_Layer.Logger.Log.Fatal("while the saving process of "+UserDetails.getUserName()+ " accord, there was an error -"+e.Message);
            }
        }


        public static Boolean isEmailExist(String Email)
        {
            //Hashtable tempHash = new Hashtable();
            //tempHash[Email]
            String[] arr = Email.Split('@');
            String username = arr[0];
            UserStruct tempUser = new UserStruct();
            for ( int i=0; i<users.Count; i++)
            {
                tempUser = users[i];
                if (tempUser.getUserName().Equals(username))
                    return true;
            }
            return false;
            /*
            if (!File.Exists(UsersFile)) // exist file check
            {
                Stream s = File.Create(UsersFile);
                s.Close();
            }
            
            Stream stream = File.OpenRead(UsersFile);
            BinaryFormatter formatter = new BinaryFormatter();
            Email = Email.Split('@')[0]; // Change to user name
            stream.Position = 0;
            try
            {
                while (stream.Position < stream.Length)
                { // pass on all the data in te file
                    UserStruct UserDetails = (UserStruct)formatter.Deserialize(stream);
                    if (UserDetails.getUser().ContainsKey(Email)) // and check if the email is there
                    {
                        stream.Close();
                        return true;
                    }
                }
                stream.Close();
                return false;
            }
            catch (Exception e)
            {
                MileStone4.DataAcces_Layer.Logger.Log.Fatal("while check the if the email "+Email+" exist, an error accord - "+e.Message);
                return false;
            }
            */
        }

        public static UserStruct? isPasswordExist(String Email, String Password)
        {
            UserStruct tempUser = new UserStruct();
            String[] arr = Email.Split('@');
            String username = arr[0];
            for (int i = 0; i < users.Count; i++)
            {
                tempUser = users[i];
                if (tempUser.getUserName().Equals(username))
                {
                    String tempPass = tempUser.getUser()[username].ToString();
                    if (tempPass.Equals(Password))
                        return tempUser;
                }
            }
            return null;
            /*
            if (!File.Exists(UsersFile)) // file exist check
            {
                File.Create(UsersFile);
            }

            Stream stream = File.OpenRead(UsersFile);
            BinaryFormatter formatter = new BinaryFormatter();
            stream.Position = 0;
            try
            {
                while (stream.Position < stream.Length)
                { // pass on the data in the file
                    UserStruct UserDetails = (UserStruct)formatter.Deserialize(stream);
                    String passtry = UserDetails.getUser()[Email].ToString();
                    if (passtry.Equals(Password))
                    { // check if the password match to the email
                        stream.Close();
                        return UserDetails;
                    }
                }
            }
            catch (Exception e)
            {
                MileStone4.DataAcces_Layer.Logger.Log.Fatal("while check if email "+Email+" and password "+Password+" match, an error accord - "+e.Message);
            }
            stream.Close();
            return null;
            */
        }

        public static void load()
        {
            //File.Delete(UsersFile);


            if (!File.Exists(UsersFile)) // file exist check
            {
                Stream s = File.Create(UsersFile);
                s.Close();
            }
            try
            {
                Stream stream = File.OpenRead(UsersFile);
                BinaryFormatter formatter = new BinaryFormatter();
                stream.Position = 0;
                while (stream.Position < stream.Length)
                { // load all the data to RAM
                    UserStruct currentUser = (UserStruct)formatter.Deserialize(stream);
                    users.Add(currentUser);
                }
                stream.Close();
            }
            catch (Exception e)
            {
                MileStone4.DataAcces_Layer.Logger.Log.Fatal("error accord while load the DB - "+e.Message);
            }
            /*
            Console.WriteLine("   ~  Welcome to Kanban!  ~");
            while (true)
            { 
            Console.WriteLine("What do you want to do?");
            String todo = Console.ReadLine();
                switch (todo) // action for the user
                {
                    case ("registration"):
                        User user = new User();
                        Console.WriteLine("Please fill the fields below");

                        Console.Write("Email: ");
                        String email = Console.ReadLine();
                        while (!CheckEmailValid(email))
                        { // check email
                            Console.Write("The Email is incorrect, pls enter anouther one: ");
                            email = Console.ReadLine();
                        }
                        user.Registration(email, "wedontcheckpasswordnow"); // check if the email is can be registration

                        Console.Write("Password: ");
                        String password = Console.ReadLine();
                        while (!CheckPassword(password)) //check password valid
                        {
                            Console.Write("The password is incorrect, pls enter anouther one: ");
                            password = Console.ReadLine();
                        }

                        if (user.Registration(email, password)) // registration
                            Console.WriteLine("The registration complete :) ");
                        Logger.Log.Info("The user "+ email + " is now registry"); // log the event
                        break;
                    case ("login"):
                        User userLogin = new User(); 
                        Console.WriteLine("    ~   Kanbon   ~");
                        Console.Write("Email: "); // enter the 
                        String emailLogin = Console.ReadLine();
                        Console.Write("Password: ");
                        String passLogin = Console.ReadLine();
                        userLogin = aUser.login(emailLogin, passLogin); // try to login
                        if (userLogin != null)
                            Console.WriteLine("You in the Kanban!");
                        break;
                }
                
            }
            */
        }
    }
}

    