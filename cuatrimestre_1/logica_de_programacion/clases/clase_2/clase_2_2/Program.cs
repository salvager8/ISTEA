using System;

namespace clase_2_2
{
	class Program
	{
		static void Main(string[] args)
		{
			Auto ferrari = new Auto();
			ferrari.Modelo = "FXX";
			ferrari.Id = 1;
			ferrari.Precio = 10000;
			ferrari.Color = "Rojo";

			Console.WriteLine("Propiedades del auto: "+ferrari.Modelo+", "+ferrari.Id+", "+ferrari.Precio+", "+ferrari.Color);
			ferrari.Imprimir();
			 
		}
	}

	public class Vehiculo
	{
		public string Modelo{get; set;}
		public int Id{get; set;}
		public double Precio{get; set;}
		public string Color{get; set;}

		public void Imprimir() {
			Console.WriteLine("Metodo de la clase Vehiculo");
		}
	}

	public class Auto:Vehiculo
	{
		public int cant_pasajeros{get; set;}
	}

	public class Camion:Vehiculo
	{
		public int cant_pasajeros{get; set;}
	}
}
