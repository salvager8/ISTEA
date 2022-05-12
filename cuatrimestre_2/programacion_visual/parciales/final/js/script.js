let usuarios;
let imagenActual;
let intentosFallidos = 0;

window.setInterval(() => {
	let today = new Date();
	const date = today.getDate()+'-'+(today.getMonth()+1)+'-'+today.getFullYear();
	const time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
	$("#current-time").text(`Es el dia ${date}, y son las ${time}`);
}, 1000);

const validarEmail = email => {
	const regExp = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
	return regExp.test(String(email).toLowerCase());
}

const validacionClave = id => {
	var expresionRegular = /^\d{1,2}$/;
	if (!expresionRegular.test(id)) {
	//retorno falso si la clave es invalida
	return false;
	}
	//retorno verdadero si la clave es valida
	return true;
}

const obtenerInformacion = id => {
	if (id < 1 || id > 826) {
		$("#error-id").show();
		return;
	}
	$("#error-id").hide();
	const xhttp = new XMLHttpRequest();
	xhttp.onreadystatechange = function() {
		if (this.readyState == 4 && this.status == 200) {
			let response = JSON.parse(this.responseText);

			$("#id").html(response.id);
			$("#name").text(response.name);
			$("#status").text(response.status);
			$("#species").text(response.species);
			$("#type").text(response.type);
			$("#gender").text(response.gender);
			imagenActual = response.image;
		}
	};
	xhttp.open("GET", `https://rickandmortyapi.com/api/character/${id}`, true);
	xhttp.send();
}

const obtenerUsuarios = async _ => {
	const xhttp = new XMLHttpRequest();
	xhttp.onreadystatechange = function() {
		if (this.readyState == 4 && this.status == 200) {
			let response = JSON.parse(this.responseText);
			usuarios = response;
			return usuarios
		}
	};
	xhttp.open("GET", `https://jsonplaceholder.typicode.com/users`, false);
	xhttp.send();
}

const cerrarSesion = _ => {
	$("#login").show();
	$("#content").hide();
	$("#login-user").val("");
	$("#login-pass").val("");
	$("#actual-image").html("");
	$("#id").html("");
	$("#name").text("");
	$("#status").text("");
	$("#species").text("");
	$("#type").text("");
	$("#gender").text("");
	$("#id-character").val("");
	$("error-id").hide();
}

const validarUsuario = (user, contrasena) => {
	for (const usuario of usuarios) {
		if (user == usuario.email && contrasena == usuario.id) {
			$("#login-error-user").hide();
			$("#login-error-pass").hide();
			$("#login-error-submit").hide();
			$("#login").hide();
			$("#content").show();
			$("#logged-user").text(usuario.name);
			intentosFallidos = 0;
			return true;
		}
	}
	intentosFallidos++;
	if (intentosFallidos >= 3) {
		cerrarSesion();
		$("#login").hide();
		$("#locked").show();
	}
	return false;
}

$(document).ready(() => {
	$("#login-form").submit(evento => {
		obtenerUsuarios();

		evento.preventDefault();
		$("#login-error-user").hide();
		$("#login-error-pass").hide();
		$("#login-error-submit").hide();
		const user = $("#login-user").val();
		const contrasena = $("#login-pass").val();

		const esEmailValido = validarEmail(user);
		const esContrasenaValida = validacionClave(contrasena);
		if (!esEmailValido || !esContrasenaValida) {
			if (!esEmailValido) {
				$("#login-error-user").show();
			}
			if (!esContrasenaValida) {
				$("#login-error-pass").show();
			}
			return;
		}

		if (!validarUsuario(user, contrasena)) {
			$("#login-error-user").hide();
			$("#login-error-pass").hide();
			$("#login-error-submit").show();
		}
	})

	$("#id-character").on('keypress', evento => {
		if(evento.which == 13) {
			obtenerInformacion($("#id-character").val());
		}
	})

	$("#log-out").click(() => {
		cerrarSesion();
	})

	$("#name").click(() => {
		if (!imagenActual) {
			return;
		}
		$("#actual-image").html("");
		$("#actual-image").prepend(`<img src="${imagenActual}" />`)
	})
})