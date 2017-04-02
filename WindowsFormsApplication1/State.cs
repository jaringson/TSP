using System;
using System.Collections;
using System.Collections.Generic;


namespace TSP
{

    public class State
    {
        public int number;
        public double[,] matrix;
        public double lower_bound;
        public int depth;
        public double priority = double.PositiveInfinity;
        public ArrayList route = new ArrayList();
        public List<Tuple<int, int>> zeros = new List<Tuple<int, int>>();

        public State(int length)
        {
            matrix = new double[length, length];
        }



        public List<int> get_children(int row)
        {
            List<int> r = new List<int>();
            for(int j = 0; j< matrix.GetLength(0); j++)
            {
                if (matrix[row, j] != double.PositiveInfinity) r.Add(j);
            }
            return r;
        }

        public void do_priority()
        {
            priority = lower_bound - 100 * depth;
        }

        public void find_lb(double previous_lb)
        {
            double total = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                double min = double.PositiveInfinity;
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] < min) min = matrix[i, j];
                }
                if (min != double.PositiveInfinity)
                {
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        if(matrix[i,j] != double.PositiveInfinity)
                        {
                            matrix[i, j] -= min;
                            zeros.Add(new Tuple<int, int>(i, j));
                        }
                    }
                    total += min;
                }
                
            }
            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                double min = double.PositiveInfinity;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, j] < min) min = matrix[i, j];
                }
                if (min != double.PositiveInfinity)
                {
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        if (matrix[i, j] != double.PositiveInfinity)
                        {
                            matrix[i, j] -= min;
                            zeros.Add(new Tuple<int, int>(i, j));
                        }
                    }
                    total += min;
                }
                
            }
            lower_bound = total + previous_lb;
        }
    }
}
