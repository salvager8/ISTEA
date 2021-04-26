using System;

namespace tarea_5
{
    class Program
    {
		static void GetSummary() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tDado un array de fechas de pagos de cuotas de un gimnasio calcular los días entre el pago de la ultima si son mayores");
			Console.WriteLine("\tde 60 días el socio deberá abonar nuevamente la matricula por lo cual se deberá mostrar por pantalla la cantidad de");
			Console.WriteLine("\tsocios que deben abonar.");
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

			// Array de fechas de socios
            DateTime[] lastPayment = new DateTime[]
            {
				new DateTime(2021, 1, 14),
				new DateTime(2021, 4, 18),
				new DateTime(2021, 4, 18),
				new DateTime(2021, 3, 23),
				new DateTime(2021, 2, 12),
				new DateTime(2021, 1, 11),
				new DateTime(2021, 2, 8),
				new DateTime(2021, 3, 23),
				new DateTime(2021, 1, 23),
				new DateTime(2021, 3, 23),
				new DateTime(2021, 4, 18),
				new DateTime(2021, 12, 18),
				new DateTime(2020, 11, 18),
				new DateTime(2020, 10, 18),
				new DateTime(2021, 4, 18),
				new DateTime(2021, 4, 18)    
            };

			// Inicializar variables
			TimeSpan dueLastPayment;
			int days;

			// Ciclo principal
			for (int i = 0; i < lastPayment.Length; i++) {
				dueLastPayment = DateTime.Today.Subtract(lastPayment[i]);
				days = dueLastPayment.Days;

				if (days > 60) {
					Console.WriteLine("El socio {0} debera abonar nuevamente la matricula", i + 1);
				}
			}
        }
    }
}
