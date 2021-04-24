using System;

namespace clase_4
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Clear();
			char[] operaciones={'C','D','D','D','C','D','D','D','C','C','C','D','C','C','C','D','C','C','C','D','C','D','C','D','D','D','D','D','C','D','C','D','D','D','C','D','C','D','C','D','C','D','C','D','D'};
			int[] estadoCuentas = new int[45];
			int[] movimientoCuentas = new int[45];
			Random randomManager = new Random();
			for (int i = 0; i < movimientoCuentas.Length; i++) {
				estadoCuentas[i] = randomManager.Next(0, 10000);
				movimientoCuentas[i] = randomManager.Next(0, 20000);
			}

			int indice = 0;
			foreach (char operacion in operaciones) {
				if (operacion == 'C') {
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.WriteLine("Se ha concedido un credito de {0:c} a la cuenta {1} con balance {2:c}", movimientoCuentas[indice], indice + 1, estadoCuentas[indice]);
					Console.ResetColor();
					estadoCuentas[indice] += movimientoCuentas[indice];	
				} else if (operacion == 'D') {
					if (estadoCuentas[indice] > movimientoCuentas[indice]) {
						estadoCuentas[indice] -= movimientoCuentas[indice];
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.WriteLine("Se ha debitado un total de {0:c} a la cuenta {1} con balance {2:c}", movimientoCuentas[indice], indice + 1, estadoCuentas[indice]);
						Console.ResetColor();
					} else {
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("No se ha podido debitar el total de {0:c} la cuenta numero {1} con balance {2:c} por falta de saldo", movimientoCuentas[indice], indice + 1, estadoCuentas[indice]);
						Console.ResetColor();
					}
				}
				indice++;
			}
		}
	}
}
