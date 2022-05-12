package mx.com.rlr

import android.annotation.SuppressLint
import android.content.pm.PackageManager
import android.location.Location
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Toast
import androidx.core.app.ActivityCompat
import com.google.android.gms.location.FusedLocationProviderClient
import com.google.android.gms.location.LocationCallback
import com.google.android.gms.location.LocationRequest
import com.google.android.gms.location.LocationResult
import com.google.android.gms.tasks.OnSuccessListener

class MainActivity : AppCompatActivity() {

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
        setContentView(R.layout.activity_main)

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

                for (ubicacion in locationResult?.locations!!) {
                    Toast.makeText(applicationContext, ubicacion.latitude.toString() + " , " + ubicacion.longitude.toString(), Toast.LENGTH_LONG).show()
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