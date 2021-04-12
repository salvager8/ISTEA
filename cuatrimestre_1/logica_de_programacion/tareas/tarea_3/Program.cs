using System;
using System.Threading;
using System.Globalization;

namespace tarea_3
{
    class Program
    {
		// Imprime el enunciado por pantalla y pide el ingreso de un enter
		static void GetSummary() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tDesarrolle una aplicacion consola que permita el ingreso por pantalla de una orientacion,");
			Console.WriteLine("\tnombre y edad de los alumnos, como salida debe mostrar el curso al cual es asignado el alumno junto");
			Console.WriteLine("\tcon sus datos.");
			Console.ResetColor();
			Console.WriteLine("\n[ENTER]");
			ConsoleKeyInfo keyInfo = Console.ReadKey();
			while(keyInfo.Key != ConsoleKey.Enter)
    			keyInfo = Console.ReadKey();
			Console.Clear();
		}

		// Funcion helper para esperar por consola y reiniciar el color
		static void HandleSleep() {
			Thread.Sleep(2000);
			Console.Clear();
			Console.ResetColor();
		}

		// Toma el valor de entrada para el array e intenta parsearlo a un entero
		static int GetNumberInput(int inputValue, string message)
		{
			// Imprimir el mensaje en cuestion por pantalla
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(message);
			Console.ResetColor();

			// Condicionales
			bool isInt = Int32.TryParse(Console.ReadLine(), out inputValue);

			// Condiciones que deben cumplirse para que el input este correcto
			if (!isInt) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n\tEl texto ingresado no es un numero entero, intente nuevamente");
				HandleSleep();
				inputValue = GetNumberInput(inputValue, message);
			} else if (inputValue > 18 || inputValue < 14) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n\tLa edad debe estar entre 14 y 18, intente nuevamente");
				HandleSleep();
				inputValue = GetNumberInput(inputValue, message);
			}
			return inputValue;
		}

		// Construye el curso de un estudiante
		static string GetStudentCourseText(char orientationLetter, int age) {
			string message = "Curso ";
			message += Convert.ToString(age - 13);
			message += orientationLetter;
			return message;
		}

        static void Main(string[] args)
        {
			// Enunciado
			GetSummary();

			// Pedir la orientacion por consola
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write("Por favor ingrese una orientacion valida: (Contable o Sistemas): ");
			Console.ResetColor();
			string orientation = Console.ReadLine().ToLower();
			// Console.Clear();

			// Inicializar las variables
			string alumnName = "";
			int alumnAge = 0;
			string courseName = "";
			bool aux = true;

			// Convierte un texto a PascalCase
			TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

			// Ciclo principal
			while (aux) {
				switch (orientation) {
					case "contable":
						Console.Clear();
						aux = false;
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.Write("Ingrese el nombre del alumno: ");
						Console.ResetColor();
						alumnName = myTI.ToTitleCase(Console.ReadLine().ToLower());
						Console.Clear();
						alumnAge = GetNumberInput(0, "Ingrese la edad del alumno: ");
						courseName = GetStudentCourseText('A', alumnAge);
						break;
					case "sistemas":
						Console.Clear();
						aux = false;
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.Write("Ingrese el nombre del alumno: ");
						Console.ResetColor();
						alumnName = myTI.ToTitleCase(Console.ReadLine().ToLower());
						Console.Clear();
						alumnAge = GetNumberInput(0, "Ingrese la edad del alumno: ");
						courseName = GetStudentCourseText('B', alumnAge);
						break;
					default:
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("\n\tLa orientacion no es valida, intente nuevamente.");
						HandleSleep();
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.Write("Por favor ingrese una orientacion valida: (Contable o Sistemas): ");
						Console.ResetColor();
						orientation = Console.ReadLine().ToLower();
						break;
				}
			}

			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(
				string.Format(
					"\n\nEl alumno {0} de {1} años de edad fue asignado al {2} con Orientacion {3}\n\n",
					alumnName,
					alumnAge,
					courseName,
					myTI.ToTitleCase(orientation)
				)
			);
			Console.ResetColor();
        }
    }
}
