using System;

namespace tarea_2_viernes
{
	class Program
	{
		// Imprime el enunciado por pantalla
		static void GetSummary()
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("\nEnunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tDada la siguiente cadena de caracteres devolver la cantidad de 0 y 1 por pantalla");
			Console.WriteLine("\tstring Oracion =\"1101 0011 1010 1100 1001 0001 1000 1000 1100 0011 1100 0011 1100\";");
			Console.ResetColor();
		}

		static void Main(string[] args)
		{
			// Enunciado
			GetSummary();

			// Dada la siguiente cadena de caracteres devolver la cantidad de 0 y 1 por pantalla.
			string oracion = "1101 0011 1010 1100 1001 0001 1000 1000 1100 0011 1100 0011 1100";
			int countZeroes = 0;
			int countOnes = 0;

			for (int i = 0; i < oracion.Length; i++) { 
				if (oracion[i] == '1') {
					countOnes++;
				} else if (oracion[i] == '0') {
					countZeroes++;
				}
			}

			Console.WriteLine(string.Format("\nLa cantidad de ceros (0) es: {0} y la cantidad de unos (1) es: {1}", countZeroes, countOnes));
		}
	}
}
