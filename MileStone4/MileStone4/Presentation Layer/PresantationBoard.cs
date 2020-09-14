using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MileStone4.Interface_Layer;

namespace MileStone4.Presentation_Layer
{
    class PresantationBoard
    {
        public List<PresantationTask> tasks { get; set; }

        public PresantationBoard(ILBoard board)
        {
            this.tasks = new List<PresantationTask>();
            foreach (var task in board.tasks)
            {
                this.tasks.Add(new PresantationTask(task));
            }
        }

        public Grid toGrid()
        {
            Grid grid = new Grid();
            int counter = 0;
            foreach (var item in tasks)
            {
                RowDefinition row = new RowDefinition();
                row.Height = System.Windows.GridLength.Auto;

                grid.RowDefinitions.Add(row);
                Panel l = item.toPanel();
                Grid.SetColumn(l, 0);
                Grid.SetRow(l, counter);
                grid.Children.Add(l);
                counter++;
            }
            return grid;
        }

        public List<Panel> ToPanelList()
        {
            List<Panel> ans = new List<Panel>();
            foreach (var item in this.tasks)
            {
                ans.Add(item.toPanel());
            }
            return ans;
        }
    }
}
