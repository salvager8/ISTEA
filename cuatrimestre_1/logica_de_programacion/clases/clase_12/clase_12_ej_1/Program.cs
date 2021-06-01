using System;

namespace clase_12_ej_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string ruta = "";

			string contenido = DateTime.Now + " Inicio Aplicacion";


			int primerValor = 0;
			int segundoValor = 0;
			int resultado = 0;

			try {
				Console.Write("Ingrese el primer valor: ");
				primerValor = Int32.Parse(Console.ReadLine());
				Console.Write("Ingrese el segundo valor: ");
				segundoValor = Int32.Parse(Console.ReadLine());


				resultado = primerValor / segundoValor;
			} catch (FormatException) {
				Console.WriteLine("Se ha ingresado un dato no valido");
			}
        }
    }
}
