using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MileStone4.Business_Layer;
using MileStone4.DataAcces_Layer;

namespace MileStone4.Interface_Layer
{
    class UserTest
    {
        private static string UsersFile = "Users.bin"; // DB


        public static void doTest()
        {
            List<UserStruct> users = new List<UserStruct>();

            if (!File.Exists(UsersFile)) // file exist check
            {
                Stream s = File.Create(UsersFile);
                s.Close();
            }
            Stream stream = File.OpenRead(UsersFile);
            BinaryFormatter formatter = new BinaryFormatter();
            while (stream.Position < stream.Length)
            { // load all the data to RAM
                UserStruct currentUser = (UserStruct)formatter.Deserialize(stream);
                users.Add(currentUser);
            }
            stream.Close();
            File.Delete(UsersFile);


            //Console.Write("Create new User: ");
            User user = new User();
            if (user.Registration("try@post.bgu.ac.il", "Tmv2wa")) // user exist in registion
                Console.WriteLine("Create new User: Succed");
            else
                Console.WriteLine("Create new User: Not Succed");
            Hashtable userDetails = new Hashtable();
            userDetails[user.Email] = user.Password; // remove the new user
            UserStruct userRemove = new UserStruct(userDetails, 0);
            users.Remove(userRemove);

            Stream stream1 = File.OpenWrite(UsersFile);
            BinaryFormatter formatter1 = new BinaryFormatter();
            foreach (var item in users)
            {
                formatter.Serialize(stream1, item);
            }
            stream1.Close();

            User user1 = new User();
            if (PresistenceUser.isEmailExist("almoge@post.bgu.ac.il")) // check exist user
            {
                user1 = aUser.login("almoge@post.bgu.ac.il", "wpmvJ2wa");
            }
            else
            {
                user.Registration("almoge@post.bgu.post.ac.il", "wpmvJ2wa");
                user1 = aUser.login("almoge@post.bgu.ac.il", "wpmvJ2wa"); // check user login
            }


            Console.WriteLine("login check: " + (user1 != null)); // user exist, password correct

            Console.WriteLine("pls enter 1 two times");
            User user3 = aUser.login("almoge", "r4Erf"); // User exist, password worng

            Console.WriteLine("if you enter 2 time one, password check work");

            // if (!PresistenceUser.CheckEmailValid("almog@post")) // check email
            //    Console.WriteLine("cheack email: succed");
            // else
            //    Console.WriteLine("Check email: not succed");

            //if (!PresistenceUser.CheckPassword("t5"))
            //   Console.WriteLine("password check: succed"); // check password
            // else
            //    Console.WriteLine("password check: not succed");

            //User user2 = aUser.login("rami", "123qW");
            //Console.WriteLine("not find user2: " + (user1 == null)); // user dont exist
        }
    }
}
