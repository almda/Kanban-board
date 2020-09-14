using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone4.DataAcces_Layer
{
   public static class DAL
    {

        private const String DB = "MyDataBase.db";
        private const String Connection_String = "Data Source=MyDataBase.db;Version=3;";

        public static SQLiteConnection connection = null;

        public static void OpenConnect()
        {
            if (connection == null)
                connection = new SQLiteConnection(Connection_String);
            connection.Open();
        }

        public static void CloseConnect()
        {
            connection.Close();
        }



        /// <summary>
        /// completly delete all data from DB
        /// </summary>
        public static void CleanDB()
        {

            List<String> commands = new List<string>();

            DAL.OpenConnect();

            SQLiteCommand command = new SQLiteCommand(null, DAL.connection);
            command.CommandText = "SELECT name FROM sqlite_master WHERE type='table'";
            command.Prepare();
            SQLiteDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                commands.Add((reader["name"]+""));
            }

            reader.Close();
            command.Dispose();
            DAL.CloseConnect();

            foreach (var item in commands)
            {
                DAL.OpenConnect();
                command = new SQLiteCommand(null, DAL.connection);
                command.CommandText = "DELETE FROM "+item;
                command.Prepare();
                int changes = command.ExecuteNonQuery();
                command.Dispose();
                DAL.CloseConnect();
            }
        }

       

    }
}
