package mx.com.rlr

import android.content.Context
import android.content.Intent
import android.text.TextUtils
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.foursquare.android.nativeoauth.*
import com.foursquare.android.nativeoauth.model.AccessTokenResponse
import com.foursquare.android.nativeoauth.model.AuthCodeResponse

class Foursquare(var activity: AppCompatActivity) {

    init {

    }

    fun iniciarSesion() {
        val intent = FoursquareOAuth.getConnectIntent(activity.applicationContext, CLIENT_ID)

        if (FoursquareOAuth.isPlayStoreIntent(intent)) {
            //Mostrar mensaje de que no tiene instalada la app
            toastMessage(activity.applicationContext, "No tienes la app instalada de Foursquare...")
            activity.startActivity(intent)
        } else {
            activity.startActivityForResult(intent, CODIGO_CONEXION)
        }
    }

    fun validarActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        when(requestCode) {
            CODIGO_CONEXION -> conexionCompleta(resultCode, data)

            CODIGO_INTERCAMBIO_TOKEN -> intercambioTokenCompleta(resultCode, data)
        }
    }

    fun conexionCompleta(resultCode: Int, data: Intent?) {
        val codigoRespuesta = FoursquareOAuth.getAuthCodeFromResult(resultCode, data)
        val exception = codigoRespuesta.exception

        if (exception == null) {
            //La autenticancion fue exitosa
            val codigo = codigoRespuesta.code
            realizarIntercambioToken(codigo)
        } else {
            if (exception is FoursquareCancelException) { // Cancel.
                toastMessage(activity.applicationContext, "Cancelado")
            } else if (exception is FoursquareDenyException) { // Deny.
                toastMessage(activity.applicationContext, "Denegado")
            } else if (exception is FoursquareOAuthException) { // OAuth error.
                val errorMessage = exception.message
                val errorCode = exception.errorCode
                toastMessage(activity.applicationContext, "$errorMessage [$errorCode]")
            } else if (exception is FoursquareUnsupportedVersionException) { // Unsupported Fourquare app version on the device.
                toastError(activity.applicationContext, exception)
            } else if (exception is FoursquareInvalidRequestException) { // Invalid request.
                toastError(activity.applicationContext, exception)
            } else { // Error.
                toastError(activity.applicationContext, exception)
            }
        }
    }

    private fun realizarIntercambioToken(codigo:String) {
        val intent = FoursquareOAuth.getTokenExchangeIntent(activity.applicationContext, CLIENT_ID, CLIENT_SECRET, codigo)
        activity.startActivityForResult(intent, CODIGO_INTERCAMBIO_TOKEN)
    }

    private fun intercambioTokenCompleta(resultCode: Int, data: Intent?) {
        val respuestaToken = FoursquareOAuth.getTokenFromResult(resultCode, data)
        val exception = respuestaToken.exception

        if (exception == null) {
            val accessToken = respuestaToken.accessToken

            //La autenticancion fue exitosa
            toastMessage(activity.applicationContext, "Access token: $accessToken")

            if(!guardarToken(accessToken)) toastMessage(activity.applicationContext, "Error al guardar el Token")
            else navegarSiguienteActividad()

        } else {
            if (exception is FoursquareOAuthException) { // OAuth error.
                val errorMessage = exception.message
                val errorCode = exception.errorCode
                toastMessage(activity.applicationContext, "$errorMessage [$errorCode]")
            } else { // Other exception type.
                toastError(activity.applicationContext, exception)
            }
        }
    }

    fun hayToken():Boolean {
        return getToken()!=""
    }

    fun getToken():String?{
        val settings=activity.getSharedPreferences(SETTINGS, 0)
        val token=settings.getString(ACCESS_TOKEN, "")
        return token
    }

    //Guardar el Token utilizando SharedPrefrences
    private fun guardarToken(token:String):Boolean {
        if (token.isEmpty()){
            return false
        }
        val settings=activity.getSharedPreferences(SETTINGS, 0)
        val editor=settings.edit()
        editor.putString(ACCESS_TOKEN, token)

        editor.apply()
        return true
    }

    fun navegarSiguienteActividad() {
        activity.startActivity(Intent(this.activity, SecondActivity::class.java))
        activity.finish()
    }

    fun mensaje(mensaje:String) {
        Toast.makeText(activity.applicationContext, mensaje, Toast.LENGTH_LONG).show()
    }

    companion object {
        private const val CODIGO_CONEXION = 200
        private const val CODIGO_INTERCAMBIO_TOKEN = 201

        private val SETTINGS = "settings"
        private val ACCESS_TOKEN="accessToken"
        /**
         * Obtain your client id and secret from:
         * https://foursquare.com/developers/apps
         */
        //Obtenido de la consola de desarrollador del api de Foursquare
        private const val CLIENT_ID = "VORSML0I5SGYX5VB3B05DOCLL2O22NFQU2NYX0AJURPGFGFD"
        private const val CLIENT_SECRET = "U4A2GTKKW44E0SSNJVGMHKDOEFAQN0IORDCVS1H5EGEBDTW0"
        fun toastMessage(context: Context?, message: String?) {
            Toast.makeText(context, message, Toast.LENGTH_SHORT).show()
        }

        fun toastError(context: Context?, t: Throwable) {
            Toast.makeText(context, t.message, Toast.LENGTH_SHORT).show()
        }
    }

}