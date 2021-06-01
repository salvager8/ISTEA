using System;
using System.Threading;

namespace ejercicio_1
{
    class Program
    {
		static void ImprimirEnunciado() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tRealizar una aplicacion consola que permita el ingreso por pantalla de 2 numeros enteros y devuelva");
			Console.WriteLine("\tla multipliacion de los mismos, luego se debera pasar ese valor a otra funcion que imprima por pantalla el resultado.");
			Console.ResetColor();
			Console.WriteLine("\n[ENTER]");
			ConsoleKeyInfo keyInfo = Console.ReadKey();
			while(keyInfo.Key != ConsoleKey.Enter)
    			keyInfo = Console.ReadKey();
			Console.Clear();
		}

		static void ImprimirError() {
			Thread.Sleep(2000);
			Console.Clear();
			Console.ResetColor();
		}

		static void ImprimirResultado(string mensaje) {
			Console.WriteLine(mensaje);
		}

		static int ObtenerNumeroEntrada(int valorEntrada, string mensaje)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			bool esint = int.TryParse(Console.ReadLine(), out valorEntrada);
			if (!esint) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n\tEl texto ingresado no es un numero, intente nuevamente");
				ImprimirError();
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje);
			}

			return valorEntrada;
		}

		static int CalcularOperacion(int primerNumero, int segundoNumero) {
			return primerNumero * segundoNumero;
		}

        static void Main(string[] args)
        {
			ImprimirEnunciado();
			Console.Clear();
			int primerNumero = ObtenerNumeroEntrada(0, "Ingrese el primer numero: ");
			Console.Clear();
			int segundoNumero = ObtenerNumeroEntrada(0, "Ingrese el segundo numero: ");
			Console.Clear();
			int resultado = CalcularOperacion(primerNumero, segundoNumero);
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Green;
			ImprimirResultado(String.Format("\nEl resultado de {0} por {1} es: {2}\n", primerNumero, segundoNumero, resultado));
        }
    }
}
