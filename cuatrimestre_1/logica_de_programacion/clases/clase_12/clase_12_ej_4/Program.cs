using System;

namespace clase_12_ej_4
{

// 	2-Se deberá generar una aplicación consola que instacie 4 arrays unidimencionales en donde se cargue de manera
// aleatoria las notas del 1er. cuatrimestre, 2do. cuatrimestre, nota final y estado de la cursada


// 1-4 desaprobada.
// 4-8 aprobado
// >8 promocionado




// La aplicación debe ser dinámica y como mínimo 30 notas, que se deben mostrar por pantalla cada 10 alumnos.
// el array se debe cargar en una función y mostrar en otra.
    class Program
    {
		static void MostrarDatos(int[] notasPrimerCuatrimestre, int[] notasSegundoCuatrimestre, int[] notasFinal, string[] estadoCursada, int paginadoActual) {
			for (int i = paginadoActual; i < paginadoActual + 10; i++) {
				if (i == notasPrimerCuatrimestre.Length) {
					break;
				}
				Console.Write(
					"|\t{0}\t|\t{1}\t|\t{2}\t|\t{3}\t|\n",
					notasPrimerCuatrimestre[i],
					notasSegundoCuatrimestre[i],
					notasFinal[i],
					estadoCursada[i]
				);
			}

			ConsoleKeyInfo keyInfo = Console.ReadKey();
			if (keyInfo.Key == ConsoleKey.LeftArrow) {
				paginadoActual -= 10;
				if (paginadoActual < 0) {
					paginadoActual = 0;
				}
			} else if (keyInfo.Key == ConsoleKey.RightArrow) {
				if (paginadoActual + 10 > notasPrimerCuatrimestre.Length) {
					Console.WriteLine("No hay mas nada que mostrar");
				} else {
					paginadoActual += 10;
				}
			}

			Console.Clear();
			MostrarDatos(notasPrimerCuatrimestre, notasSegundoCuatrimestre, notasFinal, estadoCursada, paginadoActual);
		}

        static void Main(string[] args)
        {
            int[] notasPrimerCuatrimestre = new int[30];
			int[] notasSegundoCuatrimestre = new int[30];
			int[] notasFinal = new int[30];
			string[] estadoCursada = new string[30];
			Random randomManager = new Random();

			for (int i = 0; i < 30; i++) {
				notasPrimerCuatrimestre[i] = randomManager.Next(1, 11);
				notasSegundoCuatrimestre[i] = randomManager.Next(1, 11);
				notasFinal[i] = (notasPrimerCuatrimestre[i] + notasSegundoCuatrimestre[i]) / 2;

				if (notasFinal[i] <= 4) {
					estadoCursada[i] = "desaprobado";
				} else if (notasFinal[i] <= 8) {
					estadoCursada[i] = "aprobado";
				} else {
					estadoCursada[i] = "promocionado";
				}
			}

			MostrarDatos(notasPrimerCuatrimestre, notasSegundoCuatrimestre, notasFinal, estadoCursada, 0);
        }
    }
}
