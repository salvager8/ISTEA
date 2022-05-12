package mx.com.loginAPIejemplo

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.widget.ImageView
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import com.facebook.*
import com.facebook.login.LoginResult
import com.facebook.login.widget.LoginButton
import com.google.gson.Gson
import com.squareup.picasso.Picasso

class MainActivity : AppCompatActivity() {

    private var callbackManager:CallbackManager? = null
    private var profileTracker:ProfileTracker? = null
    private var accessToken:AccessToken? = null

    private var imageViewFoto:ImageView? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        callbackManager = CallbackManager.Factory.create()

        val loginButton = findViewById<LoginButton>(R.id.login_button)
        loginButton.setPermissions("email")

        val textViewNombre = findViewById<TextView>(R.id.textViewNombre)
        imageViewFoto = findViewById(R.id.imageViewFoto)

        // If using in a fragment
        //loginButton.setFragment(this)

        if (AccessToken.getCurrentAccessToken() != null && Profile.getCurrentProfile() != null) {
            accessToken = AccessToken.getCurrentAccessToken()
            textViewNombre.text = Profile.getCurrentProfile().firstName + " " + Profile.getCurrentProfile().lastName

            cargarFoto()
        }

        // Callback registration
        loginButton.registerCallback(callbackManager, object : FacebookCallback<LoginResult?> {
            override fun onSuccess(loginResult: LoginResult?) {
                // App code
                val accessToken = AccessToken.getCurrentAccessToken()
                val isLoggedIn = accessToken != null && !accessToken.isExpired

                //Log.d("ACCESS-TOKEN", accessToken.token)

                if (Profile.getCurrentProfile() == null) {
                    profileTracker = object : ProfileTracker() {
                        override fun onCurrentProfileChanged(
                            oldProfile: Profile?,
                            currentProfile: Profile?
                        ) {
                            Log.d("NOMBRE", currentProfile!!.firstName)
                            textViewNombre.text =
                                "${currentProfile.firstName} ${currentProfile.lastName}"
                            profileTracker?.stopTracking()
                        }
                    }
                } else {
                    val profile = Profile.getCurrentProfile()
                    Log.d("NOMBRE", profile!!.firstName)
                    textViewNombre.setText("${profile.firstName} ${profile.lastName}")
                }


            }

            override fun onCancel() {
                // App code
            }

            override fun onError(exception: FacebookException) {
                // App code
            }
        })
    }


    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        callbackManager?.onActivityResult(requestCode, resultCode, data)
        super.onActivityResult(requestCode, resultCode, data)
    }

    fun cargarFoto() {
        val request = GraphRequest.newMeRequest(
            this.accessToken
        ) { objeto, response ->
            // Application code
            //val url = response.jsonObject.getJSONObject("picture").getJSONObject("data").getString("url")
            var json = response.jsonObject.toString()

            val gson = Gson()
            val dataResponse = gson.fromJson(json, PicturesResponse::class.java)

            Log.d("GRAPH", dataResponse.picture?.data?.url)

            Picasso.get().load(dataResponse.picture?.data?.url).into(imageViewFoto)
        }
        val parameters = Bundle()
        parameters.putString("fields", "picture.height(300)")
        request.parameters = parameters
        request.executeAsync()
    }

}