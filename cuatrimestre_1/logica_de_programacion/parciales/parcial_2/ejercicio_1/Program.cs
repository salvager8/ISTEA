using System;
using System.Threading;
using System.Text.RegularExpressions;

namespace ejercicio_1
{
	class Program
	{
		// Imprime el enunciado por pantalla y pide el ingreso de un enter
		static void ImprimirEnunciado() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tRealizar una aplicación consola que permita el ingreso por pantalla de nombre, apellido, dni, fecha de nacimiento de un alumno,");
			Console.WriteLine("\tluego desarrolle una función que calcule la edad del alumno y si es mayor de 18 devuelva al main una cadena con la expresión");
			Console.WriteLine("\t“El alumno tiene aprobado el ingreso”, “El alumno no tiene aprobado el ingreso”");
			Console.ResetColor();
			Console.WriteLine("\nPresione [ENTER] para continuar");
			ConsoleKeyInfo keyInfo = Console.ReadKey();
			while (keyInfo.Key != ConsoleKey.Enter)
				keyInfo = Console.ReadKey();
			Console.Clear();
		}

		// Imprime un error por pantalla
		static void ImprimirError(string mensaje) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("\n\t");
			Console.WriteLine(mensaje);
			Thread.Sleep(2000);
			Console.Clear();
			Console.ResetColor();
		}

		// Obtiene un numero de entrada y valida que sea correcto
		static int ObtenerNumeroEntrada(int valorEntrada, string mensaje) {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			bool esEntero = int.TryParse(Console.ReadLine(), out valorEntrada);
			if (!esEntero) {
				ImprimirError("El dato ingresado no es un numero entero, intente nuevamente");
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje);
			} else if (valorEntrada < 0) {
				ImprimirError("El numero ingresado no puede ser negativo");
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje);
			} else if (valorEntrada > 99999999) {
				ImprimirError("El numero ingresado es demasiado grande para ser un DNI, corrija los datos e intente nuevamente");
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje);
			}

			return valorEntrada;
		}

		// Obtiene un string de entrada y valida que no este vacio
		static string ObtenerStringEntrada(string valorEntrada, string mensaje) {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			valorEntrada = Console.ReadLine();
			if (String.IsNullOrEmpty(valorEntrada)) {
				ImprimirError("El valor no puede estar vacio, intente nuevamente\n");
				valorEntrada = ObtenerStringEntrada(valorEntrada, mensaje);
			} else {
				string patron = @"[^a-zA-Z]";
				Regex rg = new Regex(patron);
				int tieneNumeros = rg.Matches(valorEntrada).Count;
				if (tieneNumeros > 0) {
					ImprimirError("El valor solo puede contener letras\n");
					valorEntrada = ObtenerStringEntrada(valorEntrada, mensaje);
				}
			}
			return valorEntrada;
		}

		// Obtiene una fecha por string e intenta parsearla a un datetime
		static DateTime ObtenerFechaEntrada(string valorEntrada, string mensaje) {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			valorEntrada = Console.ReadLine();
			DateTime response;
			bool esFechaValida = DateTime.TryParse(valorEntrada, out response);
			if (!esFechaValida) {
				ImprimirError("El dato ingresado no es una fecha valida, intente nuevamente (mm/dd/yyyy)");
				response = ObtenerFechaEntrada(valorEntrada, mensaje);
			} else {
				DateTime Now = DateTime.Now;
				int edad = 0;
				try {
					edad = new DateTime(DateTime.Now.Subtract(response).Ticks).Year - 1;
				} catch {
					ImprimirError("La edad ingresada no es valida ya que aun no ha pasado");
					response = ObtenerFechaEntrada(valorEntrada, mensaje);
				}
				if (edad < 0 || edad > 120) {
					ImprimirError("La edad ingresada no es valida, ingrese una edad inferior a 120 años");
					response = ObtenerFechaEntrada(valorEntrada, mensaje);
				}
			}
			return response;
		}

		// Valida el ingreso de un usuario y devuelve un string dependiendo del status de su ingreso
		static string ValidarIngreso(DateTime fechaNacimiento) {
			DateTime Now = DateTime.Now;
			int edad = new DateTime(DateTime.Now.Subtract(fechaNacimiento).Ticks).Year - 1;

			if (edad >= 18) {
				Console.ForegroundColor = ConsoleColor.Green;
				return "El alumno tiene aprobado el ingreso";
			} else {
				Console.ForegroundColor = ConsoleColor.Red;
				return "El alumno no tiene aprobado el ingreso, recuerde que debe ser mayor de 18 años";
			}
		}

		// Funcion principal
		static void Main(string[] args)
		{
			ImprimirEnunciado();

			string nombre = ObtenerStringEntrada("", "Introduzca el nombre del alumno: ");
			string apellido = ObtenerStringEntrada("", "Introduzca el apellido del alumno: ");
			int dni = ObtenerNumeroEntrada(0, "Introduzca el dni del alumno: ");
			DateTime fechaNacimiento = ObtenerFechaEntrada("", "Introduzca la fecha de nacimiento del alumno (mm/dd/yyyy): ");

			string respuestaIngreso = ValidarIngreso(fechaNacimiento);
			Console.Clear();
			Console.WriteLine("Alumno {0} {1} con DNI: {2}, nacido el {3}", apellido, nombre, dni, fechaNacimiento.ToShortDateString());
			Console.WriteLine("resultado:");
			Console.Write("\n\t");
			Console.WriteLine(respuestaIngreso);
			Console.Write("\n\n");
			Console.ResetColor();
		}
	}
}
