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
    /// Interaction logic for registration.xaml
    /// </summary>
    /// 
    class B : ANotifyPropChange
    {
        /*
        public String email { get; set; }
        public String password { get; set; }
        public String emailmsg { get; set; }
        public String passwordmsg { get; set; }
        public String emailvalid { get; set; }
        public String passwordvalid { get; set; }
        */
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
        private String PasswordC;

        public String passwordC
        {
            get { return PasswordC; }
            set
            {
                PasswordC = value;
                OnPropertyChanged();
            }
        }



    }

    public partial class registration : Window
    {
        B a = new B();
        public registration()
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
            String pass = "c";
            String passC = "t";
            MileStone4.AlmogException unicorn = new MileStone4.AlmogException();
            unicorn.Value = new List<String>();
            if (a.email == null)
                ((List<String>)unicorn.Value).Add("Email empty");
            if (a.password == null)
                ((List<String>)unicorn.Value).Add("Password empty");
            else
                pass = a.password;
            if (a.passwordC == null)
                ((List<String>)unicorn.Value).Add("PasswordC empty");
            else
                passC = a.passwordC;
            if (!(pass.Equals(passC)) & (pass != "c") & (passC != "t"))
                ((List<String>)unicorn.Value).Add("Password isn't confirm");
            if (((List<String>)unicorn.Value).Count != 0)
                throw unicorn;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //unicornI.Source = new BitmapImage(new Uri(@"Fonts/unicorn.png", UriKind.Relative));
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
                if (((List<String>)ex.Value).Contains("PasswordC empty"))
                {
                    a.passwordmsg = "Password confirm empty";
                    //a.passwordvalid = "*";
                    a.password = "";
                    a.passwordC = "";
                }
                if (((List<String>)ex.Value).Contains("Password isn't confirm"))
                {
                    a.passwordmsg = "Password not match";
                    //a.passwordvalid = "*";
                    a.password = "";
                    a.passwordC = "";
                }

            }

            if (check == 1)
            {
                arr = a.email.Split('@');
                try
                {
                    MileStone4.Interface_Layer.InterfaceLayer.registration(a.email, a.password);
                    a.passwordmsg = "Registration successful!!!";
                    MileStone4.DataAcces_Layer.Logger.Log.Info("the user '" + arr[0] + "' is now register");
                }

                catch (AlmogException exi)
                {
                    emailmsgname.DataContext = a;
                    //String str = exi.Message;
                    if (((List<String>)exi.Value).Contains("Email isn't valid") || ((List<String>)exi.Value).Contains("Email's ending isn't valid"))
                    {
                        a.emailmsg = "did you mean?";
                        a.email = arr[0] + "@post.bgu.ac.il";
                        a.emailvalid = "UserName@post.bgu.ac.il";
                    }
                    if (((List<String>)exi.Value).Contains("Username dosen't contains only letters"))
                    {
                        int i = ((List<String>)exi.Value).IndexOf("Username dosen't contains only letters");
                        a.email = ((List<String>)exi.Value)[i + 1] + "@post.bgu.ac.il";
                        a.emailvalid = "Username can only contain letters";
                        a.emailmsg = "did you mean?";
                    }
                    if (((List<String>)exi.Value).Contains("Email already exists"))
                    {
                        MileStone4.DataAcces_Layer.Logger.Log.Warn("There was an atempt to register with this user - " + arr[0]);
                        a.emailmsg = "Email already exists";
                        //a.emailvalid = "*";
                    }
                    if (((List<String>)exi.Value).Contains("Password isn't valid"))
                    {
                        MileStone4.DataAcces_Layer.Logger.Log.Warn("The user " + arr[0] + " insert the worng password");
                        a.passwordmsg = "Password isn't valid";
                        a.passwordvalid = "4-20 characters, at least one: \n" +
                            "digital, capital letter and letter";
                        a.password = "";
                        a.passwordC = "";
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            login window = new login();
            window.Show();
            App.Current.MainWindow = window;
            this.Close();
        }

        
    }
}
