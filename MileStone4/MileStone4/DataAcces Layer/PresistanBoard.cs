using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MileStone4.DataAcces_Layer
{
    public static class PresistanBoard
    {
        private const string BoardFilePath = "Boards.bin";
        private static List<BoardStruct> boards = new List<BoardStruct>();


        static PresistanBoard()
        {
            load();
        }

        public static BoardStruct getBoard(int Id)
        {
            foreach (var item in boards)
            {
                if (item.Id == Id)
                    return item;
            }
            //Console.WriteLine("a board with that id doesn't exist in the database");
            return null;
        }


        public static void load()
        {
            boards = new List<BoardStruct>();
            if (!File.Exists(BoardFilePath))
            {
                Stream s = File.Create(BoardFilePath);
                s.Close();
            }

            Stream stream = File.OpenRead(BoardFilePath);
            BinaryFormatter formatter = new BinaryFormatter();
            while (stream.Position < stream.Length)
            {
                BoardStruct currentBoard = (BoardStruct)formatter.Deserialize(stream);
                boards.Add(currentBoard);
            }
            stream.Close();
        }

        public static void saveBoard(BoardStruct board)
        {
            boards.Add(board);
            Stream stream = File.OpenWrite(BoardFilePath);
            stream.Position = stream.Length;
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, board);
            stream.Close();
        }

        public static void updateBoard(BoardStruct board)
        {
            BoardStruct boardToUpdate = new BoardStruct();
            foreach (var item in boards)
            {
                if (item.Id == board.Id)
                    boardToUpdate = item;
            }
            boards.Remove(boardToUpdate);
            boards.Add(board);
            Stream stream = File.Create(BoardFilePath);
            BinaryFormatter formatter = new BinaryFormatter();
            foreach (var item in boards)
            {
                formatter.Serialize(stream, item);
            }
            stream.Close();
        }

        public static void delete(int Id)
        {
           BoardStruct taskToRemove = new BoardStruct();
            foreach (var item in boards)
            {
                if (item.Id == Id)
                    taskToRemove = item;
            }
            boards.Remove(taskToRemove);
            Stream stream = File.Create(BoardFilePath);
            BinaryFormatter formatter = new BinaryFormatter();
            foreach (var item in boards)
            {
                formatter.Serialize(stream, item);
            }
            stream.Close();
        }

        public static int getMaxID()
        {

            int max = 0;
            foreach (var item in boards)
            {
                if (item.Id > max)
                    max = item.Id;
            }
            return max;
        }

    }
}
