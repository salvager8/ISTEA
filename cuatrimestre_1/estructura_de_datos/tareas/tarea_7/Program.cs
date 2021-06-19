using System;
using System.Threading;
using System.Collections.Generic;

namespace tarea_7
{
	class Program
	{
		// Imprime el enunciado por pantalla y pide el ingreso de un enter
		static void ImprimirTitulo() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tSe deberá generar una aplicación consola que implemente una lista de datos de tipo string junto su respectivo menú con las opciones de agregar,");
			Console.WriteLine("\teliminar, mostrar, contar elementos, buscar ,ordenar y borrar la lista.");
			Console.WriteLine("\tTodo debe estar incluido dentro de funciones para tal fin, y como el ejercicio anterior, en caso de que el usuario");
			Console.WriteLine("\tno agregue ningún dato se ingresara \"como nuevo elemento\"");
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

		static int ObtenerNumeroEntrada(int valorEntrada, string mensaje, bool validarOpcion = false) {
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			bool esEntero = int.TryParse(Console.ReadLine(), out valorEntrada);
			if (!esEntero) {
				ImprimirError("\n\tEl dato ingresado no es un numero entero, intente nuevamente");
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje, validarOpcion);
			} else if (validarOpcion) {
				if (valorEntrada > 8 || valorEntrada < 1) {
					ImprimirError("\nLa opcion ingresada no es valida (1 al 8), intente nuevamente");
					valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje, validarOpcion);
				}
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

		static void Main(string[] args)
		{
			ImprimirTitulo();
			List<string> datos = new List<string>();
			int opcion = ObtenerNumeroEntrada(0, "Ingrese la opcion a elegir (1 - agregar, 2 - eliminar, 3 - mostrar, 4 - contar elementos, 5 - buscar, 6 - ordenar, 7 - borrar la lista, 8 - salir): ", true);
			
			string dato = "";
			int contador = 0;
			bool auxiliar = true;
			while (auxiliar) {
				switch (opcion) {
					case 1:
						dato = ObtenerStringDeEntrada("", "Ingrese el dato a anadir: ");
						datos.Add(dato);
						break;
					case 2:
						dato = ObtenerStringDeEntrada("", "Ingrese el dato a eliminar: ");
						datos.Add(dato);
						contador = 0;
						foreach (string valor in datos) {
							if (valor == dato) {
								datos.RemoveAt(contador);
								break;
							}
							contador++;
						}
						break;
					case 3:
						Console.WriteLine("Mostrando Lista:");
						foreach (string valor in datos) {
							Console.ForegroundColor = ConsoleColor.Blue;
							Console.WriteLine("");
							Console.WriteLine(valor);
						}
						Console.ResetColor();
						Thread.Sleep(3000);
						break;
					case 4:
						contador = 0;
						foreach (string valor in datos) {
							contador++;
						}
						Console.WriteLine("El conteo de Elementos dio como resultado {}", contador + 1);
						break;
					case 5:
						dato = ObtenerStringDeEntrada("", "Ingrese el dato a buscar: ");
						datos.Add(dato);
						contador = 0;
						foreach (string valor in datos) {
							if (valor == dato) {
								Console.WriteLine("Dato Encontrado!");
								Thread.Sleep(3000);
								contador = 1;
							}
						}
						if (contador == 0) {
							Console.WriteLine("Dato no Encontrado.");
							Thread.Sleep(3000);
						}
						break;
					case 6:
						datos.Sort();
						Console.WriteLine("Mostrando Lista Ordenada:");
						foreach (string valor in datos) {
							Console.ForegroundColor = ConsoleColor.Blue;
							Console.WriteLine("");
							Console.WriteLine(valor);
						}
						Console.ResetColor();
						Thread.Sleep(3000);
						break;
					case 7:
						datos = new List<string>();
						Console.WriteLine("Lista eliminada");
						Thread.Sleep(3000);
						break;
					case 8:
						auxiliar = false;
						continue;
				}
				Console.Clear();
				opcion = ObtenerNumeroEntrada(0, "Ingrese la opcion a elegir (1 - agregar, 2 - eliminar, 3 - mostrar, 4 - contar elementos, 5 - buscar, 6 - ordenar, 7 - borrar la lista, 8 - salir): ", true);
			}
			Console.WriteLine("Gracias!");
		}
	}
}
