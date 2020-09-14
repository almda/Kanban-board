using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using MileStone4.Business_Layer;
using System.Data.SQLite;

namespace MileStone4
{



    class thingy
    {
        public List<string> list { get; set; }

        public thingy()
        {
            this.list = new List<string>();
        }
    }


    class thingy2
    {
        public List<Panel> list { get; set; }

        public thingy2()
        {
            this.list = new List<Panel>();
        }
    }

    /// <summary>
    /// Interaction logic for tester.xaml
    /// </summary>
    /// 
    public partial class tester : Window
    {
        public tester()
        {
            InitializeComponent();
            a();
        
        }

        private void a()
        {
            DataAcces_Layer.DAL.CleanDB();
        }

        private void b()
        {
            int i = 0;
            DataAcces_Layer.DAL.OpenConnect();
            SQLiteCommand c = new SQLiteCommand("SELECT * FROM Tasks", DataAcces_Layer.DAL.connection);
            SQLiteDataReader r = c.ExecuteReader();
            c.Dispose();
            
            r.Close();
            
            DataAcces_Layer.DAL.CloseConnect();
            
            MessageBox.Show("asd");
        }
      

    }
}
