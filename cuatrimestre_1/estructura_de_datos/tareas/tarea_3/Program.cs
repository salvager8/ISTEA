using System;

namespace tarea_1
{
	class Program
	{
		// Imprime el enunciado por pantalla
		static void GetSummary()
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("\nEnunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tDesarrollar una aplicación consola que implemente un array de enteros de al menos");
			Console.WriteLine("\t200 posiciones con valores aleatorios y que devuelva por pantalla impresos todos");
			Console.WriteLine("\tlos valores , el máximo y el mínimo.");
			Console.ResetColor();
		}

		// Toma el valor de entrada para el array e intenta parsearlo a un entero
		static int GetNumberInput(int inputValue, bool shouldCheckSize, string message)
		{
			Console.Write(message);

			// Condicionales
			bool isInt = Int32.TryParse(Console.ReadLine(), out inputValue);
			bool isArraySizeValid = true;

			// Preguntar si se quiere validar el tamano del numero
			if (shouldCheckSize) {
				isArraySizeValid = inputValue >= 200;
			}

			// Condiciones que deben cumplirse para que el input este correcto
			if (!isInt) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("El texto ingresado no es un numero entero, intente nuevamente");
				Console.ResetColor();
				inputValue = GetNumberInput(inputValue, shouldCheckSize, message);
			} else if (!isArraySizeValid) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("El numero que ingreso es menor a 200, intente nuevamente");
				Console.ResetColor();
				inputValue = GetNumberInput(inputValue, shouldCheckSize, message);
			}
			return inputValue;
		}

		static void Main(string[] args)
		{
			// Enunciado
			GetSummary();

			// Pedir ingreso de tamano de array
			int arraySize = GetNumberInput(0, true, "\n\nIngrese el tamano del array: ");

			// Pedir numero maximo y minimo para el random
			int randomBottomNumber = GetNumberInput(0, false, "\n\nIngrese el valor para el valor random minimo: ");
			int randomTopNumber = GetNumberInput(0, false, "\n\nIngrese el valor para el valor random maximo: ");

			while (randomTopNumber < randomBottomNumber) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("El valor inferior no puede ser mayor que el superior,");
				Console.ResetColor();
				randomTopNumber = GetNumberInput(0, false, "Ingrese un valor superior o igual a "+randomBottomNumber+": ");
			}

			// Pedir tipo de impresion
			Console.Write("\nIngrese el tipo de impresion (r - raw / <any> - formatted): ");
			char printType;
			bool isValidPrintType = char.TryParse(Console.ReadLine(), out printType);
			if (!isValidPrintType) {
				printType = 'f';
			}

			// Inicializar variables
			int[] arraySolution = new int[arraySize];
			int? maxNumber = null;
			int? minNumber = null;
			string message = "\t";

			// Inicializar clase Random para generar numeros aleatorios
			Random randomManager = new Random();

			// Ciclo principal
			for (int i = 0; i < arraySize; i++) {
				int newArrayEntry = randomManager.Next(randomBottomNumber, randomTopNumber);
				if (maxNumber == null) {
					maxNumber = newArrayEntry;
				}
				if (minNumber == null) {
					minNumber = newArrayEntry;
				}
				if (newArrayEntry > maxNumber) {
					maxNumber = newArrayEntry;
				}
				if (newArrayEntry < minNumber) {
					minNumber = newArrayEntry;
				}
				arraySolution[i] = newArrayEntry;
				message += Convert.ToString(newArrayEntry) + ", ";
				if ((i + 1) % 10 == 0) {
					message += "\n\t";
				}
			}

			// Imprimir a consola los resultados
			Console.WriteLine("\nEl Array queda como: \n");
			Console.WriteLine("{");
			if (printType == 'r') {
				Console.WriteLine(string.Join(", ", arraySolution));
			} else {
				Console.Write(message);
			}
			Console.WriteLine("\n}");
			Console.WriteLine("\nEl Maximo valor del array es: "+maxNumber);
			Console.WriteLine("El Minimo valor del array es: "+minNumber);
		}
	}
}
