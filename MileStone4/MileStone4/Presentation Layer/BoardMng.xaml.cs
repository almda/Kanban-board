using System;
using System.Collections.Generic;
using System.Collections;
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

namespace MileStone4.Presentation_Layer
{
    /// <summary>
    /// Interaction logic for BoardMng.xaml
    /// </summary>
    /// 
    class C : ANotifyPropChange
    {
        private String LimitBacklog;

        public String limitBacklog
        {
            get { return LimitBacklog; }
            set
            {
                LimitBacklog = value;
                OnPropertyChanged();
            }
        }
        private String LimitInProgress;

        public String limitInProgress
        {
            get { return LimitInProgress; }
            set
            {
                LimitInProgress = value;
                OnPropertyChanged();
            }
        }

        private String LimitDone;

        public String limitDone
        {
            get { return LimitDone; }
            set
            {
                LimitDone = value;
                OnPropertyChanged();
            }
        }

        private String TotalLimits;

        public String totalLimits
        {
            get { return TotalLimits; }
            set
            {
                TotalLimits = value;
                OnPropertyChanged();
            }
        }

        private String ProjectName;

        public String projectName
        {
            get { return ProjectName; }
            set
            {
                ProjectName = value;
                OnPropertyChanged();
            }
        }

        private String SaveText;

        public String saveText
        {
            get { return SaveText; }
            set
            {
                SaveText = value;
                OnPropertyChanged();
            }
        }

        private String AddBoardMsg;

        public String addBoardMsg
        {
            get { return AddBoardMsg; }
            set
            {
                AddBoardMsg = value;
                OnPropertyChanged();
            }
        }

        public C()
        {
            this.limitBacklog = "";
            this.limitDone = "";
            this.limitInProgress = "";
            this.totalLimits = "";

        }


    }
    class listOfBoards : ANotifyPropChange
    {
        private Hashtable boardsfull = MileStone4.Interface_Layer.InterfaceLayer.getBoards();
        private List<String> boardsName = new List<string>();
        private CollectionView BoardS;

        public CollectionView boardS
        {
            get { return BoardS; }
            set
            {
                BoardS = value;
                OnPropertyChanged();
            }
        }

        public listOfBoards()
        {
            try
            {
                boardsName.Add("");
                foreach (String item in boardsfull.Values)
                    boardsName.Add(item);
                this.BoardS = new CollectionView(boardsName);
            }
            catch (Exception e)
            {
                this.BoardS = null;
            }
        }
        public void remove(String BoardName)
        {
            this.boardsName.Remove(BoardName);
            this.BoardS = new CollectionView(boardsName);
        }
    }
    class listOfBoardsToDel : ANotifyPropChange
    {
        private Hashtable boardsfull = MileStone4.Interface_Layer.InterfaceLayer.getBoards();
        private List<String> boardsName = new List<string>();
        private CollectionView BoardD;

        public CollectionView boardD
        {
            get { return BoardD; }
            set
            {
                BoardD = value;
                OnPropertyChanged();
            }
        }

        public listOfBoardsToDel()
        {
            try
            {
                boardsName.Add("");
                foreach (String item in boardsfull.Values)
                    boardsName.Add(item);
                this.BoardD = new CollectionView(boardsName);
            }
            catch (Exception e)
            {
                this.BoardD = null;
            }
        }
        public void remove(String BoardName)
        {
            this.boardsName.Remove(BoardName);
            this.BoardD = new CollectionView(boardsName);
        }
    }
    public partial class BoardMng : Window
    {
        C c = new C();
        int BackLogCount = 1;
        int InProgressCount = 1;
        int DoneCount = 1;
        int TotalCount = 1;
        Hashtable boardsfull = MileStone4.Interface_Layer.InterfaceLayer.getBoards();
        private listOfBoards listofboards = new listOfBoards();
        private listOfBoardsToDel listofboardsToDel = new listOfBoardsToDel();
        public BoardMng(String text)
        {
            InitializeComponent();
            String who = text;
            DataContext = c;
            if (who.Length == 0)
                BeforeLogin();
            this.selectBoard.DataContext = listofboards;
            this.deleteBoard.DataContext = listofboardsToDel;

        }
        private void refreshBoards()
        {
            this.boardsfull = MileStone4.Interface_Layer.InterfaceLayer.getBoards();
            this.listofboards = new listOfBoards();
            this.listofboardsToDel = new listOfBoardsToDel();
            this.selectBoard.DataContext = listofboards;
            this.deleteBoard.DataContext = listofboardsToDel;
            int i = listofboards.boardS.Count;
            if (listofboards.boardS.Count == 1)
                BeforeLogin();
        }
        private void BeforeLogin()
        {
            selectBoard.Visibility = Visibility.Hidden;
            selectBoardL.Visibility = Visibility.Hidden;
            deleteBoard.Visibility = Visibility.Hidden;
            deleteBoardL.Visibility = Visibility.Hidden;
            saveSelect.Visibility = Visibility.Hidden;
            saveDelete.Visibility = Visibility.Hidden;
            openKanban.Visibility = Visibility.Hidden;
        }
        private void HaveBoard()
        {
            selectBoard.Visibility = Visibility.Visible;
            selectBoardL.Visibility = Visibility.Visible;
            deleteBoard.Visibility = Visibility.Visible;
            deleteBoardL.Visibility = Visibility.Visible;
            saveSelect.Visibility = Visibility.Visible;
            saveDelete.Visibility = Visibility.Visible;
            openKanban.Visibility = Visibility.Visible;
        }
        private void DownBackLog(object sender, RoutedEventArgs e)
        {
            if (c.limitBacklog != null)
            {
                if (c.limitBacklog.Length != 0)
                    BackLogCount = int.Parse(c.limitBacklog);
                infinity.Visibility = Visibility.Hidden;
                if (BackLogCount > 0)
                    BackLogCount--;
                c.limitBacklog = BackLogCount + "";
                if (BackLogCount == 0)
                {
                    infinity.Visibility = Visibility.Visible;
                    c.limitBacklog = "";
                }
            }
        }
        private void UpBackLog(object sender, RoutedEventArgs e)
        {
            if (c.limitBacklog != null && c.limitBacklog.Length != 0)
                BackLogCount = int.Parse(c.limitBacklog);
            infinity.Visibility = Visibility.Hidden;
            BackLogCount++;
            c.limitBacklog = BackLogCount + "";
            if (BackLogCount == 0)
            {
                infinity.Visibility = Visibility.Visible;
                c.limitBacklog = "";
            }
        }
        private void TextBackLog(object sender, RoutedEventArgs e)
        {
            infinity.Visibility = Visibility.Hidden;
        }
        private void DownInProgress(object sender, RoutedEventArgs e)
        {
            if (c.limitInProgress != null)
            {
                if (c.limitInProgress != null && c.limitInProgress.Length != 0)
                    InProgressCount = int.Parse(c.limitInProgress);
                infinityIP.Visibility = Visibility.Hidden;
                if (InProgressCount > 0)
                    InProgressCount--;
                c.limitInProgress = InProgressCount + "";
                if (InProgressCount == 0)
                {
                    infinityIP.Visibility = Visibility.Visible;
                    c.limitInProgress = "";
                }
            }
        }
        private void UpInProgress(object sender, RoutedEventArgs e)
        {
            if (c.limitInProgress != null && c.limitInProgress.Length != 0)
                InProgressCount = int.Parse(c.limitInProgress);
            infinityIP.Visibility = Visibility.Hidden;
            InProgressCount++;
            c.limitInProgress = InProgressCount + "";
            if (InProgressCount == 0)
            {
                infinityIP.Visibility = Visibility.Visible;
                c.limitInProgress = "";
            }
        }
        private void TextIP(object sender, RoutedEventArgs e)
        {
            infinityIP.Visibility = Visibility.Hidden;
        }
        private void DownDone(object sender, RoutedEventArgs e)
        {
            if (c.limitDone != null)
            {
                if (c.limitDone != null && c.limitDone.Length != 0)
                    DoneCount = int.Parse(c.limitDone);
                infinityDone.Visibility = Visibility.Hidden;
                if (DoneCount > 0)
                    DoneCount--;
                c.limitDone = DoneCount + "";
                if (DoneCount == 0)
                {
                    infinityDone.Visibility = Visibility.Visible;
                    c.limitDone = "";
                }
            }
        }
        private void UpDone(object sender, RoutedEventArgs e)
        {
            if (c.limitDone != null && c.limitDone.Length != 0)
                DoneCount = int.Parse(c.limitDone);
            infinityDone.Visibility = Visibility.Hidden;
            DoneCount++;
            c.limitDone = DoneCount + "";
            if (DoneCount == 0)
            {
                infinityDone.Visibility = Visibility.Visible;
                c.limitDone = "";
            }
        }
        private void TextDone(object sender, RoutedEventArgs e)
        {
            infinityDone.Visibility = Visibility.Hidden;
        }
        private void DownTotal(object sender, RoutedEventArgs e)
        {
            if (c.totalLimits != null)
            {
                if (c.totalLimits != null && c.totalLimits.Length != 0)
                    TotalCount = int.Parse(c.totalLimits);
                infinityTotal.Visibility = Visibility.Hidden;
                if (TotalCount > 0)
                    TotalCount--;
                c.totalLimits = TotalCount + "";
                if (TotalCount == 0)
                {
                    infinityTotal.Visibility = Visibility.Visible;
                    c.totalLimits = "";
                }
            }
        }
        private void UpTotal(object sender, RoutedEventArgs e)
        {
            if (c.totalLimits != null && c.totalLimits.Length != 0)
                TotalCount = int.Parse(c.totalLimits);
            infinityTotal.Visibility = Visibility.Hidden;
            TotalCount++;
            c.totalLimits = TotalCount + "";
            if (TotalCount == 0)
            {
                infinityTotal.Visibility = Visibility.Visible;
                c.totalLimits = "";
            }
        }
        private void TextTotal(object sender, RoutedEventArgs e)
        {
            infinityTotal.Visibility = Visibility.Hidden;
        }
        private int getIDBoard(String BoardName)
        {
            int idD = Constants.NullID;
            if (!BoardName.Equals(""))
            {
                foreach (int key in boardsfull.Keys)
                {
                    if (boardsfull[key].Equals(BoardName))
                        idD = key;
                }
            }
            return idD;
        }
        private void saveS_click(object sender, RoutedEventArgs e)
        {
            try
            {
                String BoardName = listofboards.boardS.CurrentItem.ToString();
                int idA = getIDBoard(BoardName);
                if (idA != Constants.NullID)
                {
                    MileStone4.Interface_Layer.InterfaceLayer.SelectBoard(idA);
                    c.saveText = "The changes have been saved";
                    refreshBoards();
                }
                else
                    c.saveText = c.saveText + "board to switch not selected";
            }
            catch (AlmogException exi)
            {
                c.saveText = c.saveText + "board to delete not selected";
            }
        }
        private void saveD_click(object sender, RoutedEventArgs e)
        {
            try
            {
                String BoardName = listofboardsToDel.boardD.CurrentItem.ToString();
                int idD = getIDBoard(BoardName);
                if (idD != Constants.NullID)
                {
                    MileStone4.Interface_Layer.InterfaceLayer.DeleteBoard(idD);
                    c.saveText = "The changes have been saved";
                    refreshBoards();
                }
                else
                    c.saveText = "board to delete not selected";
            }
            catch (AlmogException exi)
            {
                c.saveText = "board to switch not selected";
            }
        }
        private void addboard_click(object sender, RoutedEventArgs e)
        {
            int LBL = Constants.InfiniteLimit;
            int LIP = Constants.InfiniteLimit;
            int LD = Constants.InfiniteLimit;
            int TL = Constants.InfiniteLimit;
            if (!c.limitBacklog.Equals(""))
                LBL = int.Parse(c.limitBacklog);
            if (!c.limitInProgress.Equals(""))
                LIP = int.Parse(c.limitInProgress);
            if (!c.limitDone.Equals(""))
                LD = int.Parse(c.limitDone);
            if (!c.totalLimits.Equals(""))
                TL = int.Parse(c.totalLimits);
            try
            {
                MileStone4.Interface_Layer.InterfaceLayer.createBoard(c.projectName, LBL, LIP, LD, TL);
                c.addBoardMsg = "Board add to your kanban";
                HaveBoard();
                refreshBoards();
            }
            catch (AlmogException unicorn)
            {
                if (((String)unicorn.Value).Equals("Limits problem"))
                    c.addBoardMsg = "Total limit needs to be \n bigger than sum of all limit";
            }
        }

        private void go_Click(object sender, RoutedEventArgs e)
        {
            BoardGui window = new BoardGui();
            window.Show();
            App.Current.MainWindow = window;
            this.Close();
        }
    }
}
