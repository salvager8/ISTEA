package mx.com.remoteconfigsoportetecnico

import androidx.appcompat.app.AppCompatActivity
import android.annotation.SuppressLint
import android.content.Context
import android.graphics.Color
import android.os.Bundle
import android.os.Handler
import android.view.MotionEvent
import android.view.View
import android.view.View.GONE
import android.view.View.VISIBLE
import android.view.inputmethod.InputMethodManager
import android.widget.Button
import android.widget.LinearLayout
import android.widget.TextView
import com.google.android.material.snackbar.Snackbar
import com.google.firebase.remoteconfig.FirebaseRemoteConfig
import com.google.firebase.remoteconfig.FirebaseRemoteConfigSettings
import kotlinx.android.synthetic.main.activity_registrar_turno.*
import mx.com.remoteconfigsoportetecnico.databinding.ActivityRegistrarTurnoBinding

/**
 * An example full-screen activity that shows and hides the system UI (i.e.
 * status bar and navigation/system bar) with user interaction.
 */
class FullscreenActivity : AppCompatActivity() {

    private lateinit var binding: ActivityRegistrarTurnoBinding

    private lateinit var fullscreenContent: TextView

    private var mFirebaseRemoteConfig: FirebaseRemoteConfig? = null

    private val hideHandler = Handler()

    @SuppressLint("InlinedApi")
    private val hidePart2Runnable = Runnable {
        // Delayed removal of status and navigation bar

        // Note that some of these constants are new as of API 16 (Jelly Bean)
        // and API 19 (KitKat). It is safe to use them, as they are inlined
        // at compile-time and do nothing on earlier devices.
        fullscreenContent.systemUiVisibility =
                View.SYSTEM_UI_FLAG_LOW_PROFILE or
                        View.SYSTEM_UI_FLAG_FULLSCREEN or
                        View.SYSTEM_UI_FLAG_LAYOUT_STABLE or
                        View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY or
                        View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION or
                        View.SYSTEM_UI_FLAG_HIDE_NAVIGATION
    }

    private val hideRunnable = Runnable { hide() }

    @SuppressLint("ClickableViewAccessibility")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityRegistrarTurnoBinding.inflate(layoutInflater)
        setContentView(binding.root)
        supportActionBar?.setDisplayHomeAsUpEnabled(true)

        // Set up the user interaction to manually show or hide the system UI.
        fullscreenContent = findViewById(R.id.fullscreen_content)

        // Upon interacting with UI controls, delay any scheduled hide()
        // operations to prevent the jarring behavior of controls going away
        // while interacting with the UI.
        //findViewById<Button>(R.id.dummy_button).setOnTouchListener(delayHideTouchListener)

        configFirebaseRemoteConfig()

        binding.btnRequest.setOnClickListener{
            btnRequestClicked()
        }
    }

    private fun configFirebaseRemoteConfig() {
        mFirebaseRemoteConfig = FirebaseRemoteConfig.getInstance()

        val configSettings = FirebaseRemoteConfigSettings.Builder()
            .setMinimumFetchIntervalInSeconds(1)
            .build()

        mFirebaseRemoteConfig?.setConfigSettingsAsync(configSettings)

        mFirebaseRemoteConfig?.setDefaultsAsync(R.xml.remove_config_defaults)

        configFetch()
    }

    private fun configFetch() {
        mFirebaseRemoteConfig?.fetch(0)?.addOnCompleteListener { task ->
            if (task.isSuccessful) {
                mFirebaseRemoteConfig?.activate()
                Snackbar.make(fullscreenContent, R.string.registrarTurno_message_config_remote, Snackbar.LENGTH_LONG).show()
            } else {
                Snackbar.make(fullscreenContent, R.string.registrarTurno_message_config_local, Snackbar.LENGTH_LONG).show()
            }
        }

        displayMainMessage()
    }

    private fun displayMainMessage() {
        val value: String =  mFirebaseRemoteConfig?.getString(F_SHOW_NAME)!!
        if (value == "true") {
            binding.editTextName.visibility = VISIBLE
        } else {
            binding.editTextName.visibility = GONE
        }

        var messageRemote: String = mFirebaseRemoteConfig?.getString(F_MAIN_MESSAGE)!!
        //eliminamos el doble salto de linea "\\"
        messageRemote = messageRemote.replace("\\n", "\n")
        fullscreenContent.text = messageRemote

        configColorsRemote()
    }

    private fun configColorsRemote() {
        val colorPrimaryRemote: String = mFirebaseRemoteConfig?.getString(F_COLOR_PRIMARY)!!
        val colorTextMessageRemote: String = mFirebaseRemoteConfig?.getString(F_COLOR_TEXT_MESSAGE)!!
        val colorButtonRemote: String = mFirebaseRemoteConfig?.getString(F_COLOR_BUTTON)!!

        binding.contentMain.setBackgroundColor(Color.parseColor(colorPrimaryRemote))
        fullscreenContent.setTextColor(Color.parseColor(colorTextMessageRemote))
        binding.btnRequest.setBackgroundColor(Color.parseColor(colorButtonRemote))
    }

    //Función del boton una vez que pulsamos click se oculta el teclado
    private fun btnRequestClicked() {
        val view: View? = this.currentFocus
        if (view != null) {
            val imm: InputMethodManager = getSystemService(Context.INPUT_METHOD_SERVICE) as InputMethodManager
            if (imm != null) {
                imm.hideSoftInputFromWindow(view.windowToken, 0)
            }
        }
        //reasignamos el foco como cuando iniciamos la app
        fullscreen_content_controls.requestFocus()
        //limpiamos los campos de nombre y telefono
        binding.editTextName.setText("")
        binding.editTextPhone.setText("")
        //mandamos un mensaje de que fue exitosa
        fullscreenContent.setText(R.string.registrarTurno_message_success)
    }

    override fun onPostCreate(savedInstanceState: Bundle?) {
        super.onPostCreate(savedInstanceState)

        // Trigger the initial hide() shortly after the activity has been
        // created, to briefly hint to the user that UI controls
        // are available.
        delayedHide(100)
    }

    private fun hide() {
        // Hide UI first
        supportActionBar?.hide()

        hideHandler.postDelayed(hidePart2Runnable, 100)
    }

    /**
     * Schedules a call to hide() in [delayMillis], canceling any
     * previously scheduled calls.
     */
    private fun delayedHide(delayMillis: Int) {
        hideHandler.removeCallbacks(hideRunnable)
        hideHandler.postDelayed(hideRunnable, delayMillis.toLong())
    }

    companion object {
        private const val F_MAIN_MESSAGE = "main_message"
        private const val F_SHOW_NAME = "show_name"
        private const val F_COLOR_PRIMARY = "color_primary"
        private const val F_COLOR_TEXT_MESSAGE = "color_text_message"
        private const val F_COLOR_BUTTON = "color_button"
    }

}