package mx.com.ejemplofragmentos02

import android.content.Intent
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.FrameLayout
import android.widget.ListView
import android.widget.Toast
import androidx.fragment.app.FragmentTransaction
import kotlinx.android.synthetic.main.fragment_lista_peliculas.*
import kotlinx.android.synthetic.main.fragment_lista_peliculas.view.*

class ListaPeliculas : Fragment() {

    companion object {
        var peliculas:ArrayList<Pelicula>? = null
    }

    var nombrePeliculas:ArrayList<String>? = null

    var lista:ListView? = null

    var hayDoblePanel = false

    var posicionActual = 0

    override fun onActivityCreated(savedInstanceState: Bundle?) {
        super.onActivityCreated(savedInstanceState)

        configurarListView()

        val frameDetalles = activity!!.findViewById<FrameLayout>(R.id.detalles)
        hayDoblePanel = frameDetalles != null && frameDetalles.visibility == View.VISIBLE

        if (savedInstanceState != null) {
            posicionActual = savedInstanceState.getInt("INDEX", 0)
        }

        if (hayDoblePanel) {
            lista?.choiceMode = ListView.CHOICE_MODE_SINGLE
            mostrarDetalles(posicionActual)
        }
    }

    private fun configurarListView() {
        peliculas = ArrayList()
        peliculas?.add(Pelicula("About Time", R.drawable.poster01))
        peliculas?.add(Pelicula("Silver Linings Playbook", R.drawable.poster02))
        peliculas?.add(Pelicula("Hunger Games Catching Fire", R.drawable.poster03))
        peliculas?.add(Pelicula("Batman The Dark Knight", R.drawable.poster04))

        nombrePeliculas = obtenerNombrePeliculas(peliculas!!)

        val adaptador = ArrayAdapter(activity!!, android.R.layout.simple_list_item_activated_1, nombrePeliculas!!)

        lista = activity!!.findViewById(R.id.listViewLista)
        lista?.adapter = adaptador

        lista?.setOnItemClickListener { adapterView, view, i, l ->
            //Toast.makeText(vista.context, nombrePeliculas?.get(i), Toast.LENGTH_SHORT).show()
            /* val intent = Intent(vista.context, DetallesActivity::class.java)
            intent.putExtra("INDEX", i)
            startActivity(intent) */
            mostrarDetalles(i)
        }
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        val vista = inflater.inflate(R.layout.fragment_lista_peliculas, container, false)

        return vista
    }

    fun mostrarDetalles(index:Int) {
        posicionActual = index

        if (hayDoblePanel) {

            lista?.setItemChecked(index, true)

            var fragDetalles = activity?.supportFragmentManager?.findFragmentById(R.id.detalles) as? ContenidoPeliculas

            if (fragDetalles == null || fragDetalles.obtenerIndex() != index) {
                fragDetalles = ContenidoPeliculas().nuevaInstancia(index)

                val fragmentTransaction = activity!!.supportFragmentManager.beginTransaction()

                fragmentTransaction.replace(R.id.detalles, fragDetalles)

                fragmentTransaction.setTransition(FragmentTransaction.TRANSIT_FRAGMENT_FADE)

                fragmentTransaction.commit()

            }

        } else {
            val intent = Intent(activity, DetallesActivity::class.java)
            intent.putExtra("INDEX", index)
            startActivity(intent)
        }
    }

    fun obtenerNombrePeliculas(peliculas:ArrayList<Pelicula>):ArrayList<String>{
        val nombres = ArrayList<String>()

        for (pelicula in peliculas) {
            nombres.add(pelicula.nombre)
        }
        return nombres
    }

}