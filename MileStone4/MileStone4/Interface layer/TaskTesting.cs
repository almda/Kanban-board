using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MileStone4.DataAcces_Layer;
using MileStone4.Business_Layer;

namespace MileStone4.Interface_Layer
{
    class TaskTesting
    {

        public static void test()
        {
            PresistanTasks.load();
            while(true)
            {
                Console.WriteLine();
                Console.Write("please enter action:  ");
                string str = Console.ReadLine();
                switch(str)
                {
                    case ("create"):
                        Console.Write("title: ");
                        string title = Console.ReadLine();
                        Console.Write("description:  ");
                        string des = Console.ReadLine();
                        DateTime time = new DateTime(2019, 4, 2, 10, 10, 10);
                        iTask task = new Task(title, des, time);
                        task.save();
                        break;

                    case ("delete"):
                        Console.Write("enter id:  ");
                        int id = int.Parse(Console.ReadLine());
                        Task.getTaskByID(id).delete();
                        break;

                    case ("update"):
                        Console.Write("title: ");
                        string title2 = Console.ReadLine();
                        Console.Write("description:  ");
                        string des2 = Console.ReadLine();
                        Console.Write("enter id:  ");
                        int id2 = int.Parse(Console.ReadLine());
                        DateTime time2 = new DateTime(2019, 5, 2, 10, 10, 10);
                        Task.getTaskByID(id2).update(title2, des2, time2);
                        break;

                    case ("print"):
                        Console.WriteLine(PresistanTasks.getMaxID());
                        break;

                    case ("clear"):
                        clear();
                        break;
                        

                }
            }


        }


        public static void clear()
        {
            File.Delete("Tasks.bin");
           
            PresistanTasks.load();
        }


        public static void doTest()
        {
            Console.WriteLine("Task Tests:");
            Console.WriteLine("deleting tasks file");
            clear();

            iTask task1 = new Task("task1","", new DateTime(2019, 10, 5));
            task1.save();
            
            iTask task2 = new Task("task2", "task2 description", new DateTime(2020, 10, 5));
            task2.save();
            task1.update(null, "task1 description", null);
            TaskStruct task1struct = task1.toStruct();
            TaskStruct task2struct = task2.toStruct();
            Console.WriteLine("test update, save and create:");
            if(Console.CursorTop != 3)
                Console.WriteLine("false");
            else
                Console.WriteLine("true");

            Console.WriteLine("test if creation worked correctly:");
            if (!task2struct.DueDate.Equals(new DateTime(2020, 10, 5) ) )
                Console.WriteLine("false");
            else
                Console.WriteLine("true");
            Console.WriteLine("test if creation set creaton date correctly");
            if(!task2struct.CreationDate.Date.Equals(DateTime.Now.Date))
                Console.WriteLine("false");
            else
                Console.WriteLine("true");

            Console.WriteLine("chek if update updated correctly:");
            if (!task1struct.DueDate.Equals(new DateTime(2019, 10, 5)))
                Console.WriteLine("false");
            else
                Console.WriteLine("true");


            task1struct = PresistanTasks.getTask(1);
            Console.WriteLine("check if get task correctly, and updated correctly:");
            if(task1struct.Description.Equals(""))
                Console.WriteLine("false");
            else
                Console.WriteLine("true");

            if (!task1struct.Description.Equals("task1 description"))
                Console.WriteLine("false");
            else
                Console.WriteLine("true");

            task2.delete();
            Console.WriteLine("check if delete correctly: ");
            TaskStruct thing = PresistanTasks.getTask(2);
            Console.SetCursorPosition(0, Console.CursorTop -1);
            Console.WriteLine("                                                        ");
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            if (thing.Description != null)
                Console.WriteLine("false");
            else
                Console.WriteLine("true");


            Console.WriteLine("check if saving an illegal task doesn;t save:");
                iTask task3 = new Task("", "task3 description", DateTime.Now);
            task3.save();
            Console.SetCursorPosition(0, Console.CursorTop - 2);
            Console.WriteLine("                                                               ");
            Console.WriteLine("                                                               ");
            Console.SetCursorPosition(0, Console.CursorTop - 2);
            if (PresistanTasks.getMaxID() == 3)
                Console.WriteLine("false");
            else
                Console.WriteLine("true");

        }


    }
}
