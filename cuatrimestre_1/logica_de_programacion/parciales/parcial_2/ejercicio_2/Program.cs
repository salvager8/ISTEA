using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;

namespace ejercicio_2
{
	class Program
	{
		// Productos creador
		private static List<string> productos =		new List<string>()	{ "Microondas", "Television", "Auriculares"};
		private static List<string> descripciones =	new List<string>()	{ "300W ideal para el hogar", "LCD de 32 pulgadas", "alta calidad y buen sonido" };
		private static List<int>	cantidades =	new List<int>()		{ 5, 763, 90021 };
		private static List<double> precios =		new List<double>()	{ 1200.45, 34221.12, 100 };

		// Usuarios
		private static List<string> usuarios =		new List<string>() { "admin", "salva" };
		private static List<string> contrasenas =	new List<string>() { "admin", "corgis" };
		private static List<string> permisos =		new List<string>() { "admin", "user" };

		// Imprime el enunciado por pantalla y pide el ingreso de un enter
		private static void ImprimirEnunciado() {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("Enunciado: \n");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\tDesarrolle una aplicación consola para un sistema de ventas de productos de una cadena de electrodomésticos,");
			Console.WriteLine("\tla misma contara con la siguiente estructura:\n");
			Console.WriteLine("\tProducto		-> Lista tipo string.");
			Console.WriteLine("\tDescripcion		-> Lista tipo string.");
			Console.WriteLine("\tCantidad		-> Lista tipo entero.");
			Console.WriteLine("\tPrecio			-> Lista tipo doble.");
			Console.ResetColor();
			EnterParaContinuar();
		}

		// Imprime el error por pantalla
		private static void ImprimirError(string mensaje) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("\n\t");
			Console.WriteLine(mensaje);
			Thread.Sleep(2000);
			Console.Clear();
			Console.ResetColor();
		}

		// Valida un string ingresado por pantalla
		private static string ObtenerStringEntrada(string valorEntrada, string mensaje, bool deberiaLimpiar = true) {
			if (deberiaLimpiar) {
				Console.Clear();
			}
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			valorEntrada = Console.ReadLine();
			if (String.IsNullOrEmpty(valorEntrada)) {
				ImprimirError("El valor no puede estar vacio, intente nuevamente\n");
				valorEntrada = ObtenerStringEntrada(valorEntrada, mensaje, deberiaLimpiar);
			}
			return valorEntrada;
		}

		// Valida un numero ingresado por pantalla
		private static int ObtenerNumeroEntrada(int valorEntrada, string mensaje) {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			bool esEntero = int.TryParse(Console.ReadLine(), out valorEntrada);
			if (!esEntero) {
				ImprimirError("El dato ingresado no es un numero entero, intente nuevamente");
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje);
			} else if (valorEntrada < 0) {
				ImprimirError("El numero ingresado no puede ser negativo");
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje);
			} else if (valorEntrada > 99999999) {
				ImprimirError("El numero ingresado es demasiado grande para ser un DNI, corrija los datos e intente nuevamente");
				valorEntrada = ObtenerNumeroEntrada(valorEntrada, mensaje);
			}

			return valorEntrada;
		}

		// Valida un double ingresado por pantalla
		private static double ObtenerDobleEntrada(double valorEntrada, string mensaje) {
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write(mensaje);
			Console.ResetColor();

			bool esDoble = double.TryParse(Console.ReadLine(), out valorEntrada);
			if (!esDoble) {
				ImprimirError("El dato ingresado no es un numero entero, intente nuevamente");
				valorEntrada = ObtenerDobleEntrada(valorEntrada, mensaje);
			} else if (valorEntrada < 0) {
				ImprimirError("El numero ingresado no puede ser negativo");
				valorEntrada = ObtenerDobleEntrada(valorEntrada, mensaje);
			}

			return valorEntrada;
		}

		// Alinea al centro un string relativo al ancho de la tabla
		static string AlinearAlCentro(string texto, int ancho) {
			texto = texto.Length > ancho ? texto.Substring(0, ancho - 3) + "..." : texto;

			if (string.IsNullOrEmpty(texto)) {
				return new string(' ', ancho);
			}
			else {
				return texto.PadRight(ancho - (ancho - texto.Length) / 2).PadLeft(ancho);
			}
		}

		// Imprime una linea para la tabla
		static void ImprimirLinea(params string[] columnas) {
			int ancho = (70 - columnas.Length) / columnas.Length;
			string fila = "|";

			foreach (string columna in columnas) {
				fila += AlinearAlCentro(columna, ancho) + "|";
			}

			Console.WriteLine(fila);
		}

		// Construye una tabla para mostrarla por pantalla
		private static void MostrarProductos() {
			Console.WriteLine("");
			int contador = 0;
			Console.BackgroundColor = ConsoleColor.Blue;
			ImprimirLinea("Producto", "Descripcion", "Cantidad", "Precio");
			Console.ResetColor();
			foreach (string producto in productos) {
				if (cantidades[contador] > 0) {
					Console.ForegroundColor = ConsoleColor.Black;
					Console.BackgroundColor = ConsoleColor.Cyan;
				} else {
					Console.ForegroundColor = ConsoleColor.Black;
					Console.BackgroundColor = ConsoleColor.Magenta;
				}
				ImprimirLinea(productos[contador], descripciones[contador], cantidades[contador].ToString(), precios[contador].ToString());
				contador++;
			}
			Console.ResetColor();
		}

		// Busca un producto en los productos anadidos
		private static int? BuscarProducto(string productoEntrada, List<string> productos) {
			int posicion = 0;
			bool productoExiste = false;
			foreach (string producto in productos) {
				if (producto.ToLower() == productoEntrada.ToLower()) {
					productoExiste = true;
					break;
				}
				posicion++;
			}
			if (productoExiste) {
				return posicion;
			} else {
				return -1;
			}
		}

		// Pide un enter por pantalla para continuar
		private static void EnterParaContinuar() {
			Console.WriteLine("\nPresione [ENTER] para continuar");
			ConsoleKeyInfo keyInfo = Console.ReadKey();
			while (keyInfo.Key != ConsoleKey.Enter)
				keyInfo = Console.ReadKey();
			Console.Clear();
		}

		// Valida si un texto es vacio, en caso de que no imprime en un archivo
		static void ImprimirEnArchivo(ref string mensaje, string ruta) {
			if (String.IsNullOrEmpty(mensaje)) {
				return;
			}
			try {
				File.AppendAllLines(ruta, new String[] { mensaje });
				mensaje = "";
			} catch {
				ImprimirError("Hay errores al intentar escribir en el archivo");
				Environment.Exit(0);
			}
		}

		// Valida el login de un usuario y muestra un mensaje por pantalla
		static int ValidarLogin() {
			string usuarioActual = ObtenerStringEntrada("", "Introduzca su nombre de usuario: ");
			string contrasenaActual = ObtenerStringEntrada("", "Introduzca su contrasena: ");

			int contador = 0;
			foreach (string usuario in usuarios) {
				if (usuarioActual == usuario && contrasenaActual == contrasenas[contador]) {
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write("\n\t");
					Console.WriteLine("Bienvenido/a {0}", usuarioActual);
					Thread.Sleep(2000);
					Console.Clear();
					Console.ResetColor();
					if (permisos[contador] == "admin") {
						return 2;
					} else {
						return 1;
					}
				}
				contador++;
			}
			return 0;
		}

		// Function principal
		public static void Main(string[] args)
		{
			string ruta = "./ventas.log";
			string mensajeParaLog = "Ventas de la fecha " + DateTime.Now + "\n";

			// Imprime el enunciado por pantalla
			ImprimirEnunciado();

			// Valida el usuario
			int tipoDeUsuario = 0;
			while (tipoDeUsuario == 0) {
				tipoDeUsuario = ValidarLogin();

				if (tipoDeUsuario == 0) {
					ImprimirError("Combinacion de usuario y contrasena incorrectos, intente nuevamente.");
				}
			}

			// Ciclo principal
			bool salir = false;
			while (!salir) {
				int? posicion = null;
				string productoEntrada = "";
				int opcion = ObtenerNumeroEntrada(0, "Introduzca la accion a realizar:\n\t1 - Mostrar productos\n\t2 - Anadir un producto\n\t3 - Modificar el precio de un producto\n\t4 - Vender un producto\n\t5 - Modificar el stock de un producto\n\t0 - Salir\n=> ");
				switch(opcion) {
					case 0:
						// Salir
						string mensaje = "\n";
						ImprimirEnArchivo(ref mensaje, ruta);
						salir = true;
						break;
					case 1:
						// Muestra los productos por pantalla
						MostrarProductos();
						EnterParaContinuar();
						break;
					case 2:
						if (tipoDeUsuario == 1) {
							ImprimirError("No tienes permiso para realizar esta accion");
							break;
						}
						// Anadir un producto
						productos.Add(ObtenerStringEntrada("", "Introduzca el nombre del producto: "));
						descripciones.Add(ObtenerStringEntrada("", "Introduzca la descripcion del producto: "));
						cantidades.Add(ObtenerNumeroEntrada(0, "Introduzca la cantidad existente del producto: "));
						precios.Add(ObtenerDobleEntrada(0, "Introduzca el precio por unidad del producto: "));
						break;
					case 3:
						if (tipoDeUsuario == 1) {
							ImprimirError("No tienes permiso para realizar esta accion");
							break;
						}
						// Modificar el precio de un producto
						MostrarProductos();
						productoEntrada = ObtenerStringEntrada("", "\nIntroduzca el nombre del producto al que se quiere cambiar el precio: ", false);
						posicion = BuscarProducto(productoEntrada, productos);
						if (posicion != null && posicion != -1) {
							precios[Convert.ToInt16(posicion)] = ObtenerDobleEntrada(0, "Introduzca el nuevo precio por unidad del producto: ");
						} else {
							ImprimirError("El producto que intrudujo no es un producto valido.");
						}
						break;
					case 4:
						if (tipoDeUsuario == 1) {
							ImprimirError("No tienes permiso para realizar esta accion");
							break;
						}
						Console.WriteLine("Seleccione un producto:\n");
						// Vender un producto
						MostrarProductos();
						productoEntrada = ObtenerStringEntrada("", "\nIntroduzca el nombre del producto que se quiere vender: ", false);
						posicion = BuscarProducto(productoEntrada, productos);
						if (posicion != null && posicion != -1) {
							int cantidadActual = cantidades[Convert.ToInt16(posicion)];
							double precioActual = precios[Convert.ToInt16(posicion)];
							if (cantidadActual > 0) {
								int cantidad = ObtenerNumeroEntrada(0, "Por favor introduzca la cantidad del producto que quiere vender: ");
								if (cantidadActual >= cantidad) {
									string ahora = DateTime.Now.ToShortDateString();
									double montoVenta = precioActual * cantidad;
									Console.WriteLine("Se efectuo la venta de {0} unidades del producto {1} el dia {2} con un precio unitario de: {3:c2}", cantidadActual, productoEntrada, ahora, precioActual);
									Console.ForegroundColor = ConsoleColor.Green;
									Console.WriteLine("Total a pagar: {0:c2}", montoVenta);
									Console.ResetColor();
									mensajeParaLog += String.Format("{0} - Producto: {1} - Precio unitario: {2} - Cantidad: {3} - Monto de venta: {4}", ahora, productoEntrada, precioActual, cantidad, montoVenta);
									ImprimirEnArchivo(ref mensajeParaLog, ruta);
									cantidades[Convert.ToInt16(posicion)] -= cantidad;
									Console.WriteLine(mensajeParaLog);
									EnterParaContinuar();
								} else {
									ImprimirError("No hay suficientes unidades para efectuar la venta");
								}
							} else {
								ImprimirError("El producto que introdujo no esta disponible.");
							}
						} else {
							ImprimirError("El producto que introdujo no es un producto valido.");
						}
						break;
					case 5:
						if (tipoDeUsuario == 1) {
							ImprimirError("No tienes permiso para realizar esta accion");
							break;
						}
						// Modificar el stock de un producto
						MostrarProductos();
						productoEntrada = ObtenerStringEntrada("", "\nIntroduzca el nombre del producto al que se quiere cambiar el stock: ", false);
						posicion = BuscarProducto(productoEntrada, productos);
						if (posicion != null && posicion != -1) {
							cantidades[Convert.ToInt16(posicion)] = ObtenerNumeroEntrada(0, "Introduzca el nuevo stock por unidad del producto: ");
						} else {
							ImprimirError("El producto que intrudujo no es un producto valido.");
						}
						break;
					default:
						ImprimirError("Opcion no reconocida, pruebe con numeros del 0 al 5");						
						break;
				}
			}
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("\n\t");
			Console.WriteLine("Hasta Luego!");
			Console.Write("\n");
			Console.ResetColor();
		}
	}
}
