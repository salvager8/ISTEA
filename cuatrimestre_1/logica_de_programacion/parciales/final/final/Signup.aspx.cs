using System;
using System.Web.UI;
using Biblioteca;

namespace final
{
    public partial class Signup : Page
    {
		protected void Page_Load(object sender, EventArgs e)
        {
            OcultarTodo();
        }

        private void OcultarTodo()
		{
            LabelError_SignupUsuario.Visible = false;
            LabelError_SignupPassword.Visible = false;
            LabelError_SignupPasswordValid.Visible = false;
            LabelError_SignupTomadoUsuario.Visible = false;
        }

        protected void SignupButton_Click(object sender, EventArgs e)
		{
            OcultarTodo();
            var nombreUsuario = TextBox_SignupUsuario.Text;
            var contrasenaUsuario = TextBox_SignupPassword.Text;
            var validarContrasenaUsuario = TextBox_SignupPasswordValidate.Text;
            bool esFormularioValido = true;

            bool esNombreValido = Usuario.ValidarStringEntrada(nombreUsuario);
            if (!esNombreValido)
			{
                LabelError_SignupUsuario.Visible = true;
                esFormularioValido = false;
            }

            bool estaNombreTomado = Usuario.ValidarUsuarioUnico(nombreUsuario);
            if (estaNombreTomado)
            {
                LabelError_SignupTomadoUsuario.Visible = true;
                esFormularioValido = false;
            }

            bool esContrasenaValida = Usuario.ValidarContrasena(contrasenaUsuario);
            if (!esContrasenaValida)
            {
                LabelError_SignupPassword.Visible = true;
                esFormularioValido = false;
            }

            bool tieneIgualesContrasenas = contrasenaUsuario == validarContrasenaUsuario;
            if (!tieneIgualesContrasenas)
            {
                LabelError_SignupPasswordValid.Visible = true;
                esFormularioValido = false;
            }

            if (esFormularioValido)
			{
                var codigoAdmin = TextBox_Admin.Text;
                bool contieneCodigoAdmin = Usuario.ValidarStringEntrada(codigoAdmin);
                int tipo = 2;
                if (contieneCodigoAdmin && codigoAdmin == "21232f297a57a5a743894a0e4a801fc3")
                {
                    tipo = 1;
                }
                Session["UsuarioLoggeado"] = Usuario.UltimoId();
                Usuario.Introducir(nombreUsuario, contrasenaUsuario, tipo);
                Response.Redirect("~/Default.aspx");
            }
        }
    }
}