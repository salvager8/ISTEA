package mx.com.rlr

import android.annotation.SuppressLint
import android.content.pm.PackageManager
import android.graphics.Color
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Toast
import androidx.core.app.ActivityCompat
import com.android.volley.Request
import com.android.volley.Response
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley
import com.google.android.gms.location.FusedLocationProviderClient
import com.google.android.gms.location.LocationCallback
import com.google.android.gms.location.LocationRequest
import com.google.android.gms.location.LocationResult

import com.google.android.gms.maps.CameraUpdateFactory
import com.google.android.gms.maps.GoogleMap
import com.google.android.gms.maps.OnMapReadyCallback
import com.google.android.gms.maps.SupportMapFragment
import com.google.android.gms.maps.model.*

class MapsActivity : AppCompatActivity(), OnMapReadyCallback, GoogleMap.OnMarkerClickListener, GoogleMap.OnMarkerDragListener {

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

    private var mapa:Mapa? = null

    private var markerListener = this
    private var dragListener = this

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_maps)
        // Obtain the SupportMapFragment and get notified when the map is ready to be used.
        val mapFragment = supportFragmentManager
                .findFragmentById(R.id.map) as SupportMapFragment
        mapFragment.getMapAsync(this)

        fusedLocationClient = FusedLocationProviderClient(this)
        inicializarLocationRequest()
    }

    private fun inicializarLocationRequest() {
        locationRequest = LocationRequest()
        //Configuracion del tiempo en que se estara actualizando la ubicacion, en este caso va de 1 a 5 segundos
        locationRequest?.interval = 10000
        locationRequest?.fastestInterval = 5000
        //configurando la proximidad en el nivel  mas alta
        locationRequest?.priority = LocationRequest.PRIORITY_HIGH_ACCURACY
    }

    /**
     * Manipulates the map once available.
     * This callback is triggered when the map is ready to be used.
     * This is where we can add markers or lines, add listeners or move the camera. In this case,
     * we just add a marker near Sydney, Australia.
     * If Google Play services is not installed on the device, the user will be prompted to install
     * it inside the SupportMapFragment. This method will only be triggered once the user has
     * installed Google Play services and returned to the app.
     */
    override fun onMapReady(googleMap: GoogleMap) {
        mapa = Mapa(googleMap, applicationContext, markerListener, dragListener)

        mapa?.cambiarEstiloMapa()

        mapa?.marcadoresEstaticos()

        mapa?.crearListeners()

        mapa?.prepararMarcadores()

        mapa?.dibujarLineas()
    }

    //Permiten controlar el marcador para arrastrar y soltar
    override fun onMarkerDragStart(marcador: Marker?) {
        Toast.makeText(this, "Empezando a mover el marcador", Toast.LENGTH_SHORT).show()
        Log.d("MARCADOR INICIAL", marcador?.position?.latitude.toString() + " , " + marcador?.position?.longitude.toString())
        //Otra forma de marcar
        //val index = listaMarcadores?.indexOf(marcador)
        //Log.d("MARCADOR INICIAL", listaMarcadores?.get(index!!)?.position?.latitude.toString())
    }

    override fun onMarkerDrag(marcador: Marker?) {
        title = marcador?.position?.latitude.toString() + " , " + marcador?.position?.longitude.toString()
    }

    override fun onMarkerDragEnd(marcador: Marker?) {
        Toast.makeText(this, "Acabó el evento drag & drop", Toast.LENGTH_SHORT).show()
        Log.d("MARCADOR FINAL", marcador?.position?.latitude.toString() + " , " + marcador?.position?.longitude.toString())
        //Otra forma de marcar
        //val index = listaMarcadores?.indexOf(marcador)
        //Log.d("MARCADOR FINAL", listaMarcadores?.get(index!!)?.position?.latitude.toString())
    }

    override fun onMarkerClick(marcador: Marker?): Boolean {
        var numeroClicks = marcador?.tag as? Int

        if (numeroClicks != null) {
            numeroClicks++
            marcador?.tag = numeroClicks

            Toast.makeText(this, "Se han dado " + numeroClicks.toString() + " clicks", Toast.LENGTH_LONG).show()
        }

        return false
    }

    private fun validarPermisoUbicacion(): Boolean {
        val hayUbicacionPrecisa = ActivityCompat.checkSelfPermission(this, permisoFineLocation) == PackageManager.PERMISSION_GRANTED
        val hayUbicacionOrdinaria = ActivityCompat.checkSelfPermission(this, permisoCoarseLocation) == PackageManager.PERMISSION_GRANTED

        return hayUbicacionPrecisa && hayUbicacionOrdinaria
    }

    //Con esto eliminamos el error por que ya implementamos el Permiso
    @SuppressLint("MissingPermission")
    private fun obtenerUbicacion() {
        /*
        fusedLocationClient?.lastLocation?.addOnSuccessListener(this, object: OnSuccessListener<Location> {
            override fun onSuccess(location: Location?) {
                if (location != null) {
                    Toast.makeText(applicationContext, location.latitude.toString() + " - " + location.longitude.toString(), Toast.LENGTH_LONG).show()
                }
            }

        })
        */
        callback = object: LocationCallback() {
            override fun onLocationResult(locationResult: LocationResult?) {
                super.onLocationResult(locationResult)

                if (mapa != null) {

                    mapa?.configurarMiUbicacion()

                    for (ubicacion in locationResult?.locations!!) {
                        //Toast.makeText(applicationContext, ubicacion.latitude.toString() + " , " + ubicacion.longitude.toString(), Toast.LENGTH_LONG).show()
                        // Add a marker in Sydney and move the camera
                        mapa?.miPosicion = LatLng(ubicacion.latitude, ubicacion.longitude)
                        mapa?.anadirMarcadorMiPosicion()
                    }
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
        requestPermissions(arrayOf(permisoFineLocation, permisoCoarseLocation), CODIGO_SOLICITUD_PERMISO)
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