using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using MileStone4;
using MileStone4.DataAcces_Layer;

namespace MileStone4.Business_Layer
{
    public class Board : IBoard,IBoardSerialization
    {
        private static int StaticBoardId = BoardDAL.getMaxID();
       //private static bool saveHelper = false; //needed?

        public int Id;
        public string ProjectName;
        private List<Column> ColumnsList;
        private int totalLimit = Constants.InfiniteLimit;

        public Board(string Pname)
        {
            Id = ++StaticBoardId;
            ProjectName = Pname;
            ColumnsList = new List<Column>();
            Column col = new Column("Backlog", Constants.InfiniteLimit);
            col.GetSave().Invoke(0, this.Id);
            ColumnsList.Add(col);
            col = new Column("InProgress", Constants.InfiniteLimit);
            col.GetSave().Invoke(1, this.Id);
            ColumnsList.Add(col);
            col = new Column("Done", Constants.InfiniteLimit);
            col.GetSave().Invoke(2, this.Id);
            ColumnsList.Add(col);
            totalLimit = Constants.InfiniteLimit;
            save();
        }

        public Board(string Pname, int c1Limit, int c2Limit, int c3Limit, int tLimit)
        {
            checkLimits(c1Limit, c2Limit, c3Limit, tLimit);
            /*try { checkLimits(c1Limit, c2Limit, c3Limit, tLimit); }
            catch(ArgumentException)
            {
                Console.WriteLine("Total limit lower then colums limits");
                return;
            }*/
            Id = ++StaticBoardId;
            ProjectName = Pname;
            ColumnsList = new List<Column>();
            totalLimit = tLimit;
            save();

            Column col = new Column("Backlog", c1Limit);
            col.GetSave().Invoke(0, this.Id);
            ColumnsList.Add(col);
            col = new Column("InProgress", c2Limit);
            col.GetSave().Invoke(1, this.Id);
            ColumnsList.Add(col);
            col = new Column("Done", c3Limit);
            col.GetSave().Invoke(2, this.Id);
            ColumnsList.Add(col);
            
        }

        //From task to Object
        public Board(BoardStruct boardStruct)
        {
            Id = boardStruct.Id;
            ProjectName = boardStruct.ProjectName;
            ColumnsList = new List<Column>();
            foreach (var item in boardStruct.ColumnList)
            {
                ColumnsList.Add(new Column(item));
            }
            totalLimit = boardStruct.totalLimit;
        }

        private void checkLimits(int c1Limit, int c2Limit, int c3Limit, int tLimit)
        {
            if (tLimit!= Constants.InfiniteLimit && (c1Limit== Constants.InfiniteLimit | c2Limit== Constants.InfiniteLimit | c3Limit == Constants.InfiniteLimit)) {
                //throw new ArgumentException();
                AlmogException exp = new AlmogException();
                exp.Value = "Limits problem";
                throw exp;
            }

            if ((c1Limit != Constants.InfiniteLimit & c2Limit != Constants.InfiniteLimit & c3Limit != Constants.InfiniteLimit & tLimit!= Constants.InfiniteLimit) && tLimit < c1Limit + c2Limit + c3Limit)
            {
                //throw new ArgumentException();
                AlmogException exp = new AlmogException();
                exp.Value = "Limits problem";
                throw exp;
            }
        }
       
        public List<Column> getColumns() { return ColumnsList; }

        public bool AddToLeft(int bt, Func<int, bool> saveFunc)
        {
            if (ColumnsList[0].getLimit() == Constants.InfiniteLimit || ColumnsList[0].tasksList.Count < ColumnsList[0].getLimit())
            {
                ColumnsList[0].addTask(bt,  saveFunc);
                return true;
            }
            return false;
        }

        /* This fuction gets a taskId and returns the index of the column (in ColumnsList) wich contains it
         if it not exist, returns constans.NullID*/
        private int findMyColumn(int taskId)
        {
            for(int i=0; i < ColumnsList.Count; i++)
            {
                Column toCheck = ColumnsList[i];
                if (toCheck.tasksList.Contains(taskId)) { return i; }
            }
            return Constants.NullID;
        }

        public int MoveToNext(int bt)
        {
            int sourceColumnIndex = findMyColumn(bt);
            if (sourceColumnIndex== Constants.NullID || ColumnsList.Count == sourceColumnIndex+1) return Constants.NullID; //checks if thats the last column || task id not found
            Column sourceCol = ColumnsList[sourceColumnIndex];
            Column targetCol = ColumnsList[sourceColumnIndex+1];

            if (targetCol.tasksList.Count < targetCol.getLimit() | targetCol.getLimit()== Constants.InfiniteLimit)
            {
                ColumnsList[sourceColumnIndex].tasksList.Remove(bt);
                ColumnsList[sourceColumnIndex+1].tasksList.Add(bt);
            
                return ColumnsList[sourceColumnIndex + 1].Id;
            }
            return Constants.NullID;
        }

        public void SetTotalLimit(int newLimit)
        {
            this.totalLimit = newLimit;
            BoardDAL.UpdateLimit(this.Id, newLimit);
        }

        //public void addColumn(string Cname, int limit)
        //{


        //    Column toAdd = new Column(Cname, limit);
        //    toAdd.GetSave().Invoke(ColumnsList.Count, this.Id);
        //    ColumnsList.Add(toAdd);
        //}

        public void addColumn(string Cname, int limit)
        {
            //check limits
            if (limit == Constants.InfiniteLimit & this.totalLimit != Constants.InfiniteLimit)
            {
                AlmogException exp = new AlmogException();
                exp.Value = "adding the column would make the sum of the columns limit axceed the total limt";
                throw exp;
            }

            int updatedLimit = 0;

            for (int i = 0; i < ColumnsList.Count; i++)
                updatedLimit += ColumnsList[i].getLimit();

            if (updatedLimit + limit > this.totalLimit)
            {
                AlmogException exp = new AlmogException();
                exp.Value = "adding the column would make the sum of the columns limit axceed the total limt";
                throw exp;
            }

                //limits ok
                Column toAdd = new Column(Cname, limit);
            toAdd.GetSave().Invoke(ColumnsList.Count, this.Id);
            ColumnsList.Add(toAdd);
        }


        public bool removeColumn(int id)//id is ColumnID
        {
            for (int i = 0; i < ColumnsList.Count; i++)
                if (ColumnsList[i].Id == id)
                {
                    ColumnsList.Remove(ColumnsList[i]);
                    Column.Delete(id);
                    return true;
                }
            return false;
        }

      

        public bool switchColumns(int id1, int id2)
        {
            int index1= -1, index2=-1;
            for (int i = 0; i < ColumnsList.Count; i++)
            {
                if (ColumnsList[i].Id == id1) index1 = i;
                if (ColumnsList[i].Id == id2) index2 = i;
            }

            if (index1 == -1 || index2 == -1) return false; //not found
            else
            {
                Column temp = ColumnsList[index1];
                
                ColumnsList[index1]= ColumnsList[index2];
                ColumnsList[index2] = temp;
                ColumnsList[index1].UpdateLocation(Constants.TempColumnLocation); // temp change to avoid conflict
                ColumnsList[index2].UpdateLocation(index2);
                ColumnsList[index1].UpdateLocation(index1);

            }
            return true;
        }

        override
        public string ToString()
        {
            return "Id: " + this.Id+"\n"+ "Project: "+ProjectName+"\n";
        }

        public void save()
        {
            BoardDAL.saveBoard(this.toStruct());
        }

        public void delete()
        {
            BoardDAL.deleteBoard(this.Id);
        }

        //public void update()
        //{
        //    //BoardDAL.updateBoard(this.toStruct());
        //}

        public BoardStruct toStruct()
        {
            List<ColumnStruct> list = new List<ColumnStruct>();
            foreach (var item in this.ColumnsList)
            {
                list.Add(item.toStruct());
            }
            BoardStruct @struct = new BoardStruct(this.Id, this.ProjectName, list, this.totalLimit);
            return @struct;
        }

        public List<int> getTasks()
        {
            List<int> tasks = new List<int>();
            foreach (var item in ColumnsList)
            {
                tasks.AddRange(item.GetTasks());
            }
            return tasks;
        }


        public static Board GetBoard(int boardID)
        {
            return new Board(BoardDAL.getBoard(boardID));
        }



    }
}
