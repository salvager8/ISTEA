using System;
using System.Threading;

namespace ejercicio_4
{
    class Program
    {
		static void ImprimirError() {
			Thread.Sleep(2000);
			Console.Clear();
			Console.ResetColor();
		}

		static int ObtenerNumeroEntrada(int valorEntrada, string mensaje, bool validarEdad)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			bool esInt = int.TryParse(Console.ReadLine(), out valorEntrada);
			if (!esInt) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n\tEl texto ingresado no es un numero, intente nuevamente");
				ImprimirError();
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje, validarEdad);
			} else if (valorEntrada < 0) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n\tEl valor ingresado debe ser positivo");
				ImprimirError();
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje, validarEdad);
			} else if (!validarEdad && valorEntrada > 99999999) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n\tEl numero ingresado no es un DNI valido");
				ImprimirError();
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje, validarEdad);
			} else if (validarEdad && valorEntrada > 120) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n\tEl numero ingresado no es una edad valida");
				ImprimirError();
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje, validarEdad);
			}

			return valorEntrada;
		}

		static string ConcatenarResultado(string nombre, string apellido, int dni, int edad) {
			return String.Format("Nombre: {0},\nApellido: {1},\nDNI: {2},\nEdad: {3}", nombre, apellido, dni, edad);
		}

		static void ImprimirResultado(string mensaje) {
			Console.WriteLine(mensaje);
		}

        static void Main(string[] args)
        {
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Ingrese su nombre: ");
			Console.ResetColor();
            string nombre = Console.ReadLine();

			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Ingrese su apellido: ");
			Console.ResetColor();
			string apellido = Console.ReadLine();

			int dni = ObtenerNumeroEntrada(0, "Ingrese su DNI (solo numeros): ", false);
			int edad = ObtenerNumeroEntrada(0, "Ingrese su edad (solo numeros): ", true);

			string resultado = ConcatenarResultado(nombre, apellido, dni, edad);
			ImprimirResultado(resultado);
        }
    }
}
