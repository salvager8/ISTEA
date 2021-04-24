using System;

namespace tarea_4
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

				// Validar si el valor es el 27
				if (arrayEntry == 27) {
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(String.Format("El numero {0} esta en la posicion [{1}, {2}]", arrayEntry, row, column));
					Console.ResetColor();
					column++;
					continue;
				}

				// Caso default cuando es diferente a 27
				Console.WriteLine(String.Format("El numero {0} esta en la posicion [{1}, {2}]", arrayEntry, row, column));
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
