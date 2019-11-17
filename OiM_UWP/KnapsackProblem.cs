using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OiM_UWP
{
    class KnapsackProblem
    {
        public static double[] CountQuotient(Matrix matrix)
        {
            double[] quotient = new double[matrix.getMatrix().GetLength(1)];
            for (int j = 0; j < matrix.getMatrix().GetLength(1); j++)
            {
                quotient[j] = ((double)matrix.getMatrix()[0, j] / matrix.getMatrix()[1, j]);
            }
            return quotient;
        }

        public static int FindMaxIndex(Matrix matrix, int c, double[] quotient)
        {
            double max = -1;
            int index = -1;
            for (int j = 0; j < matrix.getMatrix().GetLength(1); j++)
            {
                if (quotient[j] >= max && matrix.getMatrix()[1, j] <= c)
                {
                    max = quotient[j];
                    index = j;
                }
            }
            return index;
        }

        public static int CheckWeights(Matrix matrix, int c)
        {
            int min = matrix.getMatrix()[1, 0];
            for (int j = 1; j < matrix.getMatrix().GetLength(1); j++)
            {
                if (matrix.getMatrix()[1, j] < min)
                {
                    min = matrix.getMatrix()[1, j];
                }
            }
            if (min > c)
            {
                return 0;
            }
            return 1;
        }

        public static Matrix GreedyMethod(Matrix matrix, int c, double[] q, ref int val)
        {
            int index;
            int counter = 0;
            double[] quotient = q;
            int capacity = c;
            while (capacity > 0)
            {
                index = FindMaxIndex(matrix, capacity, quotient);
                capacity = capacity - matrix.getMatrix()[1, index] * (capacity / matrix.getMatrix()[1, index]);
                counter++;
                if (CheckWeights(matrix, capacity) == 0)
                {
                    capacity = 0;
                }
            }

            Matrix result = new Matrix(2, counter, false);
            capacity = c;
            int items;
            int value = 0;
            for (int i = 0; i < counter; i++)
            {
                index = FindMaxIndex(matrix, capacity, quotient);
                items = (capacity / matrix.getMatrix()[1, index]);
                capacity = capacity - matrix.getMatrix()[1, index] * items;

                result.setCell(0, i, items);
                result.setCell(1, i, index + 1);

                value = value + matrix.getMatrix()[0, index] * items;
            }
            val = value;
            return result;
        }

        public static Matrix[] DynamicMethod(Matrix matrix, int capacity)
        {
            Matrix p = new Matrix(matrix.getMatrix().GetLength(1) + 1, capacity + 1, false, 0);
            Matrix q = new Matrix(matrix.getMatrix().GetLength(1) + 1, capacity + 1, false, 0);
            for (int i = 1; i <= matrix.getMatrix().GetLength(1); i++)
            {
                for (int j = 1; j < p.getMatrix().GetLength(1); j++)
                {
                    if (matrix.getMatrix()[1, i - 1] <= j && (p.getMatrix()[i - 1, j] < p.getMatrix()[i, j - matrix.getMatrix()[1, i - 1]] + matrix.getMatrix()[0, i - 1]))
                    {
                        p.setCell(i, j, Math.Max(p.getMatrix()[i - 1, j], (p.getMatrix()[i, j - matrix.getMatrix()[1, i - 1]] + matrix.getMatrix()[0, i - 1])));
                        q.setCell(i, j, i);

                    }
                    else
                    {
                        p.setCell(i, j, p.getMatrix()[i - 1, j]);
                        q.setCell(i, j, q.getMatrix()[i - 1, j]);
                    }

                }
            }
            return new Matrix[2] { p, q };
        }
        public static int[] ShowItems(Matrix m, Matrix q)
        {
            int row = q.getMatrix().GetLength(0) - 1;
            int[] items = new int[0];
            int capacity = q.getMatrix().GetLength(1) - 1;
            int i = 0;

            while (capacity > 0 && q.getMatrix()[row, capacity] > 0)
            {

                int[] newItems = new int[items.Length + 1];
                for (int j = 0; j < items.Length; j++)
                {
                    newItems[j] = items[j];
                }
                newItems[i] = q.getMatrix()[row, capacity];
                capacity -= m.getMatrix()[1, q.getMatrix()[row, capacity] - 1];
                items = newItems;
                i++;
            }
            return items;
        }
    }
}
