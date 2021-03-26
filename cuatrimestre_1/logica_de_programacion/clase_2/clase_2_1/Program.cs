using System;

namespace clase_2_1
{
	class Program
	{
		static void Main(string[] args)
		{
			DateTime fecha;

			fecha = DateTime.Now;

			Console.WriteLine("Fecha y Hora: "+fecha);
			Console.WriteLine("Fecha Corta: "+fecha.ToShortDateString());
			Console.WriteLine("Fecha Dias: "+fecha.ToString("dd"));
			Console.WriteLine("Fecha Mes: "+fecha.ToString("MM"));
			Console.WriteLine("Fecha Mes: "+fecha.ToString("MMMM"));
			Console.WriteLine("Fecha Mes-Año: "+fecha.ToString("y"));
			Console.WriteLine("Fecha Año Corto: "+fecha.ToString("yy"));
			Console.WriteLine("Fecha Año: "+fecha.ToString("yyyy"));
			Console.WriteLine("Fecha Larga Completa: "+fecha.ToString("D"));
			Console.WriteLine("Fecha Larga Completa: "+fecha.ToString("F"));
			Console.WriteLine("Fecha Larga Completa: "+fecha.ToString("G"));

			TimeSpan delta_tiempo;
			int cantidad_dias;
			DateTime fecha_pago = new DateTime(2019, 2, 19);
			delta_tiempo = (DateTime.Today - fecha_pago);

			Console.WriteLine(delta_tiempo);
		}
	}
}
