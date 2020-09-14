using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone4
{
    public class AlmogException : Exception
    {
        public object Value { get; set; }
    }
}
