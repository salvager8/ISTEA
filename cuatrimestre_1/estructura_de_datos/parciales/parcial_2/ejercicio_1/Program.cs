using System;
using System.Threading;

namespace ejercicio_1
{
    class Program
    {
		// Opciones de la tabla
		private static int opcion = 0;

		// Imprime el enunciado por pantalla y pide el ingreso de un enter
		private static void ImprimirEnunciado() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tRealizar una aplicación consola para calcular la potencia de un número, tanto");
			Console.WriteLine("\tbase como exponente se deben ingresar por pantalla. Se debe implementar");
			Console.WriteLine("\tcon un algoritmo recursivo.\n");
			Console.WriteLine("\tRealizar el código también sin función recursiva para mostrar la comparativa");
			Console.WriteLine("\tentre recursividad e iteración.");
			Console.ResetColor();
			EnterParaContinuar();
		}

		// Pide un enter por pantalla para continuar
		private static void EnterParaContinuar() {
			Console.WriteLine("\nPresione [ENTER] para continuar");
			ConsoleKeyInfo keyInfo = Console.ReadKey();
			while (keyInfo.Key != ConsoleKey.Enter)
				keyInfo = Console.ReadKey();
			Console.Clear();
		}

		// Imprime el error por pantalla
		private static void ImprimirError(string mensaje) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("\n\t");
			Console.WriteLine(mensaje);
			Thread.Sleep(2000);
			Console.Clear();
			Console.ResetColor();
		}

		// Alinea al centro un string relativo al ancho de la tabla
		static string AlinearAlCentro(string texto, int ancho) {
			texto = texto.Length > ancho ? texto.Substring(0, ancho - 3) + "..." : texto;

			if (string.IsNullOrEmpty(texto)) {
				return new string(' ', ancho);
			}
			else {
				return texto.PadRight(ancho - (ancho - texto.Length) / 2).PadLeft(ancho);
			}
		}

		// Imprime una linea para la tabla
		static void ImprimirLinea(params string[] columnas) {
			int ancho = (20 - columnas.Length) / columnas.Length;
			string fila = "|";

			foreach (string columna in columnas) {
				fila += AlinearAlCentro(columna, ancho) + "|";
			}

			Console.WriteLine(fila);
		}

		// Construye una tabla para mostrarla por pantalla
		private static void MostrarOpciones() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Black;
			Console.WriteLine("");
			Console.BackgroundColor = ConsoleColor.Blue;
			ImprimirLinea("Menu de ejecucion");
			Console.BackgroundColor = ConsoleColor.White;
			if (opcion == 0) {
				Console.BackgroundColor = ConsoleColor.Cyan;
				ImprimirLinea("Recursivo");
				Console.BackgroundColor = ConsoleColor.White;
				ImprimirLinea("Iterativo");
			} else {
				ImprimirLinea("Recursivo");
				Console.BackgroundColor = ConsoleColor.Cyan;
				ImprimirLinea("Iterativo");
			}
			Console.ResetColor();
		}

		// Valida un double ingresado por pantalla
		private static double ObtenerDobleEntrada(double valorEntrada, string mensaje) {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			bool esDoble = double.TryParse(Console.ReadLine(), out valorEntrada);
			if (!esDoble) {
				ImprimirError("El dato ingresado no es un numero entero, intente nuevamente");
				valorEntrada = ObtenerDobleEntrada(valorEntrada, mensaje);
			} else if (valorEntrada < 0) {
				ImprimirError("El numero ingresado no puede ser negativo");
				valorEntrada = ObtenerDobleEntrada(valorEntrada, mensaje);
			}

			return valorEntrada;
		}

		// Valida un numero ingresado por pantalla
		private static int ObtenerNumeroEntrada(int valorEntrada, string mensaje) {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			bool esEntero = int.TryParse(Console.ReadLine(), out valorEntrada);
			if (!esEntero) {
				ImprimirError("El dato ingresado no es un numero entero, intente nuevamente");
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje);
			}

			return valorEntrada;
		}

		// Valida las opciones del usuario
		static void OpcionDeUsuario() {
			ConsoleKeyInfo keyInfo = Console.ReadKey();
			if (keyInfo.Key == ConsoleKey.UpArrow) {
				if (opcion == 1) {
					opcion = 0;
				}
			} else if (keyInfo.Key == ConsoleKey.DownArrow) {
				if (opcion == 0) {
					opcion = 1;
				}
			}

			if (keyInfo.Key != ConsoleKey.Enter) {
				MostrarOpciones();
				OpcionDeUsuario();
			}
		}

		// Corre el ejercicio recursivamente
		static double EjecutarRecursivo(double basePotencia, int exponentePotencia, ref double resultado, bool negativo) {
			if (exponentePotencia == 0) {
				return 1;
			}
			if (exponentePotencia > 1) {
				resultado *= basePotencia;
				EjecutarRecursivo(basePotencia, exponentePotencia - 1, ref resultado, negativo);
			}
			if (negativo) {
				return 1 / resultado;
			}
			return resultado;
		}

		// Corre el ejercicio iterativamente
		static double EjecutarIterativo(double basePotencia, int exponentePotencia, ref double resultado, bool negativo) {
			if (exponentePotencia == 0) {
				return 1;
			}
			while (exponentePotencia > 1) {
				resultado *= basePotencia;
				exponentePotencia--;
			}
			if (negativo) {
				return 1 / resultado;
			}
			return resultado;
		}

        static void Main(string[] args)
        {
			ImprimirEnunciado();
			MostrarOpciones();
			OpcionDeUsuario();

			double basePotencia = ObtenerDobleEntrada(0, "Ingrese la base para calcular la potencia: ");
			int exponentePotencia = ObtenerNumeroEntrada(0, "Ingrese el exponente para calcular la potencia: ");
			double resultado = basePotencia;
			bool negativo = exponentePotencia < 0 ? true : false;

			switch (opcion) {
				case 0:
					resultado = EjecutarRecursivo(basePotencia, negativo ? -1 * exponentePotencia : exponentePotencia, ref resultado, negativo);
					break;
				case 1:
					resultado = EjecutarIterativo(basePotencia, negativo ? -1 * exponentePotencia : exponentePotencia, ref resultado, negativo);
					break;
				default:
					ImprimirError("Opcion no reconocida, se saldra de la aplicacion");
					break;
			}
			Console.WriteLine("El resultado de {0} elevado a la {1} es: ", basePotencia, exponentePotencia);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(String.Format("\n\t{0}", resultado));
			Console.ResetColor();
        }
    }
}
