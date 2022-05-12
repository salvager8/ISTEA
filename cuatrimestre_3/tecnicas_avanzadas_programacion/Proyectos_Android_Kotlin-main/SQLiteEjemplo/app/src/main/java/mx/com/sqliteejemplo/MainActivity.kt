package mx.com.sqliteejemplo

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.google.android.material.floatingactionbutton.FloatingActionButton

class MainActivity : AppCompatActivity() {

    var lista:RecyclerView? = null
    var adaptador:AdaptadorCustom? = null
    var layoutManager:RecyclerView.LayoutManager? = null

    var alumnos:ArrayList<Alumno>? = null
    var crud:AlumnoCRUD? =  null

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

        crud = AlumnoCRUD(this)

        alumnos = crud?.getAlumnos()

        adaptador = AdaptadorCustom(alumnos!!, object:ClickListener{
            override fun onClick(vista: View, index: Int) {
                //click
                val intent = Intent(applicationContext, DetalleAlumno::class.java)
                intent.putExtra("ID", alumnos?.get(index)?.id)
                startActivity(intent)
            }

        }, object: LongClickListener{
            override fun longClick(vista: View, index: Int) {

            }

        })
        lista?.adapter = adaptador

    }

}