﻿using System;
using System.Linq;

namespace OiM_UWP
{
    internal class Matrix
    {
        public Matrix(int x, int y, bool rand, int val = 0)
        {
            array = new int[x, y];
            if (rand)
            {
                Random random = new Random();
                for (int i = 0; i < x; ++i)
                {
                    for (int j = 0; j < y; j++)
                    {
                        array[i, j] = random.Next(1, 30);
                    }
                }
            }
            else
            {
                for (int i = 0; i < x; ++i)
                {
                    for (int j = 0; j < y; j++)
                    {
                        array[i, j] = val;
                    }
                }
            }
        }

        private int[,] array { get; set; }

        public void setArray(int[,] arr)
        {
            array = arr;
        }
        public int[,] getMatrix()
        {
            return array;
        }

        public string[] getAsText()
        {
            string[] lines = new string[array.GetLength(0) + 1];
            lines[0] = array.GetLength(1).ToString();
            for (int i = 1; i <= array.GetLength(0); ++i)
            {
                lines[i] = string.Join(" ", Enumerable.Range(0, array.GetLength(1)).Select(x => array[i - 1, x]).ToArray());
            }
            return lines;
        }

        public int getMin()
        {
            int max = array[0, 0];
            for (int i = 0; i < array.GetLength(0); ++i)
            {
                for (int j = 0; j < array.GetLength(1); ++j)
                {
                    if (array[i, j] > max)
                    {
                        max = array[i, j];
                    }
                }
            }
            int mi = max;
            for (int i = 0; i < array.GetLength(0); ++i)
            {
                for (int j = 0; j < array.GetLength(1); ++j)
                {
                    if (array[i, j] < mi && array[i, j] != 0)
                    {
                        mi = array[i, j];
                    }
                }
            }
            return mi;
        }

        public void setCell(int x, int y, int val)
        {
            array[x, y] = val;
        }
        public int[,] copyArray(Matrix matrix)
        {
            int[,] newData = new int[matrix.array.GetLength(0), matrix.array.GetLength(1)];
            for (int i = 0; i < newData.GetLength(0); ++i)
            {
                for (int j = 0; j < newData.GetLength(1); ++j)
                {
                    newData[i, j] = matrix.array[i, j];
                }
            }
            return newData;
        }


    }
}
