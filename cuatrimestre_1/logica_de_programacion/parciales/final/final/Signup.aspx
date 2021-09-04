<%@ Page Title="Signup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="final.Signup" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="title">
		Registrarse
	</div>
	<div class="columns">
		<div class="column">
			<div class="field">
				<label class="label is-pulled-left">Introduzca su nuevo nombre de usuario:</label>
				<div class="control">
					<asp:TextBox ID="TextBox_SignupUsuario" class="input" runat="server" />
				</div>
				<div class="message is-danger">
					<asp:Label ID="LabelError_SignupUsuario" runat="server" Visible="false">El nombre de usuario no puede estar vacio</asp:Label>
				</div>
				<div class="message is-danger">
					<asp:Label ID="LabelError_SignupTomadoUsuario" runat="server" Visible="false">El nombre de usuario ya esta tomado, elija uno nuevo</asp:Label>
				</div>
			</div>

			<div class="field">
				<label class="label is-pulled-left">Introduzca su nueva contrasena:</label>
				<div class="control">
					<asp:TextBox ID="TextBox_SignupPassword" class="input" runat="server" />
				</div>
				<div class="message is-danger">
					<asp:Label ID="LabelError_SignupPassword" runat="server" Visible="false"> La contrasena debe tener al menos 8 caracteres, un numero, una minuscula y una mayuscula.</asp:Label>
				</div>
			</div>

			<div class="field">
				<label class="label is-pulled-left">Introduzca nuevamente su contrasena:</label>
				<div class="control">
					<asp:TextBox ID="TextBox_SignupPasswordValidate" class="input" runat="server" />
				</div>
				<div class="message is-danger">
					<asp:Label ID="LabelError_SignupPasswordValid" runat="server" Visible="false"> Las contrasenas no coinciden.</asp:Label>
				</div>
			</div>

			<div class="field">
				<label class="label is-pulled-left">Codigo Admin:</label>
				<div class="control">
					<asp:TextBox ID="TextBox_Admin" class="input" type="password" runat="server" />
				</div>
			</div>

			<br>
			<div class="field">
				<div class="columns is-centered">
					<asp:Button ID="Button_SignupButton" class="button is-primary is-pulled-right" Text="Registrarse" runat="server" OnClick="SignupButton_Click" />
				</div>
			</div>
		</div>
		<div class="error">
			<asp:Label ID="Label_SignupMensajeError" runat="server" Visible="false"> Ha ocurrido un error</asp:Label>
		</div>
	</div>
</asp:Content>
