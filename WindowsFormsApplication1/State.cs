using System;
using System.Collections;
using System.Collections.Generic;


namespace TSP
{

    public class State
    {
        // This class is to hold each subproblem from the reduced matrix.
        // It has members for the matrix, the number in the list of the cities,
        // depth, lower bound, and priority number it should have for the queue
        public int number;
        public double[,] matrix;
        public double lower_bound;
        public int depth;
        public double priority = double.PositiveInfinity;
        public ArrayList route = new ArrayList();
        
        public State(int length)
        {
            matrix = new double[length, length];
        }



        public List<int> get_children(int row)
        {
            // Time complexity of O(n) because we only loop over
            // one row.
            List<int> r = new List<int>();
            for(int j = 0; j< matrix.GetLength(0); j++)
            {
                if (matrix[row, j] != double.PositiveInfinity) r.Add(j);
            }
            return r;
        }

        public void do_priority()
        {
            priority = lower_bound - 500 * depth;
        }

        public void check_zeros(List<Tuple<int,int>> z)
        {
            // This function at most would be time complexity of O(n^2).
            // This is because one row and column (length n) are iterated over twice.
            // However, in pratice there are less zeros that were changed 
            // to inifinty. This is faster than the lb_find() function.
            for (int i = 0; i < z.Count; i++)
            {
                double min_row = double.PositiveInfinity;
                double min_col = double.PositiveInfinity;
                for(int j=0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[z[i].Item1, j] < min_row) min_row = matrix[z[i].Item1, j];
                    if (matrix[j, z[i].Item2] < min_col) min_col = matrix[j, z[i].Item2];
                }
                if(min_row != double.PositiveInfinity && min_row != 0)
                {
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        if (matrix[z[i].Item1, j] != double.PositiveInfinity)
                        {
                            matrix[z[i].Item1, j] -= min_row;
                        }
                    }
                    lower_bound += min_row;
                }
                if (min_col != double.PositiveInfinity && min_col != 0)
                {
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        if (matrix[j, z[i].Item2] != double.PositiveInfinity)
                        {
                            matrix[j, z[i].Item2] -= min_col;
                        }
                    }
                    lower_bound += min_col;
                }
            }
        }

        public void find_lb()
        {
            // Takes a most O(n^3) time because if there is a value that 
            // needs to be reduced on each row and column, then we apply it to 
            // every member on that row or column. This would be worst case.
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                double min = double.PositiveInfinity;
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] < min) min = matrix[i, j];
                }
                if (min != double.PositiveInfinity && min != 0)
                {
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        if(matrix[i,j] != double.PositiveInfinity)
                        {
                            matrix[i, j] -= min;
                        }
                    }
                    lower_bound += min;
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
                        if (matrix[i, j] != double.PositiveInfinity && min != 0)
                        {
                            matrix[i, j] -= min;
                        }
                    }
                    lower_bound += min;
                }
                
            }
        }
    }
}
