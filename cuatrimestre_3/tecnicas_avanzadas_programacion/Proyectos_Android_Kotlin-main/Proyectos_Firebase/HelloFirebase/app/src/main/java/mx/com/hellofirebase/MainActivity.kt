package mx.com.hellofirebase

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Toast
import com.google.firebase.database.*
import mx.com.hellofirebase.databinding.ActivityMainBinding

class MainActivity : AppCompatActivity() {

    private lateinit var binding: ActivityMainBinding

    override fun onCreate(savedInstanceState: Bundle?) {

        super.onCreate(savedInstanceState)

        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        //textViewMessage = findViewById(R.id.textViewMessage)

        val database: FirebaseDatabase = FirebaseDatabase.getInstance()
        val reference: DatabaseReference = database.getReference(PATH_START).child(PATH_MESSAGE)

        reference.addValueEventListener(object: ValueEventListener {
            override fun onDataChange(dataSnapshot: DataSnapshot) {
                binding.textViewMessage.text = dataSnapshot.getValue(String::class.java)
            }

            override fun onCancelled(databaseError: DatabaseError) {
                Toast.makeText(applicationContext, "Error al consultar en Firebase.", Toast.LENGTH_LONG).show()
            }
        })

        binding.btnSend.setOnClickListener {
            val database: FirebaseDatabase = FirebaseDatabase.getInstance()
            val reference: DatabaseReference = database.getReference(PATH_START).child(PATH_MESSAGE)

            reference.setValue(binding.editTextMessage.text.toString().trim())
            binding.editTextMessage.setText("")
        }

    }

    companion object {
        private const val PATH_START: String = "start"
        private const val PATH_MESSAGE: String = "message"
    }
}