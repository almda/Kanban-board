using MileStone4.DataAcces_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone4.Business_Layer
{
    public interface IBoardSerialization
    {
        void save();
        void delete();
       // void update();
        BoardStruct toStruct();
    }
}
