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
using MileStone4;
using MileStone4.Presentation_Layer;

namespace MileStone4
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    /// 

    class A : ANotifyPropChange
    {
        private String Email;

        public String email
        {
            get { return Email; }
            set
            {
                Email = value;
                OnPropertyChanged();
            }
        }
        private String Password;

        public String password
        {
            get { return Password; }
            set
            {
                Password = value;
                OnPropertyChanged();
            }
        }
        private String Emailmsg;

        public String emailmsg
        {
            get { return Emailmsg; }
            set
            {
                Emailmsg = value;
                OnPropertyChanged();
            }
        }
        private String Passwordmsg;

        public String passwordmsg
        {
            get { return Passwordmsg; }
            set
            {
                Passwordmsg = value;
                OnPropertyChanged();
            }
        }
        private String Emailvalid;

        public String emailvalid
        {
            get { return Emailvalid; }
            set
            {
                Emailvalid = value;
                OnPropertyChanged();
            }
        }
        private String Passwordvalid;

        public String passwordvalid
        {
            get { return Passwordvalid; }
            set
            {
                Passwordvalid = value;
                OnPropertyChanged();
            }
        }





    }

    public partial class login : Window
    {
        A a = new A();
        public login()
        {
            InitializeComponent();
            DataContext = a;
            unicornT.Source = new BitmapImage(new Uri(@"", UriKind.Relative));
        }

        private void CleanButton()
        {
            a.emailmsg = "";
            a.emailvalid = "";
            a.passwordmsg = "";
            a.passwordvalid = "";
        }
        private void ButtomCheck() // textbox valid check 
        {
            MileStone4.AlmogException unicorn = new MileStone4.AlmogException();
            unicorn.Value = new List<String>();
            if (a.email == null)
                ((List<String>)unicorn.Value).Add("Email empty");
            if (a.password == null)
                ((List<String>)unicorn.Value).Add("Password empty");
            if (((List<String>)unicorn.Value).Count != 0)
                throw unicorn;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CleanButton();
            unicornT.Source = new BitmapImage(new Uri(@"Fonts/Talk.png", UriKind.Relative));

            String[] arr;
            CleanButton();
            int check = 0;
            try
            {
                ButtomCheck();
                check = 1;
            }
            catch (AlmogException ex)
            {
                if (((List<String>)ex.Value).Contains("Email empty"))
                {
                    a.emailmsg = "Email empty";
                    //a.emailvalid = "*";
                }
                if (((List<String>)ex.Value).Contains("Password empty"))
                {
                    a.passwordmsg = "Password empty";
                    //a.passwordvalid = "*";
                }

            }
            if (check == 1)
            {
                arr = a.email.Split('@');
                String user = arr[0];
                try
                {
                    MileStone4.Interface_Layer.InterfaceLayer.login(a.email, a.password);
                    MileStone4.DataAcces_Layer.Logger.Log.Info("The user " + user + " loged in");
                    int idB = MileStone4.Interface_Layer.InterfaceLayer.getBoard();
                    String idb = "";
                    if (idB >= 0)
                        idb = idB.ToString();

                    if (MileStone4.Interface_Layer.InterfaceLayer.isNewUser())
                    {
                        BoardMng window1 = new BoardMng(idb);
                        window1.Show();
                        App.Current.MainWindow = window1;
                        this.Close();
                    }
                    else
                    {
                        BoardGui window2 = new BoardGui();
                        window2.Show();
                        App.Current.MainWindow = window2;
                        this.Close();
                    }



                }
                catch (AlmogException exi)
                {
                    emailmsgname.DataContext = a;
                    if (((List<String>)exi.Value).Count == 1 & ((List<String>)exi.Value).Contains("Email's ending isn't valid"))
                    {
                        a.passwordvalid = "Login successful!!!";
                        MileStone4.DataAcces_Layer.Logger.Log.Info("the user " + user + "loged in");
                        BoardGui window = new BoardGui();
                        window.Show();
                        App.Current.MainWindow = window;
                        this.Close();
                    }
                    if (((List<String>)exi.Value).Contains("Email isn't valid"))
                    {
                        a.email = user + "@post.bgu.ac.il";
                        a.emailmsg = "did you mean?";
                        a.emailvalid = "UserName@post.bgu.ac.il";
                    }
                    if (((List<String>)exi.Value).Contains("Username dosen't contains only letters"))
                    {
                        int i = ((List<String>)exi.Value).IndexOf("Username dosen't contains only letters");
                        a.email = ((List<String>)exi.Value)[i + 1];
                        a.emailmsg = "did you mean?";
                        a.emailvalid = "Username can only contain letters";
                    }
                    if (((List<String>)exi.Value).Contains("Email doesn't exist"))
                    {
                        a.emailvalid = "Email doesn't exist";
                        a.emailmsg = "*";
                    }
                    if (((List<String>)exi.Value).Contains("Password isn't correct"))
                    {
                        a.passwordmsg = "Password isn't correct";
                        a.password = "";
                        MileStone4.DataAcces_Layer.Logger.Log.Warn("the user " + user + " password input is incorect");


                    }
                    if (((List<String>)exi.Value).Contains("Password isn't valid"))
                    {
                        a.passwordmsg = "Password isn't valid";
                        a.passwordvalid = "4-20 characters, at least one: \n" +
                            "digital, capital letter and letter";
                        a.password = "";
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            registration window = new registration();
            window.Show();
            App.Current.MainWindow = window;
            this.Close();
        }
    }
}
