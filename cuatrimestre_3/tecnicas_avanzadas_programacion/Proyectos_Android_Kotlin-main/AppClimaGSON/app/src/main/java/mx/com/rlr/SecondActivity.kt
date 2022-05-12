package mx.com.rlr

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.TextView
import android.widget.Toast
import com.android.volley.Request
import com.android.volley.Response
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley
import com.google.gson.Gson

class SecondActivity : AppCompatActivity() {

    var tViewGrados:TextView? = null
    var tViewCity:TextView? = null
    var tViewInfo:TextView? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_second)

        tViewGrados = findViewById<TextView>(R.id.textViewGrados)
        tViewCity = findViewById<TextView>(R.id.textViewCity)
        tViewInfo = findViewById<TextView>(R.id.textViewInfo)

        val ciudad = intent.getStringExtra("mx.com.rlr.ciudades.CIUDAD")

        if (Network.hayRed(this)) {
            //Ejecutar solicitud HTTP
            solicitudHTTPVolley("http://api.openweathermap.org/data/2.5/weather?id="+ciudad+"&appid=754de82f96978f5d049814acc15e67c6&units=metric&lang=es")
            //754de82f96978f5d049814acc15e67c6
            //Mexico city: 3530597
        } else {
            //Mostrar mensaje de error
            Toast.makeText(this, "Error!", Toast.LENGTH_LONG).show()
        }
        /*
        val ciudadCdmx = Ciudad("Ciudad de México", 27, "Soleado")
        val ciudadBerlin = Ciudad("Berlín", 9, "Cielo nublado")

        if (ciudad == "ciudad-mexico") {
            //Se muestra la informacion de ciudad de méxico
            tViewGrados?.text = ciudadCdmx.grados.toString() + "°"
            tViewCity?.text = ciudadCdmx.nombre
            tViewInfo?.text = ciudadCdmx.infoClima

        } else if (ciudad == "ciudad-berlin") {
            //Se muestra la informacion de ciudad de berlín
            tViewGrados?.text = ciudadBerlin.grados.toString() + "°"
            tViewCity?.text = ciudadBerlin.nombre
            tViewInfo?.text = ciudadBerlin.infoClima
        } else {
            Toast.makeText(this, "Error!", Toast.LENGTH_LONG).show()
        }*/

    }

    //Método para HTTP con Volley
    private fun solicitudHTTPVolley(url:String) {
        val queue = Volley.newRequestQueue(this)

        val solicitud = StringRequest(Request.Method.GET, url,{
                response ->
            try {
                Log.d("solicitudHTTPVolley", response)

                val gson = Gson()
                val ciudad = gson.fromJson(response, Ciudad::class.java)
                //Se muestra la informacion de ciudad de méxico
                tViewGrados?.text = ciudad.main?.temp.toString() + "°"
                tViewCity?.text = ciudad.name
                tViewInfo?.text = ciudad.weather?.get(0)?.description

            } catch (e:Exception) {

            }
        }, Response.ErrorListener {  })

        queue.add(solicitud)
    }
}