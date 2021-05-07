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
			Console.WriteLine("\tSe deberá realizar una aplicación consola que instancie un array bidireccional de 3 filas por 9 columnas con números aleatorios enteros entre 1 y 10,");
			Console.WriteLine("\tpor otro lado se deberá ingresar por pantalla 2 números enteros con el mismo requisito de los que se cargaron previamente.");
			Console.WriteLine("\n\tLa aplicación deberá recorrer todo el array y confirmar si ambos números están en el mismo, en caso afirmativo se mostrara una leyenda por pantalla");
			Console.WriteLine("\t\"Bingo, ambos números están en grilla\" y en caso contrario un mensaje que se deberá volver a intentar la aplicación finalizara cuando el primer valor");
			Console.WriteLine("\tsea igual a 0. Se deberá imprimir por pantalla toda la grilla con los números ganadores resaltados de alguna manera.");
			Console.WriteLine("\n\tLos valores ingresados por pantalla no pueden ser iguales entre si.");
			Console.ResetColor();
			Console.WriteLine("\nPresione [ENTER] para continuar");
			ConsoleKeyInfo keyInfo = Console.ReadKey();
			while (keyInfo.Key != ConsoleKey.Enter)
				keyInfo = Console.ReadKey();
			Console.Clear();
		}

		// Encapsula la impresion de un error en color rojo
		static void ImprimirError(string mensaje, int[,] bingo) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("\n\t{0}", mensaje);
			Thread.Sleep(2000);
			Console.Clear();
			Console.ResetColor();
			ImprimirBingo(bingo);
		}

		// Imprime una linea de guiones por consola
		static void ImprimirLinea() {
			Console.WriteLine("\t-------------------------------------------------------------------------------------------------------------");
		}

		// Imprime la grilla de bingo por pantalla
		static bool ImprimirBingo(int[,] bingo, int primerNumero = 0, int segundoNumero = 0) {
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("Tabla de Bingo:\n");
			Console.ResetColor();
			bool primerNumeroEnBingo = false;
			bool segundoNumeroEnBingo = false;

			ImprimirLinea();
			Console.Write("\t|");
			int contador = 0;
			foreach (int numero in bingo) {
				if (contador == 9) {
					Console.Write("\n");
					contador = 0;
					ImprimirLinea();
					Console.Write("\t|");
				}

				if (primerNumero > 0) {
					if (numero == primerNumero) {
						Console.BackgroundColor = ConsoleColor.Red;
						primerNumeroEnBingo = true;
					} else if (numero == segundoNumero) {
						Console.BackgroundColor = ConsoleColor.Red;
						segundoNumeroEnBingo = true;
					}
				}

				if (numero > 9) {
					Console.Write("     "+numero+"    ");
				} else {
					Console.Write("     "+numero+"     ");
				}

				Console.ResetColor();
				contador++;
				Console.Write("|");
			}
			Console.Write("\n");
			ImprimirLinea();

			if (primerNumero > 0 && (!primerNumeroEnBingo || !segundoNumeroEnBingo)) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\nNo ha habido bingo, debe volver a intentar");
				Console.ResetColor();
				Console.WriteLine("\nPresione [ENTER] para continuar");
				ConsoleKeyInfo keyInfo = Console.ReadKey();
				while (keyInfo.Key != ConsoleKey.Enter)
					keyInfo = Console.ReadKey();
				Console.Clear();
			}

			return primerNumeroEnBingo && segundoNumeroEnBingo;
		}

		// Toma el valor de entrada e intenta convertirlo a un entero
		static int ObtenerNumeroDeEntrada(int valorEntrada, string mensaje, int[,] bingo, bool esPrimerNumero) {
			Console.Clear();
			ImprimirBingo(bingo);
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			bool isInt = int.TryParse(Console.ReadLine(), out valorEntrada);
			if (!isInt) {
				ImprimirError("El texto debe ser un numero entero, intente nuevamente\n", bingo);
				valorEntrada = ObtenerNumeroDeEntrada(valorEntrada, mensaje, bingo, esPrimerNumero);
			} else if (valorEntrada < 0) {
				ImprimirError("El numero ingresado debe ser positivo, intente nuevamente\n", bingo);
				valorEntrada = ObtenerNumeroDeEntrada(valorEntrada, mensaje, bingo, esPrimerNumero);
			} else if (esPrimerNumero && (valorEntrada < 0 || valorEntrada > 10)) {
				ImprimirError("El numero ingresado es invalido, debe estar entre 0 y 10, 0 para salir\n", bingo);
				valorEntrada = ObtenerNumeroDeEntrada(valorEntrada, mensaje, bingo, esPrimerNumero);
			} else if (!esPrimerNumero && (valorEntrada < 1 || valorEntrada > 10)) {
				ImprimirError("El numero ingresado es invalido, debe estar entre 1 y 10\n", bingo);
				valorEntrada = ObtenerNumeroDeEntrada(valorEntrada, mensaje, bingo, esPrimerNumero);
			}

			return valorEntrada;
		}

		static void ImprimirCierre() {
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Hasta Luego!");
			Console.ResetColor();
		}

		static void Main(string[] args)
		{
			ImprimirEnunciado();

			int[,] bingo = new int [3, 9];
			Random manejadorRandom = new Random();

			// Genera el array bidimendional de la grilla de bingo
			for (int i = 0; i < bingo.GetLength(0); i++) {
				for (int j = 0; j < bingo.GetLength(1); j++) {
					bingo[i, j] = manejadorRandom.Next(1, 11);
				}
			}

			// Inicializacion de variables
			int primerNumero = 0;
			int segundoNumero = 0;

			// Ciclo principal
			Console.Clear();
			while (true) {
				while (true) {
					primerNumero = ObtenerNumeroDeEntrada(0, "\nPor favor ingrese el primer numero del bingo: ", bingo, true);
					if (primerNumero == 0) {
						return;
					}
					segundoNumero = ObtenerNumeroDeEntrada(0, "\nPor favor ingrese el segundo numero del bingo: ", bingo, false);

					if (primerNumero != segundoNumero) {
						break;
					} else {
						ImprimirError("Los numeros no pueden ser iguales, intente nuevamente", bingo);
					}
				}

				Console.Clear();
				if (ImprimirBingo(bingo, primerNumero, segundoNumero)) {
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("\nBingo, ambos números están en grilla");
					Console.ResetColor();
					Console.WriteLine("\nPresione [ENTER] para continuar");
					ConsoleKeyInfo keyInfo = Console.ReadKey();
					while (keyInfo.Key != ConsoleKey.Enter)
						keyInfo = Console.ReadKey();
					Console.Clear();
				}
			}
		}
	}
}
