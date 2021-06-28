using System;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace tarea_9
{
	class Program
	{
		// Opciones de la tabla
		private static int opcion = 0;
		private static List<string> llamados = new List<string>();
		private static Dictionary<string, string> usuarios = new Dictionary<string, string>();
		private static string[] opciones = new string[] { "Ingresar llamados", "Ingresar usuarios", "Ver llamados en espera", "Atender Llamado", "Salir" };

		// Imprime el enunciado por pantalla y pide el ingreso de un enter
		private static void ImprimirEnunciado() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\t1-Generar una aplicación consola que implemente una cola con llamados telefónicos en espera, se deberán generar como mínimo 4 funciones.");
			Console.WriteLine("\t-Dibujar Menú");
			Console.WriteLine("\t-Ingresar llamados.");
			Console.WriteLine("\t-Ingresar usuarios.\n");
			Console.WriteLine("\t-Ver llamados en espera.");
			Console.WriteLine("\t-Ingresar llamados debe ingresar nombre y apellido de cliente.");
			Console.WriteLine("\t-ver llamados debe mostrar la cantidad de llamados en espera y el detalle de los mismos.");
			Console.WriteLine("\t-atender llamados debe mostrar la leyenda correspondiente si no hay llamados en espera y en caso de haber debe mostrar el llamado que se atendió");
			Console.ResetColor();
			EnterParaContinuar();
		}

		// Pide un enter por pantalla para continuar
		private static void EnterParaContinuar() {
			Console.WriteLine("\nPresione [ENTER] para continuar");
			ConsoleKeyInfo keyInfo = Console.ReadKey();
			while (keyInfo.Key != ConsoleKey.Enter)
				keyInfo = Console.ReadKey();
			Console.Clear();
		}

		// Imprime el error por pantalla
		private static void ImprimirError(string mensaje) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("\n\t");
			Console.WriteLine(mensaje);
			Thread.Sleep(2000);
			Console.Clear();
			Console.ResetColor();
		}

		// Imprime el mensaje de exito por pantalla
		private static void ImprimirExito(string mensaje) {
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("\n\t");
			Console.WriteLine(mensaje);
			Thread.Sleep(2000);
			Console.Clear();
			Console.ResetColor();
		}

		// Alinea al centro un string relativo al ancho de la tabla
		static string AlinearAlCentro(string texto, int ancho) {
			texto = texto.Length > ancho ? texto.Substring(0, ancho - 3) + "..." : texto;

			if (string.IsNullOrEmpty(texto)) {
				return new string(' ', ancho);
			}
			else {
				return texto.PadRight(ancho - (ancho - texto.Length) / 2).PadLeft(ancho);
			}
		}

		// Imprime una linea para la tabla
		static void ImprimirLinea(int ancho = 35, params string[] columnas) {
			string fila = "";
			foreach (string columna in columnas) {
				fila += AlinearAlCentro(columna, ancho);
			}

			Console.WriteLine(fila);
		}

		// Construye una tabla para mostrarla por pantalla
		private static void MostrarOpciones() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Black;
			Console.WriteLine("");
			Console.BackgroundColor = ConsoleColor.Blue;
			ImprimirLinea(40, "Menu de ejecucion");
			Console.BackgroundColor = ConsoleColor.White;

			int contador = 0;
			foreach (string tituloOpcion in opciones) {
				if (contador == opcion) {
					Console.BackgroundColor = ConsoleColor.Cyan;
				} else {
					Console.BackgroundColor = ConsoleColor.White;
				}
				ImprimirLinea(40, tituloOpcion);
				Console.ResetColor();
				Console.ForegroundColor = ConsoleColor.Black;
				contador++;
			}
		}

		// Construye una tabla para mostrarla por pantalla
		private static void MostrarDatos() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Black;
			Console.WriteLine("");
			if (llamados.Count == 0) {
				Console.Clear();
				Console.WriteLine("");
				Console.BackgroundColor = ConsoleColor.White;
				ImprimirLinea(35, "No hay registros");
				Console.ResetColor();
				EnterParaContinuar();
			} else {
				Console.BackgroundColor = ConsoleColor.Blue;
				ImprimirLinea(70, "Lista de llamados");
				Console.BackgroundColor = ConsoleColor.White;
				foreach (string llamado in llamados) {
					ImprimirLinea(70, llamado);
				}
				ImprimirLinea(35, "Total llamados: ", Convert.ToString(llamados.Count));
				Console.ResetColor();
				EnterParaContinuar();
			}
		}

		// Valida un string ingresado por pantalla
		private static string ObtenerStringEntrada(string valorEntrada, string mensaje, bool validarPass = false) {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			valorEntrada = Console.ReadLine();
			if (String.IsNullOrEmpty(valorEntrada)) {
				ImprimirError("El valor no puede estar vacio, intente nuevamente.\n");
				valorEntrada = ObtenerStringEntrada(valorEntrada, mensaje, validarPass);
			} else if (validarPass) {
				string patron = @"^((?=.{8,}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*|(?=.{8,}$)(?=.*\d)(?=.*[a-zA-Z])(?=.*[!\u0022#$%&'()*+,./:;<=>?@[\]\^_`{|}~-]).*)";
				Regex rg = new Regex(patron);
				Match match = rg.Match(valorEntrada);
				if (!match.Success) {
					ImprimirError("La contrasena debe contener por lo menos 8 caracteres, un numero y un caracter especial.\n");
					valorEntrada = ObtenerStringEntrada(valorEntrada, mensaje, validarPass);
				}
			}
			return valorEntrada;
		}

		static void OpcionDeUsuario() {
			ConsoleKeyInfo keyInfo = Console.ReadKey();
			if (keyInfo.Key == ConsoleKey.UpArrow) {
				opcion--;
			} else if (keyInfo.Key == ConsoleKey.DownArrow) {
				opcion++;
			}
			
			if (opcion < 0) {
				opcion = 0;
			} else if (opcion > 4) {
				opcion = 4;
			}

			if (keyInfo.Key != ConsoleKey.Enter) {
				MostrarOpciones();
				OpcionDeUsuario();
			}
		}

		static bool ValidarEmailEntrada(string email) {
			string patron = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
				+ "@"
				+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
			Regex rg = new Regex(patron);
			Match match = rg.Match(email);
			if (match.Success)
				return true;
			else
				return false;
		}

		static void IngresarLlamados() {
			string nombre = ObtenerStringEntrada("", "Introduzca el nombre: ");
			string apellido = ObtenerStringEntrada("", "Introduzca la apellido: ");
			llamados.Add(String.Format("{0} {1}", nombre, apellido));
		}

		static void IngresarUsuarios() {
			string usuario = ObtenerStringEntrada("", "Introduzca el el usuario: ");
			string contrasena = ObtenerStringEntrada("", "Introduzca la contrasena: ", true);
			usuarios.Add(usuario, contrasena);
		}

		static void AtenderLlamado() {
			Console.Clear();
			if (llamados.Count == 0) {
				Console.WriteLine("");
				Console.BackgroundColor = ConsoleColor.White;
				ImprimirLinea(35, "No hay registros");
				Console.ResetColor();
				EnterParaContinuar();
			} else {
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.Blue;
				ImprimirLinea(70, "Llamado tomado: ");
				Console.BackgroundColor = ConsoleColor.White;;
				foreach (string llamado in llamados) {
					ImprimirLinea(70, llamado);
					break;
				}
				Console.ResetColor();
				EnterParaContinuar();
			}
		}

		static void Main(string[] args)
		{
			ImprimirEnunciado();
			bool auxiliar = true;
			while (auxiliar) {
				// Ingresar llamados", "Ingresar usuarios", "Ver llamados en espera", "Salir
				MostrarOpciones();
				OpcionDeUsuario();
				switch (opcion) {
					case 0:
						IngresarLlamados();
						break;
					case 1:
						IngresarUsuarios();
						break;
					case 2:
						MostrarDatos();
						break;
					case 3:
						AtenderLlamado();
						break;
					case 4:
						auxiliar = false;
						break;
				}
			}
			Console.WriteLine("");
		}
	}
}
