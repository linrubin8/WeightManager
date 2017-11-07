using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FastReport.FastQueryBuilder
{
    internal class PosCounter
    {
        private Point next;
        private int initX;
        private int initY;

        public int StepX = 10;
        public int StepY = 10;
        public int MaxX = 100;
        public int MaxY = 100;

        public Point Next
        {
            get
            {
                next.Y += StepY;
                next.X += StepX;

                if (next.X > MaxX)
                    next.X = initX;
                if (next.Y > MaxY)
                    next.Y = initY;

                return next;
            }
        }

        public PosCounter(int X, int Y)
        {
            next.X = X - StepX;
            next.Y = Y - StepY;
            initX = X;
            initY = Y;
        }
    }
}
