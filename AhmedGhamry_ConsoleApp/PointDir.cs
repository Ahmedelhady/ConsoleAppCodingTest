using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AhmedGhamry_ConsoleApp
{
    public class PointDir
    {
        public enum PointDirection
        {
            N,
            S,
            E,
            W
        };
        public Point Position { get; set; }
        public PointDirection Direction { get; set; }

        public PointDir(int XDir, int YDir, PointDirection Direction)
        {
            this.Position = new Point(XDir,YDir);
            this.Direction = Direction;
        }
        public PointDir()
        {
            this.Position = new Point(0, 0);
            this.Direction = PointDirection.N;
        }
    }
}
