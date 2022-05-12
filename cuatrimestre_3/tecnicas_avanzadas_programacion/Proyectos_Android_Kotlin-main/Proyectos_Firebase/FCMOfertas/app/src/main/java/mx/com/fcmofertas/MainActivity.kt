package mx.com.fcmofertas

import android.content.Context
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Spinner
import android.widget.TextView
import android.widget.Toast
import com.google.android.gms.tasks.OnCompleteListener
import com.google.android.gms.tasks.OnSuccessListener
import com.google.firebase.iid.FirebaseInstanceId
import com.google.firebase.iid.InstanceIdResult
import com.google.firebase.messaging.FirebaseMessaging
import mx.com.fcmofertas.databinding.ActivityMainBinding

class MainActivity : AppCompatActivity() {

    private var spinnerTopics: Spinner? = null

    private var mTopicsSet: MutableSet<String>? = null
    private var mSharedPreferences: SharedPreferences? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        val binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        spinnerTopics = findViewById(R.id.spinnerTopics)

        onTokenRefresh()

        //val topic: String = resources.getStringArray(R.array.topicsValues)[spinnerTopics!!.selectedItemPosition]


        binding.btnSuscribir.setOnClickListener{
            val topic: String = resources.getStringArray(R.array.topicsValues)[spinnerTopics!!.selectedItemPosition]
            //val topic: String = spinnerTopics?.selectedItem.toString()
            if (!mTopicsSet!!.contains(topic)) {
                FirebaseMessaging.getInstance().subscribeToTopic(topic)
                mTopicsSet?.add(topic)
                saveSharedPreferences()
            }
        }

        binding.btnDesuscribir.setOnClickListener {
            val topic: String = resources.getStringArray(R.array.topicsValues)[spinnerTopics!!.selectedItemPosition]
            //val topic: String = spinnerTopics?.selectedItem.toString()
            if (mTopicsSet!!.contains(topic)) {
                FirebaseMessaging.getInstance().unsubscribeFromTopic(topic)
                mTopicsSet?.remove(topic)
                saveSharedPreferences()
            }
        }

        configSharedPreferences()
    }



    private fun configSharedPreferences() {
        mSharedPreferences = getPreferences(Context.MODE_PRIVATE)

        mTopicsSet = mSharedPreferences?.getStringSet(SP_TOPICS, HashSet<String>())

        showTopics()
    }

    private fun showTopics() {
        val textViewTopics: TextView = findViewById(R.id.textViewTopics)
        textViewTopics.text = mTopicsSet.toString()
    }

    private fun saveSharedPreferences() {
        val editor = mSharedPreferences?.edit()
        editor?.clear()
        editor?.putStringSet(SP_TOPICS, mTopicsSet)
        editor?.apply()

        showTopics()
    }

    private fun onTokenRefresh() {
        // Get token
        // [START retrieve_current_token]
        FirebaseInstanceId.getInstance().instanceId
            .addOnCompleteListener(OnCompleteListener { task ->
                if (!task.isSuccessful) {
                    Log.w("MainActivity", "getInstanceId failed", task.exception)
                    return@OnCompleteListener
                }

                // Get new Instance ID token
                val token = task.result?.token

                // Log and toast
                //val msg = getString("InstanceID Token: %s", token)
                Log.d("tokenId", token)
                //Toast.makeText(baseContext, token, Toast.LENGTH_SHORT).show()
            })
        // [END retrieve_current_token]
    }

    companion object {
        private const val SP_TOPICS = "sharedPreferencesTopics"
    }
}