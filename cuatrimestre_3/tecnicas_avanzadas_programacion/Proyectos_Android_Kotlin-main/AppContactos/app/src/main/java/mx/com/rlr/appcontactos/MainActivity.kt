package mx.com.rlr.appcontactos

import android.app.SearchManager
import android.content.Context
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.Menu
import android.view.MenuItem
import android.widget.GridView
import android.widget.ListView
import android.widget.Switch
import android.widget.ViewSwitcher
import androidx.appcompat.widget.SearchView
import androidx.appcompat.widget.Toolbar

class MainActivity : AppCompatActivity() {

    var lista:ListView? = null
    var grid:GridView? = null
    var viewSwitcher:ViewSwitcher? = null

    companion object {
        var contactos:ArrayList<Contacto>? = null
        var adaptador:AdaptadorCustom? = null
        var adaptadorGrid:AdaptadorCustomGrid? = null

        fun agregarContacto(contacto: Contacto) {
            adaptador?.addItem(contacto)
            adaptadorGrid?.addItem(contacto)
        }

        fun obtenerContacto(index:Int):Contacto {
            if (adaptador?.isEnabled(1)!!) {
                return adaptador?.getItem(index) as Contacto
            } else {
                return adaptadorGrid?.getItem(index) as Contacto
            }

        }

        fun eliminarContacto(index: Int) {
            adaptador?.removeItem(index)
            adaptadorGrid?.removeItem(index)
        }

        fun actualizarContacto(index: Int, nuevoContacto: Contacto) {
            adaptador?.updateItems(index, nuevoContacto)
            adaptadorGrid?.updateItems(index, nuevoContacto)
        }
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val toolbar = findViewById<Toolbar>(R.id.toolbar)
        setSupportActionBar(toolbar)

        contactos = ArrayList()
        contactos?.add(Contacto("Roberto", "Ledesma", "RLR", 25, 61.0F, "Akil 376", "5544897476", "rob017led@gmail.com", R.drawable.foto_01))
        contactos?.add(Contacto("Carlos", "Vela", "LA", 31, 77.0F, "LA", "554482176", "cvela@gmail.com", R.drawable.foto_02))
        contactos?.add(Contacto("Laura", "Rios", "Rios", 22, 51.0F, "Sur", "554412332", "lrios@gmail.com", R.drawable.foto_04))

        lista = findViewById<ListView>(R.id.listViewLista)
        grid = findViewById<GridView>(R.id.gridViewLista)
        adaptador = AdaptadorCustom(this, contactos!!)
        adaptadorGrid = AdaptadorCustomGrid(this, contactos!!)

        viewSwitcher = findViewById(R.id.viewSwitcher)

        lista?.adapter = adaptador
        grid?.adapter = adaptadorGrid

        lista?.setOnItemClickListener { adapterView, view, position, id ->
            val intent = Intent(this, Detalle::class.java)
            intent.putExtra("ID", position.toString())
            startActivity(intent)
        }

        grid?.setOnItemClickListener { adapterView, view, position, id ->
            val intent = Intent(this, Detalle::class.java)
            intent.putExtra("ID", position.toString())
            startActivity(intent)
        }
    }

    override fun onCreateOptionsMenu(menu: Menu?): Boolean {
        menuInflater.inflate(R.menu.menu_main, menu)
        //COnfigurar boton de bsuqueda de contactos
        val searchManager = getSystemService(Context.SEARCH_SERVICE) as SearchManager
        val itemBusqueda = menu?.findItem(R.id.searchView)
        val searchView = itemBusqueda?.actionView as SearchView
        //Configuracion boton Switch
        val itemSwitch = menu?.findItem(R.id.switchView)
        itemSwitch?.setActionView(R.layout.switch_item)
        val switchView = itemSwitch?.actionView?.findViewById<Switch>(R.id.switchChangeView)

        searchView.setSearchableInfo(searchManager.getSearchableInfo(componentName))
        searchView.queryHint = "Buscar contacto..."

        searchView.setOnQueryTextFocusChangeListener { view, hasFocus ->
            //Preparar los datos
        }

        searchView.setOnQueryTextListener(object: SearchView.OnQueryTextListener {
            override fun onQueryTextChange(newText: String?): Boolean {
                //filtrar
                adaptador?.filtrar(newText!!)
                adaptadorGrid?.filtrar(newText!!)
                return true
            }

            override fun onQueryTextSubmit(p0: String?): Boolean {
                //filtrar
                return true
            }
        })

        switchView?.setOnCheckedChangeListener { buttonView, isChecked ->
            viewSwitcher?.showNext()
        }

        return super.onCreateOptionsMenu(menu)
    }

    override fun onOptionsItemSelected(item: MenuItem): Boolean {
        when(item?.itemId) {

            R.id.itemNewContact -> {
                val intent = Intent(this, NuevoContacto::class.java)
                startActivity(intent)
                return true
            }

            else -> {
                return super.onOptionsItemSelected(item)
            }
        }
    }

    override fun onResume() {
        super.onResume()

        adaptador?.notifyDataSetChanged()
        adaptadorGrid?.notifyDataSetChanged()
    }

}