package mx.com.rlr

import android.content.Context
import android.graphics.Color
import android.util.Log
import com.android.volley.Request
import com.android.volley.Response
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley
import com.google.android.gms.maps.CameraUpdateFactory
import com.google.android.gms.maps.GoogleMap
import com.google.android.gms.maps.model.*

class Mapa(mapa: GoogleMap, context: Context, var markerClickListener: GoogleMap.OnMarkerClickListener, var markerDragListener: GoogleMap.OnMarkerDragListener) {

    private var mMap:GoogleMap? = null
    private var context:Context? = null

    private var listaMarcadores: ArrayList<Marker>? = null

    var miPosicion: LatLng? = null

    //Marcadores de mapa
    private var marcadorGolden: Marker? = null
    private var marcadorPiramides: Marker? = null
    private var marcadorPisa: Marker? = null

    init {
        this.mMap = mapa
        this.context = context
    }

    fun dibujarLineas() {
        /* De estos 3 puntos de coordenas dibujan lineas
        val coordenadasLineas = PolygonOptions()
            .add(LatLng(19.43521049853097 , -99.1391833126545))
            .add(LatLng(19.43704459928318 , -99.14094418287276))
            .add(LatLng(19.43211767906261 , -99.14187826216221))
            //Modificando a linea punteada
            .pattern(arrayListOf<PatternItem>(Dot(), Gap(20f))) //Gap es la distancia entre los puntos
            .color(Color.CYAN)
            .width(30f)
        mMap.addPolygon(coordenadasLineas)
         */
        /* De estos 3 puntos de coordenas se dibujan en este caso un triangulo(poligono) uniendo las coordenadas
        val coordenadasPoligonos = PolygonOptions()
            .add(LatLng(19.43521049853097 , -99.1391833126545))
            .add(LatLng(19.43704459928318 , -99.14094418287276))
            .add(LatLng(19.43211767906261 , -99.14187826216221))
            //Modificando a linea punteada
            .strokePattern(arrayListOf<PatternItem>(Dash(10f), Gap(10f))) //Gap es la distancia entre los puntos
            .strokeColor(Color.BLUE)
            .fillColor(Color.GREEN)
            .strokeWidth(10f)
        mMap.addPolygon(coordenadasPoligonos)
         */
        // De una coordenada dibujamos un circulo
        val coordenadasCirculo = CircleOptions()
            .center(LatLng(19.43521049853097 , -99.1391833126545))
            .radius(120.0)
            //Modificando a linea punteada
            .strokePattern(arrayListOf<PatternItem>(Dash(10f), Gap(10f))) //Gap es la distancia entre los puntos
            .strokeColor(Color.WHITE)
            .fillColor(Color.YELLOW)
            .strokeWidth(15f)
        mMap?.addCircle(coordenadasCirculo)
    }

    fun cambiarEstiloMapa() {
        //mMap.mapType = GoogleMap.MAP_TYPE_TERRAIN
        val cambioDeMapa = mMap?.setMapStyle(MapStyleOptions.loadRawResourceStyle(this.context, R.raw.estilo_mapa))

        if (!cambioDeMapa!!) {
            //Mencionar que hubo un problema al cambiar el tipo de mapa
        }
    }

    fun crearListeners() {
        mMap?.setOnMarkerClickListener(markerClickListener)
        mMap?.setOnMarkerDragListener(markerDragListener)
    }

    fun marcadoresEstaticos() {
        //Golden Gate: 37.8199286, -122.4782551
        //Piramides de Giza: 29.9772962, 31.1324955
        //Torre de Pisa: 43.722952, 10.396597
        val GOLDEN_GATE = LatLng(37.8199286, -122.4782551)
        val PIRAMIDES_GIZA = LatLng(29.9772962, 31.1324955)
        val TORRE_PISA = LatLng(43.722952, 10.396597)

        marcadorGolden = mMap?.addMarker(
            MarkerOptions()
            .position(GOLDEN_GATE)
            .icon(BitmapDescriptorFactory.fromResource(R.drawable.ic_f1))
            .snippet("F1 USA") //Descripción
            .alpha(0.6f) //transparencia del icono
            .title("Golden Gate"))
        marcadorGolden?.tag = 0

        marcadorPiramides= mMap?.addMarker(
            MarkerOptions()
            .position(PIRAMIDES_GIZA)
            .icon(BitmapDescriptorFactory.fromResource(R.drawable.ic_f1))
            .snippet("F1 EGIPTO") //Descripción
            .alpha(1F) //transparencia del icono
            .title("Pirámides de Giza"))
        marcadorPiramides?.tag = 0

        marcadorPisa = mMap?.addMarker(
            MarkerOptions()
            .position(TORRE_PISA)
            .icon(BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_YELLOW))
            .alpha(0.9F) //transparencia del icono
            .title("Torre de Pisa"))
        marcadorPisa?.tag = 0
    }

    fun prepararMarcadores() {
        listaMarcadores = ArrayList()

        mMap?.setOnMapLongClickListener {
                location: LatLng? ->

            listaMarcadores?.add(mMap?.addMarker(MarkerOptions()
                .position(location!!)
                .icon(BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_YELLOW))
                .alpha(0.9F) //transparencia del icono
                .title("Torre de Pisa"))!!
            )
            listaMarcadores?.last()!!.isDraggable = true

            val coordenadas = LatLng(listaMarcadores?.last()!!.position.latitude, listaMarcadores?.last()!!.position.longitude)

            val origen = "origin=" + miPosicion?.latitude + "," + miPosicion?.longitude + "&"
            val destino = "destination=" + coordenadas.latitude + "," + coordenadas.longitude + "&"

            val parametros = origen + destino + "sensor=false&mode=driving"

            //NO SE PUEDE USAR GRATIS, SE NECESITA UNA TARJETA DE CREDITO PARA ACTIVARLA
            cargarURL("http://maps.googleapis.com/maps/api/directions/json?" + parametros)
        }
    }

    private fun cargarURL(url:String) {
        val queue = Volley.newRequestQueue(this.context)

        val solicitud = StringRequest(Request.Method.GET, url, Response.Listener<String>{
                response ->
            Log.d("HTTP", response)
        }, Response.ErrorListener { })

        queue.add(solicitud)
    }

    fun configurarMiUbicacion() {
        mMap?.isMyLocationEnabled = true
        mMap?.uiSettings?.isMyLocationButtonEnabled = true
    }

    fun anadirMarcadorMiPosicion() {
        mMap?.addMarker(MarkerOptions().position(miPosicion!!).title("Aquí estoy!"))
        mMap?.moveCamera(CameraUpdateFactory.newLatLng(miPosicion))
    }

}