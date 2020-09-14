using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;
using MileStone4.Business_Layer;

namespace MileStone4.Interface_Layer
{
    public class ILBoard
    {
        public List<ILTask> tasks { get; set; }

        public ILBoard(int boardID)
        {
            tasks = new List<ILTask>();
            List<int> list = new List<int>();
            if (boardID != Constants.NullID)
            {
                Board b = Board.GetBoard(boardID);
                foreach (var column in b.getColumns())
                {
                    foreach (var item in column.GetTasks())
                    {
                        tasks.Add(new ILTask(column.name, item));
                    }

                }
            }
        }

        public void Sort(IComparer<ILTask> comparer)
        {
            tasks.Sort(comparer);
        }

        public ILBoard(int boardID, String name)
        {
            tasks = new List<ILTask>();
            List<int> list = new List<int>();
            Board b = Board.GetBoard(boardID);
            foreach (var column in b.getColumns())
            {
                foreach (var item in column.GetTasks())
                {
                    tasks.Add(new ILTask(column.name, item, name));
                }

            }
        }

        public void add(ILTask t)
        {
            tasks.Add(t);
        }

 

    }
}
