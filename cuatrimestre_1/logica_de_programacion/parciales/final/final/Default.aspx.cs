using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioteca;
using System.Data;
using System.Text;
using System.Threading;

namespace final
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["UsuarioLoggeado"]) < 1)
			{
                Response.Redirect("~/Login.aspx");
			}

            Label_DefaultTitle.Text = "Bienvenido/a " + Usuario.ObtenerNombreUsuario(Convert.ToInt32(Session["UsuarioLoggeado"]));
            GenerarTabla();
        }

        private void OcultarTodo()
        {
            Boton_AnadirProducto.Visible = false;
            Boton_EliminarProducto.Visible = false;
            TextBox_DefaultDelete.Visible = false;
            Label_DefaultDelete.Visible = false;
            Boton_EliminarProductoConfirmar.Visible = false;
            Boton_ModificarProducto.Visible = false;
            Boton_ModificarProductoConfirmar.Visible = false;
            Boton_ModificarProductoBuscar.Visible = false;
            Label_DefaultModify.Visible = false;
            TextBox_DefaultModify.Visible = false;
            Label_ModificarNombre.Visible = false;
            TextBox_ModificarNombre.Visible = false;
            Label_ModificarDescripcion.Visible = false;
            TextBox_ModificarDescripcion.Visible = false;
            Label_ModificarPrecio.Visible = false;
            TextBox_ModificarPrecio.Visible = false;
            Boton_AnadirProductoConfirmar.Visible = false;
        }

        private void MostrarBase()
        {
            Boton_AnadirProducto.Visible = true;
            Boton_ModificarProducto.Visible = true;
            Boton_EliminarProducto.Visible = true;
        }

        private void GenerarTabla()
        {
            if (Producto.productos.Count < 1) {
                OcultarTodo();
                Boton_AnadirProducto.Visible = true;
                Literal_DefaultTable.Text = "<p class=\"message is-dark\"><b>No se encontraron registros</b></p>";
                return;
            }
            StringBuilder htmlTableString = new StringBuilder();
            htmlTableString.AppendLine("<table class=\"table is-bordered is-striped is-hoverable\">");
            htmlTableString.AppendLine("<thead>");
            htmlTableString.AppendLine("<tr>");
            htmlTableString.AppendLine("<th>ID</th>");
            htmlTableString.AppendLine("<th>Nombre</th>");
            htmlTableString.AppendLine("<th>Descripcion</th>");
            htmlTableString.AppendLine("<th>Precio</th>");
            htmlTableString.AppendLine("</tr>");
            htmlTableString.AppendLine("</thead>");
            htmlTableString.AppendLine("<tbody>");
            foreach (Producto producto in Producto.productos)
            {
                CrearFila(htmlTableString, producto.Id, producto.Nombre, producto.Descripcion, producto.Precio);
            }
            htmlTableString.AppendLine("</tbody>");
            htmlTableString.AppendLine("</table>");
            Literal_DefaultTable.Text = htmlTableString.ToString();
        }

        private StringBuilder CrearFila(StringBuilder htmlTableString, int id, string nombre, string descripcion, double precio)
        {
            htmlTableString.AppendLine("<tr>");
            htmlTableString.AppendLine("<td>" + id + "</td>");
            htmlTableString.AppendLine("<td>" + nombre + "</td>");
            htmlTableString.AppendLine("<td>" + descripcion + "</td>");
            htmlTableString.AppendLine("<td>" + String.Format("{0:c2}", precio) + "</td>");
            htmlTableString.AppendLine("</tr>");
            return htmlTableString;
        }

        public void DefaultDeleteButton_Click(object sender, EventArgs e)
        {
            Label_MensajeProductoModificado.Visible = false;
            Label_MensajeProductoEliminado.Visible = false;
            Label_MensajeProductoAnadido.Visible = false;
            Label_FalloDefault.Visible = false;
            bool esUsuarioAdmin = Usuario.EsAdmin(Convert.ToInt32(Session["UsuarioLoggeado"]));
            if (!esUsuarioAdmin)
            {
                Label_FalloDefault.Text = "Este usuario no tiene permisos para realizar esta accion";
                Label_FalloDefault.Visible = true;
                OcultarTodo();
                MostrarBase();
                return;
            }
            OcultarTodo();
            TextBox_DefaultDelete.Visible = true;
            Label_DefaultDelete.Visible = true;
            Boton_EliminarProductoConfirmar.Visible = true;
        }

        public void DefaultDeleteButtonConfirm_Click(object sender, EventArgs e)
        {
            OcultarTodo();
            TextBox_DefaultDelete.Visible = true;
            Label_DefaultDelete.Visible = true;
            Boton_EliminarProductoConfirmar.Visible = true;

            var idParaEliminarEntrada = TextBox_DefaultDelete.Text;
            var idParaEliminar = 0;
            try
            {
                idParaEliminar = Convert.ToInt32(idParaEliminarEntrada);
            }
            catch
            {
                Label_FalloDefault.Text = "El id que ingreso no es valido, intente nuevamente.";
                Label_FalloDefault.Visible = true;
                OcultarTodo();
                MostrarBase();
                return;
            }
            if (idParaEliminar < 1)
            {
                Label_FalloDefault.Text = "El id que ingreso debe ser positivo y mayor a cero.";
                Label_FalloDefault.Visible = true;
                OcultarTodo();
                MostrarBase();
                return;
            }
            bool estaProductoBorrado = Producto.Borrar(idParaEliminar);

            if (!estaProductoBorrado)
            {
                Label_FalloDefault.Text = "El producto no se encontro";
                Label_FalloDefault.Visible = true;
                OcultarTodo();
                MostrarBase();
                return;
            } else
            {
                Label_MensajeProductoEliminado.Visible = true;
                OcultarTodo();
                MostrarBase();
            }

            GenerarTabla();
        }

        public void DefaultModifyButton_Click(object sender, EventArgs e)
        {
            Label_MensajeProductoModificado.Visible = false;
            Label_MensajeProductoEliminado.Visible = false;
            Label_MensajeProductoAnadido.Visible = false;
            Label_FalloDefault.Visible = false;
            bool esUsuarioAdmin = Usuario.EsAdmin(Convert.ToInt32(Session["UsuarioLoggeado"]));
            if (!esUsuarioAdmin)
            {
                Label_FalloDefault.Text = "Este usuario no tiene permisos para realizar esta accion";
                Label_FalloDefault.Visible = true;
                OcultarTodo();
                MostrarBase();
                return;
            }
            OcultarTodo();
            Label_DefaultModify.Visible = true;
            TextBox_DefaultModify.Visible = true;
            Boton_ModificarProductoBuscar.Visible = true;
        }

        public void DefaultModifyButtonSearch_Click(object sender, EventArgs e)
        {
            OcultarTodo();
            Label_DefaultModify.Visible = true;
            TextBox_DefaultModify.Visible = true;
            Boton_ModificarProductoBuscar.Visible = true;

            var idParaModificarEntrada = TextBox_DefaultModify.Text;
            var idParaModificar = 0;
            try
            {
                idParaModificar = Convert.ToInt32(idParaModificarEntrada);
            }
            catch
            {
                Label_FalloDefault.Text = "El id que ingreso no es valido, intente nuevamente.";
                Label_FalloDefault.Visible = true;
                OcultarTodo();
                MostrarBase();
                return;
            }
            if (idParaModificar < 1)
            {
                Label_FalloDefault.Text = "El id que ingreso debe ser positivo y mayor a cero.";
                Label_FalloDefault.Visible = true;
                OcultarTodo();
                MostrarBase();
                return;
            }

            bool esProductoExistente = Producto.Buscar(idParaModificar);

            if (!esProductoExistente)
            {
                Label_FalloDefault.Text = "El producto no se encontro";
                Label_FalloDefault.Visible = true;
                OcultarTodo();
                MostrarBase();
                return;
            }

            TextBox_ModificarNombre.Text = "";
            TextBox_ModificarDescripcion.Text = "";
            TextBox_ModificarPrecio.Text = "";
            OcultarTodo();
            Boton_ModificarProductoConfirmar.Visible = true;
            Label_ModificarNombre.Visible = true;
            TextBox_ModificarNombre.Visible = true;
            Label_ModificarDescripcion.Visible = true;
            TextBox_ModificarDescripcion.Visible = true;
            Label_ModificarPrecio.Visible = true;
            TextBox_ModificarPrecio.Visible = true;
        }

        public void DefaultModifyButtonConfirm_Click(object sender, EventArgs e) {
            OcultarTodo();
            Boton_ModificarProductoConfirmar.Visible = true;

            var nombre = TextBox_ModificarNombre.Text;
            var descripcion = TextBox_ModificarDescripcion.Text;
            var precio = TextBox_ModificarPrecio.Text;
            var idParaModificar = Convert.ToInt32(TextBox_DefaultModify.Text);
            string nombreModificar = "";
            string descripcionModificar = "";
            double precioModificar = 0;
            try
            {
                nombreModificar += nombre.ToString();
                descripcionModificar += descripcion.ToString();
                precioModificar = Convert.ToDouble(precio);
                if (precioModificar < 0)
                {
                    Label_FalloDefault.Text = "El precio no puede ser negativo";
                    Label_FalloDefault.Visible = true;
                    OcultarTodo();
                    MostrarBase();
                    return;
                }
            }
            catch
            {
                Label_FalloDefault.Text = "Los datos que ingreso no son validos, por favor intente nuevamente";
                Label_FalloDefault.Visible = true;
                OcultarTodo();
                MostrarBase();
                return;
            }
            bool estaProductoModificado = Producto.Modificar(idParaModificar, nombreModificar, descripcionModificar, precioModificar);

            if (!estaProductoModificado)
            {
                Label_FalloDefault.Text = "El producto no se pudo modificar";
                Label_FalloDefault.Visible = true;
                OcultarTodo();
                MostrarBase();
                return;
            }
            else
            {
                Label_MensajeProductoModificado.Visible = true;
                OcultarTodo();
                MostrarBase();
            }

            GenerarTabla();
        }

        public void DefaultAnadirButton_Click(object sender, EventArgs e)
        {
            Label_MensajeProductoModificado.Visible = false;
            Label_MensajeProductoEliminado.Visible = false;
            Label_MensajeProductoAnadido.Visible = false;
            Label_FalloDefault.Visible = false;
            TextBox_ModificarNombre.Text = "";
            TextBox_ModificarDescripcion.Text = "";
            TextBox_ModificarPrecio.Text = "";
            OcultarTodo();
            Boton_AnadirProductoConfirmar.Visible = true;
            Label_ModificarNombre.Visible = true;
            TextBox_ModificarNombre.Visible = true;
            Label_ModificarDescripcion.Visible = true;
            TextBox_ModificarDescripcion.Visible = true;
            Label_ModificarPrecio.Visible = true;
            TextBox_ModificarPrecio.Visible = true;
        }

        public void DefaultAnadirButtonConfirm_Click(object sender, EventArgs e)
        {
            var nombre = TextBox_ModificarNombre.Text;
            var descripcion = TextBox_ModificarDescripcion.Text;
            var precio = TextBox_ModificarPrecio.Text;
            string nombreModificar = "";
            string descripcionModificar = "";
            double precioModificar = 0;
            try
            {
                nombreModificar += nombre.ToString();
                descripcionModificar += descripcion.ToString();
                precioModificar = Convert.ToDouble(precio);
                if (precioModificar < 0)
                {
                    Label_FalloDefault.Text = "El precio no puede ser negativo";
                    Label_FalloDefault.Visible = true;
                    OcultarTodo();
                    MostrarBase();
                    return;
                }
            }
            catch
            {
                Label_FalloDefault.Text = "Los datos que ingreso no son validos, por favor intente nuevamente";
                Label_FalloDefault.Visible = true;
                OcultarTodo();
                MostrarBase();
                return;
            }
            Producto.Anadir(nombreModificar, descripcionModificar, precioModificar);
            Label_MensajeProductoAnadido.Visible = true;
            OcultarTodo();
            MostrarBase();
            GenerarTabla();
        }
    }
}