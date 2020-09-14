using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MileStone4.DataAcces_Layer;

namespace MileStone4.Business_Layer
{
    public interface iTask
    {
        /// <summary>
        /// return the ID of the task
        /// </summary>
        /// <returns></returns>
        int getID();

        DateTime getDueDate();

        String getTitle();
        String getDescription();

        /// <summary>
        /// delete the task from the database
        /// </summary>
        void Delete();

        /// <summary>
        /// updates the task. change the content and updates the database
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Description"></param>
        /// <param name="dueDate"></param>
        Func<int, bool> GetUpdate(string title, string description, DateTime? dueDate);

        /// <summary>
        /// save the task to the database
        /// </summary>
        Func<int, bool> GetSave();

        /// <summary>
        /// creat a TaskStruct with the same fields as the task
        /// </summary>
        /// <returns></returns>
        TaskStruct toStruct();



    }
}
