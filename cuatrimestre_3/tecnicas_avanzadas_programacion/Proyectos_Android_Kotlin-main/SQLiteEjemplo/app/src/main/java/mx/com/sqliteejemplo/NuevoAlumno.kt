package mx.com.sqliteejemplo

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText

class NuevoAlumno : AppCompatActivity() {

    var crud:AlumnoCRUD?=  null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_nuevo_alumno)

        val id = findViewById<EditText>(R.id.editTextId)
        val nombre = findViewById<EditText>(R.id.editTextNombre)
        val btnNuevoAlumno = findViewById<Button>(R.id.btnNuevoAlumno)

        crud = AlumnoCRUD(this)

        btnNuevoAlumno.setOnClickListener {
            crud?.nuevoAlumno(Alumno(id.text.toString(), nombre.text.toString()))
            startActivity(Intent(this, MainActivity::class.java))
        }

    }
}