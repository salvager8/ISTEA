using System;
using System.Threading;

namespace tarea_2
{
    class Program
    {
		static void GetSummary() {
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("\nEnunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tIngresar por pantalla uno de los puestos de trabajo abajo mencionados, luego ingresar los días trabajados (1 a 30) , por");
			Console.WriteLine("\tultimo mostrar por pantalla el sueldo base, los días trabajados y el monto a liquidar según los días trabajados.");
			Console.WriteLine("\t\tIngeniero=$120.000\n\t\tAnalista=$110.000\n\t\tDesarrollador=$100.000");
			Console.WriteLine("\t- El ingreso del puesto debe ser indistinto ya sea mayúscula/minúscula o cualquier  combinación.");
			Console.WriteLine("\t- Si se ingresa otro puesto debe mostrar un mensaje de error.");
			Console.WriteLine("\t- Validar el ingreso correcto de la cantidad de días.");
			Console.WriteLine("\t- Los días trabajados tienen que ser entre 1 y 30.");
			Console.WriteLine("\n\t Como tarea investigativa y opcional, se requiere mostrar los resultados como formato moneda.");
			Console.ResetColor();
			Console.WriteLine("[ENTER]");
			ConsoleKeyInfo keyInfo = Console.ReadKey();
			while(keyInfo.Key != ConsoleKey.Enter)
    			keyInfo = Console.ReadKey();
			Console.Clear();
		}

		static int CalculatePayment(int baseSalary, int daysWorked) {
			return (baseSalary * daysWorked / 30);
		}

		static void HandleSleep() {
			Thread.Sleep(2000);
			Console.Clear();
			Console.ResetColor();
		}

        static void Main(string[] args) {
			// Enunciado
			GetSummary();

			int salaryEngineer = 120000;
			int salaryAnalist = 110000;
			int salaryDeveloper = 100000;
			int baseSalary = 0;

			Console.Write("Por favor ingrese un puesto de trabajo (Ingeniero, Analista o Desarrollador): ");
			string position = Console.ReadLine().ToLower();
			bool auxiliar = true;

			while (auxiliar) {
				switch (position) {
					case "ingeniero":
						baseSalary = salaryEngineer;
						auxiliar = false;
						break;
					case "analista":
						baseSalary = salaryAnalist;
						auxiliar = false;
						break;
					case "desarrollador":
						baseSalary = salaryDeveloper;
						auxiliar = false;
						break;
					default:
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("\n\tEl Puesto de trabajo no es valido, intente nuevamente.");
						HandleSleep();
						Console.Write("Por favor ingrese un puesto de trabajo valido (Ingeniero, Analista o Desarrollador): ");
						position = Console.ReadLine().ToLower();
						break;
				}
			}

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("\n\tHa selecionado la posicion: "+position);
			HandleSleep();

			Console.Write("Por favor ingrese los dias trabajados: ");
			int daysWorked;
			bool isDaysWorkedInt = int.TryParse(Console.ReadLine(), out daysWorked);
			
			while (true) {
				if (isDaysWorkedInt) {
					if (daysWorked > 30 || daysWorked < 1) {
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("\n\tPor favor introduzca un numero mayor o igual a 1 y menor o igual a 30");
						HandleSleep();
						Console.Write("Por favor ingrese los dias trabajados: ");
						isDaysWorkedInt = int.TryParse(Console.ReadLine(), out daysWorked);
					} else { break; }
				} else {
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\n\tEl dato que introdujo no es un numero entero, por favor introduzca un numero entero entre 1 y 30");
					HandleSleep();
					Console.Write("Por favor ingrese los dias trabajados: ");
					isDaysWorkedInt = int.TryParse(Console.ReadLine(), out daysWorked);
				}
			}

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(String.Format("\n\tHa trabajado: {0} dias.", daysWorked));
			HandleSleep();

			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine(String.Format("\nEl salario base del {0} es de: {1}", position, baseSalary.ToString("$#,0")));
			Console.WriteLine(String.Format("Los dias trabajados fueron {0} dias", daysWorked));
			Console.WriteLine("El monto a liquidar es de: "+CalculatePayment(baseSalary, daysWorked).ToString("$#,0"));
			Console.ResetColor();
		}
    }
}
