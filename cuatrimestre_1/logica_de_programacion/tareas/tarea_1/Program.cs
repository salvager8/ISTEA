using System;

namespace tarea_1
{
	class Program
	{
		static void Main(string[] args)
		{
			// Enunciado
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("\nEnunciado: \n");
			Console.ResetColor();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tSe requiere armar una aplicación consola que dados 2 valores del tipo del tipo decimal");
			Console.WriteLine("\tingresados por el usuario genere las 4 operaciones básicas, que deberán también solicitarse");
			Console.WriteLine("\tpor pantalla: S – Suma, R – Resta, D – División y M – Multiplicación");
			Console.WriteLine("\tEL resultado deberá mostrarse con su respectiva leyenda.\n\n");

			// Declarar las variables
			double valor_a;
			double valor_b;
			bool auxiliar = true;

			// Solicitar el ingreso del primer valor y llenar la primera variable
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Ingrese el primer valor:");
			Console.ResetColor();
			valor_a = Convert.ToDouble(Console.ReadLine());

			// Solicitar el ingreso del segundo valor y llenar la segunda variable
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("\nIngrese el segundo valor:");
			Console.ResetColor();
			valor_b = Convert.ToDouble(Console.ReadLine());

			// Ciclo principal
			while (auxiliar) {

				// Solicitar el ingreso del operador segun leyenda
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine(String.Format("\nPor favor ingrese la operacion a realizar entre los valores {0} y {1}", valor_a, valor_b));
				Console.WriteLine("S – Suma, R – Resta, D – División y M – Multiplicación");
				Console.ResetColor();
				char operador = Convert.ToChar((Convert.ToString(Console.ReadLine()).ToUpper())[0]);

				// Logica principal para calcular e imprimir los resultados
				Console.ForegroundColor = ConsoleColor.White;
				switch (operador) {
					case 'S':
						Console.WriteLine(String.Format("\n\tEl valor de la suma es: {0}", valor_a + valor_b));
						break;
					case 'R':
						Console.WriteLine(String.Format("\n\tEl valor de la resta es: {0}", valor_a - valor_b));
						break;
					case 'D':
						if (valor_b == 0) {
							Console.ForegroundColor = ConsoleColor.Red;
							Console.WriteLine("\n\tNo puedes dividir por cero");
							Console.ResetColor();
							break;
						}
						Console.WriteLine(String.Format("\n\tEl valor de la división es: {0}", valor_a / valor_b));
						break;
					case 'M':
						Console.WriteLine(String.Format("\n\tEl valor de la multiplicación es: {0}", valor_a * valor_b));
						break;
					default:
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine(String.Format("\n\tNo se reconoce esa operacion, intenta con S – Suma, R – Resta, D – División y M – Multiplicación"));
						Console.ResetColor();
						break;
				}

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
