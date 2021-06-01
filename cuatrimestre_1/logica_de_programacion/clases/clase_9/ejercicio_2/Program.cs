using System;

namespace ejercicio_2
{
    class Program
    {

		static string ConcatenarResultado(string primeraCadena, string segundaCadena) {
			return primeraCadena + " " + segundaCadena;
		}
        static void Main(string[] args)
        {
            string primeraCadena = Console.ReadLine();
            string segundaCadena = Console.ReadLine();

			string resultado = ConcatenarResultado(primeraCadena, segundaCadena);\
			Console.WriteLine(resultado);
        }
    }
}
