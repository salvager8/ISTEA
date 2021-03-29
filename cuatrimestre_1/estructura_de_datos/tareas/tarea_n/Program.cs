using System;

namespace tarea_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Enunciado

            // Pedir ingreso de tamano de array
            Console.Write("\nIngrese el tamano del array: ");
            int arraySize = Convert.ToInt16(Console.ReadLine());

            // Pedir tipo de impresion
            Console.Write("\nIngrese el tpo de impresion: (r - raw / f - formatted)");
            char printType = Console.ReadLine()[0];

            // Inicializar variables
            int[] arraySolution = new int[arraySize];
            int maxNumber = 0;
            string message = "\t";
            
            // Inicializar clase Random para generar numeros aleatorios
            Random randomManager = new Random();

            // Ciclo principal
            for (int i = 0; i < arraySize; i++) {
                int newArrayEntry = randomManager.Next(10, 1000);
                if (newArrayEntry > maxNumber) {
                    maxNumber = newArrayEntry;
                }
                arraySolution[i] = newArrayEntry;
                message += Convert.ToString(newArrayEntry) + ", ";
                if ((i + 1) % 10 == 0) {
                    message += "\n\t";
                }
            }


            Console.WriteLine(message);

            // Console.WriteLine("El Array queda como: ");
            // Console.WriteLine("{ " + string.Join(", ", arraySolution) + " }");
            // Console.WriteLine("El Maximo valor del array es: "+maxNumber);
        }
    }
}
