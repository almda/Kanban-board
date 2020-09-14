﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MileStone4.Interface_Layer;

namespace MileStone4.Interface_Layer.Comperators
{
    class DescriptionComperatos : IComparer<ILTask>
    {
        public int Compare(ILTask x, ILTask y)
        {
            return x.description.CompareTo(y.description);
        }
    }
}
