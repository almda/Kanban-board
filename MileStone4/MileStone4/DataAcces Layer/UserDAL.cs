using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.SQLite;


namespace MileStone4.DataAcces_Layer
{
    static class UserDAL
    {
        public static void saveUser(UserStruct UserDetails)
        {
            SQLiteCommand command = new SQLiteCommand();
            try
            {
                DAL.OpenConnect();
                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "INSERT INTO Users(UserName, Password)  " +
                        " VALUES (@User_UserName, @User_Password)";
                SQLiteParameter UserName = new SQLiteParameter("@User_UserName", UserDetails.getUserName());
                SQLiteParameter Password = new SQLiteParameter("@User_Password", UserDetails.getPassword());
                command.Parameters.Add(UserName);
                command.Parameters.Add(Password);
                command.Prepare();
                int changes = command.ExecuteNonQuery();
                command.Dispose();
                DAL.CloseConnect();
               Logger.Log.Info("The registrtion of " + UserDetails.getUserName() + " is Succeeded");

            }
            catch (SQLiteException e)
            {
                Logger.Log.Fatal("while the saving process of " + UserDetails.getUserName() + " accord, there was an error -" + e.Message);
                command.Dispose();
                DAL.CloseConnect();
            }

        }

        public static void saveBoard(String UserName, int idBoard)
        {
            SQLiteCommand command = new SQLiteCommand();
            try
            {
                DAL.OpenConnect();
                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "INSERT INTO UsersBoards(UName, Bid)  " +
                        " VALUES (@UsersBoards_UName, @UsersBoards_Bid)";
                SQLiteParameter UName = new SQLiteParameter("@UsersBoards_UName", UserName);
                SQLiteParameter Bid = new SQLiteParameter("@UsersBoards_Bid", idBoard);
                command.Parameters.Add(UName);
                command.Parameters.Add(Bid);
                command.Prepare();
                int changes = command.ExecuteNonQuery();
                command.Dispose();
                DAL.CloseConnect();

            }
            catch (SQLiteException e)
            {
                Logger.Log.Fatal("while the saving process of " + UserName + " accord, there was an error -" + e.Message);
                command.Dispose();
                DAL.CloseConnect();
            }
        }

        public static Boolean isEmailExist(String Email)
        {
            SQLiteCommand command = new SQLiteCommand();
            SQLiteDataReader reader = null;
            try
            {
                DAL.OpenConnect();
                String commandText = "SELECT UserName FROM Users";
                command = new SQLiteCommand(commandText, DAL.connection);
                reader = command.ExecuteReader();
                String[] arr = Email.Split('@');
                String username = arr[0];
                String tempUser = "";
                while (reader.Read())
                {
                    tempUser = reader["UserName"].ToString();
                    if (tempUser.Equals(username))
                    {
                        command.Dispose();
                        reader.Close();
                        DAL.CloseConnect();
                        return true;
                    }
                }
                command.Dispose();
                reader.Close();
                DAL.CloseConnect();
                return false;
            }
            catch(SQLiteException e)
            {
                Logger.Log.Fatal("while the checking if the email: "+ Email +" exists, there was an error: " + e.Message);
                if (reader != null)
                    reader.Close();
                command.Dispose();
                DAL.CloseConnect();
                return false;
            }
        }

        public static UserStruct? isPasswordExist(String Email, String Password)
        {
            SQLiteCommand command = new SQLiteCommand();
            SQLiteDataReader reader = null;
            try
            {
                DAL.OpenConnect();
                String commandText = "SELECT * FROM Users";
                command = new SQLiteCommand(commandText, DAL.connection);
                reader = command.ExecuteReader();
                String username = Email.Split('@')[0];
                String name = "";
                Hashtable User = new Hashtable();
                while (reader.Read())
                {
                    name = reader["UserName"].ToString();
                    if (name.Equals(username))
                    {
                        String tempPass = reader["Password"].ToString();
                        if (tempPass.Equals(Password))
                        {
                            User[username] = Password;
                            command.Dispose();
                            reader.Close();
                            DAL.CloseConnect();
                            return new UserStruct(User, getBoards(username));
                        }
                    }
                }
                command.Dispose();
                reader.Close();
                DAL.CloseConnect();
                return null;
            }
            catch(SQLiteException e)
            {
                Logger.Log.Fatal("while the checking if the password: " + Password + " exists with the email: " + Email + " , there was an error: " + e.Message);
                if (reader != null)
                    reader.Close();
                command.Dispose();
                DAL.CloseConnect();
                return null;
            }
        }


        public static List<int> getBoards(String Uname)
        {
            List<int> ans = new List<int>();
            SQLiteCommand command = new SQLiteCommand();
            SQLiteDataReader reader = null;
            try
            {
                DAL.OpenConnect();
                String commandText = "SELECT Bid FROM UsersBoards WHERE UName = @UserName";
                command = new SQLiteCommand(commandText, DAL.connection);
                SQLiteParameter uname = new SQLiteParameter("@UserName", Uname);
                command.Parameters.Add(uname);
                reader = command.ExecuteReader();
                
                Hashtable User = new Hashtable();
                while (reader.Read())
                {
                    ans.Add(int.Parse("" + reader["Bid"]));
                }
                command.Dispose();
                reader.Close();
                DAL.CloseConnect();
            }
            catch(SQLiteException e)
            {
                MileStone4.DataAcces_Layer.Logger.Log.Fatal("while the loading process of " + Uname + " accord, there was an error -" + e.Message);
                if (reader != null)
                    reader.Close();
                command.Dispose();
                DAL.CloseConnect();
            }
            return ans;
        }


        public static void deleteBoard(String UserName, int BoardId)
        {
            SQLiteCommand command = new SQLiteCommand();
            try
            {
                DAL.OpenConnect();
                String commandText = "DELETE FROM UsersBoards WHERE UName = @UserName and Bid = @BoardId";
                command = new SQLiteCommand(commandText, DAL.connection);
                SQLiteParameter uname = new SQLiteParameter("@UserName", UserName);
                SQLiteParameter bid = new SQLiteParameter("@BoardId", BoardId);
                command.Parameters.Add(uname);
                command.Parameters.Add(bid);
                command.Prepare();
                int changes = command.ExecuteNonQuery();
                command.Dispose();
                DAL.CloseConnect();
                if (changes == 0)
                    Logger.Log.Error("faild to delete board with id: " + BoardId + " since it does not exist");
                else
                    Logger.Log.Info("deleted board with id: " + BoardId + " from the user " + UserName);
            }
            catch (SQLiteException e)
            {
                MileStone4.DataAcces_Layer.Logger.Log.Fatal("while delete board " + BoardId + " from the user " + UserName + " accord, there was an error -" + e.Message);
                command.Dispose();
                DAL.CloseConnect();
            }
        }
    }
}
