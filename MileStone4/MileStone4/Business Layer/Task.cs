using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MileStone4.DataAcces_Layer;

namespace MileStone4.Business_Layer
{

    public class Task : iTask
    {
        private static int IDcounter = TaskDAL.GetMaxID() + 1;
        private int ID { get; set; }
        
        private String Title
        {
            get;
            set;
        }

        
        private String Description
        {
            get;
            set;
        }

        private DateTime DueDate { get; set; }

        private readonly DateTime CreationDate;

        private bool Legal = false;

        public Task(String title, String description, DateTime dueDate)
        {
            this.Legal = true;
            this.CreationDate = DateTime.Now.Date;
            validTask(title, description, dueDate);
            this.ID = IDcounter;
            IDcounter++;

            this.Title = title;
            this.Description = description;
            this.DueDate = dueDate;

        }

        public Task(TaskStruct taskStruct)
        {
            this.ID = taskStruct.ID;
            this.Title = taskStruct.Title;
            this.Description = taskStruct.Description;
            this.DueDate = taskStruct.DueDate;
            this.CreationDate = taskStruct.CreationDate;
        }


        public int getID()
        {
            return this.ID;
        }
       
        public TaskStruct toStruct()
        {
            TaskStruct @struct = new TaskStruct(this.ID, this.Title, this.Description, this.DueDate, this.CreationDate);
            return @struct;
        }

        private void validTask(string title, string description, DateTime duedate)
        {
            if (title == null || title.Length > 50 | title.Length == 0)
            {
                Logger.Log.Error("tried to have taks with the illegal title: " + title);
                this.Legal = false;
                AlmogException kipod = new AlmogException();
                kipod.Value = "title cannot be empty or longer than 50 characters";
                throw kipod;
                //Console.WriteLine("title cannot be empty or longer than 50 characters");
            }
            if (description != null && description.Length > 300)
            {
                Logger.Log.Error("tried to have taks with the illegal description: " + description);
                // Console.WriteLine("descreiption cannot be more than 300 characters");
                this.Legal = false;
                AlmogException kipod = new AlmogException();
                kipod.Value = "descreiption cannot be more than 300 characters";
                throw kipod;
            }
            if (duedate.Date.CompareTo(this.CreationDate.Date) < 0)
            {
                Logger.Log.Error("tried to have taks with the illegal dueDate: " + duedate + "; the creationDate is: " + this.CreationDate);
                //  Console.WriteLine("due date cannot be ealier than creation date");
                this.Legal = false;
                AlmogException kipod = new AlmogException();
                kipod.Value = "due date cannot be ealier than creation date";
                throw kipod;
            }


        }


   
        public DateTime getDueDate()
        {
            return this.DueDate;
        }

        public string getTitle()
        {
            return this.Title;
        }

        public string getDescription()
        {
            return this.Description;
        }




        public Func<int, bool> GetSave()
        {
            if (!this.Legal)
            {
                Logger.Log.Error("did not save task id: " + this.ID + " becuse it is illegal.  title: '" + this.Title + "'; desciption: '" + this.Description + "'; dueDate: " + this.DueDate.Date + "; creationDate: " + this.CreationDate.Date);
                AlmogException kipod = new AlmogException();
                kipod.Value = "did not save task because it's illegal";
                throw kipod;
            }
            return (Cid) => { TaskDAL.SaveTask(this.toStruct(), Cid); return true; };
        }


        public Func<int, bool> GetUpdate(string title, string description, DateTime? dueDate)
        {
            Func<int, bool> ans = (Cid) =>
            {
                this.Legal = true;
                String validTitle = title;
                if (title == null)
                    validTitle = this.Title;
                String validDescription = description;
                if (description == null)
                    validDescription = this.Description;
                DateTime validduedate = this.DueDate;
                if (dueDate != null)
                    validduedate = (DateTime)dueDate;

                validTask(validTitle, validDescription, validduedate);

                if (!this.Legal)
                {
                    Logger.Log.Error("did not update task id: " + this.ID + " becuse it is still illegal/  title: '" + validTitle + "'; desciption: '" + validDescription + "'; dueDate: " + validduedate.Date + "; creationDate: " + this.CreationDate.Date);
                    // Console.WriteLine("did not update task because it's illegal");
                    AlmogException kipod = new AlmogException();
                    kipod.Value = "did not update task because it's illegal";
                    throw kipod;
                }
                else
                {

                    if (title != null)
                        this.Title = title;
                    if (description != null)
                        this.Description = description;
                    if (dueDate != null)
                        this.DueDate = (DateTime)dueDate;
                    TaskDAL.UpdateTask(this.toStruct(), Cid);
                    return true;
                }
            };
            return ans;
        }


        public void Delete()
        {
            TaskDAL.DeleteTask(this.ID);
        }


        public static Task GetTaskByID(int id)
        {
            return new Task(TaskDAL.GetTask(id));
        }

        public static void DeleteTask(int id)
        {
            TaskDAL.DeleteTask(id);
        }

        public static List<int> GetTasksByColumnID(int ColumnId)
        {
            return TaskDAL.GetTaskByColumn(ColumnId);
        }

        public static void UpdateColumn(int TaskId, int newColumnId)
        {
            TaskDAL.UpdateTaskColumn(TaskId, newColumnId);
        }

        public static void DeleteTasksByColumn(int column_id)
        {
            TaskDAL.DeleteTasksByColumn(column_id);
        }


    }

}
