using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OiM_UWP
{
    static class JohnsonAlgorithm
    {
        static public List<int>[] CreateLists(int[,] matrix)
        {
            List<int>[] lists = new List<int>[2];
            List<int> n1 = new List<int>();
            List<int> n2 = new List<int>();

            int index;

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (matrix[0, i] < matrix[1, i])
                {
                    if (n1.Count == 0)
                    {
                        n1.Add(i);
                    }
                    else
                    {
                        index = FindIndexAsc(matrix, n1, i);
                        n1.Insert(index, i);
                    }
                }
                else
                {
                    if (n2.Count == 0)
                    {
                        n2.Add(i);
                    }
                    else
                    {
                        index = FindIndexDesc(matrix, n2, i);
                        n2.Insert(index, i);
                    }
                }
            }

            lists[0] = n1;
            lists[1] = n2;

            return lists;
        }

        static public int FindIndexAsc(int[,] matrix, List<int> list, int task_index)
        {
            for (int j = 0; j < list.Count; j++)
            {
                if (matrix[0, task_index] < matrix[0, list[j]])
                {
                    return j;
                }
            }
            return list.Count;
        }

        static public int FindIndexDesc(int[,] matrix, List<int> list, int task_index)
        {
            for (int j = 0; j < list.Count; j++)
            {
                if (matrix[1, task_index] >= matrix[1, list[j]])
                {
                    return j;
                }
            }
            return list.Count;
        }

        static public List<int> ConnectLists(List<int> first_list, List<int> second_list)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < first_list.Count; i++)
            {
                list.Add(first_list[i] + 1);
            }
            for (int j = 0; j < second_list.Count; j++)
            {
                list.Add(second_list[j] + 1);
            }

            return list;
        }

        static public List<string>[] CreateTasksAxis(int[,] matrix, List<int> queue)
        {
            List<string>[] axis = new List<string>[2];
            List<string> machine1 = new List<string>();
            List<string> machine2 = new List<string>();

            int cost;

            for (int i = 0; i < queue.Count; i++)
            {
                for (int j = 0; j < matrix[0, (queue[i] - 1)]; j++)
                {
                    machine1.Add(queue[i].ToString());
                }
            }

            for (int i = 0; i < queue.Count; i++)
            {
                cost = matrix[1, (queue[i] - 1)];

                while (cost != 0)
                {
                    if (machine2.Count < machine1.Count)
                    {
                        if (machine1[machine2.Count] == queue[i].ToString())
                        {
                            machine2.Add(" ");
                        }
                        else
                        {
                            machine2.Add(queue[i].ToString());
                            cost -= 1;
                        }
                    }
                    else
                    {
                        machine2.Add(queue[i].ToString());
                        cost -= 1;
                    }
                }
            }

            axis[0] = machine1;
            axis[1] = machine2;

            return axis;
        }
    }
}
