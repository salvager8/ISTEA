using System;

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
		A partir de un array bidimensional de 4 filas por 30 columnas, se deberá
		confeccionar una aplicación consola que realice las 4 operaciones básicas
		sobre el array que se cargara con valores aleatorios teniendo en cuenta que:

		La fila Nro.1 y 3 se deberán cargar con valores aleatorios entre 100 y 1000.
		La Fila 2 se deberá cargar con valores aleatorios entre 1 y 5 representando la
		operación a realizar.
		La fila 4 mostrara el resultado con el color correspondiente a la operación
		realizada.
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
