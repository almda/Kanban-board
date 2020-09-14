using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone4.DataAcces_Layer
{
    public class ColumnStruct
    {
        public readonly int Id;
        public readonly string Name;
        public readonly int Limit;

        public ColumnStruct() { }

        public ColumnStruct(int id, string name, int limit)
        {
            this.Id = id;
            this.Name = name;
            this.Limit = limit;
        }

        override
        public bool Equals(object other)
        {
            if (other is ColumnStruct)
                return ((ColumnStruct)other).Id == this.Id;
            return false;
        }
    }
}
