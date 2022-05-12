package mx.com.appcheckins

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.TextView
import com.google.gson.Gson

class DetallesVenue : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_detalles_venue)

        val textViewNombre = findViewById<TextView>(R.id.textViewNombre)
        val textViewState = findViewById<TextView>(R.id.textViewState)
        val textViewCountry = findViewById<TextView>(R.id.textViewCountry)
        val textViewCategory = findViewById<TextView>(R.id.textViewCategory)
        val textViewVerified = findViewById<TextView>(R.id.textViewVerified)
        val textViewhereNow = findViewById<TextView>(R.id.textViewhereNow)

        val venuesActualString = intent.getStringExtra(PantallaPrincipal.VENUE_ACTUAL)

        val gson = Gson()
        val venueActual = gson.fromJson(venuesActualString, Venue::class.java)

        //Log.d("venueActual", venueActual.name)

        textViewNombre.text = venueActual.name
        textViewState.text = venueActual.location?.state
        textViewCountry.text = venueActual.location?.country
        textViewCategory.text = venueActual.categories?.get(0)?.name
        textViewVerified.text = venueActual.verified.toString()
        textViewhereNow.text = venueActual.hereNow?.count.toString()

    }
}