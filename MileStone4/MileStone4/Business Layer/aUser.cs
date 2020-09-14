using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using MileStone4.DataAcces_Layer;

namespace MileStone4.Business_Layer
{

    public abstract class aUser // Abstract class
    {

        // ---------------------- fields ----------------------
        public String Password;
        public String Email;
        public List<int> MyBoard;
        public int BoardCurrentId;

        // ---------------------- constructors ----------------------
        public aUser()
        {
            Password = null;
            Email = null;
            MyBoard = new List<int>();
            BoardCurrentId = Constants.NullID;
        }

        // ---------------------- methods ----------------------
        public abstract void Registration(String email, String password);
        public abstract Boolean CheckEmailUnuiqeness(String Email);
        public void addBoard(int idBoard)
        {
            if (this.MyBoard.Count == 0)
                this.BoardCurrentId = idBoard;
            this.MyBoard.Add(idBoard);
            String[] arr = Email.Split('@');
            UserDAL.saveBoard(arr[0], idBoard);
        }
        public void SelectBoard(int idBoard)
        {
            this.BoardCurrentId = idBoard;
        }
        public void DeleteBoard(int idBoard)
        {
            if (this.MyBoard.Contains(idBoard))
            {
                this.MyBoard.Remove(idBoard);
                String[] arr = Email.Split('@');
                UserDAL.deleteBoard(arr[0], idBoard);
                if (idBoard == BoardCurrentId)
                {
                    if (MyBoard.Count == 0)
                        BoardCurrentId = Constants.NullID;
                    else
                        BoardCurrentId = MyBoard[0];
                }
            }
            else
                throw new Exception("not such a board");
        }
        public Hashtable getBoards(List<int> MyBoards)
        {
            return BoardDAL.boardsNameId(MyBoards);
        }
        public Boolean isNewUser()
        {
            if (this.MyBoard.Count == 0)
                return true;
            return false;
        }
        public static User login(String username, String passuser)
        {
            MileStone4.AlmogException unicorn = new MileStone4.AlmogException();
            unicorn.Value = new List<String>();
            try
            {
                username = username.Split('@')[0]; // turn email to user name
            }
            catch
            {
                ((List<String>)unicorn.Value).Add("Email isn't valid");
            }

            // check if the email exist
            if (!UserDAL.isEmailExist(username))
            {
                ((List<String>)unicorn.Value).Add("Email doesn't exist");
                throw unicorn;
            }

            // check if the password fits the email
            UserStruct? UserDeatils = (UserStruct?)UserDAL.isPasswordExist(username, passuser);
            if (UserDeatils == null)
            {
                ((List<String>)unicorn.Value).Add("Password isn't correct");
                throw unicorn;
            }
            else // login succsess
            {
                MileStone4.DataAcces_Layer.Logger.Log.Info("The user " + username + " loged in");
                return (new User(username, passuser, UserDeatils.Value.getBoard()));
            }

        }
    }
}
