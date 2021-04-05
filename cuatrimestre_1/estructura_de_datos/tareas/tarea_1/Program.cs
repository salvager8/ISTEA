using System;
using System.Threading;

namespace tarea_1
{
	class Program
	{
		// Imprime el enunciado por pantalla
		static void GetSummary()
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("\nEnunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tCrear una aplicación que permita el ingreso por pantalla de un numero de hasta 4 cifras");
			Console.WriteLine("\ty devuelve si es par o impar, el proceso se debe repetir hasta que se ingrese el valor 0.");
			Console.ResetColor();
		}

		// Toma el valor de entrada e intenta parsearlo a un entero
		static int GetNumberInput(int inputValue)
		{
			// Pedir el valor por consola al usuario
			Console.Write("\n\nIngrese un valor de hasta 4 digitos: ");

			// Condicionales
			bool isInt = Int32.TryParse(Console.ReadLine(), out inputValue);
			bool isGreaterThanFourDigits = (inputValue > 9999 || inputValue < -9999);

			// Condiciones que deben cumplirse para que el input este correcto
			if (!isInt) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("El texto ingresado no es un numero entero, intente nuevamente");
				Console.ResetColor();
				inputValue = GetNumberInput(inputValue);
			} else if (isGreaterThanFourDigits) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("El numero que ingreso tiene mas de 4 digitos, intente nuevamente");
				Console.ResetColor();
				inputValue = GetNumberInput(inputValue);
			}
			return inputValue;
		}

		static void Main(string[] args)
		{
			// Enunciado
			GetSummary();

			// Inicializacion de variables
			int inputValue = 0;

			// Ciclo principal
			do {
				inputValue = GetNumberInput(inputValue);

				if (inputValue == 0) {
					continue;
				}

				if (inputValue % 2 == 0) {
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.WriteLine(string.Format("\n\tEl numero {0} es par", inputValue));
					Console.ResetColor();
					Thread.Sleep(3000);
					Console.Clear();
				} else {
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.WriteLine(string.Format("\n\tEl numero {0} es impar", inputValue));
					Console.ResetColor();
					Thread.Sleep(3000);
					Console.Clear();
				}
			} while (inputValue != 0);

			Console.WriteLine("Hasta Luego.");
		}
	}
}
