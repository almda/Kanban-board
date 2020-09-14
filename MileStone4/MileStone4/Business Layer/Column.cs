using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MileStone4.DataAcces_Layer;

namespace MileStone4.Business_Layer
{[Serializable]
    public class Column
    {
        //private static int StaticColumnId = 0;

        public int Id;
        public string name;
        public List<int> tasksList;
        private int Limit = Constants.InfiniteLimit;

        public Column(string name, int limit)
        {
            if(limit<=0 && limit != Constants.InfiniteLimit)
            {
               // Console.WriteLine("invalid limit");
                return;
            }
            this.Id = ColumnDAL.getMaxID() +1;
            this.name = name;
            this.Limit = limit;
            tasksList=new List<int>();
            
        }

        public Column(ColumnStruct column)
        {
            this.Id = column.Id;
            this.name = column.Name;
            this.Limit = column.Limit;
            this.tasksList = Task.GetTasksByColumnID(column.Id);
        }

        public bool addTask(int taskId, Func<int, bool> saveFunc)
        {
            if (this.Limit != Constants.InfiniteLimit & (tasksList.Count >= Limit || tasksList.Contains(taskId)))
                return false;
            tasksList.Add(taskId);
            saveFunc.Invoke(this.Id);
            return true;
        }

        public bool deleteTask(int taskId)
        {
            if (!tasksList.Contains(taskId))
                return false;
            tasksList.Remove(taskId);
            return true;
        }

        public string getName() { return name; }
        public bool setName(string name)
        {
            if (name == null) return false;
            this.name = name;
            return true;
        }

        public int getLimit() { return Limit; }
        public void setLimit(int newLimit) { Limit = newLimit; }
       


        public List<int> GetTasks()
        {
            return tasksList;   
        }

        public ColumnStruct toStruct()
        {
            return new ColumnStruct(this.Id, this.name, this.Limit);
        }

        public Func<int, int, bool> GetSave()
        {
            return (location, bid) => { ColumnDAL.saveColumn(this.toStruct(), location, bid); return true; };
        }

        public void UpdateLocation(int newLoc)
        {
            ColumnDAL.UpdateLocation(this.Id, newLoc);
        }

        public static void Delete(int Cid)
        {
            ColumnDAL.deleteColumn(Cid);
        }
    }
}
