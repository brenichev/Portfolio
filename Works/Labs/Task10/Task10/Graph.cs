using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.CodeAnalysis;

namespace Task10
{
    public class Graph
    {
        int size;
        byte[,] matrix;
        int[] values;

        public Graph(int Size, int[] Val, byte[,] Matrix)
        {
            this.size = Size;
            matrix = Matrix;
            values = Val;
        }

        public byte[,] Matrix {
            get
            {
                return matrix;
            }
        }

        [ExcludeFromCodeCoverage]
        public static Graph ReadMatrix()
        {
            int size = 0;
            bool check = true;

            do
            {
                Console.WriteLine("Введите количество вершин графа");
                check = int.TryParse(Console.ReadLine(), out size);
            } while (!check);

            int[] values = new int[size];
            int i = 0;
            do
            {
                Console.WriteLine("Введите значения вершин графа ");
                string[] row = Console.ReadLine().Split(' ');
                while (check && i < size)
                {
                    check = int.TryParse(row[i], out values[i]);
                    i++;
                }
                if (!check || i < size)
                    Console.WriteLine("Неверно введены значения графа");
            } while (!check);

            i = 0;
            byte[,] matrix = new byte[size, size];
            Console.WriteLine();
            Console.WriteLine("Введите матрицу смежности графа");
            do
            {
                Console.WriteLine("Введите строку матрицы графа с номером " + (i + 1));
                string[] row = Console.ReadLine().Split(' ');
                check = true;
                int j = 0;

                if (row.Length != size)
                    Console.WriteLine("Элементов в строке должно быть " + size + ", введено = " + row.Length);
                else
                {
                    while (check && j < size)
                    {
                        check = byte.TryParse(row[j], out matrix[i, j]);
                        if (matrix[i, j] != 0 && matrix[i, j] != 1)
                            check = false;
                        j++;
                    }
                    if (check)
                        i++;
                }
                if (!check)
                    Console.WriteLine("У элемента с номером " + j + " неверный тип данных");
            } while (i < size);
            return new Graph(size, values, matrix);
        }

        [ExcludeFromCodeCoverage]
        public void WriteMatrix()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    Console.Write(matrix[i, j] + " ");
                Console.WriteLine();
            }
        }

        public void Constrict(int Val)
        {
            if (!values.Contains(Val))
            {
                Console.WriteLine("В графе нет вершины с указанным значением");
                return;
            }
            int FirstV = Array.IndexOf(values, Val);

            int i = FirstV + 1;
            while (i < size)
            {
                if (values[i] == Val)
                {
                    for (int col = 0; col < size; col++)//перенос по столбцам
                        if (matrix[i, col] == 1 && col != FirstV)
                            matrix[FirstV, col] = 1;

                    for (int row = 0; row < size; row++)//перенос по строкам
                        if (matrix[row, i] == 1 && row != FirstV)
                            matrix[row, FirstV] = 1;

                    RemoveV(i);
                }
                else
                    i++;
            }
        }

        // Удаление вершины графа
        void RemoveV(int index)
        {
            int[] NewValues = new int[size - 1];
            for (int i = 0; i < index; i++)
                NewValues[i] = values[i];
            for (int i = index + 1; i < size; i++)
                NewValues[i - 1] = values[i];
            values = NewValues;

            byte[,] NewMatrix = new byte[size - 1, size - 1];


            for (int i = 0; i < index; i++)
                for (int j = 0; j < index; j++)
                    NewMatrix[i, j] = matrix[i, j];

            for (int i = 0; i < NewMatrix.GetLength(0); i++)
                for (int j = index; j < NewMatrix.GetLength(1); j++)
                    NewMatrix[i, j] = matrix[i, j + 1];

            for (int i = index; i < NewMatrix.GetLength(0); i++)
                for (int j = 0; j < NewMatrix.GetLength(1); j++)
                    NewMatrix[i, j] = matrix[i + 1, j];
            matrix = NewMatrix;

            size--;
        }
    }
}
