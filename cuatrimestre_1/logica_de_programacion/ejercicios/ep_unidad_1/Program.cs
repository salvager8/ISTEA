using System;

namespace ep_unidad_1
{
	public class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Ejercicios practicos unidad 1:");
			Console.WriteLine("\n");

			Excercise._1();
			Console.WriteLine("\n");

			Excercise._2();
			Console.WriteLine("\n");

			Excercise._3();
			Console.WriteLine("\n");

			Excercise._6();
			Console.WriteLine("\n");
		}
	}

	public class Excercise
	{
		public static void _1() {
			// Enunciado
			Console.WriteLine("1. Dados dos valores enteros x devolver la suma de ambos por pantalla.");
			
			// Declaracion y asignacion de variables
			int valor_a = 2;
			int valor_b = 3;

			// Calculo de total
			int total = valor_a + valor_b;

			// Escribir el resultado
			Console.WriteLine("     Valor 1: "+valor_a);
			Console.WriteLine("     Valor 2: "+valor_b);
			Console.WriteLine("     Total: "+valor_a+" + "+valor_b+" = "+total);
		}

		public static void _2() {
			//Enunciado
			Console.WriteLine("2. Dados dos valores enteros ingresados por usuario devolver la suma de ambos y mostrarlo por pantalla.");
			
			// Declaracion y asignacion de variables
			Console.WriteLine("Ingrese el primer valor:");
			int valor_a = int.Parse(Console.ReadLine());
			Console.WriteLine("Ingrese el segundo valor:");
			int valor_b = int.Parse(Console.ReadLine());

			// Calcular el resultado
			int total = valor_a + valor_b;

			// Escribir el resultado
			Console.WriteLine("     Valor 1: "+valor_a);
			Console.WriteLine("     Valor 2: "+valor_b);
			Console.WriteLine("     Total: "+valor_a+" + "+valor_b+" = "+total);
		}

		public static void _3() {
			//Enunciado
			Console.WriteLine("3. Ingresar dos valores por pantalla y devolver las 4 operaciones matemáticas básicas junto con su respectiva leyenda.");
			
			// Declaracion y asignacion de variables
			Console.WriteLine("Ingrese el primer valor:");
			double valor_a = int.Parse(Console.ReadLine());
			Console.WriteLine("Ingrese el segundo valor:");
			double valor_b;
			valor_b = int.Parse(Console.ReadLine());

			// Validacion de datos proporcionados por el usuario
			while (valor_b == 0) {
				Console.WriteLine("\n");
				Console.WriteLine("El valor de b debe ser distinto de 0 para la division");
				Console.WriteLine("Ingrese el segundo valor nuevamente:");
				valor_b = int.Parse(Console.ReadLine());
			}

			// Mostrar datos del usuario en pantalla
			Console.WriteLine("     Valor 1: "+valor_a);
			Console.WriteLine("     Valor 2: "+valor_b);

			// Mostrar resultado de la suma
			Console.WriteLine("Suma:");
			Console.WriteLine("     Total: "+valor_a+" + "+valor_b+" = "+(valor_a+valor_b));
			Console.WriteLine("\n");

			// Mostrar resultado de la resta
			Console.WriteLine("Resta:");
			Console.WriteLine("     Total: "+valor_a+" - "+valor_b+" = "+(valor_a-valor_b));
			Console.WriteLine("\n");

			// Mostrar resultado de la multiplicacion
			Console.WriteLine("Multiplicacion:");
			Console.WriteLine("     Total: "+valor_a+" * "+valor_b+" = "+(valor_a*valor_b));
			Console.WriteLine("\n");

			// Mostrar resultado de la division
			Console.WriteLine("Division:");
			Console.WriteLine("     Total: "+valor_a+" / "+valor_b+" = "+(valor_a/valor_b));
		}

		public static void _6() {
			// Enunciado
			Console.WriteLine("6. Dadas las siguientes 2 oraciones devolver como resultado, la primera todo en minúscula la segunda todo en mayúscula y la cantidad de letras de cada una, por pantalla sin modificar el contenido.");
			
			// Declarar e inicializar las variables
			string oracion_1 = "LA CASA EN EL BOSQUE ESTA HECHA";
			string oracion_2 = "de madera de pino y nogal rojo";

			// Calcular y mostrar la respuesta
			Console.WriteLine("La longitud de la primera oracion es: "+oracion_1.Length);
			Console.WriteLine("La longitud de la segunda oracion es: "+oracion_2.Length);
		}
	}
}