using System;
using System.Threading;

namespace ejercicio_2
{
	class Program
	{
		// Imprime el enunciado por pantalla y pide el ingreso de un enter
		static void ImprimirEnunciado() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tDesarrolle una aplicación consola con menú incluido que solicite una opción para calcular la superficie de figuras");
			Console.WriteLine("\tgeométricas básicas, triangulo, rectángulo, cuadrado, círculo y una opción para salir.");
			Console.WriteLine("\n\tCualquier otro se deberá mostrar un mensaje de error al igual que si se ingresa un dato no valido.");
			Console.WriteLine("\n\tComo resultado se deberá mostrar la superficie de la figura elegida y luego volver al menú principal.");
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
			Thread.Sleep(3000);
			Console.Clear();
			Console.ResetColor();
		}

		// Toma el valor de entrada e intenta convertirlo a un doble flotante
		static double ObtenerNumeroDeEntrada(double valorEntrada, string mensaje, bool validarFigura) {
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Clear();
			Console.Write(mensaje);
			Console.ResetColor();

			bool isDouble = double.TryParse(Console.ReadLine(), out valorEntrada);
			if (!isDouble) {
				ImprimirError("El texto ingresado no es un numero, intente nuevamente\n");
				valorEntrada = ObtenerNumeroDeEntrada(valorEntrada, mensaje, validarFigura);
			} else if (valorEntrada < 0) {
				ImprimirError("El numero ingresado debe ser positivo, intente nuevamente\n");
				valorEntrada = ObtenerNumeroDeEntrada(valorEntrada, mensaje, validarFigura);
			} else if (validarFigura && valorEntrada != 0 && valorEntrada != 1 && valorEntrada != 2 && valorEntrada != 3 && valorEntrada != 4) {
				ImprimirError("El numero ingresado debe ser 1 (Triangulo), 2 (Rectangulo), 3 (Cuadrado), 4 (Circulo) o 0 (Salir)\n");
				valorEntrada = ObtenerNumeroDeEntrada(valorEntrada, mensaje, validarFigura);
			}
			return valorEntrada;
		}

		// Toma el valor de la figura y construye el mensaje con los resultados
		static string ConstruirMensajeResultado(double figura) {
			double baseFigura = 0;
			double alturaFigura = 0;
			double radioFigura = 0;
			const double pi = Math.PI;
			switch (figura) {
				case 1:
					baseFigura = ObtenerNumeroDeEntrada(0,"Ingrese la base: ", false);
					alturaFigura = ObtenerNumeroDeEntrada(0, "Ingrese la altura: ", false);
					Console.Clear();
					Console.ForegroundColor = ConsoleColor.Green;
					return String.Format("Datos:\n\tFigura: Triangulo\n\tBase: {0}\n\tAltura: {1}\n\nSuperficie: {2}", baseFigura, alturaFigura, (baseFigura * alturaFigura) / 2);
				case 2:
					baseFigura = ObtenerNumeroDeEntrada(0, "Ingrese la base: ", false);
					alturaFigura = ObtenerNumeroDeEntrada(0,"Ingrese la altura: ", false);
					Console.Clear();
					Console.ForegroundColor = ConsoleColor.Green;
					return String.Format("Datos:\n\tFigura: Rectangulo\n\tBase: {0}\n\tAltura: {1}\n\nSuperficie: {2}", baseFigura, alturaFigura, baseFigura * alturaFigura);
				case 3:
					baseFigura = ObtenerNumeroDeEntrada(0, "Ingrese la base: ", false);
					Console.Clear();
					Console.ForegroundColor = ConsoleColor.Green;
					return String.Format("Datos:\n\tFigura: Cuadrado\n\tBase: {0}\n\nSuperficie: {1}", baseFigura, Math.Pow(baseFigura, 2.0));
				case 4:
					radioFigura = ObtenerNumeroDeEntrada(0, "Ingrese el radio: ", false);
					Console.Clear();
					Console.ForegroundColor = ConsoleColor.Green;
					return String.Format("Datos:\n\tfigura: Circulo\n\tRadio: {0}\n\nSuperficie: {1}", radioFigura, pi * Math.Pow(radioFigura, 2.0));
				default:
					return "";
			}
		}

		static void Main(string[] args) {
			// Imprime el enunciado por pantalla
			ImprimirEnunciado();

			// Inicializacion de variables
			double figura = 0;

			// Ciclo principal
			while (true) {
				// Pedida de ingreso de datos
				figura = ObtenerNumeroDeEntrada(0, "Elija una de las siguientes opciones :\n\n1 - Triangulo\n2 - Rectangulo\n3 - Cuadrado\n4 - Circulo\n0 - Salir\n\n", true);
				if (figura == 0) {
					break;
				}

				// Imprimir resultados por pantalla
				Console.WriteLine(ConstruirMensajeResultado(figura));
				Console.ResetColor();
				Console.WriteLine("\n\nPresione [ENTER] para continuar");
				ConsoleKeyInfo keyInfo = Console.ReadKey();
				while (keyInfo.Key != ConsoleKey.Enter)
					keyInfo = Console.ReadKey();
				Console.Clear();
			}

			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("\nHasta Luego!\n");
		}
	}
}
