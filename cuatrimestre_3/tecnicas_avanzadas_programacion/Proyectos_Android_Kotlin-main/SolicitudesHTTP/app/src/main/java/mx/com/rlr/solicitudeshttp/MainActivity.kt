package mx.com.rlr.solicitudeshttp

import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.android.volley.Request
import com.android.volley.Response
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley
import okhttp3.Call
import okhttp3.OkHttpClient
import org.json.JSONObject
import java.io.IOException


class MainActivity : AppCompatActivity(), CompletadoListener {
    private var precioDolar = 0

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val btnValidarRed = findViewById<Button>(R.id.btnValidarRed)
        val btnSolicitudHTTP = findViewById<Button>(R.id.btnSolicitudHTTP)
        val btnVolley = findViewById<Button>(R.id.btnVolley)
        val btnOkHTTP = findViewById<Button>(R.id.btnOkHTTP)

        obtenerPrecioDolar()

        btnValidarRed.setOnClickListener {
            // Código para validar RED
            if (Network.hayRed(this)) {
                Toast.makeText(this, "Si hay red!", Toast.LENGTH_LONG).show()
            } else {
                Toast.makeText(this, "No estas conectado a Internet!", Toast.LENGTH_LONG).show()
            }
        }

        //Método para HTTP nativo
        btnSolicitudHTTP.setOnClickListener {
            // Código para validar RED
            if (Network.hayRed(this)) {
                //Log.d("btnSolicitudOnClick", descargarDatos("http://www.google.com"))

                DescargaURL(this).execute("http:www.google.com")

            } else {
                Toast.makeText(this, "No estas conectado a Internet!", Toast.LENGTH_LONG).show()
            }
        }

        //Método para HTTP con Volley
        btnVolley.setOnClickListener {
            // Código para validar RED
            if (Network.hayRed(this)) {
                solicitudHTTPVolley("https://api-dolar-argentina.herokuapp.com/api/dolarblue")

            } else {
                Toast.makeText(this, "No estas conectado a Internet!", Toast.LENGTH_LONG).show()
            }
        }
        //Método para HTTP con OkHTTP
        btnOkHTTP.setOnClickListener {
            // Código para validar RED
            if (Network.hayRed(this)) {
                solicitudOkHTTP("http://www.google.com")

            } else {
                Toast.makeText(this, "No estas conectado a Internet!", Toast.LENGTH_LONG).show()
            }
        }

    }

    private fun obtenerPrecioDolar() {
        val inputValorPeso = findViewById<EditText>(R.id.valorPeso)

        val url = "https://api-dolar-argentina.herokuapp.com/api/dolarblue"

        val queue = Volley.newRequestQueue(this)

        val solicitud = StringRequest(Request.Method.GET, url,{
                respuesta ->
            try {
                val jsonObject = JSONObject(respuesta)
                val precioDolar = jsonObject.get("venta").toString()
                val valorDolar = precioDolar
                inputValorPeso.setText(valorDolar)
                this.precioDolar = precioDolar.toInt()
            } catch (e:Exception) {

            }
        }, Response.ErrorListener {  })

        queue.add(solicitud)
    }

    override fun descargaCompleta(resultado: String) {
        Log.d("descargaCompleta", resultado)
    }

    //Método para HTTP con Volley
    private fun solicitudHTTPVolley(url:String) {
        val inputValorPeso = findViewById<EditText>(R.id.valorPeso)

        val queue = Volley.newRequestQueue(this)

        val solicitud = StringRequest(Request.Method.GET, url,{
            response ->
            try {
                val jsonObject = JSONObject(response)
                val valorDolar = jsonObject.get("venta").toString()
                inputValorPeso.setText(valorDolar)
            } catch (e:Exception) {

            }
        }, Response.ErrorListener {  })

        queue.add(solicitud)
    }

    //Método para HTTP con OkHTTP
    private fun solicitudOkHTTP(url:String) {
        val cliente = OkHttpClient()
        val solicitud = okhttp3.Request.Builder().url(url).build()

        cliente.newCall(solicitud).enqueue(object: okhttp3.Callback{
            override fun onFailure(call: Call, e: IOException) {
                //implementar un posible error
            }

            override fun onResponse(call: Call, response: okhttp3.Response) {
                val resultado = response.body?.string()

                this@MainActivity.runOnUiThread {
                    try {
                        Log.d("solicitudOkHTTP", resultado)
                    } catch (e:Exception) {

                    }
                }
            }
        })
    }

}