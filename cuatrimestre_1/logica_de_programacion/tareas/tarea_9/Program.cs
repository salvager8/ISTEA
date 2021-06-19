using System;
using System.Threading;

namespace tarea_9
{
	class Program
	{
		static void ImprimirEnunciado() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tSe requiere crear una aplicación consola que implemente 4 arrays unidimencionales (Nombre, apellido, edad y genero)");
			Console.WriteLine("\ty se carguen por pantalla como mínimo 2 líneas de datos, luego que se muestre por pantalla con el formato correcto.");
			Console.ResetColor();
			Console.WriteLine("\n[ENTER]");
			ConsoleKeyInfo keyInfo = Console.ReadKey();
			while(keyInfo.Key != ConsoleKey.Enter)
				keyInfo = Console.ReadKey();
			Console.Clear();
		}

		static void ImprimirError(string mensaje) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(mensaje);
			Thread.Sleep(2000);
			Console.Clear();
			Console.ResetColor();
		}

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

		static void ImprimirError() {
			Thread.Sleep(2000);
			Console.Clear();
			Console.ResetColor();
		}

		static int ObtenerNumeroEntrada(int valorEntrada, string mensaje) {
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			bool esEntero = int.TryParse(Console.ReadLine(), out valorEntrada);
			if (!esEntero) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n\tEl dato ingresado no es un numero entero, intente nuevamente");
				ImprimirError();
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje);
			}

			return valorEntrada;
		}

		static void Main(string[] args) {
			ImprimirEnunciado();

			string[] nombres = new string[2];
			string[] apellidos = new string[2];
			int[] edades = new int[2];
			char[] generos = new char[2];

			int contador = 0;

			while (contador < 2) {
				nombres[contador] = ObtenerStringDeEntrada("", "Ingrese el nombre: ");
				apellidos[contador] = ObtenerStringDeEntrada("", "Ingrese el apellido: ");
				edades[contador] = ObtenerNumeroEntrada(0, "Ingrese la edad: ");
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.Write("Ingrese el sexo: ");
				Console.ResetColor();
				generos[contador] = Convert.ToChar((Convert.ToString(Console.ReadLine()).ToUpper())[0]);
				contador++;
				Console.Clear();
			}

			contador = 0;
			foreach (string nombre in nombres) {
				Console.WriteLine("Nombre: {0}", nombre);
				Console.WriteLine("Apellido: {0}", apellidos[contador]);
				Console.WriteLine("Edad: {0}", edades[contador]);
				Console.WriteLine("Sexo: {0}", generos[contador]);
				Console.WriteLine("");
			}

		}
	}
}
