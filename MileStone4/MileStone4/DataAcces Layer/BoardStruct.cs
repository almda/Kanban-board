using MileStone4.Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone4.DataAcces_Layer
{
    [Serializable]
    public class BoardStruct
    {
        public readonly int Id;
        public readonly string ProjectName;
        public readonly List<ColumnStruct> ColumnList;
        public readonly int totalLimit;

        public BoardStruct() { }

        public BoardStruct(int id, string ProjectName, List<ColumnStruct> colist, int TotalLimit)
        {
            this.Id = id;
            this.ProjectName = ProjectName;
            ColumnList = new List<ColumnStruct>(colist);
            this.totalLimit = TotalLimit;
        }

        override
        public bool Equals(object other)
        {
            if (other is BoardStruct)
                return ((BoardStruct)other).Id == this.Id;
            return false;
        }

        /*public void print()
        {
            Console.WriteLine(Id);
        }*/
    }
}

