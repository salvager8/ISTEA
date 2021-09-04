using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioteca;

namespace final
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["UsuarioLoggeado"]) > 1)
            {
                Response.Redirect("~/Default.aspx");
            }
            Label_MensajeError.Visible = false;
        }

        public void ShowText()
        {
            EnableDisableText(true);
        }

        public void EnableDisableText(bool condicional)
        {
            Label_MensajeError.Visible = condicional;
        }

        protected void LoginButton_Click(object Sender, EventArgs e)
        {
            var usuario = TextBox_Usuario.Text;
            var contrasena = TextBox_Password.Text;

            var idUsuarioLoggeado = Usuario.ValidarIngreso(usuario, contrasena);

            if (idUsuarioLoggeado > 0)
            {
                Session["UsuarioLoggeado"] = idUsuarioLoggeado;
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                ShowText();
            }
        }

        protected void SignupButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Signup.aspx");
        }
    }
}