using System;

namespace clase_2_3
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Introduzca el usuario");
			string usuario = Console.ReadLine();

			Console.WriteLine("Introduzca su password");
			string pass = Console.ReadLine();

			// if (usuario == "admin") {
			// 	if(pass == "123456") {
			// 		Console.WriteLine("Usuario y Password Correcto");
			// 	} else {
			// 		Console.WriteLine("Password Incorrecto");
			// 	}
			// } else {
			// 	Console.WriteLine("Usuario Incorrecto");
			// }

			switch (usuario) {
				case "admin":
					if(pass == "123456") {
						Console.WriteLine("Usuario y Password Correcto");
						break;
					}
					Console.WriteLine("Password Incorrecto");
					break;
				default:
					Console.WriteLine("Usuario Incorrecto");
					break;
			}
		}
	}
}
