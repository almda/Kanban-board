using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone4.Business_Layer
{
    public interface IBoard
    {
        //get the column's list
        List<Column> getColumns();
        void addColumn(string Cname, int limit);
        bool removeColumn(int id);
        bool switchColumns(int id1, int id2);

        //move between columns
        bool AddToLeft(int bt, Func<int, bool> saveFunc);
        int MoveToNext(int bt);

        //set the limits
        void SetTotalLimit(int newLimit);

        //return a list of all tasks
        List<int> getTasks();

    }
}
