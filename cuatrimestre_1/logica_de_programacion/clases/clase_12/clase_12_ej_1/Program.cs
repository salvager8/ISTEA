using System;
using System.IO;

namespace clase_12_ej_1
{
    class Program
    {
		static void ImprimirEnArchivo (string mensaje, string ruta) {
			File.AppendAllLines(ruta, new String[] {mensaje});
		}

        static void Main(string[] args)
        {
            string ruta = "/mnt/c/Users/Salva ISTEA/Desktop/ISTEA/cuatrimestre_1/logica_de_programacion/clases/clase_12/logs/clase_12.txt";

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
				contenido = DateTime.Now + " Inicio Aplicacion";
			} catch (DivideByZeroException) {
				Console.WriteLine("Se ha intentado dividir por 0");
				contenido = DateTime.Now + " Inicio Aplicacion";
			} catch (Exception) {
				Console.WriteLine("Se ha producido un error");
				contenido = DateTime.Now + " Inicio Aplicacion";
			} finally {
				contenido = DateTime.Now + " Final Aplicacion";
				ImprimirEnArchivo(contenido, ruta);
			}
        }
    }
}
