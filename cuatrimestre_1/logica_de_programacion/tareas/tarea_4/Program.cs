using System;
using System.Threading;

namespace tarea_4
{
    class Program
    {
		// Imprime el enunciado por pantalla y pide el ingreso de un enter
		static void GetSummary() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tDados 45 empleados de una empresa, ingresar legajo, sueldo y sexo (1=femenino y 2=masculino) mostrar cuantos");
			Console.WriteLine("\templeados ganan mas de $70.000, mostrar cantidad de mujeres y hombres y el sueldo promedio por cada genero.");
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
		static float GetNumberInput(float inputValue, string message, bool shouldValidateSex)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(message);
			Console.ResetColor();

			bool isFloat = float.TryParse(Console.ReadLine(), out inputValue);
			if (!isFloat) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n\tEl texto ingresado no es un numero, intente nuevamente");
				HandleSleep();
				inputValue = GetNumberInput(inputValue, message, shouldValidateSex);
			} else if (inputValue < 0) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n\tEl numero ingresado debe ser positivo, intente nuevamente");
				HandleSleep();
				inputValue = GetNumberInput(inputValue, message, shouldValidateSex);
			} else if (shouldValidateSex && inputValue != 1 && inputValue != 2) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n\tEl sexo debe ser 1 (femenino) o 2 (masculino)");
				HandleSleep();
				inputValue = GetNumberInput(inputValue, message, shouldValidateSex);
			}
			return inputValue;
		}

        static void Main(string[] args)
        {
			// Imprime el enunciado
			GetSummary();

			// Pedir los datos por pantalla
			int employees = 45;
			int countWealthyEmployees = 0;
			int countMale = 0;
			float maleSalary = 0;
			int countFemale = 0;
			float femaleSalary = 0;

			for (int i = 0; i < employees; i++) {
				Console.Clear();
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.WriteLine("Por favor ingrese el legajo, sueldo y sexo del empleado {0}", i + 1);
				Console.Write("Legajo: ");
				Console.ResetColor();
				string legacy = Console.ReadLine();
				float salary = GetNumberInput(0, "\nSueldo: ", false);
				int sex = Convert.ToInt16(GetNumberInput(0, "\nSexo  (1 femenino o 2 masculino): ", true));
				if (salary > 70000) {
					countWealthyEmployees++;
				}
				if (sex == 1) {
					countFemale++;
					femaleSalary += salary;
				} else {
					countMale++;
					maleSalary += salary;
				}
			}

			Console.Clear();
			Console.WriteLine("Cantidad de empleados que ganan mas de 70.000: {0}", countWealthyEmployees);
			Console.WriteLine("Cantidad de mujeres: {0}", countFemale);
			Console.WriteLine("Sueldo promedio de las mujeres: {0}", femaleSalary / countFemale);
			Console.WriteLine("Cantidad de hombres: {0}", countMale);
			Console.WriteLine("Sueldo promedio de los hombres: {0}\n", maleSalary / countMale);
        }
    }
}
