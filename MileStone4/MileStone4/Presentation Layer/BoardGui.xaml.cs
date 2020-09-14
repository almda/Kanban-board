using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MileStone4.Interface_Layer;
using MileStone4.Presentation_Layer;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace MileStone4
{

    class listOfColumns : ANotifyPropChange
    {

        private CollectionView columns1;

        public CollectionView Columns1
        {
            get { return columns1; }
            set
            {
                columns1 = value;
                OnPropertyChanged();
            }
        }

        private CollectionView columns2;

        public CollectionView Columns2
        {
            get { return columns2; }
            set
            {
                columns2 = value;
                OnPropertyChanged();
            }
        }


        private CollectionView columns3;

        public CollectionView Columns3
        {
            get { return columns3; }
            set
            {
                columns3 = value;
                OnPropertyChanged();
            }
        }





        public listOfColumns(List<int> columns)
        {

            this.Columns1 = new CollectionView(columns);
            this.Columns2 = new CollectionView(columns);
            this.Columns3 = new CollectionView(columns);
        }
        public listOfColumns()
        {
            List<int> columns = InterfaceLayer.GetColumns();
            this.Columns1 = new CollectionView(columns);
            this.Columns2 = new CollectionView(columns);
            this.Columns3 = new CollectionView(columns);
        }

        public void refresh()
        {
            List<int> columns = InterfaceLayer.GetColumns();
            this.Columns1 = new CollectionView(columns);
            this.Columns2 = new CollectionView(columns);
            this.Columns3 = new CollectionView(columns);
            OnPropertyChanged();
        }
    }

    class taskCreator
    {
        public String title { get; set; }
        public String des { get; set; }
        public DateTime due { get; set; }
    }


    class taskPanels : ANotifyPropChange
    {
        private List<Panel> Panels;

        public List<Panel> panels
        {
            get { return Panels; }
            set
            {
                Panels = value;
                OnPropertyChanged();
            }
        }



        public taskPanels()
        {
            this.panels = new List<Panel>();
        }


    }

    class errors : ANotifyPropChange
    {
        private String Error;

        public String error
        {
            get { return Error; }
            set
            {
                Error = value;
                OnPropertyChanged();
            }
        }


        public errors()
        {
            error = "";
        }

    }

    class ColumnCreator : ANotifyPropChange
    {
        private String Name;

        public String name
        {
            get { return Name; }
            set
            {
                Name = value;
                OnPropertyChanged();
            }
        }

        private String Limit;

        public String limit
        {
            get { return Limit; }
            set
            {
                Limit = value;
                OnPropertyChanged();
            }
        }

        public ColumnCreator()
        {
            name = "";
            limit = "";
        }


    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BoardGui : Window
    {
        private static taskPanels panles = new taskPanels();
        // private static ItemsControl itemControler;
        //  private static Panel CreatorPanel;
        //  private static Panel SwitchPanel;
        private static taskCreator taskC = new taskCreator();
        private static listOfColumns columns = new listOfColumns();

        private static IComparer<ILTask> comparer = new Interface_Layer.Comperators.ColumnComperator();

        private static errors errors = new errors();

        private static ColumnCreator ColumnCreator = new ColumnCreator();

        public BoardGui()
        {
            InitializeComponent();
            itemcon.DataContext = panles;
            this.TaskCreater.DataContext = taskC;
            ErrorsLabel.DataContext = errors;
            this.SwitcherPanel.DataContext = columns;
            this.ColumnCanvas.DataContext = ColumnCreator;
            this.deleteCol.DataContext = columns;
            refresh();
        }
        public BoardGui(bool t)
        {

        }



        private void SwitchButton_Click(object sender, RoutedEventArgs e)
        {
            int id1 = int.Parse(columns.Columns1.CurrentItem.ToString());
            int id2 = int.Parse(columns.Columns2.CurrentItem.ToString());
            try
            {
                InterfaceLayer.switchColumns(id1, id2);
                errors.error = "";
                refresh();
            }
            catch (AlmogException exi)
            {
                errors.error = (String)exi.Value;
            }
        }



        private void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // InterfaceLayer.createTask(taskC.title, taskC.des, taskC.due, InterfaceLayer.getBoard());
                InterfaceLayer.CreateNewTask(taskC.title, taskC.des, taskC.due);
                errors.error = "";
                refresh();
            }
            catch (AlmogException exi)
            {
                errors.error = (String)exi.Value;
            }


        }

        public void move_Click(object sender, RoutedEventArgs e)
        {
            //InitializeComponent();
            Button button = (Button)sender;

            PresantationTask task = (PresantationTask)button.DataContext;
            try
            {
                InterfaceLayer.MoveToNext(task.ID);
                errors.error = "";
                refresh();

            }
            catch (AlmogException exi)
            {
                errors.error = (String)exi.Value;
            }

        }


        /*
        public void start()
        {
            File.Delete("Boards.bin");
            File.Delete("Tasks.bin");
            File.Delete("Users.bin");
            DataAcces_Layer.PresistanTasks.load();
            DataAcces_Layer.PresistanBoard.load();
            DataAcces_Layer.PresistenceUser.load();

            InterfaceLayer.registration("aaa@post.bgu.ac.il", "aaaAAA1");
            InterfaceLayer.login("aaa@post.bgu.ac.il", "aaaAAA1");
                 InterfaceLayer.createTask("asadasd", "asdasd", DateTime.Now, InterfaceLayer.getBoard());
               InterfaceLayer.createTask("zzzzz", "aaaaa", DateTime.Now, InterfaceLayer.getBoard());


           
        }
        */




        private void refresh()
        {
            ILBoard ILb = new ILBoard(InterfaceLayer.getBoard());
            ILb.Sort(comparer);
            PresantationBoard Pb = new PresantationBoard(ILb);
            panles.panels = Pb.ToPanelList();

            columns.refresh();

            //  Binding binding = new Binding("panels");
            //  BindingOperations.SetBinding(itemControler, ItemsControl.ItemsSourceProperty, binding);

            //   InitializeComponent();
            // binding = new Binding("error");
            //  BindingOperations.SetBinding(this.ErrorsLabel, Label.ContentProperty, binding);

        }

        private void Column_Sort(object sender, RoutedEventArgs e)
        {
            comparer = new Interface_Layer.Comperators.ColumnComperator();
            refresh();
        }

        private void Title_Sort(object sender, RoutedEventArgs e)
        {
            comparer = new Interface_Layer.Comperators.TitleComperator();
            refresh();
        }

        private void Des_Sort(object sender, RoutedEventArgs e)
        {
            comparer = new Interface_Layer.Comperators.DescriptionComperatos();
            refresh();
        }

        private void Date_Sort(object sender, RoutedEventArgs e)
        {
            comparer = new Interface_Layer.Comperators.DateComperator();
            refresh();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void add_column(object sender, RoutedEventArgs e)
        {
            int limit = int.Parse(ColumnCreator.limit);
            if (limit == 0)
                limit = Constants.InfiniteLimit;
            try
            {
                InterfaceLayer.addColumn(ColumnCreator.name, limit);
                ColumnCreator.limit = "";
                ColumnCreator.name = "";
                errors.error = "";
                refresh();
            }
            catch(AlmogException exi)
            {
                errors.error = (String)exi.Value;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InterfaceLayer.logout();
            login window = new login();
            window.Show();
            App.Current.MainWindow = window;
            this.Close();

        }

        private void delCol(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(columns.Columns3.CurrentItem.ToString());
            try
            {
                InterfaceLayer.DelColumn(InterfaceLayer.getBoard(), id);
                errors.error = "";
                refresh();
            }
            catch (AlmogException exi)
            {
                errors.error = (String)exi.Value;
            }
        }

        private void filter(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            ILBoard ILb = new ILBoard(InterfaceLayer.getBoard(), Filter.Text);
            
            PresantationBoard Pb = new PresantationBoard(ILb);
            panles.panels = Pb.ToPanelList();

            columns.refresh();

        }

        private void showall(object sender, RoutedEventArgs e)
        {
            refresh();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BoardMng window = new BoardMng(InterfaceLayer.getBoard() + "");
            window.Show();
            App.Current.MainWindow = window;
            this.Close();
        }
    }
}
