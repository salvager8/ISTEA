package mx.com.ejemplofragmentos

import android.content.Context
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import java.lang.ClassCastException

/**
 * A simple [Fragment] subclass.
 * Use the [MyFragment.newInstance] factory method to
 * create an instance of this fragment.
 */
class MyFragment : Fragment() {

    var btn:Button? = null
    var editTextName:EditText? = null

    var listener:NombreListener? = null

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        val view = inflater.inflate(R.layout.fragment_my, container, false)

        btn = view.findViewById(R.id.btn)
        editTextName = view.findViewById(R.id.editTextName)

        btn?.setOnClickListener {
            //Toast.makeText(view.context, editTextName?.text.toString(), Toast.LENGTH_SHORT).show()
            val nombreActual = editTextName?.text.toString()
            listener?.obtenerNombre(nombreActual)
        }

        return view
    }

    interface NombreListener {
        fun obtenerNombre(nombre:String)
    }

    override fun onAttach(context: Context) {
        super.onAttach(context)

        try {
            listener = context as NombreListener
        } catch (e:ClassCastException) {
            throw ClassCastException("${context.toString()} debes implementar la interfaz")
        }

    }

}