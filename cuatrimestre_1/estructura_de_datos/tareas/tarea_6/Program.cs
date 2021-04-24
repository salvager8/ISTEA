using System;

namespace tarea_6
{
	class Program
	{
		// Imprime el enunciado por pantalla y pide el ingreso de un enter
		static void GetSummary() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tCrear un array bidimencional de 2 filas y 30 columnas con valores aleatorios entre 0 y 1, también crear 2 variables candidato");
			Console.WriteLine("\t1 y candidato 2, suponiendo que son votos los  datos del array, contar los valores y declarar 1 ganador imprimiendo");
			Console.WriteLine("\tpor pantalla el nombre de la variable con mas votos y la cantidad de votos.");
			Console.WriteLine("\n\tmetodo a utilizar para imprimir nombre de la variable\n\tNameOf(....)");
			Console.ResetColor();
			Console.WriteLine("\n[ENTER]");
			ConsoleKeyInfo keyInfo = Console.ReadKey();
			while(keyInfo.Key != ConsoleKey.Enter)
			keyInfo = Console.ReadKey();
			Console.Clear();
		}

		static void Main(string[] args)
		{
			// Enunciado
			GetSummary();

			int[,] votes = new int[2, 30];
			int primerCandidato = 0;
			int segundoCandidato = 0;
			Random randomManager = new Random();

			for (int i = 0; i < votes.GetLength(0); i++) {
				for (int j = 0; j < votes.GetLength(1); j++) {
					int nextVote = randomManager.Next(0, 2);
					Console.Write(nextVote);
					if (nextVote == 1) {
						primerCandidato++;
					} else {
						segundoCandidato++;
					}
					votes[i, j] = nextVote;
				}
			}

			// Condicional para imprimir el mensaje por pantalla
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine(primerCandidato+" "+segundoCandidato);
			if (primerCandidato > segundoCandidato) {
				Console.WriteLine("\nEl ganador es el {0} con {1} votos\n", nameof(primerCandidato), primerCandidato);
			} else if (segundoCandidato > primerCandidato) {
				Console.WriteLine("\nEl ganador es el {0} con {1} votos\n", nameof(primerCandidato), segundoCandidato);
			} else {
				Console.WriteLine("\nHa habido un empate\n");
			}
			Console.ResetColor();
		}
	}
}
