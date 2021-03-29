using System;

namespace tarea_1
{
	class Program
	{
		static int? GetNumberInput(int? valorEntrada) {
			while (valorEntrada == null) {
				try {
					Console.Write("\n\nIngrese un valor de hasta 4 digitos: ");
					valorEntrada = Convert.ToInt32(Console.ReadLine());
				} catch {
					Console.Write("El texto ingresado no es un numero, intente nuevamente");
					Console.Write("\n\nIngrese un valor de hasta 4 digitos: ");
				}
			}
			return valorEntrada;
		}
		static void Main(string[] args)
		{
			// Enunciado
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("\nEnunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tCrear una aplicación que permita el ingreso por pantalla de un numero de hasta 4 cifras");
			Console.WriteLine("\ty devuelve si es par o impar, el proceso se debe repetir hasta que se ingrese el valor 0.");
			Console.ResetColor();

			int? valorEntrada = null;

			while (valorEntrada == null || valorEntrada != 0) {
				valorEntrada = GetNumberInput(valorEntrada);
				while (valorEntrada > 9999 || valorEntrada < -9999) {
					Console.WriteLine("El numero que ingreso tiene mas de 4 digitos, intente nuevamente");
					valorEntrada = null;
					valorEntrada = GetNumberInput(valorEntrada);
				}

				if (valorEntrada == 0) {
					continue;
				}

				if (valorEntrada % 2 == 0) {
					Console.WriteLine(string.Format("El numero {0} es par", valorEntrada));
					valorEntrada = null;
				} else {
					Console.WriteLine(string.Format("El numero {0} es impar", valorEntrada));
					valorEntrada = null;
				}
			}

			Console.WriteLine("Hasta Luego.");
		}
	}
}
