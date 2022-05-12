package mx.com.rlr

import android.annotation.SuppressLint
import android.content.pm.PackageManager
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Toast
import androidx.core.app.ActivityCompat
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.android.volley.Request
import com.android.volley.Response
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley
import com.google.android.gms.location.FusedLocationProviderClient
import com.google.android.gms.location.LocationCallback
import com.google.android.gms.location.LocationRequest
import com.google.android.gms.location.LocationResult
import com.google.gson.Gson
import mx.com.rlr.RecyclerView.AdaptadorCustom
import mx.com.rlr.RecyclerView.ClickListener
import mx.com.rlr.RecyclerView.LongClickListener

class SecondActivity : AppCompatActivity() {

    var fsq:Foursquare? = null

    //RecyclerView
    var lista: RecyclerView? = null
    var adaptador: AdaptadorCustom? = null
    var layoutManager: RecyclerView.LayoutManager? = null

    //Permiso Fine = mas preciso
    private val permisoFineLocation = android.Manifest.permission.ACCESS_FINE_LOCATION
    //Permiso Fine = menos preciso
    private val permisoCoarseLocation = android.Manifest.permission.ACCESS_COARSE_LOCATION
    private val CODIGO_SOLICITUD_PERMISO = 100
    //Obteniendo la ultima ubicacion ya existente
    var fusedLocationClient: FusedLocationProviderClient? = null
    //Obteniendo la ubicacion en tiempo real
    var locationRequest: LocationRequest? = null
    var callback: LocationCallback? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_second)

        fsq = Foursquare(this)

        lista = findViewById(R.id.recyclerViewLista)
        lista?.setHasFixedSize(true)

        fusedLocationClient = FusedLocationProviderClient(this)
        inicializarLocationRequest()

        layoutManager = LinearLayoutManager(this)
        lista?.layoutManager = layoutManager

    }

    fun obtenerLugares(lat:String, lon:String) {
        val queue = Volley.newRequestQueue(this,)
        val url = "https://api.foursquare.com/v2/venues/search?ll=$lat,$lon&client_id=VORSML0I5SGYX5VB3B05DOCLL2O22NFQU2NYX0AJURPGFGFD&client_secret=U4A2GTKKW44E0SSNJVGMHKDOEFAQN0IORDCVS1H5EGEBDTW0&v=20180323"

        val solicitud = StringRequest(Request.Method.GET, url, {
            response ->

            Log.d("RESPONSE HTTP", response)

            val gson = Gson()
            val venues = gson.fromJson(response, FoursquareRequest::class.java)
            Log.d("VENUES", venues.response?.venues?.size.toString())

            adaptador = AdaptadorCustom(venues.response?.venues!!, object: ClickListener{
                override fun onClick(vista: View, index: Int) {
                }
            }, object: LongClickListener{
                override fun longClick(vista: View, index: Int) {
                }
            })

            lista?.adapter = adaptador

        }, {

        })
        queue.add(solicitud)
    }

    private fun inicializarLocationRequest() {
        locationRequest = LocationRequest()
        //Configuracion del tiempo en que se estara actualizando la ubicacion, en este caso va de 1 a 5 segundos
        locationRequest?.interval = 10000
        locationRequest?.fastestInterval = 5000
        //configurando la proximidad en el nivel  mas alta
        locationRequest?.priority = LocationRequest.PRIORITY_HIGH_ACCURACY
    }

    private fun validarPermisoUbicacion(): Boolean {
        val hayUbicacionPrecisa = ActivityCompat.checkSelfPermission(this, permisoFineLocation) == PackageManager.PERMISSION_GRANTED
        val hayUbicacionOrdinaria = ActivityCompat.checkSelfPermission(this, permisoCoarseLocation) == PackageManager.PERMISSION_GRANTED

        return hayUbicacionPrecisa && hayUbicacionOrdinaria
    }

    //Con esto eliminamos el error por que ya implementamos el Permiso
    @SuppressLint("MissingPermission")
    private fun obtenerUbicacion() {
        callback = object: LocationCallback() {
            override fun onLocationResult(locationResult: LocationResult?) {
                super.onLocationResult(locationResult)

                for (ubicacion in locationResult?.locations!!) {
                    //Toast.makeText(applicationContext, ubicacion.latitude.toString() + " , " + ubicacion.longitude.toString(), Toast.LENGTH_LONG).show()
                    obtenerLugares(ubicacion.latitude.toString(), ubicacion.longitude.toString())
                }
            }
        }

        fusedLocationClient?.requestLocationUpdates(locationRequest, callback, null)
    }

    private  fun pedirPermisos() {
        val racional = ActivityCompat.shouldShowRequestPermissionRationale(this, permisoFineLocation)

        if (racional) {
            //Mandar un mensaje con explicacion adicional
            Toast.makeText(this, "Hola", Toast.LENGTH_LONG).show()
            solicitudPermiso()
        } else {
            solicitudPermiso()
        }
    }

    private fun solicitudPermiso() {
        ActivityCompat.requestPermissions(this, arrayOf(permisoFineLocation, permisoCoarseLocation), CODIGO_SOLICITUD_PERMISO)
    }

    override fun onRequestPermissionsResult(requestCode: Int, permissions: Array<out String>, grantResults: IntArray) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults)

        when(requestCode) {
            CODIGO_SOLICITUD_PERMISO -> {
                if (grantResults.size > 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    //obtenemos la ultima ubicacion almacenada
                    obtenerUbicacion()
                } else {
                    //no se dio permiso
                    Toast.makeText(this, "No diste permiso para acceder a la ubicación", Toast.LENGTH_LONG).show()
                }
            }
        }
    }

    private fun detenerActualizacionUbicacion() {
        fusedLocationClient?.removeLocationUpdates(callback)
    }

    override fun onStart() {
        super.onStart()

        if (validarPermisoUbicacion()) {
            obtenerUbicacion()
        } else {
            pedirPermisos()
        }
    }

    override fun onPause() {
        super.onPause()

        detenerActualizacionUbicacion()
    }
}