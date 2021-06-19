using System;
using System.IO;
using System.Collections.Generic;

namespace clase_12_ej_2 {
    class Program {
        
		static void Main(string[] args) {
            var reader = new StreamReader(File.OpenRead("/mnt/c/Users/Salva ISTEA/Desktop/ISTEA/cuatrimestre_1/logica_de_programacion/clases/clase_12/logs/clase_12_2.csv"));

			List<string> usuarios = new List<string>();
			List<string> contrasenas = new List<string>();
			List<string> tiposUsuarios = new List<string>();

			Console.Clear();
			while (!reader.EndOfStream) {
				var linea = reader.ReadLine();
				var valor = linea.Split(";");
				usuarios.Add(valor[0]);
				contrasenas.Add(valor[1]);
				tiposUsuarios.Add(valor[2]);
			}

			int cantidadUsuarios = usuarios.Count;


			for (int i = 0; i < cantidadUsuarios; i++) {
				Console.WriteLine("--user="+usuarios[i] + " --password="+contrasenas[i]+" --type="+tiposUsuarios[i]);
			}

        }
    }
}
