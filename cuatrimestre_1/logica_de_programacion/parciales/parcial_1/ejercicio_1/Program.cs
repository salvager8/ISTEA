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
			Console.WriteLine("\tRealizar una aplicación consola para una empresa de venta de electrodomésticos, se deberá ingresar un producto,");
			Console.WriteLine("\tel precio y la cantidad de cuotas a realizar la venta (6,12,18)");
			Console.WriteLine("\n\tComo resultado se deberá mostrar por pantalla el precio del producto financiado por la cantidad de cuotas");
			Console.WriteLine("\tingresada y la marcha de las cuotas desde el mes siguiente a la venta.");
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

		// Toma el valor de entrada e intenta convertirlo a un flotante
		static double ObtenerNumeroDeEntrada(double valorEntrada, string mensaje, bool validarCuotas) {
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Clear();
			Console.Write(mensaje);
			Console.ResetColor();

			bool isDouble = double.TryParse(Console.ReadLine(), out valorEntrada);
			if (!isDouble) {
				ImprimirError("El texto ingresado no es un numero, intente nuevamente\n");
				valorEntrada = ObtenerNumeroDeEntrada(valorEntrada, mensaje, validarCuotas);
			} else if (valorEntrada < 0) {
				ImprimirError("El numero ingresado debe ser positivo, intente nuevamente\n");
				valorEntrada = ObtenerNumeroDeEntrada(valorEntrada, mensaje, validarCuotas);
			} else if (validarCuotas) {
				if (valorEntrada != 6 && valorEntrada != 12 && valorEntrada != 18) {
					ImprimirError("La cuota ingresada no es valida, intente con 6, 12 o 18 cuotas\n");
					valorEntrada = ObtenerNumeroDeEntrada(valorEntrada, mensaje, validarCuotas);
				}
			} else if (valorEntrada > 2147483647) {
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("\nEl numero que ingreso es bastante grande, ¿Seguro/a quiere continuar?\n");
				Console.Write("Ingrese \"y\" para continuar, cualquier otro caracter para ingresar otro numero: ");
				string valorEntradaAdvertencia = Console.ReadLine();
				Console.WriteLine("\n");
				if (valorEntradaAdvertencia == "y") {
					return valorEntrada;
				}
				valorEntrada = ObtenerNumeroDeEntrada(0, "Por favor ingrese el precio del producto: ", false);
			}
			return valorEntrada;
		}

		// Toma el valor de entrada y trata de validar que no sea nulo o este vacio
		static string ObtenerStringDeEntrada(string valorDeEntrada, string mensaje) {
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			valorDeEntrada = Console.ReadLine();
			if (String.IsNullOrEmpty(valorDeEntrada)) {
				ImprimirError("\n\tEl valor no puede estar vacio, intente nuevamente\n");
				valorDeEntrada = ObtenerStringDeEntrada(valorDeEntrada, mensaje);
			}
			return valorDeEntrada;
		}

		// Calcula el porcentaje en funcion del numero de cuotas
		static float obtenerPorcentajeDeCuota(int cuotas) {
			switch (cuotas) {
				case 6:
					return 30;
				case 12:
					return 50;
				case 18:
					return 80;
				default:
					return 0;
			}
		}

		static void Main(string[] args) {
			// Imprime el enunciado por pantalla
			ImprimirEnunciado();

			// Inicializacion de Variables
			string producto;
			double precio;
			int cuota;
			double total;
			double precioPorCuota;

			// Obtener datos ingresados por el usuario
			producto = ObtenerStringDeEntrada("", "Por favor ingrese un producto: ");
			precio = ObtenerNumeroDeEntrada(0, "Por favor ingrese el precio del producto: ", false);
			cuota = Convert.ToInt16(ObtenerNumeroDeEntrada(0, "Por favor ingrese en cuantas cuotas quiere pagar (6 al 30%, 12 al 50%, 18 al 80%): ", true));

			// Calcular el total a pagar
			total = precio + (precio * (obtenerPorcentajeDeCuota(cuota) / 100));
			precioPorCuota = total / cuota;

			// Mostrar los resultados calculados
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("\nSuma un total financiado a pagar de: {0:c2}", total);
			DateTime ahora = DateTime.Now;
			Console.WriteLine("\nLa venta se realizo el dia {0}\n", ahora.ToString("d"));
			Console.WriteLine("Se fijo un total de {0} cuotas, cada una tiene un precio de {1:c2}", cuota, precioPorCuota);
			Console.WriteLine("Y se distribuiran de la siguiente manera:\n");
			Console.ResetColor();
			for (int i = 0; i < cuota; i++) {
				Console.WriteLine("El {0} debera pagar {1:c2}", ahora.AddMonths(i + 1).ToString("d"), precioPorCuota);
			}
			Console.Write("\n");
		}
	}
}
