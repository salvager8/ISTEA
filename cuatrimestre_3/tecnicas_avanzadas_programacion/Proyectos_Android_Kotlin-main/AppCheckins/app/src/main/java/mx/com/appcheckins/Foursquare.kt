package mx.com.appcheckins

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import com.google.gson.Gson

class Foursquare(var activity:AppCompatActivity, var secondActivity: AppCompatActivity) {

    private val CLIENT_ID = "VORSML0I5SGYX5VB3B05DOCLL2O22NFQU2NYX0AJURPGFGFD"
    private val CLIENT_SECRET = "U4A2GTKKW44E0SSNJVGMHKDOEFAQN0IORDCVS1H5EGEBDTW0"

    private val URL_BASE = "https://api.foursquare.com/v2/"
    private val VERSION = "v=20180323"

    fun navegarSiguienteActividad(){
        activity.startActivity(Intent(this.activity, secondActivity::class.java))
        activity.finish()
    }

    fun obtenerVenues(lat:String, lon:String, obtenerVenuesInterface: ObtenerVenuesInterface) {
        val network = Network(activity)
        val seccion = "venues/"
        val metodo = "search/"
        val ll = "ll=$lat,$lon"
        val keys = "client_id=$CLIENT_ID&client_secret=$CLIENT_SECRET"
        val url = "$URL_BASE$seccion$metodo?$ll&$keys&$VERSION"

        network.httpRequest(activity.applicationContext, url, object:HttpResponse {
            override fun httpResponseSuccess(response: String) {
                val gson = Gson()
                val objetoRespuesta = gson.fromJson(response, FoursquareAPIRequestVenues::class.java)

                val meta = objetoRespuesta.meta
                val venues = objetoRespuesta.response?.venues!!

                if (meta?.code == 200) {
                    //Mandar un mensaje de que el query se ejecuto correctamente
                    obtenerVenuesInterface.venuesGenerados(venues)
                } else {
                    if (meta?.code == 400) {
                        //Mandar un mensaje de que ocurrio un problema
                        Mensaje.mensajeError(activity.applicationContext, meta?.errorDetail)
                    } else {
                        //Mostrar un mensaje generico
                        Mensaje.mensajeError(activity.applicationContext, Errores.ERROR_QUERY)
                    }
                }
            }

        })
    }
}