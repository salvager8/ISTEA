<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="final.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="title">
		Login
	</div>
	<div class="columns">
		<div class="column">
			<div class="field">
				<label class="label is-pulled-left">Usuario</label>
				<div class="control">
					<asp:TextBox ID="TextBox_Usuario" class="input" runat="server" />
				</div>
			</div>

			<div class="field">
				<label class="label is-pulled-left">Contrasena</label>
				<div class="control">
					<asp:TextBox ID="TextBox_Password" type="password" class="input" runat="server" />
					<span class="icon is-small is-left">
						<i class="fas fa-user"></i>
					</span>
					<span class="icon is-small is-right">
						<i class="fas fa-check"></i>
					</span>
				</div>
			</div>
			<div class="columns is-centered">
				<div class="column is-half">
					<asp:Button ID="LoginButton" class="button is-primary is-pulled-right" Text="Iniciar Sesion" runat="server" OnClick="LoginButton_Click" />
				</div>
				<div class="column is-half">
					<asp:Button ID="RegisterButton" class="button is-ghost" Text="Registrarme" runat="server" OnClick="SignupButton_Click" />
				</div>
			</div>
		</div>
	</div>
	<div class="message is-danger has-text-centered">
		<asp:Label ID="Label_MensajeError" runat="server" Visible="false">El usuario que introdujo no es valido, intente nuevamente</asp:Label>
	</div>
</asp:Content>
