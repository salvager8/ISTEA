package mx.com.ejemplofragmentos

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.TextView

class MainActivity : AppCompatActivity(), MyFragment.NombreListener {

    var textViewName:TextView? = null
    var btnaddFrag:Button? = null
    var btnReplace:Button? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        textViewName = findViewById(R.id.textViewName)
        btnaddFrag = findViewById(R.id.btnAddFrag)
        btnReplace = findViewById(R.id.btnReplace)

        val fragmentManager = supportFragmentManager

        btnaddFrag?.setOnClickListener {
            val fragmentTransaction = fragmentManager.beginTransaction()

            val newFragment = MyFragment()
            fragmentTransaction.add(R.id.linearLayoutMain, newFragment)

            //funcion para regresar con el boton hacia atras y volver al activity sin el fragment
            fragmentTransaction.addToBackStack(null)
            fragmentTransaction.commit()
        }

        btnReplace?.setOnClickListener {
            val fragmentTransaction = fragmentManager.beginTransaction()

            //Fragmento que reemplaza al otro fragmento
            val newFragment = SecondFragment()
            fragmentTransaction.replace(R.id.linearLayoutMain, newFragment)

            //funcion para regresar con el boton hacia atras y volver al activity sin el fragment
            fragmentTransaction.addToBackStack(null)
            fragmentTransaction.commit()
        }

    }

    override fun obtenerNombre(nombre: String) {
        textViewName?.text = nombre
    }
}