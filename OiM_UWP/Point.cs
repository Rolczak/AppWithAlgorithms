using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OiM_UWP
{
    class Point
    {
        private double x, y;
        private char name;

        public Point(double first_dimension, double second_dimension, char point_name = '*')
        {
            x = first_dimension;
            y = second_dimension;
            name = point_name;
        }

        public double GetX()
        {
            return x;
        }

        public double GetY()
        {
            return y;
        }

        public void ShowPoint()
        {
            Console.WriteLine();

            if (name == '*')
            {
                Console.WriteLine("x = " + x + "\ny = " + y);
            }
            else
            {
                Console.WriteLine("Point name: " + name + "\nx = " + x + "\ny = " + y);
            }
        }
    }
}
