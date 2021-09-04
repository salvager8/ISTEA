<%@ Page Title="Bienvenido" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="final.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div>
		<asp:Label Text="Visualizacion de Productos:" runat="server" class="title" /><br>
		<asp:Label ID="Label_DefaultTitle" runat="server" class="subtitle"></asp:Label><br>
		<br>
	</div>
	<asp:Literal ID="Literal_DefaultTable" runat="server"></asp:Literal>
	<div class="has-text-centered columns">
		<asp:Label Text="Introduzca el ID del producto a eliminar:" ID="Label_DefaultDelete" class="column subtitle" runat="server" Visible="false"/>
		<asp:TextBox ID="TextBox_DefaultDelete" class="column input" runat="server" Visible="false" />
	</div>
	<div class="has-text-centered">
		<asp:Button ID="Boton_EliminarProductoConfirmar" class="button is-danger" Text="Eliminar" runat="server" Visible="false" OnClick="DefaultDeleteButtonConfirm_Click"/>
	</div>
	<div class="has-text-centered columns">
		<asp:Label Text="Introduzca el ID del producto a modificar:" ID="Label_DefaultModify" class="column subtitle" runat="server" Visible="false"/>
		<asp:TextBox ID="TextBox_DefaultModify" class="column input" runat="server" Visible="false" />
	</div>
	<div class="has-text-centered">
		<asp:Button ID="Boton_ModificarProductoBuscar" class="button is-primary" Text="Modificar" runat="server" Visible="false" OnClick="DefaultModifyButtonSearch_Click"/>
	</div>
	<div class="has-text-centered columns">
		<asp:Label Text="Nombre:" ID="Label_ModificarNombre" class="column subtitle" runat="server" Visible="false"/>
		<asp:TextBox ID="TextBox_ModificarNombre" class="column input" runat="server" Visible="false" />
		<asp:Label Text="Descripcion:" ID="Label_ModificarDescripcion" class="column subtitle" runat="server" Visible="false"/>
		<asp:TextBox ID="TextBox_ModificarDescripcion" class="column input" runat="server" Visible="false" />
		<asp:Label Text="Precio:" ID="Label_ModificarPrecio" class="column subtitle" runat="server" Visible="false"/>
		<asp:TextBox ID="TextBox_ModificarPrecio" class="column input" runat="server" Visible="false" />
	</div>
	<div class="has-text-centered">
		<asp:Button ID="Boton_ModificarProductoConfirmar" class="button is-primary" Text="Modificar" runat="server" Visible="false" OnClick="DefaultModifyButtonConfirm_Click"/>
	</div>
	<div class="has-text-centered">
		<asp:Button ID="Boton_AnadirProductoConfirmar" class="button is-primary" Text="Anadir" runat="server" Visible="false" OnClick="DefaultAnadirButtonConfirm_Click"/>
	</div>
	<div class="columns is-centered">
		<div class="column">
			<asp:Button ID="Boton_AnadirProducto" class="button is-primary" Text="Anadir un producto" runat="server" OnClick="DefaultAnadirButton_Click" />
		</div>
		<div class="column">
			<asp:Button ID="Boton_ModificarProducto" class="button is-primary" Text="Modificar un producto" runat="server" OnClick="DefaultModifyButton_Click"/>
		</div>
		<div class="column">
			<asp:Button ID="Boton_EliminarProducto" class="button is-danger" Text="Eliminar un producto" runat="server" OnClick="DefaultDeleteButton_Click"/>
		</div>
	</div>
    <div class="message is-danger has-text-centered">
		<asp:Label ID="Label_FalloDefault" runat="server" Visible="false"></asp:Label>
	</div>
	<div class="message is-success has-text-centered">
		<asp:Label ID="Label_MensajeProductoAnadido" runat="server" class="message is-success" Visible="false">El producto fue anadido</asp:Label>
	</div>
    <div class="message is-success has-text-centered">
		<asp:Label ID="Label_MensajeProductoEliminado" runat="server" class="message is-success" Visible="false">El producto fue eliminado</asp:Label>
	</div>
	<div class="message is-success has-text-centered">
		<asp:Label ID="Label_MensajeProductoModificado" runat="server" class="message is-success" Visible="false">El producto fue modificado</asp:Label>
	</div>

</asp:Content>
