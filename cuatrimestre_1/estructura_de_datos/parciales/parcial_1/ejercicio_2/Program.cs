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

		// Toma el valor de entrada e intenta convertirlo a un entero
		static int ObtenerNumeroDeEntrada(int valorEntrada, string mensaje, bool validarColumnas) {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			bool isInt = int.TryParse(Console.ReadLine(), out valorEntrada);
			if (!isInt) {
				ImprimirError("El texto debe ser un numero entero, intente nuevamente\n");
				valorEntrada = ObtenerNumeroDeEntrada(valorEntrada, mensaje, validarColumnas);
			} else if (valorEntrada <= 0) {
				ImprimirError("El numero ingresado debe ser positivo y distinto de cero, intente nuevamente\n");
				valorEntrada = ObtenerNumeroDeEntrada(valorEntrada, mensaje, validarColumnas);
			} else if (!validarColumnas && valorEntrada > 4) {
				ImprimirError("El numero ingresado es invalido, debe estar entre 1 y 4\n");
				valorEntrada = ObtenerNumeroDeEntrada(valorEntrada, mensaje, validarColumnas);
			} else if (valorEntrada > 30) {
				ImprimirError("El numero ingresado es invalido, debe estar entre 1 y 30\n");
				valorEntrada = ObtenerNumeroDeEntrada(valorEntrada, mensaje, validarColumnas);
			}

			return valorEntrada;
		}

		// Imprime un titulo con formato especial
		static void ImprimirTitulo(string mensaje) {
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Write(mensaje);
			Console.ResetColor();
		}

		// Calcula segun una operacion dada
		static double CalcularOperacion(double primerNumero, double operacion, double segundoNumero, double[] promedios) {
			switch (operacion) {
				case 1:
					if (promedios[0] == 0) {
						promedios[0] = primerNumero + segundoNumero;
					} else {
						promedios[0] = Math.Min(promedios[0], primerNumero + segundoNumero);
					}
					return primerNumero + segundoNumero;
				case 2:
					if (promedios[1] == 0) {
						promedios[1] = primerNumero - segundoNumero;
					} else {
						promedios[1] = Math.Max(promedios[1], primerNumero - segundoNumero);
					}
					return primerNumero - segundoNumero;
				case 3:
					promedios[2] += primerNumero * segundoNumero;
					return primerNumero * segundoNumero;
				case 4:
					promedios[3] += Math.Round(primerNumero / segundoNumero, 4);
					return Math.Round(primerNumero / segundoNumero, 4);
				default:
					return 0;
			}
		}

		// Cambia el color para mostrar el resultado de una operacion
		static void TomarColorPorOperacion(double operacion) {
			switch (operacion) {
				case 1:
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				case 2:
					Console.ForegroundColor = ConsoleColor.Blue;
					break;
				case 3:
					Console.ForegroundColor = ConsoleColor.Yellow;
					break;
				case 4:
					Console.ForegroundColor = ConsoleColor.Green;
					break;
				default:
					Console.ResetColor();
					break;
			}
		}

		static void Main(string[] args)
		{
			ImprimirEnunciado();

			// Declaracion de variables
			int numeroDeFilas = ObtenerNumeroDeEntrada(0, "Por favor introduzca el numero de filas (1 a 4): ", false);
			int numeroDeColumnas = ObtenerNumeroDeEntrada(0, "Por favor introduzca el numero de columnas (1 a 30): ", true);
			double[] promedios = new double [4];
			double numeroMaximo = 0;
			double numeroMinimo = 0;
			int cantidadDivisiones = 0;
			double[,] operaciones = new double [numeroDeFilas, numeroDeColumnas];
			Random manejadorRandom = new Random();

			// Genera el array bidimensional de la grilla de bingo
			for (int i = 0; i < numeroDeFilas; i++) {
				for (int j = 0; j < numeroDeColumnas; j++) {
					switch (i) {
						case 0:
						case 2:
							operaciones[i, j] = manejadorRandom.Next(100, 1001);
							break;
						case 1:
							operaciones[i, j] = manejadorRandom.Next(1, 5);
							break;
						case 3:
							operaciones[i, j] = CalcularOperacion(operaciones[0, j], operaciones[1, j], operaciones[2, j], promedios);
							if (operaciones[1, j] == 4) {
								cantidadDivisiones++;
							}
							if (numeroMaximo == 0) {
								numeroMaximo = operaciones[i, j];	
							} else {
								numeroMaximo = Math.Max(numeroMaximo, operaciones[i, j]);
							}

							if (numeroMinimo == 0) {
								numeroMinimo = operaciones[i, j];
							} else {
								numeroMinimo = Math.Min(numeroMinimo, operaciones[i, j]);
							}
							break;
					}
				}
			}

			// Declarar variables para impresion
			int deltaColumnas = 0;
			int auxiliarColumnas = numeroDeColumnas;
			int columnasMenores = 0;


			// Imprimir la tabla ya generada con formato
			Console.Clear();
			ImprimirTitulo("Tabla ordenada:\n\n");
			while (true) {
				Console.Write("\t");
				for (int i = 0; i < numeroDeFilas; i++) {
					columnasMenores = Math.Min(auxiliarColumnas, 10);
					for (int j = 0; j < columnasMenores; j++) {
						if (i == 3) {
							TomarColorPorOperacion(operaciones[1, j + deltaColumnas]);
						}
						Console.Write("\t"+operaciones[i, j + deltaColumnas]+"\t");

						Console.ResetColor();

						if (j == Math.Min(auxiliarColumnas - 1, 9) && i != numeroDeFilas - 1) {
							Console.Write("\n");
							Console.Write("\t");
						}
					}
				}
				Console.Write("\n\n");

				deltaColumnas += 10;
				auxiliarColumnas -= 10;
				if (auxiliarColumnas < 0) {
					break;
				}
			}
			
			// Impresion de resultados
			ImprimirTitulo("Resultados:");
			Console.Write("\n");

			Console.Write("\n\tEl Maximo de todos los resultados es: {0}", numeroMaximo);
			Console.Write("\n\tEl Minimo de todos los resultados es: {0}", numeroMinimo);

			Console.WriteLine("\n\nPor operacion:");

			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("\n\tSuma: \n");
			Console.ResetColor();
			Console.Write("\t\tEl minimo de las sumas es: {0}\n", promedios[0]);

			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("\n\tResta: \n");
			Console.ResetColor();
			Console.Write("\t\tEl maximo de las restas es: {0}\n", promedios[1]);


			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("\n\tMultiplicacion: \n");
			Console.ResetColor();
			Console.Write("\t\tLa suma de las multiplicaciones es: {0}\n", promedios[2]);

			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("\n\tDivision: \n");
			Console.ResetColor();
			Console.Write("\t\tEl promedio de las divisiones es: {0}\n", promedios[3] / cantidadDivisiones);
		}
	}
}
