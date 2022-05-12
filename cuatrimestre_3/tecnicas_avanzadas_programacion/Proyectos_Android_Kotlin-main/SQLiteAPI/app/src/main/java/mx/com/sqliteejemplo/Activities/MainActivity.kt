package mx.com.sqliteejemplo.Activities

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.google.android.material.floatingactionbutton.FloatingActionButton
import com.google.gson.Gson
import mx.com.sqliteejemplo.Modelo.Alumno
import mx.com.sqliteejemplo.Modelo.Alumnos
import mx.com.sqliteejemplo.R
import mx.com.sqliteejemplo.RecyclerView.AdaptadorCustom
import mx.com.sqliteejemplo.RecyclerView.ClickListener
import mx.com.sqliteejemplo.RecyclerView.LongClickListener
import mx.com.sqliteejemplo.SQLite.AlumnoCRUD
import mx.com.sqliteejemplo.Utilities.HttpResponse
import mx.com.sqliteejemplo.Utilities.Network

class MainActivity : AppCompatActivity() {

    var lista:RecyclerView? = null
    var adaptador: AdaptadorCustom? = null
    var layoutManager:RecyclerView.LayoutManager? = null

    var alumnos:ArrayList<Alumno>? = null
    var crud: AlumnoCRUD? =  null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val fab = findViewById<FloatingActionButton>(R.id.fab)

        lista = findViewById(R.id.recycletViewLista)
        lista?.setHasFixedSize(true)

        layoutManager = LinearLayoutManager(this)
        lista?.layoutManager = layoutManager

        fab.setOnClickListener {
            startActivity(Intent(this, NuevoAlumno::class.java))
        }

        //Implementar solicitud HTTP
        val network = Network(this)
        val activity = this.applicationContext
        val gson = Gson()

        crud = AlumnoCRUD(this)
        alumnos = crud?.getAlumnos()

        //"http://192.168.0.22:80/alumnos/"

        network.httpRequest(
            activity,
            "http://127.0.0.1:80/alumnos/",
            object: HttpResponse {
                override fun httpResponseSuccess(response: String) {
                    Log.d("response", response)

                    val alumnosAPI = gson.fromJson(response, Alumnos::class.java).items

                    for (alumno in alumnos!!){
                        crud?.deleteAlumno(alumno)
                    }
                    for ( alumno in alumnosAPI!! ){
                        crud?.nuevoAlumno(Alumno(alumno.id!!, alumno.nombre!!))
                    }

                    alumnos = crud?.getAlumnos()
                    configurarAdaptador(alumnos!!)
                }

                override fun httpErrorResponse(response: String) {
                    Toast.makeText(activity, "Error al hacer la solicitud HTTP", Toast.LENGTH_SHORT).show()
                    configurarAdaptador(alumnos!!)
                }

            }
        )

    }

    fun configurarAdaptador(data: ArrayList<Alumno>){
        this.adaptador = AdaptadorCustom(data!!, object : ClickListener {
            override fun onClick(vista: View, index: Int) {
                //click
                val intent = Intent(applicationContext, DetalleAlumno::class.java)
                intent.putExtra("ID", data!!.get(index).id)
                startActivity(intent)
            }
        }, object : LongClickListener {
            override fun longClick(vista: View, index: Int) {}
        })

        this.lista?.adapter = this.adaptador
    }

}