package mx.com.appcheckins

import android.content.Context
import android.widget.Toast

class Mensaje {

    companion object {

        fun mensaje(context: Context, mensaje:  Mensajes) {
            var str = ""
            when(mensaje) {
                Mensajes.RATIONALE -> {
                    str = "Requiero permisos para obtener ubicacion"
                }
            }
        }

        fun mensajeError(context: Context, error: Errores) {
            var mensaje = ""

            when (error) {
                Errores.NO_HAY_RED -> {
                    mensaje = "No se detecta ninguna conexion"
                }

                Errores.HTTP_ERROR -> {
                    mensaje = "Hubo un problema en la solicitud HTTP"
                }

                Errores.PERMISO_NEGADO -> {
                    mensaje = "No diste los permisos para tu ubicación"
                }

                Errores.ERROR_QUERY-> {
                    mensaje = "Hubo un problema en la solicitud a la API"
                }
            }

            Toast.makeText(context, mensaje, Toast.LENGTH_SHORT).show()
        }

        fun mensajeError(context: Context, error: String) {
            Toast.makeText(context, error, Toast.LENGTH_SHORT).show()
        }
    }

}