package mx.com.appcheckins

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.google.android.gms.location.LocationResult
import com.google.gson.Gson
import mx.com.appcheckins.RecyclerViewPrincipal.AdaptadorCustom
import mx.com.appcheckins.RecyclerViewPrincipal.ClickListener
import mx.com.appcheckins.RecyclerViewPrincipal.LongClickListener

class PantallaPrincipal : AppCompatActivity() {

    var ubicacion:Ubicacion? = null
    var foursquare:Foursquare? = null

    //RecyclerView
    var lista: RecyclerView? = null
    var adaptador: AdaptadorCustom? = null
    var layoutManager: RecyclerView.LayoutManager? = null

    companion object {
        val VENUE_ACTUAL = "appcheckins.PantallaPrincipal"
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_pantalla_principal)

        foursquare = Foursquare(this, this)

        lista = findViewById(R.id.rvListaLugares)
        lista?.setHasFixedSize(true)

        layoutManager = LinearLayoutManager(this)
        lista?.layoutManager = layoutManager

        ubicacion = Ubicacion(this, object: UbicacionListener {
            override fun ubicacionResponse(locationResult: LocationResult) {
                val lat = locationResult.lastLocation.latitude.toString()
                val lon = locationResult.lastLocation.longitude.toString()
                //Toast.makeText(applicationContext, locationResult.lastLocation.latitude.toString(), Toast.LENGTH_SHORT).show()
                foursquare?.obtenerVenues(lat, lon, object: ObtenerVenuesInterface {
                    override fun venuesGenerados(venues: ArrayList<Venue>) {

                        implementacionRecyclerView(venues)

                        for (venue in venues) {
                            Log.d("VENUE", venue.name)
                        }
                    }

                })
            }

        })
    }

    private fun implementacionRecyclerView(lugares:ArrayList<Venue>) {
        adaptador = AdaptadorCustom(lugares, object: ClickListener {
            override fun onClick(vista: View, index: Int) {
                //Toast.makeText(applicationContext, lugares.get(index).name, Toast.LENGTH_SHORT).show()
                val venueToJson = Gson()
                val venueActualString = venueToJson.toJson(lugares.get(index))
                //Log.d("venueActualString", venueActualString)
                val intent = Intent(applicationContext, DetallesVenue::class.java)
                intent.putExtra(VENUE_ACTUAL, venueActualString)
                startActivity(intent)
            }

        }, object: LongClickListener {
            override fun longClick(vista: View, index: Int) {
                //Validar el ActionMode
                /*if (!isActionMode) {
                    startSupportActionMode(callback)
                    isActionMode = true
                    adaptador?.seleccionarItem(index)
                } else {
                    //Hacer selecciones o deselecciones
                    adaptador?.seleccionarItem(index)
                }
                actionMode?.title = adaptador?.obtenerNumeroElementosSeleccionados().toString() + " seleccionados" */
            }

        })
        lista?.adapter = adaptador
    }

    override fun onRequestPermissionsResult(
        requestCode: Int,
        permissions: Array<out String>,
        grantResults: IntArray
    ) {
        ubicacion?.onRequestPermissionsResult(requestCode, permissions, grantResults)
    }

    override fun onStart() {
        super.onStart()
        ubicacion?.inicializarUbicacion()
    }

    override fun onPause() {
        super.onPause()
        ubicacion?.detenerActualizacionUbicacion()
    }

}