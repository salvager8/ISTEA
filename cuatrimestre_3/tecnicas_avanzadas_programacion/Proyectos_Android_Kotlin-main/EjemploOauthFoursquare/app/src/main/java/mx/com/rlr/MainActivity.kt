package mx.com.rlr

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import androidx.appcompat.app.AppCompatActivity

class MainActivity : AppCompatActivity() {

    var fsq:Foursquare? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        /* PASO ANTES DE TODO
        El primer paso es buscar utilizando CMD
        la carpeta .android
        dentro ejecutar el siguiente comando: keytool -list -v -keystore debug.keystore

        La password es: android

        En la configuracion de la API que vamos a utilizar ingresar el SHA1 en ANDROID KEY HASHES para poder ocuparla en el emulador
         */

        /*
        PASOS PARA UTILIZAR Oauth
        1. Añadir permiso de internet
        2. Añadir actividad a manifest
         */
        /*
        <activity
            android:name="com.foursquare.android.nativeoauth.TokenExchangeActivity"
            android:theme="@android:style/Theme.Dialog" />
         */

        /*
        3. Añadir librería de Foursquare
        4. Añadir click listener al boton
        5. Validar que la app existe para lanzar intent
         */
        fsq = Foursquare(this)

        val btnLogin = findViewById<Button>(R.id.btnLogin)
        /*
        //Validar que la app existe para lanzar intent
        if (fsq?.hayToken()!!) {
            startActivity(Intent(this, SecondActivity::class.java))
            //fsq?.navegarSiguienteActividad()
            //finish()
        } */

        btnLogin.setOnClickListener {
                //fsq?.iniciarSesion()
            startActivity(Intent(this, SecondActivity::class.java))
        }
    }

    /*
    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        //super.onActivityResult(requestCode, resultCode, data)
        fsq?.validarActivityResult(requestCode, requestCode, data)
    } */

}