package mx.com.rlr.ejemplogridview

import android.graphics.Color
import android.os.Bundle
import android.widget.AdapterView
import android.widget.GridView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        var frutas =  ArrayList<Fruta>()

        frutas.add(Fruta("Manzana", R.drawable.manzana))
        frutas.add(Fruta("Plátano", R.drawable.platano))
        frutas.add(Fruta("Sandía", R.drawable.sandia))
        frutas.add(Fruta("Durazno", R.drawable.durazno))

        var grid:GridView = findViewById(R.id.grid)
        //val adaptador = ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, frutas)
        val adaptador = AdaptadorCustom(this, frutas)

        grid.adapter = adaptador
        
        grid.onItemClickListener = AdapterView.OnItemClickListener { adapterView, view, position, id ->

            var save = -1

            adapterView.getChildAt(position).setBackgroundColor(Color.BLUE)

            if (save != -1 && save != position){
                adapterView.getChildAt(save).setBackgroundColor(Color.BLACK)
            }

            save = position;

            Toast.makeText(this, frutas.get(position).nombre, Toast.LENGTH_LONG).show()

        }

    }

}