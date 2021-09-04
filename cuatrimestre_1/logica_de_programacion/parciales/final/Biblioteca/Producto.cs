using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
	public class Producto
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
		public double Precio { get; set; }

		public static List<Producto> productos = new List<Producto>(){};

		public static int UltimoId()
		{
			var ultimoId = 1;
			if (productos.Count > 0)
			{
				ultimoId = productos[productos.Count - 1].Id + 1;
			}
			return ultimoId;
		}

		public static void Introducir(string nombre, string descripcion, double precio)
		{
			productos.Add(new Producto() { Id = UltimoId(), Nombre = nombre, Descripcion = descripcion, Precio = precio });
		}

		public static bool Borrar(int id)
		{
			int contador = 0;
			foreach (Producto producto in productos)
			{
				if (producto.Id == id)
				{
					productos.RemoveAt(contador);
					return true;
				}
				contador++;
			}
			return false;
		}

		public static bool Modificar(int id, string nombre, string descripcion, double precio)
		{
			foreach (Producto producto in productos)
			{
				if (producto.Id == id)
				{
					producto.Nombre = nombre;
					producto.Descripcion = descripcion;
					producto.Precio = precio;
					return true;
				}
			}
			return false;
		}

		public static void Anadir(string nombre, string descripcion, double precio)
		{
			productos.Add(new Producto() { Id = UltimoId(), Nombre = nombre, Descripcion = descripcion, Precio = precio });
		}

		public static bool Buscar(int id)
		{
			foreach (Producto producto in productos)
			{
				if (producto.Id == id)
				{
					return true;
				}
			}
			return false;
		}
	}
}
