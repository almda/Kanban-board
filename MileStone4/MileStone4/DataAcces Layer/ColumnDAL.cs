using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone4.DataAcces_Layer
{
    class ColumnDAL
    {
        public static void saveColumn(ColumnStruct column, int BoradLocation, int BoardID)
        {
            SQLiteCommand command = new SQLiteCommand();
            try
            {
                DAL.OpenConnect();
                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "INSERT INTO Columns(Cid, Name, CLimit, Bid, BLocation)  " +
                        " VALUES (@Column_id, @Column_Name, @Column_Limit, @Board_id, @Board_Location )";

                SQLiteParameter ColumnId = new SQLiteParameter("@Column_id", column.Id);
                SQLiteParameter ColumnName = new SQLiteParameter("@Column_Name", column.Name);
                SQLiteParameter ColumnLimit = new SQLiteParameter("@Column_Limit", column.Limit);

                SQLiteParameter boardID = new SQLiteParameter("@Board_id", BoardID);
                SQLiteParameter boardLoc = new SQLiteParameter("@Board_Location", BoradLocation);

                command.Parameters.Add(ColumnId);
                command.Parameters.Add(ColumnName);
                command.Parameters.Add(ColumnLimit);
                command.Parameters.Add(boardID);
                command.Parameters.Add(boardLoc);

                command.Prepare();
                int changes = command.ExecuteNonQuery();
                command.Dispose();
                DAL.CloseConnect();
                Logger.Log.Info("saved new column; id: " + column.Id + "; name: '" + column.Name + "'; limit: " + column.Limit + ";  to board with id: " +  BoardID + "; location: " + BoradLocation);

            }
            catch (SQLiteException e)
            {
                Logger.Log.Fatal("faild to save column with id: " + column.Id + "\n eror: " + e.Message);
                command.Dispose();
                DAL.CloseConnect();
            }
        }

        public static ColumnStruct getColumn(int id)
        {
            SQLiteCommand command = new SQLiteCommand();
            ColumnStruct ans = new ColumnStruct();
            try
            {
                DAL.OpenConnect();

                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "SELECT * FROM Column WHERE Cid = " + id;
                command.Prepare();
                SQLiteDataReader reader = command.ExecuteReader();
                //string ans = "";
                while (reader.Read())
                {
                    int limit = int.Parse("" + reader["CLimit"]);

                    ans = new ColumnStruct(id, "" + reader["Name"], limit);

                }
                command.Dispose();
                reader.Close();
                DAL.CloseConnect();
                return ans;
            }
            catch (SQLiteException e)
            {
                Logger.Log.Fatal("faild to retrive column with id: " + id + "\n eror: " + e.Message);
                command.Dispose();
                DAL.CloseConnect();
                return ans;
            }

        }

        public static int getMaxID()
        {
            SQLiteCommand command = new SQLiteCommand();
            try
            {
                int ans = 0;
                DAL.OpenConnect();
                String commandText = "SELECT MAX(Cid) FROM Columns";
                command = new SQLiteCommand(commandText, DAL.connection);
                command.Prepare();
                SQLiteDataReader reader = command.ExecuteReader();
                //  Boolean b = reader.HasRows;
                while (reader.Read())
                {
                    if (("" + reader["MAX(Cid)"]).Equals(""))
                        ans = 0;
                    else
                        ans = int.Parse("" + reader["MAX(Cid)"]);
                }
                command.Dispose();
                reader.Close();
                DAL.CloseConnect();
                return ans;
            }
            catch (SQLiteException e)
            {
                Logger.Log.Fatal("sql exception from getting max Column id /n" + e.Message);
                command.Dispose();
                DAL.CloseConnect();
                return 0;
            }
        }


        public static List<ColumnStruct> getByBoard(int BoardID)
        {
            SQLiteCommand command = new SQLiteCommand();
            SQLiteDataReader reader = null;
            try
            {
                
                List<ColumnStruct> ans = new List<ColumnStruct>();

                DAL.OpenConnect();
                String commandText = "SELECT * FROM Columns WHERE Bid = @Board_id ORDER BY BLocation";

                command = new SQLiteCommand(commandText, DAL.connection);
                SQLiteParameter boardID = new SQLiteParameter("@Board_id", BoardID);
                command.Parameters.Add(boardID);
                command.Prepare();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int cid = int.Parse("" + reader["Cid"]);
                    int limit = int.Parse("" + reader["CLimit"]);
                    ans.Add(new ColumnStruct(cid, "" + reader["Name"],limit));
                }
                command.Dispose();
                reader.Close();


                DAL.CloseConnect();
          
                return ans;
            }
            catch (SQLiteException e)
            {
                Logger.Log.Fatal("sql exception from getting columns of the board with id: " + BoardID + "/n" + e.Message);
                if (reader != null)
                    reader.Close();
                command.Dispose();
                DAL.CloseConnect();
                return new List<ColumnStruct>();
            }
        }


        public static void UpdateLocation(int Cid, int newLocation)
        {
            SQLiteCommand command = new SQLiteCommand();
            try
            {
                DAL.OpenConnect();
                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "UPDATE Columns " +
                        " SET BLocation =  @NewLocation    " +
                " WHERE Cid = @Column_id ";
               
                SQLiteParameter location = new SQLiteParameter("@NewLocation", newLocation);
                SQLiteParameter ColumnID = new SQLiteParameter("@Column_id", Cid);
                command.Parameters.Add(location);
                command.Parameters.Add(ColumnID);
                command.Prepare();
                int changes = command.ExecuteNonQuery();
                command.Dispose();

                DAL.CloseConnect();
                if (newLocation != Constants.TempColumnLocation)
                {
                    if (changes == 0)
                        Logger.Log.Error("faild to change column location with id: " + Cid + " since it does not exist");
                    else
                        Logger.Log.Info("changed column location for column id: " + Cid + "  new location: " + newLocation);
                }
            }
            catch (SQLiteException e)
            {
                Logger.Log.Fatal("sql exception from changing columnn location. column id: " + Cid + "/n" + e.Message);
                command.Dispose();
                DAL.CloseConnect();

            }
        }

        public static int deleteColumn(int ColumnId)
        {
            SQLiteCommand command = new SQLiteCommand();
            try
            {
                DAL.OpenConnect();
                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "DELETE FROM Columns " + " WHERE (Cid = @ColumnId) ";

                SQLiteParameter columnID = new SQLiteParameter("@ColumnId", ColumnId);

                command.Parameters.Add(columnID);
                command.Prepare();
                int changes = command.ExecuteNonQuery();
                command.Dispose();
                DAL.CloseConnect();
                if (changes == 0)
                    Logger.Log.Error("faild to delete column with id: " + ColumnId + " since it does not exist");
                else
                    Logger.Log.Info("deleted column with id: " + ColumnId);
                return changes;
            }
            catch (SQLiteException e)
            {
                Logger.Log.Fatal("sql exception from deleting column with id: " + ColumnId + "/n" + e.Message);
                command.Dispose();
                DAL.CloseConnect();
                return 0;
            }

        }

        public static void deleteColumnOfBoard(int BoardId)
        {
            List<ColumnStruct> toDelete = new List<ColumnStruct>(getByBoard(BoardId));
            for (int i = 0; i < toDelete.Count; i++)
            {
                deleteColumn(toDelete[i].Id);
                TaskDAL.DeleteTasksByColumn(toDelete[i].Id);
            }

        }
    }
}
