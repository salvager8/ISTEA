package mx.com.rlr

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.Button
import android.widget.CheckBox
import android.widget.EditText
import android.widget.Toast

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val buttonSaludar = findViewById<Button>(R.id.btnSaludar)
        val etNombre = findViewById<EditText>(R.id.editTextName)
        val checkBoxDev = findViewById<CheckBox>(R.id.checkBoxDeveloper)

        buttonSaludar.setOnClickListener(View.OnClickListener {
            if (validarDatos()) {
                if (checkBoxDev.isChecked) {
                    Toast.makeText(this, "Saludos Desarrollador " + etNombre.text + "!", Toast.LENGTH_LONG).show()
                } else {
                    Toast.makeText(this, "Saludos " + etNombre.text + "!", Toast.LENGTH_LONG).show()
                }
            } else {
                Toast.makeText(this, "Escribe tu nombre!", Toast.LENGTH_LONG).show()
            }
        })
    }

    fun validarDatos(): Boolean {

        val etNombre = findViewById<EditText>(R.id.editTextName)
        val nombreUsuario = etNombre.text

        if (nombreUsuario.isNullOrEmpty()){
            return false
        }
        return true
    }
}