using System;

namespace tarea_2
{
    class Program
    {
		static void GetSummary() {
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("\nEnunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\t");
			Console.ResetColor();
		}
		static int CalculatePayment(int baseSalary, int daysWorked) {
			return (baseSalary * daysWorked / 25);
		}
        static void Main(string[] args) {
			// Enunciado
			GetSummary();

			int salaryEngineer = 120000;
			int salaryAnalist = 110000;
			int salaryDeveloper = 100000;
			int baseSalary = 0;

			Console.Write("Por favor ingrese un puesto de trabajo: ");
			string position = Console.ReadLine().ToLower();
			bool auxiliar = true;

			do {
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
						Console.Write("Por favor ingrese un puesto de trabajo valido (Ingeniero, Analista o Desarrollador): ");
						position = Console.ReadLine().ToLower();
						break;
				}
			} while (auxiliar);

			Console.Write("Por favor ingrese los dias trabajados: ");
			int daysWorked;
			bool isDaysWorkedInt = int.TryParse(Console.ReadLine(), out daysWorked);
			
			while (!auxiliar) {
				if (isDaysWorkedInt) {
					if (daysWorked > 30 || daysWorked < 1) {
						Console.WriteLine("Por favor introduzca un numero menor a 30");
					}
				} else {
					Console.WriteLine("Por favor introduzca un numero entre 1 y 30");
				}
			}

			Console.WriteLine("El monto a liquidar es de "+CalculatePayment(baseSalary, daysWorked));

			
		}
    }
}
