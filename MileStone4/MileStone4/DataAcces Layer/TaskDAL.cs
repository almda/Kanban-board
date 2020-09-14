using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace MileStone4.DataAcces_Layer
{
    public static class TaskDAL
    {
        public static void SaveTask(TaskStruct task, int Column_id)
        {
            SQLiteCommand command = new SQLiteCommand();
            try
            {
                DAL.OpenConnect();

                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "INSERT INTO Tasks(Tid, Title, Description, DueDate, CreationDate, Cid)  " +
                        " VALUES (@Task_id, @Task_Title, @Task_Des,  @Task_DD, @Task_CD, @Column_id )";
                SQLiteParameter taskID = new SQLiteParameter("@Task_id", task.ID);
                SQLiteParameter taskTitle = new SQLiteParameter("@Task_Title", task.Title);
                SQLiteParameter taskDes = new SQLiteParameter("@Task_Des", task.Description);
                SQLiteParameter taskDD = new SQLiteParameter("@Task_DD", task.DueDate.ToString());
                SQLiteParameter taskCD = new SQLiteParameter("@Task_CD", task.CreationDate.ToString());
                SQLiteParameter ColumnID = new SQLiteParameter("@Column_id", Column_id);
                command.Parameters.Add(taskID);
                command.Parameters.Add(taskTitle);
                command.Parameters.Add(taskDes);
                command.Parameters.Add(taskDD);
                command.Parameters.Add(taskCD);
                command.Parameters.Add(ColumnID);
                command.Prepare();
                int changes = command.ExecuteNonQuery();
                command.Dispose();

                DAL.CloseConnect();
                Logger.Log.Info("saved new task; id: " + task.ID + "; title: '" + task.Title + "'; desciption: '" + task.Description + "'; dueDate: " + task.DueDate.Date + "; creationDate: " + task.CreationDate.Date);
            }
            catch(SQLiteException e)
            {
                Logger.Log.Fatal("sql exception from saving new task with: id: " + task.ID + "; title: '" + task.Title + "'; desciption: '" + task.Description + "'; dueDate: " + task.DueDate.Date + "; creationDate: " + task.CreationDate.Date + "; column id: " + Column_id + "\n " + e);
                command.Dispose();
                DAL.CloseConnect();
            }
        }

        public static TaskStruct GetTask(int Task_id)
        {
            SQLiteCommand command = new SQLiteCommand();
            SQLiteDataReader reader = null;
            try
            {
                string ans = "";
                TaskStruct task = new TaskStruct();
                DAL.OpenConnect();
                String commandText = "SELECT * FROM Tasks WHERE Tid = @Task_id";

                command = new SQLiteCommand(commandText, DAL.connection);
                SQLiteParameter taskID = new SQLiteParameter("@Task_id", Task_id);
                command.Parameters.Add(taskID);
                command.Prepare();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int tid = int.Parse("" + reader["Tid"]);
                    task = new TaskStruct(tid, "" + reader["Title"], "" + reader["Description"], DateTime.Parse((String)reader["DueDate"]), DateTime.Parse((String)reader["CreationDate"]));
                    ans += "id: " + tid + ";    title: " + reader["Title"] + ";    "
                    + "desciption: " + reader["Description"] + ";    DueDate: " + DateTime.Parse((String)reader["DueDate"]) +
                    ";     CreationDate: " + DateTime.Parse((String)reader["CreationDate"]) + ";    Column id: " + reader["Cid"];
                    ans += "\n";
                }
                if (task.Title == null)
                    Logger.Log.Error("failed to get task id:" + Task_id + " since it doesn't exist");
                else
                    Logger.Log.Info("got Task with id: " + Task_id);
                command.Dispose();
                reader.Close();
                
                DAL.CloseConnect();
                return task;
            }
            catch(SQLiteException e)
            {
                Logger.Log.Fatal("sql exception from getting task with id: "+ Task_id + "/n" + e.Message);
                if (reader != null)
                    reader.Close();
                command.Dispose();
                DAL.CloseConnect();
            }
            return new TaskStruct();
        }

        public static List<int> GetTaskByColumn(int column_id)
        {
            SQLiteCommand command = new SQLiteCommand();
            SQLiteDataReader reader = null;
            try
            {
              
                List<int> ans = new List<int>();

                DAL.OpenConnect();
                String commandText = "SELECT Tid FROM Tasks WHERE Cid = @Column_id";

                command = new SQLiteCommand(commandText, DAL.connection);
                SQLiteParameter columnID = new SQLiteParameter("@Column_id", column_id);
                command.Parameters.Add(columnID);
                command.Prepare();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int tid = int.Parse("" + reader["Tid"]);
                    ans.Add(tid);
                }
                command.Dispose();
                reader.Close();
                
                DAL.CloseConnect();
                Logger.Log.Info("got tasks of the column with id: " + column_id);
                return ans;
            }
            catch(SQLiteException e)
            {
                Logger.Log.Fatal("sql exception from getting tasks of the column with id: " + column_id + "/n" + e.Message);
                if (reader != null)
                    reader.Close();
                command.Dispose();
                DAL.CloseConnect();
                return new List<int>();
            }
        }

        public static int GetMaxID()
        {
            SQLiteCommand command = new SQLiteCommand();
            SQLiteDataReader reader = null;
            try
            {
                int ans = 0;
                DAL.OpenConnect();
                String commandText = "SELECT * FROM Tasks WHERE Tid= (SELECT MAX(Tid) FROM Tasks)";
                command = new SQLiteCommand(commandText, DAL.connection);
                command.Prepare();
                reader = command.ExecuteReader();
                //  Boolean b = reader.HasRows;
                while (reader.Read())
                {

                    ans = int.Parse("" + reader["Tid"]);
                }
                command.Dispose();
                reader.Close();
                DAL.CloseConnect();
                return ans;
            }
            catch(SQLiteException e)
            {
                Logger.Log.Fatal("sql exception from getting max task id /n" + e.Message);
                if (reader != null)
                    reader.Close();
                command.Dispose();
                DAL.CloseConnect();
                return 0;
            }
        }

        public static void UpdateTask(TaskStruct task, int Column_id)
        {
            SQLiteCommand command = new SQLiteCommand();
            try
            {
                DAL.OpenConnect();
                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "UPDATE Tasks " +
                        " SET Title =  @Task_Title , Description = @Task_Des, DueDate = @Task_DD, CreationDate = @Task_CD, Cid = @Column_id    " +
                " WHERE (Tid = @Task_id) ";
                //  command.CommandText = "UPDATE Tasks  SET Title =  @Task_Title , Description = @Task_Des, DueDate = @Task_DD, CreationDate = @Task_CD, Cid = @Column_id  WHERE Tid = @Task_id ";
                SQLiteParameter taskID = new SQLiteParameter("@Task_id", task.ID);
                SQLiteParameter taskTitle = new SQLiteParameter("@Task_Title", task.Title);
                SQLiteParameter taskDes = new SQLiteParameter("@Task_Des", task.Description);
                SQLiteParameter taskDD = new SQLiteParameter("@Task_DD", task.DueDate.ToString());
                SQLiteParameter taskCD = new SQLiteParameter("@Task_CD", task.CreationDate.ToString());
                SQLiteParameter ColumnID = new SQLiteParameter("@Column_id", Column_id);
                command.Parameters.Add(taskID);
                command.Parameters.Add(taskTitle);
                command.Parameters.Add(taskDes);
                command.Parameters.Add(taskDD);
                command.Parameters.Add(taskCD);
                command.Parameters.Add(ColumnID);
                command.Prepare();
                int changes = command.ExecuteNonQuery();
                command.Dispose();

                DAL.CloseConnect();
                if(changes == 0)
                    Logger.Log.Error("faild to update task with id: " + task.ID + " since it does not exist");
                else
                    Logger.Log.Info("updated task with id: " + task.ID + "; title: '" + task.Title + "'; desciption: '" + task.Description + "'; dueDate: " + task.DueDate.Date + "; creationDate: " + task.CreationDate.Date);
            }
            catch(SQLiteException e)
            {
                Logger.Log.Fatal("sql exception from updating task with id: "+ task.ID + "/n" + e.Message);
                command.Dispose();
                DAL.CloseConnect();
                
            }
        }

        public static void DeleteTask(int Task_id)
        {
            SQLiteCommand command = new SQLiteCommand();
            try
            {
                DAL.OpenConnect();
                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "DELETE FROM Tasks " + " WHERE (Tid = @Task_id) ";

                SQLiteParameter taskID = new SQLiteParameter("@Task_id", Task_id);

                command.Parameters.Add(taskID);
                command.Prepare();
                int changes = command.ExecuteNonQuery();
                command.Dispose();
                DAL.CloseConnect();
                if (changes == 0)
                    Logger.Log.Error("faild to delete task with id: " + Task_id + " since it does not exist");
                else
                    Logger.Log.Info("deleted task with id: " + Task_id);
            }
            catch(SQLiteException e)
            {
                Logger.Log.Fatal("sql exception from deleting task with id: " + Task_id + "/n" + e.Message);
                command.Dispose();
                DAL.CloseConnect();
            }
        }

        public static void UpdateTaskColumn(int taskId, int Column_id)
        {
            SQLiteCommand command = new SQLiteCommand();
            try
            {
                DAL.OpenConnect();
                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "UPDATE Tasks " +
                        " SET Cid = @Column_id    " +
                " WHERE (Tid = @Task_id) ";
                //  command.CommandText = "UPDATE Tasks  SET Title =  @Task_Title , Description = @Task_Des, DueDate = @Task_DD, CreationDate = @Task_CD, Cid = @Column_id  WHERE Tid = @Task_id ";
                SQLiteParameter taskID = new SQLiteParameter("@Task_id", taskId);
                SQLiteParameter ColumnID = new SQLiteParameter("@Column_id", Column_id);
                command.Parameters.Add(taskID);
                command.Parameters.Add(ColumnID);
                command.Prepare();
                int changes = command.ExecuteNonQuery();
                command.Dispose();

                DAL.CloseConnect();
                if (changes == 0)
                    Logger.Log.Error("faild to update the column of the task with id: " + taskId + " since it does not exist");
                else
                    Logger.Log.Info("updated the column of the task with id: " + taskId );
            }
            catch (SQLiteException e)
            {
                Logger.Log.Fatal("sql exception from updating the column of the task with id: " + taskId + "/n" + e.Message);
                command.Dispose();
                DAL.CloseConnect();

            }
        }


        public static void DeleteTasksByColumn(int ColumnId)
        {
            SQLiteCommand command = new SQLiteCommand();
            try
            {
                DAL.OpenConnect();
                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "DELETE FROM Tasks " + " WHERE (Cid = @Column_ID) ";

                SQLiteParameter ColumnID = new SQLiteParameter("@Column_ID", ColumnId);

                command.Parameters.Add(ColumnID);
                command.Prepare();
                int changes = command.ExecuteNonQuery();
                command.Dispose();
                DAL.CloseConnect();
                if (changes != 0)
                    Logger.Log.Info("deleted all tasks connected to the column with id: "+ ColumnId);
            }
            catch (SQLiteException e)
            {
                Logger.Log.Fatal("sql exception from deleting tasks connected to the column with id: " + ColumnId + "/n" + e.Message);
                command.Dispose();
                DAL.CloseConnect();
            }
        }


    }
}
