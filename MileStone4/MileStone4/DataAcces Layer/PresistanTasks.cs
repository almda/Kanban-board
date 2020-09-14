using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MileStone4.DataAcces_Layer
{

    public static class PresistanTasks
    {
        private const string TaskFilePath = "Tasks.bin";

        private static List<TaskStruct> tasks = new List<TaskStruct>();

    

        static PresistanTasks()
        {
            load();
        }




        public static ICollection<TaskStruct> getTasks(ICollection<int> IDs)
        {
           
            ICollection<TaskStruct> relevantTasks = new List<TaskStruct>();
            foreach (var item in tasks)
            {
                if (IDs.Contains(item.ID))
                    relevantTasks.Add(item);
            }
            return relevantTasks;
        }


        public static TaskStruct getTask(int ID)
        {
           
            foreach (var item in tasks)
            {
                if (item.ID == ID)
                    return item;
            }
            Logger.Log.Error("fiale to ge task id:" + ID + " since it doesn't exist");
            Console.WriteLine("faild to get task since it doesn't exist");
            return new TaskStruct();
           
        }



        public static void saveTask(TaskStruct task)
        {
            Stream stream = File.OpenWrite(TaskFilePath);
            try
            {
                tasks.Add(task);
                stream.Position = stream.Length;
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, task);
                Logger.Log.Info("saved new task; id: " + task.ID + "; title: '" + task.Title + "'; desciption: '" + task.Description + "'; dueDate: " + task.DueDate.Date + "; creationDate: " + task.CreationDate.Date);
            }
            catch (Exception e)
            {
                Logger.Log.Fatal("failed to save new task; id: " + task.ID + "; title: '" + task.Title + "'; desciption: '" + task.Description + "'; dueDate: " + task.DueDate.Date + "; creationDate: " + task.CreationDate.Date +"\n exception " + e);
                if (tasks.Contains(task))
                    delete(task.ID);
                Console.WriteLine("failed to save task because of an excpetion: " + e.Message);
               
            }
            stream.Close();
        }

        public static int getMaxID()
        {
        
            int max = 0;
            foreach (var item in tasks)
            {
                if (item.ID > max)
                    max = item.ID;
            }
            return max;
        }

        public static void delete(int ID)
        {
            
            TaskStruct taskToRemove = new TaskStruct();
            foreach (var item in tasks)
            {
                if (item.ID == ID)
                    taskToRemove = item;
            }
            if (taskToRemove.Title == null) //impossible for real tasks
            {
                Logger.Log.Error("faild to delete task id " + ID + " since it does not exist");
                Console.WriteLine("faild to delete task since it doesn't exist");
            }
            else
            {
                Stream stream = File.Create(TaskFilePath);
                try
                {
                    tasks.Remove(taskToRemove);
                    
                    BinaryFormatter formatter = new BinaryFormatter();
                    foreach (var item in tasks)
                    {
                        formatter.Serialize(stream, item);
                    }
                    
                }
                catch(Exception e)
                {
                    Logger.Log.Fatal("falid to delete task id " + ID +" because of exception " + e);
                    Console.WriteLine("faild to delete task becuse of an exception: " + e.Message);

                }
                stream.Close();
            }
        }

        public static void load()
        {
            Stream stream = File.OpenWrite("temp.txt");
            try
            {
                tasks = new List<TaskStruct>();
                if (!File.Exists(TaskFilePath))
                {
                    Stream s = File.Create(TaskFilePath);
                    s.Close();
                }

                stream = File.OpenRead(TaskFilePath);
                BinaryFormatter formatter = new BinaryFormatter();
                while (stream.Position < stream.Length)
                {

                    TaskStruct currentTask = (TaskStruct)formatter.Deserialize(stream);
                    tasks.Add(currentTask);
                }
                
            }
            catch(Exception e)
            {
                Logger.Log.Fatal("faild to load file cue to excpetion " + e);
                Console.WriteLine("faild to load file due to exception: " + e.Message);

            }
            stream.Close();
        }

        public static void update(TaskStruct task)
        {
            TaskStruct taskToUpdate = new TaskStruct();
            foreach (var item in tasks)
            {
                if (item.ID == task.ID)
                    taskToUpdate = item;
            }
            if (taskToUpdate.Title == null) //impossible for real tasks
            {
                Logger.Log.Error("faile to update task id: " + task.ID + "since id doesnot exist");
                Console.WriteLine("faild to update task since it doesn;t exist");
            }
            else
            {
                Stream stream = File.Create(TaskFilePath);
                try
                {

                    tasks.Remove(taskToUpdate);
                    tasks.Add(task);
                    
                    BinaryFormatter formatter = new BinaryFormatter();
                    foreach (var item in tasks)
                    {
                        formatter.Serialize(stream, item);
                    }
                    
                }
                catch (Exception e)
                {
                    Logger.Log.Fatal("faild to update task id: " + task.ID + " because of excpetion " + e);
                    Console.WriteLine("failed to update task because of an exceptiomn: "+ e.Message);
                        
                }
                stream.Close();
            }
        }


    }
}
