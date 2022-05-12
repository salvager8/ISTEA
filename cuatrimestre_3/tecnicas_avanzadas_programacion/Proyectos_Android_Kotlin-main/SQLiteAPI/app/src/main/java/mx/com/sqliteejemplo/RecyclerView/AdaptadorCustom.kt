package mx.com.sqliteejemplo.RecyclerView

import android.graphics.Color
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import mx.com.sqliteejemplo.Modelo.Alumno
import mx.com.sqliteejemplo.R

class AdaptadorCustom(items:ArrayList<Alumno>, var listener: ClickListener, var longClickListener: LongClickListener): RecyclerView.Adapter<AdaptadorCustom.ViewHolder>() {

    var items: ArrayList<Alumno>? = null
    var multiSeleccion = false
    //En esta variable guardaremos los indices de los elementos seleccionados
    var itemSeleccionados:ArrayList<Int>? = null
    var viewHolder: ViewHolder? = null

    init {
        this.items = items
        itemSeleccionados = ArrayList()
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val vista = LayoutInflater.from(parent.context).inflate(R.layout.template_alumno, parent, false)
        viewHolder = ViewHolder(vista, listener, longClickListener)

        return viewHolder!!
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        val item = items?.get(position)

        holder.id?.text = item?.id
        holder.nombre?.text = item?.nombre

        if (itemSeleccionados?.contains(position)!!) {
            holder.vista.setBackgroundColor(Color.LTGRAY)
        } else {
            holder.vista.setBackgroundColor(Color.WHITE)
        }
    }

    //ActionMode
    fun iniciarActionMode() {
        multiSeleccion = true
    }

    fun destruirActionMode() {
        multiSeleccion = false
        itemSeleccionados?.clear()
        notifyDataSetChanged()
    }

    fun terminarActionMode() {
        //eliminar elementos seleccionados
        for (item in itemSeleccionados!!) {
            itemSeleccionados?.remove(item)
        }
        multiSeleccion = false
        notifyDataSetChanged()
    }

    override fun getItemCount(): Int {
        return items?.count()!!
    }

    fun seleccionarItem(index:Int) {
        if (multiSeleccion) {
            if (itemSeleccionados?.contains(index)!!) {
                itemSeleccionados?.remove(index)
            } else {
                itemSeleccionados?.add(index)
            }
            notifyDataSetChanged()
        }
    }

    fun obtenerNumeroElementosSeleccionados():Int {
        return itemSeleccionados?.count()!!
    }

    fun eliminarSeleccionados() {
        if (itemSeleccionados?.count()!! > 0) {
            var itemsEliminados = ArrayList<Alumno>()
            for (index in itemSeleccionados!!) {
                itemsEliminados.add(items?.get(index)!!)
            }
            items?.removeAll(itemsEliminados)
            itemSeleccionados?.clear()
        }
    }

    class ViewHolder(vista:View, listener: ClickListener, longClickListener: LongClickListener): RecyclerView.ViewHolder(vista), View.OnClickListener, View.OnLongClickListener {
        var vista = vista

        var id:TextView? = null
        var nombre:TextView? = null

        var listener: ClickListener? = null
        var longListener: LongClickListener? = null

        init {
            id = vista.findViewById(R.id.textViewId)
            nombre = vista.findViewById(R.id.textViewNombre)

            this.listener = listener
            this.longListener = longClickListener
            vista.setOnClickListener(this)
            vista.setOnLongClickListener(this)
        }

        override fun onClick(v: View?) {
            this.listener?.onClick(v!!, adapterPosition)
        }

        override fun onLongClick(v: View?): Boolean {
            this.longListener?.longClick(v!!, adapterPosition)
            return true
        }
    }

}