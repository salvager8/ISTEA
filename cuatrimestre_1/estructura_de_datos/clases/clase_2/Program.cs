using System;

namespace clase_2 {
	class Program {
		static void FirstExample() {
			// Ciclo for simple
			for (int i = 10; i > 0; i--) {
				Console.WriteLine("Valor de i es: "+i);
			}
		}

		static void SecondExample() {
			// Random
			int auxiliar;
			Random nrd = new Random();
			for (int i = 0; i < 10; i++) {
				auxiliar = nrd.Next(900, 1000);
				Console.WriteLine("Valor de i es: "+i);
				Console.WriteLine("Valor de auxiliar es: "+auxiliar);
			}
		}

		static void ThirdExample() {
			// Contar caracteres en un string
			string oracion = "Salvador Gonzalez";
			int cantidadCaracteres = oracion.Length;
			int cantidadReal = 0;

			for (int i = 0; i < oracion.Length; i++) {
				if (oracion[i] == ' ') {
					cantidadReal++;
				}
			}

			Console.WriteLine("Cantidad completa: "+cantidadCaracteres);
			Console.WriteLine("Cantidad real: "+cantidadReal);
		}

		static void FourthExample() {
			// Ciclo basico do...while
			int auxiliar = 0;
			do {
				Console.WriteLine("El valor auxiliar es: "+auxiliar);
				auxiliar++;
			} while(auxiliar <= 5);
		}

		static void FifthExample() {
			// Print every char on string with do...while
			int auxiliar = 0;
			string oracion = "Salvador Gonzalez";
			int cantidadLetras = oracion.Length;

			do {
				Console.WriteLine(oracion[auxiliar]);
				auxiliar++;
			} while (auxiliar < cantidadLetras);
		}

		static void SixthExample() {
			// Simple login using do...while
			string user;
			int password = 0;
			string auxiliar = "";

			while (true) {
				Console.Write("Ingrese su Usuario: ");
				user = Console.ReadLine();

				Console.Write("Ingrese su Password: ");
				while (true) {
					var key = Console.ReadKey(true);
					if (key.Key == ConsoleKey.Enter)
						break;
					auxiliar += key.KeyChar;
				}
				password = Convert.ToInt32(auxiliar);

				if (user == "admin") {
					if (password == 123456) {
						Console.WriteLine("\nWelcome.");
						break;
					}
					Console.WriteLine("\nWrong Password");
					break;

				}
				Console.WriteLine("\nWrong User");
				break;
			}
		}

		static void SeventhExample() {
			// Array introduction
			int[] valores = new int[50];

			for (int x = 0; x < 50; x++) {
				valores[x] = x;
				Console.WriteLine("Valor del array: "+valores[x]);
				Console.WriteLine("Valor de la asignacion: "+x);
			}
		}

		static void EightExample() {
			// Clone array
			string[] paises = {"Argentina", "Brasil", "Peru"};
			string[] paisesClone = paises.Clone() as string[];
			

			for (int x = 0; x < 3; x++) {
				Console.WriteLine("Pais: "+paises[x]);
				Console.WriteLine("Pais clonado: "+paisesClone[x]);
			}

			Console.WriteLine(string.Join(" | ", paisesClone));
		}

		static void NinethExample() {
			int[] arrayEnteros = {1, 23, 45, 66, 23, 11};
			int[] arrayEnteros2 = {99, 99, 99, 99, 99, 99};

			Array.Copy(arrayEnteros, 0, arrayEnteros2, 0, 4);
			Console.WriteLine(string.Join(" - ", arrayEnteros2));
			Console.WriteLine("Cantidad del array: "+arrayEnteros.Length);
		}

		static void Main(string[] args) {
			// FirstExample();

			// SecondExample();

			// ThirdExample();

			// FourthExample();

			// FifthExample();

			// SixthExample();
			
			// SeventhExample();

			// EightExample();
			
			NinethExample();
		}
	}
}
