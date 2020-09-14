using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MileStone4.Business_Layer;
using MileStone4.DataAcces_Layer;

namespace MileStone4.Interface_Layer
{
    public class ILTask
    {

        public bool visible { get; set; }

        public string title { get; set; }
        public string description { get; set; }
        
        public DateTime dueDate { get; set; }

        public String columnName { get; set; }

        public int ID {get; set;}
       


        public ILTask(String columnName, int taskID, string name)
        {
            this.columnName = columnName;
            Business_Layer.Task task = Business_Layer.Task.GetTaskByID(taskID);
            this.title = task.getTitle();
            this.description = task.getDescription();
            this.dueDate = task.getDueDate();
            this.ID = taskID;
            this.visible = this.title.Equals(name);
        }

        public ILTask(String columnName, int taskID)
        {
            this.columnName = columnName;
            Business_Layer.Task task = Business_Layer.Task.GetTaskByID(taskID);
            this.title = task.getTitle();
            this.description = task.getDescription();
            this.dueDate = task.getDueDate();
            this.ID = taskID;
            this.visible = true;
        }

    }
}
