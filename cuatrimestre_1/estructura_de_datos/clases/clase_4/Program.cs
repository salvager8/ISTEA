using System;

namespace clase_4
{
    class Program
    {
        static void Main(string[] args)
        {
			int[,] array_2d_int = new int[5,3] { {10,21,77}, {55,42,12}, {11,67,101}, {23,27,120}, {15,45,100} };
			Console.Clear();

			for(int i = 0; i < array_2d_int.GetLength(0); i++) {
				for(int j = 0; j < array_2d_int.GetLength(1); j++) {
					if (array_2d_int[i, j] == 27) {
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("[{0}, {1}] = {2}", i, j, array_2d_int[i, j]);
						Console.ResetColor();
					} else {
						Console.WriteLine("[{0}, {1} = {2}]", i, j, array_2d_int[i, j]);
					}
				}
			}
		}
    }
}
