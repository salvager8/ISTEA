package mx.com.rlr

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.Button
import android.widget.Toast

class MainActivity : AppCompatActivity() {

    val TAG = "mx.com.rlr.ciudades.CIUDAD"

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val buttonCdmx = findViewById<Button>(R.id.btnCdmx)
        val buttonBerlin = findViewById<Button>(R.id.btnBerlin)

        buttonCdmx.setOnClickListener(View.OnClickListener {
            val intent = Intent(this, SecondActivity::class.java)
            intent.putExtra(TAG, "3530597")
            startActivity(intent)
        })

        buttonBerlin.setOnClickListener(View.OnClickListener {
            val intent = Intent(this, SecondActivity::class.java)
            intent.putExtra(TAG, "2950159")
            startActivity(intent)
        })
    }
}