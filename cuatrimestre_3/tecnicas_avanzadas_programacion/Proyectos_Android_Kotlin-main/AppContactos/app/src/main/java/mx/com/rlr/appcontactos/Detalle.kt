package mx.com.rlr.appcontactos

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.Menu
import android.view.MenuItem
import android.widget.ImageView
import android.widget.TextView
import androidx.appcompat.widget.Toolbar

class Detalle : AppCompatActivity() {

    var index:Int = 0

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_detalle)

        val toolbar = findViewById<Toolbar>(R.id.toolbar)
        setSupportActionBar(toolbar)

        var actionBar = supportActionBar
        actionBar?.setDisplayHomeAsUpEnabled(true)

        index = intent.getStringExtra("ID").toInt()
        //Log.d("INDEX", index.toString())

        mapearDatos()
    }

    fun mapearDatos() {
        val contacto = MainActivity.obtenerContacto(index)

        var tvNombre = findViewById<TextView>(R.id.textViewName)
        var tvEmpresa = findViewById<TextView>(R.id.textViewCompany)
        var tvEdad = findViewById<TextView>(R.id.textViewEdad)
        var tvPeso = findViewById<TextView>(R.id.textViewPeso)
        var tvTelefono = findViewById<TextView>(R.id.textViewTelefono)
        var tvEmail = findViewById<TextView>(R.id.textViewEmail)
        var tvDireccion = findViewById<TextView>(R.id.textViewDireccion)
        val ivFoto = findViewById<ImageView>(R.id.imageViewFoto)

        tvNombre.text = contacto.nombre + " " + contacto.apellidos
        tvEmpresa.text = contacto.empresa
        tvEdad.text = contacto.edad.toString() + " años"
        tvPeso.text = contacto.peso.toString() + " kg"
        tvTelefono.text = contacto.telefono
        tvEmail.text = contacto.email
        tvDireccion.text = contacto.direccion
        ivFoto.setImageResource(contacto.foto)
    }

    override fun onCreateOptionsMenu(menu: Menu?): Boolean {
        menuInflater.inflate(R.menu.menu_detalle, menu)
        return super.onCreateOptionsMenu(menu)
    }

    override fun onOptionsItemSelected(item: MenuItem): Boolean {
        when(item?.itemId) {

            //Configuracion del boton hacia atras para persistir los datos
            android.R.id.home -> {
                finish()
                return true
            }

            R.id.itemEliminar -> {
                MainActivity.eliminarContacto(index)
                finish()
                return true
            }

            R.id.itemEditar -> {
                val intent = Intent(this, NuevoContacto::class.java)
                intent.putExtra("ID", index.toString())
                startActivity(intent)
                return true
            }

            else -> {
                return super.onOptionsItemSelected(item)
            }
        }
    }

    override fun onResume() {
        super.onResume()
        mapearDatos()
    }
}