package mx.com.rlr

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.Menu
import android.view.MenuItem
import android.view.View
import android.widget.Toast
import androidx.appcompat.view.ActionMode
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import androidx.swiperefreshlayout.widget.SwipeRefreshLayout

class MainActivity : AppCompatActivity() {

    var lista:RecyclerView? = null
    var adaptador:AdaptadorCustom? = null
    var layoutManager:RecyclerView.LayoutManager? = null

    //variable para validar el Action Mode
    var isActionMode = false
    var actionMode:ActionMode? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val platillos = ArrayList<Platillo>()

        platillos.add(Platillo("Platillo 1", 250.0, 3.5F, R.drawable.platillo01))
        platillos.add(Platillo("Platillo 2", 333.0, 5.0F, R.drawable.platillo02))
        platillos.add(Platillo("Platillo 3", 125.0, 2.0F, R.drawable.platillo03))
        platillos.add(Platillo("Platillo 4", 200.0, 3.0F, R.drawable.platillo04))
        platillos.add(Platillo("Platillo 5", 175.0, 4.0F, R.drawable.platillo05))
        platillos.add(Platillo("Platillo 6", 210.0, 1.5F, R.drawable.platillo06))
        platillos.add(Platillo("Platillo 7", 190.0, 3.5F, R.drawable.platillo07))
        platillos.add(Platillo("Platillo 8", 80.0, 4.5F, R.drawable.platillo08))
        platillos.add(Platillo("Platillo 9", 100.0, 5.0F, R.drawable.platillo09))
        platillos.add(Platillo("Platillo 10", 235.0, 3.0F, R.drawable.platillo10))

        lista = findViewById(R.id.recyclerViewLista)
        lista?.setHasFixedSize(true)

        layoutManager = LinearLayoutManager(this)
        lista?.layoutManager = layoutManager

        //Action Mode
        val callback = object: androidx.appcompat.view.ActionMode.Callback {

            override fun onCreateActionMode(mode: ActionMode?, menu: Menu?): Boolean {
                //Permite inicializar el Action Mode
                adaptador?.iniciarActionMode()
                actionMode = mode
                //inflat menu
                menuInflater.inflate(R.menu.menu_contextual, menu)
                return true
            }

            override fun onPrepareActionMode(mode: ActionMode?, menu: Menu?): Boolean {
                mode?.title = "0 seleccionados"
                return false
            }

            override fun onActionItemClicked(mode: ActionMode?, item: MenuItem?): Boolean {
                //implementacion boton eliminar
                when(item?.itemId) {
                    R.id.itemDelete -> {
                        adaptador?.eliminarSeleccionados()
                    }
                    else -> {return true}
                }
                //Cuando le damos click
                adaptador?.terminarActionMode()
                mode?.finish()
                isActionMode = false
                return true
            }

            override fun onDestroyActionMode(mode: ActionMode?) {
                adaptador?.destruirActionMode()
                isActionMode = false
            }

        }

        adaptador = AdaptadorCustom(platillos, object:ClickListener{
            override fun onClick(vista: View, index: Int) {
                Toast.makeText(applicationContext, platillos.get(index).nombre, Toast.LENGTH_SHORT).show()
            }

        }, object: LongClickListener{
            override fun longClick(vista: View, index: Int) {
                //Validar el ActionMode
                if (!isActionMode) {
                    startSupportActionMode(callback)
                    isActionMode = true
                    adaptador?.seleccionarItem(index)
                } else {
                    //Hacer selecciones o deselecciones
                    adaptador?.seleccionarItem(index)
                }
                actionMode?.title = adaptador?.obtenerNumeroElementosSeleccionados().toString() + " seleccionados"
            }

        })
        lista?.adapter = adaptador

        //Refrescar el layout
        val swipeToRefresh = findViewById<SwipeRefreshLayout>(R.id.swipeToRefresh)
        swipeToRefresh.setOnRefreshListener {
            for (i in 1..170000000) {
            }
            swipeToRefresh.isRefreshing = false
            platillos.add(Platillo("Platillo 11", 250.0, 3.5F, R.drawable.platillo01))
            adaptador?.notifyDataSetChanged()
        }

    }
}