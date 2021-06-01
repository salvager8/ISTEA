using System;
using System.Threading;

namespace tarea_8
{
    class Program
    {
		static void ImprimirEnunciado() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\t1-Cree una aplicación consola que devuelva el valor de la suma de una array enviado por argumento.");
			Console.WriteLine("\t2-Cree una aplicación consola que devuelva el valor de la media de una array enviado por argumento.");
			Console.WriteLine("\t3-Cree una función que devuelva el máximo de un array.");
			Console.WriteLine("\t4-Cree una función que devuelva el mínimo de un array.");
			Console.WriteLine("\t5-enviar a una función un array bidimencional y un numero entero y la función deberá devolver \"Dato Encontrado\" si el dato esta dentro del array.");
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

		static int ObtenerNumeroEntrada(int valorEntrada, string mensaje, bool validarOpcion) {
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			bool esEntero = int.TryParse(Console.ReadLine(), out valorEntrada);
			if (!esEntero) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n\tEl dato ingresado no es un numero entero, intente nuevamente");
				ImprimirError();
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje, validarOpcion);
			} else if (validarOpcion) {
				if (valorEntrada > 5 || valorEntrada < 1) {
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\nEl ejercicio ingresado no es valido (1 al 5), intente nuevamente");
					ImprimirError();
					valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje, validarOpcion);
				}
			}

			return valorEntrada;
		}

		static string[] ObtenerArrayDeString(string valorEntrada) {
			string[] transformado = valorEntrada.Split(",".ToCharArray());
			return transformado;
		}

		static int Ejercicio1() {
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Selecciono el ejercicio 1, una aplicación consola que devuelva el valor de la suma de un array enviado por argumento.");
			Thread.Sleep(3000);
			Console.ResetColor();

			Console.Write("\nPor favor ingrese el array: ");
			string arrayEntrada = Console.ReadLine();
			string[] arrayTransformado = ObtenerArrayDeString(arrayEntrada);
			int totalSuma = 0;
			foreach (string valor in arrayTransformado) {
				int valorActual = 0;
				bool esEntero = int.TryParse(valor, out valorActual);
				if (esEntero) {
					totalSuma += valorActual;
				} else {
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\nAlguno de los valores que introdujo no es un valor entero, intente nuevamente");
					ImprimirError();
					return Ejercicio1();
				}
			}
			return totalSuma;
		}

		static int Ejercicio2() {
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Selecciono el ejercicio 2, una aplicación consola que devuelva el valor del average de un array enviado por argumento.");
			Thread.Sleep(3000);
			Console.ResetColor();

			Console.Write("\nPor favor ingrese el array: ");
			string arrayEntrada = Console.ReadLine();
			string[] arrayTransformado = ObtenerArrayDeString(arrayEntrada);
			int totalSuma = 0;
			int contador = 0;
			foreach (string valor in arrayTransformado) {
				int valorActual = 0;
				bool esEntero = int.TryParse(valor, out valorActual);
				if (esEntero) {
					totalSuma += valorActual;
				} else {
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Alguno de los valores que introdujo no es un valor entero, intente nuevamente");
					ImprimirError();
					return Ejercicio2();
				}
				contador++;
			}
			return totalSuma / contador;
		}

		static int Ejercicio3() {
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Selecciono el ejercicio 3, una aplicación consola que devuelva el valor maximo de un array enviado por argumento.");
			Thread.Sleep(3000);
			Console.ResetColor();

			Console.Write("\nPor favor ingrese el array: ");
			string arrayEntrada = Console.ReadLine();
			string[] arrayTransformado = ObtenerArrayDeString(arrayEntrada);
			foreach (string valor in arrayTransformado) {
				int valorActual = 0;
				bool esEntero = int.TryParse(valor, out valorActual);
				if (!esEntero) {
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Alguno de los valores que introdujo no es un valor entero, intente nuevamente");
					ImprimirError();
					return Ejercicio2();
				}
			}
			return ;
		}

        static void Main(string[] args) {
			ImprimirEnunciado();

			int ejercicio = ObtenerNumeroEntrada(0, "Por favor, elija un ejercicio (1 al 5): ", true);
			int total = 0;
			switch (ejercicio) {
				case 1:
					total = Ejercicio1();
					Console.WriteLine("La suma del array que introdujo es: {0}", total);
					break;
				case 2:
					total = Ejercicio2();
					Console.WriteLine("El average del array que introdujo es: {0}", total);
					break;
				case 3:
					total = Ejercicio3();
					Console.WriteLine("El maximo del array que introdujo es: {0}", total);
					break;
				case 4:
					total = Ejercicio4();
					Console.WriteLine("El minimo del array que introdujo es: {0}", total);
					break;
			}
        }
    }
}
