using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using MileStone4.Business_Layer;
using MileStone4;

namespace MileStone4.Interface_Layer
{
    public static class InterfaceLayer
    {
        private static aUser logged = null;


        private static Boolean CheckEmailValid(String email)
        {
            if (email == null || email.Length == 0)
            {
                MileStone4.DataAcces_Layer.Logger.Log.Error("the user " + email + " insert null password");
                return false;
            }
            String ans = "";
            MileStone4.AlmogException unicorn = new MileStone4.AlmogException();
            unicorn.Value = new List<String>();
            try
            {
                String[] splitEmail = email.Split('@');  // split the check
                foreach (Char value in splitEmail[0])
                { // is letter?
                    if (122 >= (int)value & (int)value >= 97)
                        ans += value;
                }
                if (ans != splitEmail[0])
                {
                    ((List<String>)unicorn.Value).Add("Username dosen't contains only letters");
                    ((List<String>)unicorn.Value).Add(ans);
                }
                if (splitEmail.Length == 1)
                    ((List<String>)unicorn.Value).Add("Email's ending isn't valid");
                else if (splitEmail.Length >= 2 && splitEmail[1] != "post.bgu.ac.il") // is it an email?
                    ((List<String>)unicorn.Value).Add("Email isn't valid");
            }
            catch (Exception e) // for anything else
            {
                String str = e.Message;
                ((List<String>)unicorn.Value).Add("Email isn't valid");
                MileStone4.DataAcces_Layer.Logger.Log.Error("User " + email + " cause error: " + e.Message);

            }
            if (((List<String>)unicorn.Value).Count != 0) // if there is message, inform 
                throw unicorn;
            return true;
        }

        private static Boolean CheckPassword(String Password)
        {
            int countNum = 0;
            int countCap = 0;
            int countLet = 0;
            if (Password == null || Password.Length < 4 || Password.Length > 20) // password length
                return false;
            foreach (char value in Password)
            {
                if (Char.IsDigit(value))
                    countNum++; // at least one number
                if (97 <= (int)value && (int)value <= 122)
                    countLet++; // at least one letter
                if (60 <= (int)value && (int)value <= 90)
                    countCap++; // at least one capital letter
            }
            return (countNum > 0 & countLet > 0 & countCap > 0);
        }



        public static void login(string Email, string password)
        {
            MileStone4.AlmogException unicorn = new MileStone4.AlmogException();
            unicorn.Value = new List<String>(); // Collection of messages

            if (!(CheckPassword(password))) // password cheack
                ((List<String>)unicorn.Value).Add("Password isn't valid");
            try
            {
                CheckEmailValid(Email);
            }
            catch (MileStone4.AlmogException e)
            {
                for (int i = 0; i < ((List<String>)e.Value).Count; i++) // add message
                    ((List<String>)unicorn.Value).Add(((List<String>)e.Value)[i]);
            }
            if (((List<String>)unicorn.Value).Count == 0 || (((List<String>)unicorn.Value).Count == 1 && ((List<String>)unicorn.Value).Contains("Email's ending isn't valid")))
                logged = aUser.login(Email, password); // create user if nothing disturbing
            else if (((List<String>)unicorn.Value).Count != 0)
                throw unicorn;
        }

        public static void registration(string Email, string password)
        {
            MileStone4.AlmogException unicorn = new MileStone4.AlmogException();
            unicorn.Value = new List<String>();
            aUser user = new User();

            if (!(CheckPassword(password))) // cheack valid
                ((List<String>)unicorn.Value).Add("Password isn't valid");
            try
            {
                CheckEmailValid(Email);
            }
            catch (MileStone4.AlmogException e)
            {
                for (int i = 0; i < ((List<String>)e.Value).Count; i++)
                    ((List<String>)unicorn.Value).Add(((List<String>)e.Value)[i]);
            }
            if (((List<String>)unicorn.Value).Count != 0)
                throw unicorn;

            if (user.CheckEmailUnuiqeness(Email))
                user.Registration(Email, password);
            else
                ((List<String>)unicorn.Value).Add("Email already exists");

            if (((List<String>)unicorn.Value).Count != 0)
                throw unicorn;
        }

        public static void logout()
        {
            logged = null;
        }


        public static void AddToLeft(int ID, Func<int, bool> saveFunc)
        {
            Board board = Board.GetBoard(getBoard());
            board.AddToLeft(ID, saveFunc);
        }

        public static void MoveToNext(int ID)
        {
            Board board = Board.GetBoard(getBoard());
            int newColId = board.MoveToNext(ID);
            if (newColId != Constants.NullID)
                Task.UpdateColumn(ID, newColId);
        }


        public static void setMaxTL(int max, int boardId)
        {
            Board board = Board.GetBoard(boardId);
            board.SetTotalLimit(max);
        }



        public static int getBoard()
        {
            return logged.BoardCurrentId;
        }

        public static Hashtable getBoards()
        {
            return logged.getBoards(logged.MyBoard);
        }

        public static List<int> getTasks(int boardID)
        {
            List<int> ans = new List<int>();
            Board board = Board.GetBoard(boardID);
            ans.AddRange(board.getTasks());
            return ans;
        }

        public static List<int> getTasks()
        {
            return getTasks(getBoard());
        }




        public static ILBoard GetILBoard()
        {
            ILBoard ans = new ILBoard(getBoard());
            return ans;
        }

        public static List<int> GetColumns()
        {
            List<int> ans = new List<int>();
            if (getBoard() == Constants.NullID)
                return ans;
            foreach (Column item in Board.GetBoard(getBoard()).getColumns())
            {
                ans.Add(item.Id);
            }

            return ans;
        }

        public static void switchColumns(int id1, int id2)
        {
            Board.GetBoard(getBoard()).switchColumns(id1, id2);
        }

     	public static void addColumn(String Name, int limit)
        {
            Board b = Board.GetBoard(getBoard());
            b.addColumn(Name, limit);
        }


        public static void DelColumn(int BoardID, int columnID)
        {
            Board b = Board.GetBoard(getBoard());
            if(b.removeColumn(columnID))
                Task.DeleteTasksByColumn(columnID);
        }



        public static int CreateNewTask(String Title, String Description, DateTime DueDate)
        {
            iTask task = new Task(Title, Description, DueDate);
            AddToLeft(task.getID(), task.GetSave());
            return task.getID();
  
        }

        public static void UpdateNewTask(String Title, String Description, DateTime? DueDate, int column_id, int ID)
        {
            iTask task = Task.GetTaskByID(ID);
            task.GetUpdate(Title, Description, DueDate).Invoke(column_id);

        }


        public static void DeleteTask(int ID)
        {
            Task.DeleteTask(ID);
        }


        public static void DeleteBoard(int BoardId)
        {
            logged.DeleteBoard(BoardId);
            Board b = Board.GetBoard(BoardId);
            b.delete();
        }
        private static void AddBoard(int id)
        {
            if (logged == null)
                throw new Exception("user not connect");
            else
            {
                logged.addBoard(id);
            }
        }
        public static void SelectBoard(int idBoard)
        {
            logged.SelectBoard(idBoard);
        }
        public static void createBoard(string Pname, int c1Limit, int c2Limit, int c3Limit, int tLimit)
        {
            Board b = new Board(Pname, c1Limit, c2Limit, c3Limit, tLimit);
            AddBoard(b.Id);
        }
        public static Boolean isNewUser()
        {
            return logged.isNewUser();
        }

    }
}
