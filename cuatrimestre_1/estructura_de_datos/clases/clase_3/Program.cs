using System;

namespace clase_3
{
    class Program
    {
        static void Main(string[] args)
        {
            // int[] test = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

			// foreach (int numero in test) {
			// 	Console.WriteLine(numero);
			// }


			string[,] matrix = new string[3, 2] { {"uno", "dos"}, {"tres", "cuatro"}, {"cinco", "seis"} };

			for (int i = 0; i < 3; i++) {
				for (int j = 0; j < 2; j++) {
					Console.Write(matrix[i, j] + "\t");
				}
				Console.WriteLine();
			}

			// Console.Write(matrix[0]);
			// for (int i = 0; i < 3; i++) {
			// 	Console.WriteLine(string.Join("\t", matrix[i]));
			// }
        }
    }
}
