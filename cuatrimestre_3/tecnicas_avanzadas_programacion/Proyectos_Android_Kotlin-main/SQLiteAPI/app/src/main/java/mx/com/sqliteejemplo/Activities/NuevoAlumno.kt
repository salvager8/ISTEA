package mx.com.sqliteejemplo.Activities

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import com.google.gson.Gson
import mx.com.sqliteejemplo.Modelo.Alumno
import mx.com.sqliteejemplo.Modelo.HttpAPIResponse
import mx.com.sqliteejemplo.R
import mx.com.sqliteejemplo.SQLite.AlumnoCRUD
import mx.com.sqliteejemplo.Utilities.HttpResponse
import mx.com.sqliteejemplo.Utilities.Network

class NuevoAlumno : AppCompatActivity() {

    var crud: AlumnoCRUD?=  null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_nuevo_alumno)

        val id = findViewById<EditText>(R.id.editTextId)
        val nombre = findViewById<EditText>(R.id.editTextNombre)
        val btnNuevoAlumno = findViewById<Button>(R.id.btnNuevoAlumno)

        crud = AlumnoCRUD(this)

        btnNuevoAlumno.setOnClickListener {
            //crud?.nuevoAlumno(Alumno(id.text.toString(), nombre.text.toString()))
            //startActivity(Intent(this, MainActivity::class.java))

            //TODO: 4. Sustituir crud con operacion HTTP
            val context = this.applicationContext
            var network = Network(this);
            val query = "?id=" + id.text.toString() + "&nombre=" + nombre.text.toString()
            var url = "http://127.0.0.1:80/alumnos/nuevoalumno$query"

            network.httpRequest(context, url, object : HttpResponse {

                override fun httpResponseSuccess(response: String) {
                    val gson = Gson()
                    val message = gson.fromJson(response, HttpAPIResponse::class.java)
                    Toast.makeText(context, message.response, Toast.LENGTH_SHORT).show()
                    startActivity(Intent(context, MainActivity::class.java))
                }

                override fun httpErrorResponse(response: String) {
                    Log.e("error response", response)
                    Toast.makeText(context, "Hubo un problema al enviar la solicitud", Toast.LENGTH_SHORT).show()
                }

            })
        }

    }
}