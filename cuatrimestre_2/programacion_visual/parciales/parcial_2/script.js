const validarEmail = email => {
	const regExp = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
	return regExp.test(String(email).toLowerCase());
}

const validarContrasena = contrasena => {
	if (isNaN(contrasena)) {
		return false;
	}

	const longitudContrasena = contrasena.toString().length;
	if (longitudContrasena > 16 || longitudContrasena < 8) {
		return false;
	}
	return true;
}

const obtenerInformacion = id => {
	const xhttp = new XMLHttpRequest();
	xhttp.onreadystatechange = function() {
		if (this.readyState == 4 && this.status == 200) {
			let response = JSON.parse(this.responseText);

			$("#name").html(response.name);
			$("#height").text(response.height);
			$("#mass").text(response.mass);
			$("#hair_color").text(response.hair_color);
			$("#skin_color").text(response.skin_color);
			$("#eye_color").text(response.eye_color);
			$("#birth_year").text(response.birth_year);
			$("#gender").text(response.gender);
		}
	};
	xhttp.open("GET", `https://swapi.dev/api/people/${id}`, true);
	xhttp.send();
}

$(document).ready(() => {
	$("#login-form").submit(evento => {
		evento.preventDefault();
		$("#login-error-user").hide();
		$("#login-error-pass").hide();
		$("#login-error-submit").hide();
		const user = $("#login-user").val();
		const contrasena = $("#login-pass").val();

		const esEmailValido = validarEmail(user);
		const esContrasenaValida = validarContrasena(contrasena);
		if (!esEmailValido || !esContrasenaValida) {
			if (!esEmailValido) {
				$("#login-error-user").show();
			}
			if (!esContrasenaValida) {
				$("#login-error-pass").show();
			}
			return;
		}

		if (user == "juanperez@gmail.com" && contrasena == 1787742836863899) {
			$("#login-error-user").hide();
			$("#login-error-pass").hide();
			$("#login-error-submit").hide();
			$("#login").hide();
			$("#content").show();
		} else {
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
})