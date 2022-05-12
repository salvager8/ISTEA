package mx.com.sqliteejemplo.Activities

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import mx.com.sqliteejemplo.Modelo.Alumno
import mx.com.sqliteejemplo.R
import mx.com.sqliteejemplo.SQLite.AlumnoCRUD

class DetalleAlumno : AppCompatActivity() {

    var crud: AlumnoCRUD?=  null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_detalle_alumno)

        val id = findViewById<EditText>(R.id.editTextId)
        val nombre = findViewById<EditText>(R.id.editTextNombre)
        val btnActualizar = findViewById<Button>(R.id.btnActualizar)
        val btnEliminar = findViewById<Button>(R.id.btnEliminar)

        val index = intent.getStringExtra("ID")

        crud = AlumnoCRUD(this)

        val alumno = crud?.getAlumno(index)



        id.setText(alumno?.id, TextView.BufferType.EDITABLE)
        nombre.setText(alumno?.nombre, TextView.BufferType.EDITABLE)

        btnActualizar.setOnClickListener {
            crud?.updateAlumno(Alumno(id.text.toString(), nombre.text.toString()))
            startActivity(Intent(this, MainActivity::class.java))
        }

        btnEliminar.setOnClickListener {
            crud?.deleteAlumno(Alumno(id.text.toString(), nombre.text.toString()))
            startActivity(Intent(this, MainActivity::class.java))
        }

    }
}