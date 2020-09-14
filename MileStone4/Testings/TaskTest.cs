using Microsoft.VisualStudio.TestTools.UnitTesting;

using MileStone4.Business_Layer;

using System;
using System.IO;
using System.Collections.Generic;
using MileStone4.DataAcces_Layer;
using MileStone4.Interface_Layer;
using MileStone4;


namespace Testings
{
    [TestClass]
    public class TaskTest
    {
        [ClassInitialize]
        public static void init(TestContext a)
        {
            DAL.CleanDB();
            InterfaceLayer.registration("aaa@post.bgu.ac.il", "aaaA1");
            InterfaceLayer.login("aaa", "aaaA1");
            InterfaceLayer.createBoard("board", Constants.InfiniteLimit, Constants.InfiniteLimit, Constants.InfiniteLimit, Constants.InfiniteLimit);
        }
        
      

        [TestMethod]
        public void TestCreation()
        {
            try
            {
                
                iTask task1 = new Task("testCre", "task1", new DateTime(2019, 12, 12));
                Assert.AreEqual(task1.getTitle(), "testCre");
              
                
            }
            catch(MileStone4.AlmogException ex)
            {
                Assert.Fail((String)ex.Value, new object[] { ex });
            }
            catch(Exception e)
            {
                Assert.Fail(e.Message, new object[] { e});
            }
        }

        [TestMethod]
        public void TestBadCreation()
        {
            try
            {
                // note - createnewTask also saves the task. the error would accore when trying to save
                InterfaceLayer.CreateNewTask("testCre", "task1", new DateTime(2018, 12, 12));

                Assert.Fail("saved although bad");

            }
            catch (MileStone4.AlmogException ex)
            {
                //good
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message, new object[] { e });
            }
        }


        private static iTask Saved_Task = null;
        [TestMethod]
        public void TestSave()
        {
            try
            {
                // task1 = new Task("testCre", "task1", new DateTime(2019, 12, 12));
                int id =  InterfaceLayer.CreateNewTask("testCre", "task1", new DateTime(2019, 12, 12));


                Saved_Task = Task.GetTaskByID(id);
                Assert.AreEqual(Saved_Task.getDescription(), "task1");

                List<int> list = TaskDAL.GetTaskByColumn(1);
                if (!list.Contains(Saved_Task.getID()))
                    Assert.Fail("didn't save the task to the correct column");

            }
            catch (MileStone4.AlmogException ex)
            {
                Assert.Fail((String)ex.Value, new object[] { ex });
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message, new object[] { e });
            }
        }


        [TestMethod]
        public void TestUpdate()
        {
            try
            {
                Saved_Task.GetUpdate("updatedTask1", "updateedDes", null).Invoke(1);
                
                iTask task2 = Task.GetTaskByID(Saved_Task.getID());
                Assert.AreEqual(task2.getTitle(), "updatedTask1");

            }
            catch (MileStone4.AlmogException ex)
            {
                Assert.Fail((String)ex.Value, new object[] { ex });
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message, new object[] { e });
            }
        }

        [TestMethod]
        public void TestMoveColumn()
        {
            try
            {
                InterfaceLayer.MoveToNext(Saved_Task.getID());

                List<int> list = TaskDAL.GetTaskByColumn(2);
                if (!list.Contains(Saved_Task.getID()))
                    Assert.Fail("didn't move the task to the correct column");

            }
            catch (MileStone4.AlmogException ex)
            {
                Assert.Fail((String)ex.Value, new object[] { ex });
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message, new object[] { e });
            }
        }


        
        [TestMethod]
        public void TestDelete()
        {
            try
            {
                InterfaceLayer.DeleteTask(Saved_Task.getID());
                Task t2 = Task.GetTaskByID(Saved_Task.getID());
                Assert.AreNotEqual(Saved_Task.getID(), t2.getID());
                Assert.AreEqual(t2.getTitle(), null);

            }
            catch (MileStone4.AlmogException ex)
            {
                Assert.Fail((String)ex.Value, new object[] { ex });
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message, new object[] { e });
            }
        }

        [ClassCleanup]
        static public void end()
        {
            InterfaceLayer.logout();
            DAL.CleanDB();
        }

    }
}
