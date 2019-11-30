using System.Linq;

namespace OiM_UWP
{
    internal class HungarianAlgorithm
    {
        public static Matrix subMinFromRow(Matrix matrix)
        {
            int minInRow;
            int matrixLength = matrix.getMatrix().GetLength(0);
            for (int i = 0; i < matrixLength; ++i)
            {
                minInRow = matrix.getMatrix()[i, 0];
                for (int j = 1; j < matrixLength; ++j)
                {
                    if (matrix.getMatrix()[i, j] < minInRow)
                    {
                        minInRow = matrix.getMatrix()[i, j];
                    }
                }
                for (int k = 0; k < matrixLength; ++k)
                {
                    matrix.setCell(i, k, matrix.getMatrix()[i, k] - minInRow);
                }
            }
            return matrix;
        }
        public static Matrix subMinFromCols(Matrix matrix)
        {
            int minInCol;
            int matrixLength = matrix.getMatrix().GetLength(0);
            for (int i = 0; i < matrixLength; ++i)
            {
                minInCol = matrix.getMatrix()[0, i];
                for (int j = 1; j < matrixLength; ++j)
                {
                    if (matrix.getMatrix()[j, i] < minInCol)
                    {
                        minInCol = matrix.getMatrix()[j, i];
                    }
                }
                for (int k = 0; k < matrixLength; ++k)
                {
                    matrix.setCell(k, i, matrix.getMatrix()[k, i] - minInCol);
                }
            }
            return matrix;
        }
        public static Matrix reduceMatrix(Matrix matrix, Matrix helper, int min)
        {
            for (int i = 0; i < matrix.getMatrix().GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.getMatrix().GetLength(1); ++j)
                {
                    if (helper.getMatrix()[i, j] == 0)
                    {
                        matrix.setCell(i, j, matrix.getMatrix()[i, j] - min);
                    }
                    else if (helper.getMatrix()[i, j] == 2)
                    {
                        matrix.setCell(i, j, matrix.getMatrix()[i, j] + min);
                    }
                }
            }
            return matrix;
        }
        public static Matrix markLines(Matrix matrix)
        {
            int markedCount = 0;
            int maxTries = 1000;

            while (markedCount != matrix.getMatrix().GetLength(0) && maxTries > 0)
            {
                int size = matrix.getMatrix().GetLength(0);
                markedCount = 0;
                Matrix helper = new Matrix(matrix.getMatrix().GetLength(0), matrix.getMatrix().GetLength(0), false);
                while (size > 0)
                {
                    for (int i = 0; i < matrix.getMatrix().GetLength(0); ++i)
                    {
                        int zeros = 0;
                        for (int j = 0; j < matrix.getMatrix().GetLength(1); ++j)
                        {
                            if (matrix.getMatrix()[i, j] == 0 && helper.getMatrix()[i, j] < 1)
                            {
                                zeros++;
                            }
                        }

                        if (zeros == size)
                        {
                            for (int m = 0; m < matrix.getMatrix().GetLength(0); ++m)
                            {
                                helper.setCell(i, m, helper.getMatrix()[i, m] + 1);
                            }

                            markedCount++;
                        }

                        zeros = 0;
                        for (int j = 0; j < matrix.getMatrix().GetLength(1); ++j)
                        {
                            if (matrix.getMatrix()[j, i] == 0 && helper.getMatrix()[j, i] < 1)
                            {
                                zeros++;
                            }
                        }

                        if (zeros == size)
                        {
                            for (int m = 0; m < matrix.getMatrix().GetLength(0); ++m)
                            {
                                helper.setCell(m, i, helper.getMatrix()[m, i] + 1);
                            }

                            markedCount++;
                        }

                    }
                    size--;
                }
                if (markedCount < matrix.getMatrix().GetLength(0))
                {
                    matrix = reduceMatrix(matrix, helper, matrix.GetMin());
                }
                maxTries--;
            }
            if (maxTries <= 0)
            {
                throw new System.Exception("Could not solve problem");
            }

            return matrix;
        }

        private static void markAllRow(int rowIndex, Matrix matrix)
        {
            for (int i = 0; i < matrix.getMatrix().GetLength(0); ++i)
            {
                if (matrix.getMatrix()[rowIndex, i] != 999)
                {
                    matrix.setCell(rowIndex, i, 500);
                }
            }
        }

        private static void markAllCol(int colIndex, Matrix matrix)
        {
            for (int i = 0; i < matrix.getMatrix().GetLength(0); ++i)
            {
                if (matrix.getMatrix()[i, colIndex] != 999)
                {
                    matrix.setCell(i, colIndex, 500);
                }
            }
        }

        private static int[] countNumberInRow(Matrix matrix, int number)
        {
            int[] count = new int[matrix.getMatrix().GetLength(0)];
            for (int i = 0; i < matrix.getMatrix().GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.getMatrix().GetLength(0); ++j)
                {
                    if (matrix.getMatrix()[i, j] == number)
                    {
                        count[i]++;
                    }
                }
            }
            return count;
        }

        private static int[] countNumberInCol(Matrix matrix, int number)
        {
            int[] count = new int[matrix.getMatrix().GetLength(0)];
            for (int i = 0; i < matrix.getMatrix().GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.getMatrix().GetLength(0); ++j)
                {
                    if (matrix.getMatrix()[j, i] == number)
                    {
                        count[i]++;
                    }
                }
            }
            return count;
        }
        public static Matrix testResult(Matrix matrix)
        {
            for (int i = 0; i < matrix.getMatrix().GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.getMatrix().GetLength(0); ++j)
                {
                    if (matrix.getMatrix()[i, j] == 0)
                    {
                        Matrix matrixTest = independentZeros(matrix, i, j);

                        if (matrixTest != null)
                        {
                            return matrixTest;
                        }
                    }
                }
            }
            for (int i = 0; i < matrix.getMatrix().GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.getMatrix().GetLength(0); ++j)
                {
                    if (matrix.getMatrix()[i, j] == 0)
                    {
                        Matrix matrixTest = independentZerosWhenFailed(matrix, i, j);

                        if (matrixTest != null)
                        {
                            return matrixTest;
                        }
                    }
                }
            }
            return null;
        }
        public static Matrix independentZeros(Matrix matrix, int row, int col)
        {
            Matrix matr = new Matrix(matrix.getMatrix().GetLength(0), matrix.getMatrix().GetLength(0), false);
            matr.setArray(matrix.copyArray(matrix));
            matr.setCell(row, col, 999);
            markAllRow(col, matr);
            markAllCol(row, matr);


            for (int i = 0; i < matr.getMatrix().GetLength(0); ++i)
            {
                for (int j = 0; j < matr.getMatrix().GetLength(0); ++j)
                {
                    if (matr.getMatrix()[i, j] == 0)
                    {
                        matr.setCell(i, j, 999);
                        markAllRow(i, matr);
                        markAllCol(j, matr);
                    }
                }
            }
            int[] tab = new int[matr.getMatrix().GetLength(0)];
            tab = tab.Select(i => 1).ToArray();

            if ((Enumerable.SequenceEqual(countNumberInRow(matr, 999), tab)))
            {
                return matr;
            }
            return null;
        }
        public static Matrix independentZerosWhenFailed(Matrix matrix, int row, int col)
        {
            Matrix matr = new Matrix(matrix.getMatrix().GetLength(0), matrix.getMatrix().GetLength(0), false);
            matr.setArray(matrix.copyArray(matrix));
            matr.setCell(row, col, 999);
            markAllRow(col, matr);
            markAllCol(row, matr);


            for (int i = 0; i < matr.getMatrix().GetLength(0); ++i)
            {
                for (int j = 0; j < matr.getMatrix().GetLength(0); ++j)
                {
                    if (matr.getMatrix()[i, j] == 0)
                    {
                        matr.setCell(i, j, 999);
                        markAllRow(i, matr);
                        markAllCol(j, matr);
                    }
                }
            }
            int[] tab = new int[matr.getMatrix().GetLength(0)];
            tab = tab.Select(i => 1).ToArray();

            if ((Enumerable.SequenceEqual(countNumberInRow(matr, 999), tab)))
            {
                return matr;
            }
            else
            {

                for (int i = 0; i < matr.getMatrix().GetLength(0); i++)
                {
                    int[] rw = countNumberInRow(matr, 999);
                    if (rw[i] != 0)
                    {
                        continue;
                    }

                    for (int j = 0; j < matr.getMatrix().GetLength(0); j++)
                    {
                        int[] cl = countNumberInCol(matr, 999);
                        if (cl[j] == 0)
                        {
                            matr.setCell(i, j, 999);
                            break;
                        }
                    }
                }
                return matr;
            }

        }
    }
}
