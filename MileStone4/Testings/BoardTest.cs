using Microsoft.VisualStudio.TestTools.UnitTesting;

using MileStone4.Business_Layer;
using MileStone4.DataAcces_Layer;
using MileStone4;
using MileStone4.Interface_Layer;

using System;
using System.IO;
using System.Collections.Generic;


namespace Testings
{

    [TestClass]
    public class BaordTest
    {

        [ClassInitialize]
        public static void init(TestContext a)
        {
            DAL.CleanDB();

        }

        [TestMethod]
        public void TestBoardBadCreate()
        {
            try
            {
                InterfaceLayer.createBoard("test", 2, 2, 2, 1);
                Assert.Fail("managed to create bad board");
            }
            catch(AlmogException e)
            {

            }
            catch(Exception e)
            {
                Assert.Fail("exception thrown during creation of bad board");
            }

            try
            {
                Board b = new Board("test", Constants.InfiniteLimit, 1, 1, 5);
                Assert.Fail("managed to create bad board");
            }
            catch (AlmogException e)
            {
                Assert.AreEqual((String)e.Value, "Limits problem");
            }
            catch (Exception e)
            {
                Assert.Fail("exception thrown during creation of bad board");
            }
           
        }

        Board loadedBaord;

        [TestMethod]
        public void testBoardInDB()
        {
            try
            {
                Board b = new Board("test", 1, 1, 1, 5);
                Assert.AreEqual(b.ProjectName, "test");
                loadedBaord = Board.GetBoard(b.Id);
                Assert.AreEqual(b.Id, loadedBaord.Id);
                Assert.AreEqual(b.ProjectName, loadedBaord.ProjectName);


            }
            catch (Exception e)
            {
                Assert.Fail("creating board caused an exception \n " + e.Message);
            }

            Assert.AreEqual(loadedBaord.getColumns().Count, 3);

            testColumnSwitch();

        }

        
        public void testColumnSwitch()
        {
            try
            {
                int idCol1 = loadedBaord.getColumns()[0].Id, idCol2 = loadedBaord.getColumns()[1].Id;
                loadedBaord.switchColumns(idCol1, idCol2);
                Board b3 = Board.GetBoard(loadedBaord.Id);
                Assert.AreEqual(b3.getColumns()[0].Id, idCol2);
            }
            catch (Exception e)
            {
                Assert.Fail("faild to switch columns due to exception \n" + e.Message);
            }

        }


        [ClassCleanup]
        static public void end()
        {
            DAL.CleanDB();
        }
    }
}
