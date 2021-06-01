using System;
using System.Threading;

namespace tarea_6
{
	class Program
	{
		static void ImprimirEnunciado() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tSe requiere armar una aplicación consola que dados 2 valores del tipo del tipo decimal");
			Console.WriteLine("\tingresados por el usuario genere las 4 operaciones básicas, que deberán también solicitarse");
			Console.WriteLine("\tpor pantalla: S – Suma, R – Resta, D – División y M – Multiplicación");
			Console.WriteLine("\tEL resultado deberá mostrarse con su respectiva leyenda.");
			Console.ResetColor();
			Console.WriteLine("\n[ENTER]");
			ConsoleKeyInfo keyInfo = Console.ReadKey();
			while(keyInfo.Key != ConsoleKey.Enter)
    			keyInfo = Console.ReadKey();
			Console.Clear();
		}

		static void CalcularOperacion(double valorA, double valorB, char operador) {
			Console.ForegroundColor = ConsoleColor.White;
			switch (operador) {
				case 'S':
					Console.WriteLine(String.Format("\n\tEl valor de la suma es: {0}", valorA + valorB));
					break;
				case 'R':
					Console.WriteLine(String.Format("\n\tEl valor de la resta es: {0}", valorA - valorB));
					break;
				case 'D':
					if (valorB == 0) {
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("\n\tNo puedes dividir por cero");
						Console.ResetColor();
						break;
					}
					Console.WriteLine(String.Format("\n\tEl valor de la división es: {0}", valorA / valorB));
					break;
				case 'M':
					Console.WriteLine(String.Format("\n\tEl valor de la multiplicación es: {0}", valorA * valorB));
					break;
				default:
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(String.Format("\n\tNo se reconoce esa operacion, intenta con S – Suma, R – Resta, D – División y M – Multiplicación"));
					Console.ResetColor();
					break;
			}
		}

		// Funcion helper para esperar por consola y reiniciar el color
		static void ImprimirError() {
			Thread.Sleep(2000);
			Console.Clear();
			Console.ResetColor();
		}

		// Toma el valor de entrada e intenta parsearlo a un doble
		static double ObtenerNumeroEntrada(double valorEntrada, string mensaje)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			bool esDoble = double.TryParse(Console.ReadLine(), out valorEntrada);
			if (!esDoble) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n\tEl texto ingresado no es un numero, intente nuevamente");
				ImprimirError();
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje);
			}

			return valorEntrada;
		}

		static void Main(string[] args)
		{
			ImprimirEnunciado();

			// Declarar las variables
			double valorA;
			double valorB;
			bool auxiliar = true;

			// Solicitar el ingreso de los valores para realizar el calculo
			valorA = ObtenerNumeroEntrada(0, "Ingrese el primer valor: ");
			Console.Clear();
			valorB = ObtenerNumeroEntrada(0, "Ingrese el segundo valor: ");

			// Ciclo principal
			while (auxiliar) {
				// Solicitar el ingreso del operador segun leyenda
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine(String.Format("\nPor favor ingrese la operacion a realizar entre los valores {0} y {1}", valorA, valorB));
				Console.WriteLine("\t(S – Suma, R – Resta, D – División y M – Multiplicación)\n");
				Console.ResetColor();
				char operador = Convert.ToChar((Convert.ToString(Console.ReadLine()).ToUpper())[0]);

				// Logica principal para calcular e imprimir los resultados
				CalcularOperacion(valorA, valorB, operador);

				// Solicitar si se quiere realizar otra operacion
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("\n\n¿Quiere realizar otra operación? [y/n]");
				Console.ResetColor();
				char repetir = Convert.ToChar(Convert.ToString(Console.ReadLine())[0]);
				if (repetir != 'y') {
					auxiliar = false;
				}
			}

			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\nHasta Luego.");
			Console.ResetColor();
		}
	}
}
