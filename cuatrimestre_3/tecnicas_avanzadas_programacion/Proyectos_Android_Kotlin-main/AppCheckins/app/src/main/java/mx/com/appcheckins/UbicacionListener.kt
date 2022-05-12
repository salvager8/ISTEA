package mx.com.appcheckins

import com.google.android.gms.location.LocationResult

interface UbicacionListener {

    fun ubicacionResponse(locationResult:LocationResult)

}