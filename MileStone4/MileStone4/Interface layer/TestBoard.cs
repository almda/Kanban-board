using MileStone4;
using MileStone4.Business_Layer;
using MileStone4.DataAcces_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MileStone4.Interface_Layer
{
    class TestBoard
    {
        public static void activateTests()
        { 
            Console.Write("----Board testing unit----\n\n");
            /*Colums Limits Checking*/
            Console.WriteLine("Test 1: creating invalid board 1");
            //-------------------------------------------------
            IBoard test1 = new Board("Test board1", 1, 2, 3, 4);//exeption, not created
            Console.WriteLine("");
            Console.WriteLine("creating invalid board 2");
            //-------------------------------------------------
            IBoard test2 = new Board("Test board1", 1, -1, 1, 70);//exeption, not created
            Console.WriteLine("");

             Console.WriteLine("Test 2: creating valid board");
             //-------------------------------------------------
             Console.Write("Testing the LeftMost board limits --> adding 2 tasks with limit of 1");
             IBoard test3 = new Board("Test board2", 1, 2, 2, 8);
             if(test3.AddToLeft(11111)==true && test3.AddToLeft(22222)==false) Console.WriteLine("...passed");

             Console.Write("Testing next Column limits --> moving task to the next column");
             if (test3.MoveToNext(11111)==true) Console.WriteLine("...passed");

             Console.Write("Testing DoneColumn(defult) limits --> moving a non existing task to next stage");
             if (test3.MoveToNext(11111) == true) Console.WriteLine("...passed");
             Console.Write("Trying to add to non existing column + Adding to empty column");
             if (test3.MoveToNext(11111) == false & test3.AddToLeft(22222)==true) Console.WriteLine("...passed\n");

            Console.WriteLine("Test 3: serialization");
            //-------------------------------------------------
            Console.Write("Testing auto saving");
            PresistanBoard.load();
            if (PresistanBoard.getBoard(1).ProjectName.Equals("Test board2")) Console.WriteLine("...passed");
            
            Console.Write("Testing auto update");
            
            Console.WriteLine("\n---Tests ended gracefully---");
        }
    }
}
