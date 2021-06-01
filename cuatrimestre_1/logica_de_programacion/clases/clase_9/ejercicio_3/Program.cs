using System;
using System.Threading;

namespace ejercicio_3
{
    class Program
    {
		static int ObtenerNumeroEntrada(int valorEntrada, string mensaje)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			bool esInt = int.TryParse(Console.ReadLine(), out valorEntrada);
			if (!esInt) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n\tEl texto ingresado no es un numero, intente nuevamente");
				ImprimirError();
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje);
			} else if (valorEntrada < 40) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n\tEl numero ingresado debe ser al menos 40");
				ImprimirError();
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje);
			}

			return valorEntrada;
		}

		static void ImprimirError() {
			Thread.Sleep(2000);
			Console.Clear();
			Console.ResetColor();
		}

		static int ObtenerValorEntrada() {
			Random randomManager = new Random();
			return randomManager.Next(1, 11);
		}

        static void Main(string[] args)
        {
			Console.Clear();
			int cantidadElementos = ObtenerNumeroEntrada(0, "Ingrese la cantidad de elementos: ");
            int[] elementos = new int[cantidadElementos];

			for (int i = 0; i < cantidadElementos; i++) {
				elementos[i] = ObtenerValorEntrada();
			}

			int contador = 0;
			foreach(int elemento in elementos) {
				if (contador == 10) {
					Console.Write("|\n");
					contador = 0;
				}
				Console.Write("|\t{0}\t", elemento);
				contador++;
			}
			Console.Write("|\n");
        }
    }
}
