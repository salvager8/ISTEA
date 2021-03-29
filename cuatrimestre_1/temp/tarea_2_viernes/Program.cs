using System;

namespace tarea_2_viernes
{
	class Program
	{
		static void Main(string[] args)
		{
			// Enunciado

			// Dada la siguiente cadena de caracteres devolver la cantidad de 0 y 1 por pantalla.
			string oracion = "1101 0011 1010 1100 1001 0001 1000 1000 1100 0011 1100 0011 1100";
			int conteoCeros = 0;
			int conteoUnos = 0;

			for (int i = 0; i < oracion.Length; i++) { 
				if (oracion[i] == '1') {
					conteoUnos++;
				} else if (oracion[i] == '0') {
					conteoCeros++;
				}
			}

			Console.WriteLine(string.Format("La cantidad de ceros (0) es: {0} y la cantidad de unos (1) es: {1}", conteoCeros, conteoUnos));
		}
	}
}
