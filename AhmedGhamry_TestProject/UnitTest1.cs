using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using AhmedGhamry_ConsoleApp;
namespace AhmedGhamry_TestProject
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /*
         * Author: AG.
         * Test function to test GetFinalPositionAndPath functionality.
         */
        [TestMethod]
        public void TestGetFinalPositionAndPath()
        {
            PointDir point1 = new PointDir(1,2,PointDir.PointDirection.N);
            PointDir point2 = new PointDir(3, 3, PointDir.PointDirection.E);
            string Command1 = "LMLMLMLMM";
            string Command2 = "MMRMMRMRRM";
            string Result = string.Empty;

            PointDir CurrentPoint1 = PointsManager.GetFinalPositionAndPath(point1, Command1)["FinalPosition"][0];
            PointDir CurrentPoint2 = PointsManager.GetFinalPositionAndPath(point2, Command2)["FinalPosition"][0];

            Result = CurrentPoint1.Position.X.ToString() + " " + CurrentPoint1.Position.Y.ToString() + " " + CurrentPoint1.Direction.ToString();
            StringAssert.Contains(Result, "1 3 N");

            Result = CurrentPoint2.Position.X.ToString() + " " + CurrentPoint2.Position.Y.ToString() + " " + CurrentPoint2.Direction.ToString();
            StringAssert.Contains(Result, "5 1 E");
        }
        /*
         * Author: AG.
         * Test function to test IsIntersect functionality.
         */
        [TestMethod]
        public void TestIsIntersect()
        {
            //Not itersected path
            List<PointDir> List1 = new List<PointDir>();
            List<PointDir> List2 = new List<PointDir>();
            PointDir point1 = new PointDir(1, 2, PointDir.PointDirection.N);
            PointDir point2 = new PointDir(1, 3, PointDir.PointDirection.E);
            PointDir point3 = new PointDir(2, 2, PointDir.PointDirection.S);
            PointDir point4 = new PointDir(3, 2, PointDir.PointDirection.N);
            List1.Add(point1);
            List1.Add(point2);
            List2.Add(point3);
            List2.Add(point4);
            //Intersected path
            List<PointDir> List3 = new List<PointDir>();
            List<PointDir> List4 = new List<PointDir>();
            PointDir point5 = new PointDir(1, 2, PointDir.PointDirection.N);
            PointDir point6 = new PointDir(1, 3, PointDir.PointDirection.E);
            PointDir point7 = new PointDir(1, 2, PointDir.PointDirection.S);
            PointDir point8 = new PointDir(2, 2, PointDir.PointDirection.N);
            List3.Add(point5);
            List3.Add(point6);
            List4.Add(point7);
            List4.Add(point7);

           Assert.IsFalse(PointsManager.IsIntersect(List1,List2));
           Assert.IsTrue(PointsManager.IsIntersect(List3, List4));
        }
    }
}
