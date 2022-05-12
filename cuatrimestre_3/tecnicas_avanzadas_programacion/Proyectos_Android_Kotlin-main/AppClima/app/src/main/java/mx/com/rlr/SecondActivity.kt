package mx.com.rlr

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.TextView
import android.widget.Toast

class SecondActivity : AppCompatActivity() {

    /*var tViewGrados:TextView? = null
    var tViewCity:TextView? = null
    var tViewInfo:TextView? = null */

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_second)

        val tViewGrados = findViewById<TextView>(R.id.textViewGrados)
        val tViewCity = findViewById<TextView>(R.id.textViewCity)
        val tViewInfo = findViewById<TextView>(R.id.textViewInfo)

        val ciudad = intent.getStringExtra("mx.com.rlr.ciudades.CIUDAD")

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
        }

    }
}