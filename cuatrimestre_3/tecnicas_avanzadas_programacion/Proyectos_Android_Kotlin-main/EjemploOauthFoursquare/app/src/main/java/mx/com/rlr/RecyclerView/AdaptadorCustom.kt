package mx.com.rlr.RecyclerView

import android.content.Context
import android.graphics.Color
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.RatingBar
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.squareup.picasso.Picasso
import kotlinx.android.synthetic.main.template_veneus.view.*
import mx.com.rlr.R
import mx.com.rlr.Venue

class AdaptadorCustom(items:ArrayList<Venue>, var listener: ClickListener, var longClickListener: LongClickListener): RecyclerView.Adapter<AdaptadorCustom.ViewHolder>() {

    var items: ArrayList<Venue>? = null
    var multiSeleccion = false
    //En esta variable guardaremos los indices de los elementos seleccionados
    var itemSeleccionados:ArrayList<Int>? = null
    var viewHolder:ViewHolder? = null

    init {
        this.items = items
        itemSeleccionados = ArrayList()
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): AdaptadorCustom.ViewHolder {
        val vista = LayoutInflater.from(parent.context).inflate(R.layout.template_veneus, parent, false)
        viewHolder = ViewHolder(vista, listener, longClickListener)

        return viewHolder!!
    }

    override fun onBindViewHolder(holder: AdaptadorCustom.ViewHolder, position: Int) {
        val item = items?.get(position)
        //holder.foto?.setImageResource(item?.foto!!)
        holder.nombre?.text = item?.name
        if (item?.categories?.size!! > 0) {
            val urlImagen = item?.categories?.get(0)!!.icon?.prefix + "bg_64" + item?.categories?.get(0)!!.icon?.suffix
            Picasso.get().load(urlImagen).into(holder.foto)
        }

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
            var itemsEliminados = ArrayList<Venue>()
            for (index in itemSeleccionados!!) {
                itemsEliminados.add(items?.get(index)!!)
            }
            items?.removeAll(itemsEliminados)
            itemSeleccionados?.clear()
        }
    }

    class ViewHolder(vista:View, listener: ClickListener, longClickListener: LongClickListener): RecyclerView.ViewHolder(vista), View.OnClickListener, View.OnLongClickListener {
        var vista = vista
        var foto:ImageView? = null
        var nombre:TextView? = null

        var listener:ClickListener? = null
        var longListener:LongClickListener? = null

        init {
            this.vista = vista
            //foto = vista.findViewById(R.id.imageViewPlatillo)
            this.foto = vista.imageViewFoto
            this.nombre = vista.textViewNombre
            //this.nombre = vista.findViewById(R.id.textViewNombre) as TextView

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