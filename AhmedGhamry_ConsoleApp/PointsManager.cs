using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AhmedGhamry_ConsoleApp
{
    public class PointsManager
    {
        public enum Commands
        {
            L,
            R,
            M
        };
      /*
       * Author: AG.
       * Function Input: Start point and direction and input commands.
       * Function Output: Dictionary of the final point and the path points.
       */
        public static Dictionary<string, List<PointDir>> GetFinalPositionAndPath(PointDir StartPoint, string Command)
        {
            Dictionary<string, List<PointDir>> ResultDic = new Dictionary<string, List<PointDir>>();
            List<PointDir> FinalPosition = new List<PointDir>();
            List<PointDir> PathPoints = new List<PointDir>();

            PointDir.PointDirection CurrentPosition = new PointDir.PointDirection();
            Point CurrentCoord = new Point();

            CurrentPosition = StartPoint.Direction;
            CurrentCoord = StartPoint.Position;

            for (int i = 0; i < Command.Length; i++)
            {
                if (Command[i].ToString().ToUpper() == "L")
                {
                    if (CurrentPosition == PointDir.PointDirection.N)
                        CurrentPosition = PointDir.PointDirection.W;
                    else if (CurrentPosition == PointDir.PointDirection.E)
                        CurrentPosition = PointDir.PointDirection.N;
                    else if (CurrentPosition == PointDir.PointDirection.S)
                        CurrentPosition = PointDir.PointDirection.E;
                    else if (CurrentPosition == PointDir.PointDirection.W)
                        CurrentPosition = PointDir.PointDirection.S;
                }
                else if (Command[i].ToString().ToUpper() == "R")
                {
                    if (CurrentPosition == PointDir.PointDirection.N)
                        CurrentPosition = PointDir.PointDirection.E;
                    else if (CurrentPosition == PointDir.PointDirection.E)
                        CurrentPosition = PointDir.PointDirection.S;
                    else if (CurrentPosition == PointDir.PointDirection.S)
                        CurrentPosition = PointDir.PointDirection.W;
                    else if (CurrentPosition == PointDir.PointDirection.W)
                        CurrentPosition = PointDir.PointDirection.N;
                }
                else if (Command[i].ToString().ToUpper() == "M")
                {
                    if (CurrentPosition == PointDir.PointDirection.N)
                        CurrentCoord.Y += 1;
                    else if (CurrentPosition == PointDir.PointDirection.E)
                        CurrentCoord.X += 1;
                    else if (CurrentPosition == PointDir.PointDirection.S)
                        CurrentCoord.Y -= 1;
                    else if (CurrentPosition == PointDir.PointDirection.W)
                        CurrentCoord.X -= 1;
                    PathPoints.Add(new PointDir(CurrentCoord.X, CurrentCoord.Y, CurrentPosition));
                }  
            }
            FinalPosition.Add(new PointDir(CurrentCoord.X, CurrentCoord.Y, CurrentPosition));

            ResultDic["FinalPosition"] = FinalPosition;
            ResultDic["PathPoints"] = PathPoints;
            return ResultDic;
        }
      /*
      * Author: AG.
      * Function Input: List of path points of the two robots.
      * Function Output: Boolean value to show if they are intersect or not.
      */
        public static bool IsIntersect(List<PointDir> PointOne, List<PointDir> PointTwo)
        {
            bool isInntersect = false;
            List<Point> PointList=new List<Point>();

            foreach(PointDir point in PointTwo)
                PointList.Add(point.Position);

            foreach(PointDir point in PointOne)
            {
                if (PointList.Contains(point.Position))
                {
                    isInntersect = true;
                    break;
                }
            }
            return isInntersect;
        }
     /*
     * Author: AG.
     * Function Input: Input line, number of entered line and the entered upper right coordinate.
     * Function Output: Boolean value to show if the entered data is correct.
     */
        public static bool CheckInputs(string Line, int LineNumber, Point UpperRightCoord)
        {
            bool IsCorrect = false;
            string[] LineArray = Line.Trim().Split(' ');
            if (LineNumber == 1 && int.Parse(LineArray[0]) > 0 && int.Parse(LineArray[1]) > 0)
            {
                if (LineArray.Length == 2)
                {
                    int tryParse = new int();
                    if (int.TryParse(LineArray[0], out tryParse) &&
                        int.TryParse(LineArray[1], out tryParse))
                        IsCorrect = true;
                }
            }
            else if (LineNumber == 2)
            {
                if (LineArray.Length == 3)
                {
                    int tryParse = new int();
                    PointDir.PointDirection EnumTryParse;

                    if (Enum.TryParse<PointDir.PointDirection>(LineArray[2].ToString().ToUpper(), out EnumTryParse) &&
                        int.TryParse(LineArray[0], out tryParse) &&
                        int.TryParse(LineArray[1], out tryParse) &&
                        int.Parse(LineArray[0]) <= UpperRightCoord.X &&
                        int.Parse(LineArray[1]) <= UpperRightCoord.Y &&
                        int.Parse(LineArray[0]) >= 0 &&
                        int.Parse(LineArray[1]) >= 0)
                        IsCorrect = true;
                }
            }
            else if (LineNumber == 3)
            {
                Line = Line.Trim().ToUpper();
                LineArray = Line.Split(' ');
                if (LineArray.Length == 1)
                {
                    IsCorrect = true;
                    PointsManager.Commands EnumTryParseCommand;
                    for (int j = 0; j < Line.Length; j++)
                    {
                        if (!Enum.TryParse<PointsManager.Commands>(Line[j].ToString().ToUpper(), out EnumTryParseCommand))
                        {
                            IsCorrect = false;
                            break;
                        }
                    }
                }
            }
            return IsCorrect;
        }
    }
}
