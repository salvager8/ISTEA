using System;

namespace clase_3
{
    class Program
    {
        static void Main(string[] args)
        {
            // double[] dailySales = new double[365];
			// double average = 0;
			// int currentNumber = 0;
			// Random randomManager = new Random();

			// for (int i = 0; i < 365; i++) {
			// 	currentNumber = randomManager.Next(1000, 9999);
			// 	Console.WriteLine(String.Format("\nEl dia {0} hubo unas ventas que suman {1:c}", i + 1, currentNumber));
			// 	dailySales[i] = currentNumber;
			// 	average = average + currentNumber;
			// }
			// Console.WriteLine(String.Format("\nEl promedio de ventas fue de {0}", average));
			char aux;

			do {
				Console.Clear();
				Console.WriteLine("Desea entrar al sistema?");
				Console.WriteLine("Y                      N");
				aux = char.ToUpper(char.Parse(Console.ReadLine()));
			} while (aux != 'N');

			Environment.Exit(0);
        }
    }
}
