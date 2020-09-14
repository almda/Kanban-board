using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone4.DataAcces_Layer
{
    public static class BoardDAL
    {
        public static void saveBoard(BoardStruct board)
        {
            SQLiteCommand command = new SQLiteCommand();
            try
            {
                DAL.OpenConnect();
                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "INSERT INTO Boards(Bid, TotalLimit, Name)  " +
                        " VALUES (@Board_id, @Board_TotalLimit, @Board_Name )";
                SQLiteParameter boardID = new SQLiteParameter("@Board_id", board.Id);
                SQLiteParameter boardLimit = new SQLiteParameter("@Board_TotalLimit", board.totalLimit);
                SQLiteParameter boardName = new SQLiteParameter("@Board_Name", board.ProjectName);
                command.Parameters.Add(boardID);
                command.Parameters.Add(boardLimit);
                command.Parameters.Add(boardName);
                command.Prepare();
                try
                {
                    int changes = command.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    if (e.Message.Contains("database is locked"))
                    {
                        AlmogException unicorn = new AlmogException();

                        unicorn.Value = new List<String>() { "DB locked. please try again later" };
                        throw unicorn;
                    }
                    else
                        throw e;
                }
                command.Dispose();
                DAL.CloseConnect();
                Logger.Log.Info("saved new board with id: " + board.Id + "; name: '" + board.ProjectName + "'; limit: " + board.totalLimit);
            }
            catch(SQLiteException e)
            {
                Logger.Log.Fatal("sql exception when saving new board with id: " + board.Id + "\n error: " + e.Message);
                command.Dispose();
                DAL.CloseConnect();
            }
        }

        public static BoardStruct getBoard(int id)
        {
            BoardStruct ans = new BoardStruct();
            SQLiteCommand command = new SQLiteCommand();
            SQLiteDataReader reader = null;
            try
            {
                List<ColumnStruct> list = ColumnDAL.getByBoard(id);
                DAL.OpenConnect();

                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "SELECT * FROM Boards WHERE Bid = " + id;
                command.Prepare();
                reader = command.ExecuteReader();
                //string ans = "";
                while (reader.Read())
                {
                    int limit = int.Parse("" + reader["TotalLimit"]);

                    ans = new BoardStruct(id, "" + reader["Name"], list, limit);
                    //ans += "id: " + reader["Bid"] + "; limit: " + reader["TotalLimit"] + "; name" + reader["Name"] + "/n";
                }
                command.Dispose();
                reader.Close();
                DAL.CloseConnect();
            }
            catch (SQLiteException e)
            {
                Logger.Log.Fatal("sql exception when retriving board with id: " + id + "\n error: " + e.Message);
                if (reader != null)
                    reader.Close();
                command.Dispose();
                DAL.CloseConnect();
            }
            return ans;
        }
        
 
        public static int getMaxID()
        {
            SQLiteCommand command = new SQLiteCommand();
            SQLiteDataReader reader = null;
            
            try
            {
                int ans = 0;
                DAL.OpenConnect();
                String commandText = "SELECT MAX(Bid) FROM Boards";
                command = new SQLiteCommand(commandText, DAL.connection);
                command.Prepare();
                reader = command.ExecuteReader();
                //  Boolean b = reader.HasRows;
                while (reader.Read())
                {
                    
                    object o = reader[0];
                    if (("" + reader["MAX(Bid)"]).Equals(""))
                        ans = 0;
                    else
                        ans = int.Parse("" + reader["MAX(Bid)"]);
                }
                command.Dispose();
                reader.Close();
                DAL.CloseConnect();
                return ans;
            }
            catch (SQLiteException e)
            {
                Logger.Log.Fatal("sql exception from getting max board id /n" + e.Message);
                if (reader != null)
                    reader.Close();
                command.Dispose();
                DAL.CloseConnect();
                return 0;
            }
        }

        public static void UpdateLimit(int board_id, int newLimit)
        {
            SQLiteCommand command = new SQLiteCommand();    
            try
            {
                DAL.OpenConnect();
                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "UPDATE Boards " +
                        " SET  TotalLimit = @New_Limit   " +
                " WHERE (Bid = @Board_id) ";
                //  command.CommandText = "UPDATE Tasks  SET Title =  @Task_Title , Description = @Task_Des, DueDate = @Task_DD, CreationDate = @Task_CD, Cid = @Column_id  WHERE Tid = @Task_id ";
                SQLiteParameter limit = new SQLiteParameter("@New_Limit", newLimit);
                SQLiteParameter boardID = new SQLiteParameter("@Board_id", board_id);
                command.Parameters.Add(limit);
                command.Parameters.Add(boardID);
                command.Prepare();
                int changes = command.ExecuteNonQuery();
                command.Dispose();

                DAL.CloseConnect();
                if (changes == 0)
                    Logger.Log.Error("faild to update the limit of the board with id: " + board_id + " since it does not exist");
                else
                    Logger.Log.Info("updated the limit of the board with id: " + board_id + "  new limit: " + newLimit);
            }
            catch (SQLiteException e)
            {
                Logger.Log.Fatal("sql exception from updating the limit of the board with id: " + board_id + "/n" + e.Message);
                command.Dispose();
                DAL.CloseConnect();

            }
        }


        private static bool existInUserBoard(int boardId)
        {
            SQLiteCommand command = new SQLiteCommand();
            SQLiteDataReader reader = null;
            try
            {
                DAL.OpenConnect();
                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "SELECT Count(*) FROM UsersBoards Where Bid = " + boardId;
                command.Prepare();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int counter = int.Parse("" + reader["Count(*)"]);
                    if (counter > 0)
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
            catch (SQLiteException e)
            {
                Logger.Log.Fatal("sql exception from checking if board with id: " + boardId +" was connected to any useres /n" + e.Message);
                if (reader != null)
                    reader.Close();
                command.Dispose();
                DAL.CloseConnect();
                return false;
            }
        }




        public static int deleteBoard(int boardId)
        {
            SQLiteCommand command = new SQLiteCommand();
            try
            {
                ColumnDAL.deleteColumnOfBoard(boardId);
                DAL.OpenConnect();
                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "DELETE FROM Boards " + " WHERE (Bid = @boardId) ";

                SQLiteParameter BID = new SQLiteParameter("@boardId", boardId);

                command.Parameters.Add(BID);
                command.Prepare();
                int changes = command.ExecuteNonQuery();
                command.Dispose();
                DAL.CloseConnect();
                if (changes == 0)
                    Logger.Log.Error("faild to delete board with id: " + boardId + " since it does not exist");
                else
                    Logger.Log.Info("deleted board with id: " + boardId);
                return changes;
            }
            catch (SQLiteException e)
            {
                Logger.Log.Fatal("sql exception from deleting board with id: " + boardId + "/n" + e.Message);
                command.Dispose();
                DAL.CloseConnect();
                return 0;
            }
        }


        public static Hashtable boardsNameId(List<int> MyBoards)
        {
            Hashtable output = new Hashtable();
            SQLiteCommand command = new SQLiteCommand();
            SQLiteDataReader reader = null;
            try
            {

                DAL.OpenConnect();
                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "SELECT Bid,Name FROM Boards";
                command.Prepare();
                reader = command.ExecuteReader();
                List<Tuple<int, String>> idList = new List<Tuple<int, String>>();
                while (reader.Read())
                {
                    string name = ("" + reader["Name"]).ToString();
                    int id = int.Parse("" + reader["Bid"]);
                    idList.Add(new Tuple<int, string>(id, name));
                }
                command.Dispose();
                reader.Close();
                DAL.CloseConnect();

                List<int> boards2Del = new List<int>();
                foreach (var board in idList)
                {
                    if (existInUserBoard(board.Item1))
                    {
                        if (MyBoards.Contains(board.Item1))
                            output.Add(board.Item1, board.Item2);
                        else
                            boards2Del.Add(board.Item1);
                    }
                }
                foreach (int id in boards2Del)
                {
                    deleteBoard(id);
                }


            }
            catch (SQLiteException e)
            {
                Logger.Log.Fatal("sql exception from checking if any boards are no longer connected with any user, and deleting said boards /n" + e.Message);
                if (reader != null)
                    reader.Close();
                command.Dispose();
                DAL.CloseConnect();
              
            }
            return output;
        }


    }
}
