using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Biblioteca
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public int Tipo { get; set; }

        private static List<Usuario> usuarios = new List<Usuario>(){};

        public static int UltimoId ()
		{
            var ultimoId = 1;
			if (usuarios.Count > 0)
			{
				ultimoId = usuarios[usuarios.Count - 1].Id + 1;
			}
            return ultimoId;
		}

		public static void Introducir (string nombre, string password, int tipo)
        {
            usuarios.Add(new Usuario() { Id = UltimoId(), Nombre = nombre, Password = password, Tipo = tipo });
        }

        public static string ObtenerNombreUsuario (int id)
		{
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.Id == id)
                {
                    return usuario.Nombre;
                }
            }
            return "";
        }

        public static int ValidarIngreso (string nombre, string password)
        {
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.Nombre == nombre)
                {
                    if (usuario.Password == password)
                    {
                        return usuario.Id;
                    }
                    return 0;
                }
            }
            return 0;
        }

        public static bool ValidarStringEntrada(string valorEntrada)
        {
            if (String.IsNullOrEmpty(valorEntrada) || valorEntrada.Length < 1)
            {
                return false;
            }
            return true;
        }

        public static bool ValidarContrasena(string contrasena)
		{
            bool esStringValido = ValidarStringEntrada(contrasena);
            if (!esStringValido)
            {
                return false;
			}
            string patron = @"^((?=.{8,}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*|(?=.{8,}$)(?=.*\d)(?=.*[a-zA-Z])(?=.*[!\u0022#$%&'()*+,./:;<=>?@[\]\^_`{|}~-]).*)";
            Regex rg = new Regex(patron);
            Match match = rg.Match(contrasena);
            if (!match.Success)
            {
                return false;
            }
            return true;
		}

        public static bool EsAdmin(int id)
		{
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.Id == id)
                {
                    if (usuario.Tipo == 1)
					{
                        return true;
					}
                    return false;
                }
            }
            return false;
        }

        public static bool ValidarUsuarioUnico(string nombre)
		{
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.Nombre == nombre)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
