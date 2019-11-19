using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OiM_UWP
{
    class TravellingSalesmanProblem
    {
        public static void CheckCoordinatesCorrectness(Point bottom_vertex, Point upper_vertex, double x, double y)
        {
            if (x < bottom_vertex.GetX() || y < bottom_vertex.GetY() ||
                x > upper_vertex.GetX() || y > upper_vertex.GetY())
            {
                throw new Exception("Otwor znajduje sie poza prostokatem. Podaj poprawne wspolrzedne");
            }
        }

        public static double Distance(Point first_point, Point second_point)
        {
            double result;
            result = Math.Sqrt(Math.Pow(first_point.GetX() - second_point.GetX(), 2) + Math.Pow(first_point.GetY() - second_point.GetY(), 2));
            return Math.Round(result);
        }

        public static double[,] CreateDistancesMatrix(Point[] holes)
        {
            double[,] distances = new double[holes.GetLength(0), holes.GetLength(0)];
            for (int i = 0; i < distances.GetLength(0); i++)
            {
                for (int j = 0; j < distances.GetLength(1); j++)
                {
                    distances[i, j] = Distance(holes[i], holes[j]);
                }
            }
            return distances;
        }

        public static double[] CreateDistancesTable(double[,] distances_matrix, int starting_vertex)
        {
            double[] distances_table = new double[distances_matrix.GetLength(0)];
            for (int i = 0; i < distances_matrix.GetLength(0); i++)
            {
                distances_table[i] = distances_matrix[starting_vertex, i];
            }
            return distances_table;
        }

        public static int FindMaxTableElementVertex(double[] distances_array)
        {
            double max = distances_array[0];
            int index = 0;
            for (int i = 1; i < distances_array.GetLength(0); i++)
            {
                if (distances_array[i] > max)
                {
                    max = distances_array[i];
                    index = i;
                }
            }
            return index;
        }

        public static double[] ModifyDistanceArray(double[] distances_array, double[,] distances_matrix, int max_index)
        {
            double[] distances = new double[distances_array.GetLength(0)];
            for (int i = 0; i < distances_array.GetLength(0); i++)
            {
                if (i == max_index)
                {
                    distances[i] = 0;
                }
                else
                {
                    distances[i] = distances_array[i];
                }
            }
            for (int i = 0; i < distances_array.GetLength(0); i++)
            {
                if (distances_array[i] <= distances_matrix[max_index, i])
                {
                    distances[i] = distances_array[i];
                }
                else
                {
                    distances[i] = distances_matrix[max_index, i];
                }
            }

            return distances;
        }

        public static double CalculateTotalCost(double[] costs_array)
        {
            double total_cost = 0;
            for (int i = 1; i < costs_array.GetLength(0) - 1; i++)
            {
                total_cost += costs_array[i];
            }
            return total_cost;
        }

        public static double[] CalculateMinCost(double[,] diststances_matrix, List<int> vertices_array, int max_index = 0, int index = (-1))
        {
            double[] result = new double[2];
            double cost = 0;
            double min;
            double min_index = 1;
            int x, y;

            if (index == -1)
            {
                cost = diststances_matrix[vertices_array[0], vertices_array[1]] + diststances_matrix[vertices_array[1], vertices_array[0]];
            }
            else
            {
                min = diststances_matrix[vertices_array[0], max_index] + diststances_matrix[max_index, vertices_array[1]] - diststances_matrix[vertices_array[0], vertices_array[1]];
                for (int i = 0; i < index; i++)
                {
                    x = vertices_array[i];
                    y = vertices_array[i + 1];
                    cost = diststances_matrix[x, max_index] + diststances_matrix[max_index, y] - diststances_matrix[x, y];
                    if (i > 0)
                    {
                        if (cost < min)
                        {
                            min = cost;
                            min_index = i + 1;
                        }
                    }
                }
                cost = min;
            }
            result[0] = cost;
            result[1] = min_index;

            return result;

        }
    }
}
