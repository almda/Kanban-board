using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone4.DataAcces_Layer
{
    [Serializable]
    public struct TaskStruct
    {
        public readonly int ID;
        public readonly String Title;
        public readonly String Description;
        public readonly DateTime DueDate;
        public readonly DateTime CreationDate;
        
        public TaskStruct(int id, string title, string description, DateTime duedate, DateTime creationdate)
        {
            this.ID = id;
            this.Title = title;
            this.Description = description;
            this.DueDate = duedate;
            this.CreationDate = creationdate;
        }

        


        override
        public bool Equals(object other)
        {
            if (other is TaskStruct)
                return ((TaskStruct)other).ID == this.ID;
            return false;
        }

        /*
        public void print()
        {
            Console.WriteLine(ID);
        }*/
    }
}
