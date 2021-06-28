using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace tarea_8
{
	class Program
	{
		// Opciones de la tabla
		private static int opcion = 0;

		private static Dictionary<string, string> cuentas = new Dictionary<string, string>();

		// Imprime el enunciado por pantalla y pide el ingreso de un enter
		private static void ImprimirEnunciado() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tGenerar una aplicación consola que implemente un diccionario con datos de usuarios y contraseñas, el mismo deberá");
			Console.WriteLine("\tcomo mínimo utilizar 3 funciones");
			Console.WriteLine("\t-Ingresar usuarios.\n");
			Console.WriteLine("\t-Modificar contraseña.(opcional que la contraseña se valide con una expresión regular)");
			Console.WriteLine("\t-Ver usuarios y contraseñas.");
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
			string[] opciones = new string[] { "Ingreso", "Modificar", "Mostrar", "Salir" };
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
			if (cuentas.Count == 0) {
				Console.Clear();
				Console.WriteLine("");
				Console.BackgroundColor = ConsoleColor.White;
				ImprimirLinea(35, "No hay registros");
				Console.ResetColor();
				EnterParaContinuar();
			} else {
				Console.BackgroundColor = ConsoleColor.Blue;
				ImprimirLinea(70, "Lista de usuarios");
				Console.BackgroundColor = ConsoleColor.Black;
				ImprimirLinea(70, "");
				Console.BackgroundColor = ConsoleColor.Blue;
				ImprimirLinea(35, "Email", "Contrasena (MD5)");
				Console.BackgroundColor = ConsoleColor.White;
				foreach (var kvp in cuentas ) {
					ImprimirLinea(35, kvp.Key, kvp.Value);
				}
				Console.ResetColor();
				EnterParaContinuar();
			}
		}

		public static string CreateMD5(string input) {
			// Use input string to calculate MD5 hash
			using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
			{
				byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
				byte[] hashBytes = md5.ComputeHash(inputBytes);

				// Convert the byte array to hexadecimal string
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < hashBytes.Length; i++)
				{
					sb.Append(hashBytes[i].ToString("X2"));
				}
				return sb.ToString();
			}
		}

		// Valida un string ingresado por pantalla
		private static string ObtenerStringEntrada(string valorEntrada, string mensaje, bool validarEmailExistente = false, bool validarPass = false) {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			valorEntrada = Console.ReadLine();
			if (String.IsNullOrEmpty(valorEntrada)) {
				ImprimirError("El valor no puede estar vacio, intente nuevamente.\n");
				valorEntrada = ObtenerStringEntrada(valorEntrada, mensaje, validarEmailExistente, validarPass);
			} else if (validarEmailExistente) {
				if (!ValidarEmailEntrada(valorEntrada)) {
					ImprimirError("El email no tiene un formato valido, por favor intente nuevamente.\n");
					valorEntrada = ObtenerStringEntrada(valorEntrada, mensaje, validarEmailExistente, validarPass);
				}
				foreach (var kvp in cuentas) {
					if (kvp.Key == valorEntrada) {
						ImprimirError("El email ya esta en uso, utilice uno distinto.");
						valorEntrada = ObtenerStringEntrada(valorEntrada, mensaje, validarEmailExistente, validarPass);
					}
				}
			} else if (validarPass) {
				string patron = @"^((?=.{8,}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*|(?=.{8,}$)(?=.*\d)(?=.*[a-zA-Z])(?=.*[!\u0022#$%&'()*+,./:;<=>?@[\]\^_`{|}~-]).*)";
				Regex rg = new Regex(patron);
				Match match = rg.Match(valorEntrada);
				if (!match.Success) {
					ImprimirError("La contrasena debe contener por lo menos 8 caracteres, un numero y un caracter especial.\n");
					valorEntrada = ObtenerStringEntrada(valorEntrada, mensaje, validarEmailExistente, validarPass);
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

		static void AnadirUsuario() {
			string email = ObtenerStringEntrada("", "Introduzca el email: ", true);
			string contrasena = CreateMD5(ObtenerStringEntrada("", "Introduzca la contrasena: ", false, true));
			cuentas.Add(email, contrasena);
		}

		static void ModificarContrasena() {
			// Cambiar la contrasena
			bool existe = false;
			if (cuentas.Count == 0) {
				Console.Clear();
				Console.WriteLine("");
				Console.BackgroundColor = ConsoleColor.White;
				ImprimirLinea(35, "No hay registros");
				Console.ResetColor();
				EnterParaContinuar();
			} else {
				string email = ObtenerStringEntrada("", "Introduzca el email: ");
				foreach (var kvp in cuentas) {
					if (kvp.Key == email) {
						existe = true;
						string contrasena = CreateMD5(ObtenerStringEntrada("", "Introduzca la contrasena: "));
						if (kvp.Value == contrasena) {
							cuentas[email] = CreateMD5(ObtenerStringEntrada("", "Introduzca la nueva contrasena: ", false, true));
						} else {
							ImprimirError("Datos incorrectos.");
						}
					}
				}
				if (!existe) {
					ImprimirError("El email no existe.");
				}
			}
		}

		static void Main(string[] args)
		{
			ImprimirEnunciado();
			bool auxiliar = true;
			while (auxiliar) {
				MostrarOpciones();
				OpcionDeUsuario();
				switch (opcion) {
					case 0:
						// Anadir un nuevo mail
						AnadirUsuario();
						break;
					case 1:
						ModificarContrasena();
						break;
					case 2:
						// Mostrar la informacion
						MostrarDatos();
						break;
					case 3:
						auxiliar = false;
						break;
				}
			}
			Console.WriteLine("");
		}
	}
}
