package mx.com.appcheckins

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.Toast

class Login : AppCompatActivity() {

    var foursquare:Foursquare? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        setContentView(R.layout.activity_login)

        val btnLogin = findViewById<Button>(R.id.btnLogin)
        foursquare = Foursquare(this, PantallaPrincipal())

        btnLogin.setOnClickListener {
            foursquare?.navegarSiguienteActividad()
        }

    }
}