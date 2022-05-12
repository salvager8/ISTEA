package mx.com.firebasemenudecomidas

import android.content.Intent
import android.os.Bundle
import android.util.TypedValue
import android.view.*
import android.widget.*
import androidx.core.widget.NestedScrollView
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.RecyclerView
import androidx.appcompat.widget.Toolbar
import com.google.android.material.floatingactionbutton.FloatingActionButton
import com.google.android.material.snackbar.Snackbar
import androidx.appcompat.app.AlertDialog
import com.google.firebase.database.*
import mx.com.firebasemenudecomidas.databinding.ActivityComidaListBinding

import mx.com.firebasemenudecomidas.dummy.DummyContent

/**
 * An activity representing a list of Pings. This activity
 * has different presentations for handset and tablet-size devices. On
 * handsets, the activity presents a list of items, which when touched,
 * lead to a [ComidaDetailActivity] representing
 * item details. On tablets, the activity presents the list of items and
 * item details side-by-side using two vertical panes.
 */
class ComidaListActivity : AppCompatActivity(), AdapterView.OnItemSelectedListener {

    private lateinit var binding: ActivityComidaListBinding

    var comidas: List<DummyContent.Comida>? = null
    var comidaUpdate: DummyContent.Comida? = null
    lateinit var arrayAdapterComidas: ArrayAdapter<String>
    /**
     * Whether or not the activity is in two-pane mode, i.e. running on a tablet
     * device.
     */
    private var twoPane: Boolean = false

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityComidaListBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val toolbar = findViewById<Toolbar>(R.id.toolbar)
        setSupportActionBar(toolbar)
        toolbar.title = title

        findViewById<FloatingActionButton>(R.id.fab).setOnClickListener { view ->
            Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
                .setAction("Action", null).show()
        }

        if (findViewById<NestedScrollView>(R.id.comida_detail_container) != null) {
            // The detail container view will be present only in the
            // large-screen layouts (res/values-w900dp).
            // If this view is present, then the
            // activity should be in two-pane mode.
            twoPane = true
        }

        binding.btnRefreshSpinner.setOnClickListener {
            onRefreshSpinnerClicked()
        }

        configSpinner()

        binding.btnSave.setOnClickListener {
            btnSave()
        }

        setupRecyclerView(findViewById(R.id.comida_list))
    }

    private fun configSpinner() {
        binding.spinnerFood.onItemSelectedListener = this

        arrayAdapterComidas = ArrayAdapter(this, android.R.layout.simple_spinner_dropdown_item)
        arrayAdapterComidas.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)

        binding.spinnerFood.adapter = arrayAdapterComidas
    }

    override fun onItemSelected(p0: AdapterView<*>?, p1: View?, p2: Int, p3: Long) {
        comidaUpdate = comidas!![p2]
        binding.editTextName.setText(comidaUpdate?.nombre)
        binding.editTextPrice.setText(comidaUpdate?.precio)
    }

    override fun onNothingSelected(p0: AdapterView<*>?) { }


    private fun btnSave() {
        val comida: DummyContent.Comida = DummyContent.Comida(binding.editTextName.text.toString().trim(), binding.editTextPrice.text.toString().trim())

        val database: FirebaseDatabase = FirebaseDatabase.getInstance()
        val reference: DatabaseReference = database.getReference(PATH_FOOD)

        //Check if comida with this name exist, and update item by name
        //val comidaUpdate: DummyContent.Comida = DummyContent.Comida(comida.nombre)
        if (comidaUpdate != null) {
            reference.child(comidaUpdate?.id!!).setValue(comida).addOnCompleteListener {
                //onComplete
                if (it.isSuccessful) {
                    comidaUpdate = null!!
                }
            }
        } else {
            reference.push().setValue(comida)
        }
        binding.editTextName.setText("")
        binding.editTextPrice.setText("")
    }

    private fun onRefreshSpinnerClicked() {
        val comidas = ArrayList<DummyContent.Comida>()
        arrayAdapterComidas.clear()

        val database: FirebaseDatabase = FirebaseDatabase.getInstance()
        val reference: DatabaseReference = database.getReference(PATH_FOOD)

        reference.addListenerForSingleValueEvent(object : ValueEventListener {
            override fun onDataChange(snapshot: DataSnapshot) {
                for (sshot: DataSnapshot in snapshot.children) {
                    val comida: DummyContent.Comida? = sshot.getValue(DummyContent.Comida::class.java)
                    comida?.id = sshot.key!!
                    comidas.add(comida!!)
                    arrayAdapterComidas.add(comida.nombre)
                }
            }

            override fun onCancelled(error: DatabaseError) {
                Toast.makeText(applicationContext, "Error al consultar comidas.", Toast.LENGTH_LONG).show()
            }

        })
    }

    override fun onCreateOptionsMenu(menu: Menu?): Boolean {
        menuInflater.inflate(R.menu.main_menu, menu)
        return super.onCreateOptionsMenu(menu)
    }

    override fun onOptionsItemSelected(item: MenuItem): Boolean {
        when(item.itemId) {
            R.id.action_info -> {
                //Toast.makeText(this, "Funciona!", Toast.LENGTH_LONG).show()
                val textViewCode = TextView(this)

                val params = LinearLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.WRAP_CONTENT)
                textViewCode.layoutParams = params
                textViewCode.gravity = Gravity.CENTER_HORIZONTAL
                textViewCode.setTextSize(TypedValue.COMPLEX_UNIT_SP, 28F)

                val database: FirebaseDatabase = FirebaseDatabase.getInstance()
                val reference: DatabaseReference = database.getReference(PATH_PROFILE).child(PATH_CODE)

                reference.addListenerForSingleValueEvent(object : ValueEventListener {
                    override fun onDataChange(snapshot: DataSnapshot) {
                        textViewCode.text = snapshot.getValue(String::class.java)
                    }

                    override fun onCancelled(error: DatabaseError) {
                        Toast.makeText(applicationContext, "No se puede cargar el código!", Toast.LENGTH_LONG).show()
                    }
                })
                //showDialog()
                val builder = AlertDialog.Builder(this)
                    .setTitle(R.string.comidaList_dialog_title)
                    .setPositiveButton(R.string.comidaList_dialog_ok, null)
                    .setView(textViewCode)
                    .show()
            }

        }
        return super.onOptionsItemSelected(item)
    }

    private fun setupRecyclerView(recyclerView: RecyclerView) {
        recyclerView.adapter = SimpleItemRecyclerViewAdapter(this, DummyContent.ITEMS, twoPane)

        val database: FirebaseDatabase = FirebaseDatabase.getInstance()
        val reference: DatabaseReference = database.getReference(PATH_FOOD)

        reference.addChildEventListener(object: ChildEventListener {
            override fun onChildAdded(snapshot: DataSnapshot, previousChildName: String?) {
                val comida: DummyContent.Comida = snapshot.getValue(DummyContent.Comida::class.java)!!
                comida.id = snapshot.key!!

                if (!DummyContent.ITEMS.contains(comida)) {
                    DummyContent.addItem(comida)
                }

                recyclerView.adapter?.notifyDataSetChanged()
            }

            override fun onChildChanged(snapshot: DataSnapshot, previousChildName: String?) {
                val comida: DummyContent.Comida? = snapshot.getValue(DummyContent.Comida::class.java)
                comida?.id = snapshot.key!!

                if (DummyContent.ITEMS.contains(comida)) {
                    DummyContent.updateItem(comida!!)
                }
                
                recyclerView.adapter?.notifyDataSetChanged()

            }

            override fun onChildRemoved(snapshot: DataSnapshot) {
                val comida: DummyContent.Comida = snapshot.getValue(DummyContent.Comida::class.java)!!
                comida.id = snapshot.key!!

                if (DummyContent.ITEMS.contains(comida)) {
                    DummyContent.deleteItem(comida)
                }

                recyclerView.adapter!!.notifyDataSetChanged()
            }

            override fun onChildMoved(snapshot: DataSnapshot, previousChildName: String?) {
                Toast.makeText(applicationContext, "Moved", Toast.LENGTH_SHORT).show()
            }

            override fun onCancelled(error: DatabaseError) {
                Toast.makeText(applicationContext, "Moved", Toast.LENGTH_SHORT).show()
            }

        })
    }

    class SimpleItemRecyclerViewAdapter(
        private val parentActivity: ComidaListActivity,
        private val values: List<DummyContent.Comida>,
        private val twoPane: Boolean
    ) :
        RecyclerView.Adapter<SimpleItemRecyclerViewAdapter.ViewHolder>() {

        private val onClickListener: View.OnClickListener

        init {
            onClickListener = View.OnClickListener { v ->
                val item = v.tag as DummyContent.Comida
                if (twoPane) {
                    val fragment = ComidaDetailFragment().apply {
                        arguments = Bundle().apply {
                            putString(ComidaDetailFragment.ARG_ITEM_ID, item.id)
                        }
                    }
                    parentActivity.supportFragmentManager
                        .beginTransaction()
                        .replace(R.id.comida_detail_container, fragment)
                        .commit()
                } else {
                    val intent = Intent(v.context, ComidaDetailActivity::class.java).apply {
                        putExtra(ComidaDetailFragment.ARG_ITEM_ID, item.id)
                    }
                    v.context.startActivity(intent)
                }
            }
        }

        override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
            val view = LayoutInflater.from(parent.context)
                .inflate(R.layout.comida_list_content, parent, false)
            return ViewHolder(view)
        }

        override fun onBindViewHolder(holder: ViewHolder, position: Int) {
            val item = values[position]
            holder.idView.text = "$" + item.precio
            holder.contentView.text = item.nombre

            with(holder.itemView) {
                tag = item
                setOnClickListener(onClickListener)

                holder.btnDelete.setOnClickListener {
                    val database: FirebaseDatabase = FirebaseDatabase.getInstance()
                    val reference: DatabaseReference = database.getReference(PATH_FOOD)
                    reference.child(item.id).removeValue()
                }
            }
        }

        override fun getItemCount() = values.size

        inner class ViewHolder(view: View) : RecyclerView.ViewHolder(view) {
            val idView: TextView = view.findViewById(R.id.id_text)
            val contentView: TextView = view.findViewById(R.id.content)
            val btnDelete: Button = view.findViewById(R.id.btnDelete)
        }
    }

    companion object {
        private const val PATH_FOOD: String = "food"
        private const val PATH_PROFILE: String = "profile"
        private const val PATH_CODE: String = "code"
    }
}