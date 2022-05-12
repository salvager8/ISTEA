package mx.com.ejemplofragmentos02

import android.content.res.Configuration
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ImageView

class DetallesActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_detalles)

        if (resources.configuration.orientation == Configuration.ORIENTATION_LANDSCAPE) {
            finish()
            return
        }

        if (savedInstanceState == null) {
            val fragDetalles = ContenidoPeliculas()
            fragDetalles.arguments = intent.extras
            supportFragmentManager.beginTransaction().add(R.id.container, fragDetalles).commit()
        }

        /*
        val index = intent.getIntExtra("INDEX", 0)

        val foto = findViewById<ImageView>(R.id.imageViewFoto)

        foto.setImageResource(ListaPeliculas.peliculas?.get(index)?.imagen!!)

         */
    }
}