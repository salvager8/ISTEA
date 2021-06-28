using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace ejercicio_2
{
	class Program
	{
		// Opciones de la tabla
		private static int opcion = 0;

		private static Dictionary<string, string> cuentas = new Dictionary<string, string>();
		private static Dictionary<string, string> textoImpresionPorCuenta = new Dictionary<string, string>();
		private static Queue<Dictionary<string, string>> impresiones = new Queue<Dictionary<string, string>>();

		// Imprime el enunciado por pantalla y pide el ingreso de un enter
		private static void ImprimirEnunciado() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tRealizar una aplicación consola que implemente una ABM de mails y");
			Console.WriteLine("\tcontraseñas a partir de un diccionario, para ingresar cualquiera de los dos");
			Console.WriteLine("\tcorrecto o pass con mínima seguridad.\n");
			Console.WriteLine("\tDeberá contar con Ingreso, Modificar (Pass) , mostrar, Eliminar.");
			Console.WriteLine("\tPor otro lado se deberá crear una cola de impresión en donde solo puedan");
			Console.WriteLine("\timprimir los usuarios existentes en el diccionario, la función enviarImprimir");
			Console.WriteLine("\tcolocara al usuario (mail) en la cola y la función imprimir quitara al usuario de");
			Console.WriteLine("\tla cola.");
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
			string[] opciones = new string[] { "Ingreso", "Modificar", "Mostrar", "Eliminar", "Encolar Impresion", "Imprimir", "Salir" };
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
		private static string ObtenerStringEntrada(string valorEntrada, string mensaje, bool validarEmailExistente = false, bool validarPass = false, bool esImpresion = false) {
			Console.Clear();
			if (esImpresion) {
				ImprimirColaImpresion();
				Console.WriteLine();
			}
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			valorEntrada = Console.ReadLine();
			if (String.IsNullOrEmpty(valorEntrada)) {
				ImprimirError("El valor no puede estar vacio, intente nuevamente.\n");
				valorEntrada = ObtenerStringEntrada(valorEntrada, mensaje, validarEmailExistente, validarPass, esImpresion);
			} else if (validarEmailExistente) {
				if (!ValidarEmailEntrada(valorEntrada)) {
					ImprimirError("El email no tiene un formato valido, por favor intente nuevamente.\n");
					valorEntrada = ObtenerStringEntrada(valorEntrada, mensaje, validarEmailExistente, validarPass, esImpresion);
				}
				foreach (var kvp in cuentas) {
					if (kvp.Key == valorEntrada) {
						ImprimirError("El email ya esta en uso, utilice uno distinto.");
						valorEntrada = ObtenerStringEntrada(valorEntrada, mensaje, validarEmailExistente, validarPass, esImpresion);
					}
				}
			} else if (validarPass) {
				string patron = @"^((?=.{8,}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*|(?=.{8,}$)(?=.*\d)(?=.*[a-zA-Z])(?=.*[!\u0022#$%&'()*+,./:;<=>?@[\]\^_`{|}~-]).*)";
				Regex rg = new Regex(patron);
				Match match = rg.Match(valorEntrada);
				if (!match.Success) {
					ImprimirError("La contrasena debe contener por lo menos 8 caracteres, un numero y un caracter especial.\n");
					valorEntrada = ObtenerStringEntrada(valorEntrada, mensaje, validarEmailExistente, validarPass, esImpresion);
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
			} else if (opcion > 6) {
				opcion = 6;
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

		static void EnviarImprimir(string email, string dato) {
			Dictionary<string, string> temporal = new Dictionary<string, string>();
			temporal.Add(email, dato);
			impresiones.Enqueue(temporal);
		}

		static void ImprimirColaImpresion() {
			Console.ForegroundColor = ConsoleColor.Black;
			Console.WriteLine("");
			Console.BackgroundColor = ConsoleColor.Blue;
			ImprimirLinea(70, "Cola de Impresion");
			Console.BackgroundColor = ConsoleColor.White;
			int contador = 0;
			foreach (Dictionary<string, string> textoImpresionPorCuenta in impresiones) {
				contador++;
				foreach (var kvp in textoImpresionPorCuenta) {
					ImprimirLinea(35, kvp.Key, kvp.Value);
				}
			}
			if (contador == 0) {
				ImprimirLinea(70, "No hay registros");
			}
			Console.ResetColor();
		}

		// Imprime todos los valores encolados en el orden que fueron agregados
		static void ImprimirCola() {
			Console.Clear();
			ImprimirColaImpresion();
			int contador = 0;
			if (impresiones.Count > 0) {
				EnterParaContinuar();
				string ruta = "./impresiones.log";
				foreach (Dictionary<string, string> textoImpresionPorCuenta in impresiones) {
					contador++;
					foreach (var kvp in textoImpresionPorCuenta) {
						string mensaje = String.Format("{0} - {1}", kvp.Key, kvp.Value);
						if (String.IsNullOrEmpty(mensaje)) {
							return;
						}
						try {
							File.AppendAllLines(ruta, new String[] { mensaje });
							mensaje = "";
						} catch {
							ImprimirError("Hay errores al intentar escribir en el archivo");
							Environment.Exit(0);
						}
					}
				}
			} else {
				ImprimirError("No hay registros para imprimir.");
			}
		}

		static void Main(string[] args)
		{
			ImprimirEnunciado();
			bool auxiliar = true;
			while (auxiliar) {
				MostrarOpciones();
				OpcionDeUsuario();

				string email = "";
				string contrasena = "";
				bool existe = false;
				switch (opcion) {
					case 0:
						// Anadir un nuevo mail
						email = ObtenerStringEntrada("", "Introduzca el email: ", true);
						contrasena = CreateMD5(ObtenerStringEntrada("", "Introduzca la contrasena: ", false, true));
						cuentas.Add(email, contrasena);
						break;
					case 1:
						// Cambiar la contrasena
						if (cuentas.Count == 0) {
							Console.Clear();
							Console.WriteLine("");
							Console.BackgroundColor = ConsoleColor.White;
							ImprimirLinea(35, "No hay registros");
							Console.ResetColor();
							EnterParaContinuar();
						} else {
							email = ObtenerStringEntrada("", "Introduzca el email: ");
							foreach (var kvp in cuentas) {
								if (kvp.Key == email) {
									existe = true;
									contrasena = CreateMD5(ObtenerStringEntrada("", "Introduzca la contrasena: "));
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
						break;
					case 2:
						// Mostrar la informacion
						MostrarDatos();
						break;
					case 3:
						// Eliminar un mail pidiendo clave y contrasena
						if (cuentas.Count == 0) {
							Console.Clear();
							Console.WriteLine("");
							Console.BackgroundColor = ConsoleColor.White;
							ImprimirLinea(35, "No hay registros");
							Console.ResetColor();
							EnterParaContinuar();
						} else {
							email = ObtenerStringEntrada("", "Introduzca el email: ");
							foreach (var kvp in cuentas) {
								if (kvp.Key == email) {
									existe = true;
									contrasena = CreateMD5(ObtenerStringEntrada("", "Introduzca la contrasena: "));
									if (kvp.Value == contrasena) {
										cuentas.Remove(kvp.Key);
										ImprimirExito("Email eliminado con exito");
									} else {
										ImprimirError("Datos incorrectos.");
									}
								}
							}
							if (!existe) {
								ImprimirError("El email no existe.");
							}
						}
						break;
					case 4:
						// Encolar imprimir registros
						if (cuentas.Count == 0) {
							Console.Clear();
							Console.WriteLine("");
							Console.BackgroundColor = ConsoleColor.White;
							ImprimirLinea(35, "No hay registros para imprimir");
							Console.ResetColor();
							EnterParaContinuar();
						} else {
							email = ObtenerStringEntrada("", "Introduzca el email: ", false, false, true);
							foreach (var kvp in cuentas) {
								if (kvp.Key == email) {
									existe = true;
									contrasena = CreateMD5(ObtenerStringEntrada("", "Introduzca la contrasena: ", false, false, true));
									if (kvp.Value == contrasena) {
										Dictionary<string, string> temporario = new Dictionary<string, string>();
										string dato = ObtenerStringEntrada("", "Introduzca la cadena a encolar para imprimir: ", false, false, true);
										temporario.Add(email, dato);
										impresiones.Enqueue(temporario);
										ImprimirExito(String.Format("Agregado a la cola de impresion el email {0}", email));
									} else {
										ImprimirError("Datos incorrectos.");
									}
								}
							}
							if (!existe) {
								ImprimirError("El email no existe.");
							}
						}
						break;
					case 5:
						ImprimirCola();
						break;
					case 6:
						auxiliar = false;
						break;
				}
			}
			Console.WriteLine("");
		}
	}
}
