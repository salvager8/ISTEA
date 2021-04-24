using System;

namespace tarea_5
{
	class Program
	{
		// Imprime el enunciado por pantalla y pide el ingreso de un enter
		static void GetSummary() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tDada el siguiente array bidimensional , mostrar por pantalla los valores y las posiciones en que están cargados");
			Console.WriteLine("\ttambién resaltar en color rojo el valor y posición del numero 27.");
			Console.WriteLine("\n\tArray Bidireccional:\n");
			Console.WriteLine("\tint[,] array_2d_int=new int[5,3] {{10,21,77} ,{55,42,12},{11,67,101},{23,27,120},{15,45,100}};");
			Console.WriteLine("\n\tUtilizando como base el Ejercicio Nro.4, recorrer todo el array y mostrar por pantalla, pintado de color rojo, los numero pares y blancos los impares.");
			Console.ResetColor();
			Console.WriteLine("\n[ENTER]");
			ConsoleKeyInfo keyInfo = Console.ReadKey();
			while(keyInfo.Key != ConsoleKey.Enter)
			keyInfo = Console.ReadKey();
			Console.Clear();
		}

		static void Main(string[] args)
		{
			// Enunciado
			GetSummary();

			// Inicializar las variables
			int[,] array_2d_int=new int[5,3] {
				{10,21,77},
				{55,42,12},
				{11,67,101},
				{23,27,120},
				{15,45,100}
			};
			int row = 0;
			int column = 0;

			// Ciclo principal
			foreach (int arrayEntry in array_2d_int) {

				// Validar si el valor es par
				if (arrayEntry % 2 == 0) {
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(String.Format("El numero {0} (par) esta en la posicion [{1}, {2}]", arrayEntry, row, column));
					Console.ResetColor();
					column++;
					if (column == 3) {
						column = 0;
						row++;
					}
					continue;
				}

				// Por defecto cuando el valor no es par
				Console.WriteLine(String.Format("El numero {0} (impar) esta en la posicion [{1}, {2}]", arrayEntry, row, column));
				column++;

				if (column == 3) {
					column = 0;
					row++;
				}
			}

			Console.WriteLine();
		}
	}
}
