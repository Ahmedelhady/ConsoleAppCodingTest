using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace AhmedGhamry_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Intialize variables
            Point UpperRightCoord = new Point();
            PointDir SrtPoint = new PointDir();
            int Count = 1;
            bool IsDone = false;
            bool AnotherRobot = false;
            string Command = string.Empty;  
            Dictionary<string, string> CheepdogsStartPositions = new Dictionary<string, string>();
            Dictionary<string, string> CheepdogsCommands = new Dictionary<string, string>();
            string Line = string.Empty;
            string[] LineArray;
            Console.WriteLine("Please Enter Upper right coordinates followed by sheepdogs position then instructions, press Enter two time when finished!!");
            // Get Upper right coordinates
            while (true)
            {
                Line = Console.ReadLine();
                //Check if the input input correct
                if (PointsManager.CheckInputs(Line, 1, UpperRightCoord))
                {
                    LineArray = Line.Trim().Split(' ');
                    UpperRightCoord.X = int.Parse(LineArray[0]);
                    UpperRightCoord.Y = int.Parse(LineArray[1]);
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter valid coordinates separated by space character!!");
                    continue;
                }
            }
            // Get robot initial position
            while (true)
            {
                if (!IsDone)
                {
                    while (true)
                    {
                        if (!AnotherRobot)
                            Line = Console.ReadLine();
                        //Check if the input data correct
                        if (PointsManager.CheckInputs(Line, 2, UpperRightCoord))
                        {
                            CheepdogsStartPositions[Count.ToString()] = Line;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please enter valid coordinates and position separated by space character!!");
                            continue;
                        }
                    }
                    while (true)
                    {
                        // Get the commands
                        Line = Console.ReadLine();
                        ///check if the input commands correct
                        if (PointsManager.CheckInputs(Line, 3, UpperRightCoord))
                        {
                            Command = Line.Trim().ToUpper();
                            CheepdogsCommands[Count.ToString()] = Command;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please enter valid commands!!");
                            continue;
                        }
                    }
                    Count++;
                    Line = Console.ReadLine();
                    if (Line == "")
                        IsDone = true;
                    else
                        AnotherRobot = true;
                }
                else
                    break;
            }
            Console.WriteLine("Results: ");

            for (int k = 1; k <= CheepdogsStartPositions.Count; k++)
            {
                // Intialize variables
                string cmd=string.Empty;
                bool Done = false;
                LineArray = CheepdogsStartPositions[k.ToString()].Trim().Split(' ');
                Dictionary<string, List<PointDir>> ResultDic = new Dictionary<string, List<PointDir>>();
                // Get the robot's final position and path.
                SrtPoint = new PointDir(int.Parse(LineArray[0]),
                    int.Parse(LineArray[1]),
                    (PointDir.PointDirection)Enum.Parse(typeof(PointDir.PointDirection),
                    LineArray[2].ToString().ToUpper()));

                cmd = CheepdogsCommands[k.ToString()];

                ResultDic = PointsManager.GetFinalPositionAndPath(SrtPoint, cmd);
                PointDir CurrentPoint = ResultDic["FinalPosition"][0];

                if (CurrentPoint.Position.X > UpperRightCoord.X ||
                    CurrentPoint.Position.Y > UpperRightCoord.Y ||
                    CurrentPoint.Position.X <0 ||
                    CurrentPoint.Position.Y <0)
                {
                    Console.WriteLine("Your commands are not valid,robot number: " + k.ToString() + " will get out from the paddock!!");
                    break;
                }
                
                Console.Write(CurrentPoint.Position.X + " ");
                Console.Write(CurrentPoint.Position.Y + " ");
                Console.Write(CurrentPoint.Direction);

                Console.WriteLine("");
                //check if the robots paths intersect or not.
                foreach (string key in CheepdogsStartPositions.Keys)
                {
                    if (key != k.ToString())
                    {
                        Dictionary<string, List<PointDir>> ResultDictwo = new Dictionary<string, List<PointDir>>();
                        LineArray = CheepdogsStartPositions[key].Trim().Split(' ');

                        SrtPoint = new PointDir(int.Parse(LineArray[0]),
                        int.Parse(LineArray[1]),
                        (PointDir.PointDirection)Enum.Parse(typeof(PointDir.PointDirection),
                        LineArray[2].ToString().ToUpper()));

                        cmd = CheepdogsCommands[key];

                        ResultDictwo = PointsManager.GetFinalPositionAndPath(SrtPoint, cmd);
                        if (PointsManager.IsIntersect(ResultDic["PathPoints"], ResultDictwo["PathPoints"]))
                        {
                            Console.WriteLine("Robot number: " + k.ToString() + " Intersect with Robot number: " + key + "!!");
                            Done = true;
                            break;
                        }
                    }
                }
                if (Done)
                    break;
            }

            Console.ReadLine();

        }

        
    }
}
