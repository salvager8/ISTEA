using System;
using System.Threading;

namespace ejercicio_1
{
	class Program
	{
		// Imprime el enunciado por pantalla y pide el ingreso de un enter
		static void ImprimirEnunciado() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tA partir de un array bidimensional de 4 filas por 30 columnas, se deberá confeccionar una aplicación consola que realice las 4 operaciones básicas");
			Console.WriteLine("\tsobre el array que se cargara con valores aleatorios teniendo en cuenta que:");
			Console.WriteLine("\n\t- La fila Nro.1 y 3 se deberán cargar con valores aleatorios entre 100 y 1000.");
			Console.WriteLine("\n\t- La fila 2 se deberá cargar con valores aleatorios entre 1 y 5 representando la operación a realizar.");
			Console.WriteLine("\n\t- La fila 4 mostrara el resultado con el color correspondiente a la operación realizada.");
			Console.ResetColor();
			Console.WriteLine("\nPresione [ENTER] para continuar");
			ConsoleKeyInfo keyInfo = Console.ReadKey();
			while (keyInfo.Key != ConsoleKey.Enter)
				keyInfo = Console.ReadKey();
			Console.Clear();
		}

		// Encapsula la impresion de un error en color rojo
		static void ImprimirError(string mensaje) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("\n\t{0}", mensaje);
			Thread.Sleep(2000);
			Console.Clear();
			Console.ResetColor();
		}

		// Imprime una linea de guiones por consola
		static void ImprimirLinea() {
			Console.WriteLine("\t-------------------------------------------------------------------------------------------------------------");
		}

		// Imprime la grilla de bingo por pantalla
		// static bool ImprimirBingo(int[,] bingo, int primerNumero = 0, int segundoNumero = 0) {
		// 	Console.ForegroundColor = ConsoleColor.Blue;
		// 	Console.WriteLine("Tabla de Bingo:\n");
		// 	Console.ResetColor();
		// 	bool primerNumeroEnBingo = false;
		// 	bool segundoNumeroEnBingo = false;

		// 	ImprimirLinea();
		// 	Console.Write("\t|");
		// 	int contador = 0;
		// 	foreach (int numero in bingo) {
		// 		if (contador == 9) {
		// 			Console.Write("\n");
		// 			contador = 0;
		// 			ImprimirLinea();
		// 			Console.Write("\t|");
		// 		}

		// 		if (primerNumero > 0) {
		// 			if (numero == primerNumero) {
		// 				Console.BackgroundColor = ConsoleColor.Red;
		// 				primerNumeroEnBingo = true;
		// 			} else if (numero == segundoNumero) {
		// 				Console.BackgroundColor = ConsoleColor.Red;
		// 				segundoNumeroEnBingo = true;
		// 			}
		// 		}

		// 		if (numero > 9) {
		// 			Console.Write("     "+numero+"    ");
		// 		} else {
		// 			Console.Write("     "+numero+"     ");
		// 		}

		// 		Console.ResetColor();
		// 		contador++;
		// 		Console.Write("|");
		// 	}
		// 	Console.Write("\n");
		// 	ImprimirLinea();

		// 	if (primerNumero > 0 && (!primerNumeroEnBingo || !segundoNumeroEnBingo)) {
		// 		Console.ForegroundColor = ConsoleColor.Red;
		// 		Console.WriteLine("\nNo ha habido bingo, debe volver a intentar");
		// 		Console.ResetColor();
		// 		Console.WriteLine("\nPresione [ENTER] para continuar");
		// 		ConsoleKeyInfo keyInfo = Console.ReadKey();
		// 		while (keyInfo.Key != ConsoleKey.Enter)
		// 			keyInfo = Console.ReadKey();
		// 		Console.Clear();
		// 	}

		// 	return primerNumeroEnBingo && segundoNumeroEnBingo;
		// }

		// Toma el valor de entrada e intenta convertirlo a un entero
		static int ObtenerNumeroDeEntrada(int valorEntrada, string mensaje) {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			bool isInt = int.TryParse(Console.ReadLine(), out valorEntrada);
			if (!isInt) {
				ImprimirError("El texto debe ser un numero entero, intente nuevamente\n");
				valorEntrada = ObtenerNumeroDeEntrada(valorEntrada, mensaje);
			} else if (valorEntrada < 0) {
				ImprimirError("El numero ingresado debe ser positivo, intente nuevamente\n");
				valorEntrada = ObtenerNumeroDeEntrada(valorEntrada, mensaje);
			} else if (valorEntrada < 0 || valorEntrada > 10) {
				ImprimirError("El numero ingresado es invalido, debe estar entre 0 y 10, 0 para salir\n");
				valorEntrada = ObtenerNumeroDeEntrada(valorEntrada, mensaje);
			}

			return valorEntrada;
		}

		static void ImprimirCierre() {
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Hasta Luego!");
			Console.ResetColor();
		}

		static int CalcularOperacion(int primerNumero, int operacion, int segundoNumero) {
			switch (operacion) {
				case 1:
					return primerNumero + segundoNumero;
				case 2:
					return primerNumero - segundoNumero;
				case 3:
					return primerNumero * segundoNumero;
				case 4:
					return primerNumero / segundoNumero;
				default:
					return 0;
			}
		}

		static void Main(string[] args)
		{
			ImprimirEnunciado();

			int numeroDeFilas = ObtenerNumeroDeEntrada(0, "Por favor introduzca el numero de filas (1 a 4)");
			int numeroDeColumnas = ObtenerNumeroDeEntrada(0, "Por favor introduzca el numero de columnas (1 a 30)");

			int[,] operaciones = new int [numeroDeFilas, numeroDeColumnas];
			Random manejadorRandom = new Random();

			// Genera el array bidimendional de la grilla de bingo
			for (int i = 0; i < operaciones.GetLength(0); i++) {
				for (int j = 0; j < operaciones.GetLength(1); j++) {
					switch (i) {
						case 0:
						case 2:
							operaciones[i, j] = manejadorRandom.Next(100, 1001);
							break;
						case 1:
							operaciones[i, j] = manejadorRandom.Next(1, 6);
							break;
						case 3:
							operaciones[i, j] = CalcularOperacion(operaciones[0, j], operaciones[1, j], operaciones[2, j]);
							break;
					}
				}
			}

			// Inicializacion de variables
			int primerNumero = 0;
			int segundoNumero = 0;

			
		}
	}
}
