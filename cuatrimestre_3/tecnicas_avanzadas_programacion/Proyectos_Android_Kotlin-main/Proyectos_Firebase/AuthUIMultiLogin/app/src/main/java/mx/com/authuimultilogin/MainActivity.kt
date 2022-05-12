package mx.com.authuimultilogin

import android.content.Intent
import android.content.pm.PackageInfo
import android.content.pm.PackageManager
import android.content.pm.Signature
import android.graphics.Bitmap
import android.graphics.ImageDecoder
import android.net.Uri
import android.os.Build
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.provider.MediaStore
import android.util.Base64
import android.util.Log
import android.view.Menu
import android.view.MenuInflater
import android.view.MenuItem
import android.view.View
import android.widget.Toast
import com.bumptech.glide.Glide
import com.bumptech.glide.load.engine.DiskCacheStrategy
import com.bumptech.glide.request.RequestOptions
import com.firebase.ui.auth.AuthMethodPickerLayout
import com.firebase.ui.auth.AuthUI
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.auth.FirebaseUser
import com.google.firebase.auth.UserProfileChangeRequest
import com.google.firebase.storage.FirebaseStorage
import com.google.firebase.storage.StorageReference
import com.google.firebase.storage.UploadTask
import mx.com.authuimultilogin.databinding.ActivityMainBinding
import java.io.ByteArrayOutputStream
import java.security.MessageDigest
import java.security.NoSuchAlgorithmException

class MainActivity : AppCompatActivity() {

    private lateinit var binding: ActivityMainBinding

    private var mFirebaseAuth: FirebaseAuth?= null
    private var mAuthStateListener: FirebaseAuth.AuthStateListener? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        mFirebaseAuth = FirebaseAuth.getInstance()

        mAuthStateListener = FirebaseAuth.AuthStateListener { firebaseAuth ->
            val user: FirebaseUser? = firebaseAuth.currentUser

            if (user != null) {
                onSetDataUSer(user.displayName, user.email, user.providerData[1].providerId)

                loadImage(user.photoUrl)

            } else {
                //Método que limpia la cache cuando cierras sesion
                onSignedOutCleanUp()

                val facebookIdp: AuthUI.IdpConfig = AuthUI.IdpConfig.FacebookBuilder()
                    //.setPermissions(arrayListOf("user_friends", "user_gender"))
                    .build()
                val googleIdp: AuthUI.IdpConfig = AuthUI.IdpConfig.GoogleBuilder()
                    .build()

                //Nueva interfaz personalizada de LOGIN
                val customLayout: AuthMethodPickerLayout = AuthMethodPickerLayout
                    .Builder(R.layout.custom_view_login)
                    .setEmailButtonId(R.id.btnEmail)
                    .setGoogleButtonId(R.id.btnGoogle)
                    .setFacebookButtonId(R.id.btnFacebook)
                    .setTosAndPrivacyPolicyId(R.id.textViewPolicy)
                    .build()

                startActivityForResult(AuthUI.getInstance()
                    .createSignInIntentBuilder()
                    .setIsSmartLockEnabled(false)
                        //Aqui se agregan links con nuestras politicas de privacidad
                    .setTosAndPrivacyPolicyUrls("https://example.com/terms.html", //terminos y condiciones
                        "http://tesorodejava.es/privacy.html") //politica de privacidad
                    .setAvailableProviders(arrayListOf(AuthUI.IdpConfig.EmailBuilder().build(),
                        facebookIdp, googleIdp))
                    //.setTheme(R.style.GreenTheme)
                    .setLogo(R.drawable.img_multi_login)
                    .setAuthMethodPickerLayout(customLayout)
                    .build(), RC_SIGN_IN)
            }
        }

        binding.imageViewPhotoProfile.setOnClickListener(selectPhoto)

        //Código para generar el hashCode que solicita Facebook
        try {
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P) {
                val info: PackageInfo = packageManager.getPackageInfo(
                    "mx.com.authuimultilogin",
                    PackageManager.GET_SIGNING_CERTIFICATES
                )
                for (signature: Signature in info.signingInfo.apkContentsSigners) {
                    val md: MessageDigest = MessageDigest.getInstance("SHA")
                    md.update(signature.toByteArray())
                    Log.d("KeyHash: ", Base64.encodeToString(md.digest(), Base64.DEFAULT))
                }
            } else {
                val info: PackageInfo = packageManager.getPackageInfo(
                    "mx.com.authuimultilogin",
                    PackageManager.GET_SIGNATURES
                )
                for (signature: Signature in info.signatures) {
                    val md: MessageDigest = MessageDigest.getInstance("SHA")
                    md.update(signature.toByteArray())
                    Log.d("KeyHash: ", Base64.encodeToString(md.digest(), Base64.DEFAULT))
                }
            }
        } catch (e: PackageManager.NameNotFoundException) {
        } catch (e: NoSuchAlgorithmException) {
        }

    }

    private fun onSignedOutCleanUp() {
        onSetDataUSer("", "", "")
    }

    private fun onSetDataUSer(userName: String?, email: String?, provider: String) {
        binding.textViewUserName.text = userName
        binding.textViewEmail.text = email

        var prov: String = provider
        val drawableRes:Int

        when(provider) {
            //Password_Firebase es la opcion de "ingresar por email"
            PASSWORD_FIREBASE -> {
                drawableRes = R.drawable.ic_firebase
            }
            FACEBOOK -> {
                drawableRes = R.drawable.ic_facebook
            }
            GOOGLE -> {
                drawableRes = R.drawable.ic_google
            }
            else -> {
                drawableRes = R.drawable.ic_block
                prov = PROVEEDOR_DESCONOCIDO
            }
        }
        binding.textViewProvider.setCompoundDrawablesRelativeWithIntrinsicBounds(drawableRes, 0,0,0)
        binding.textViewProvider.text = prov
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)
        if (requestCode == RC_SIGN_IN) {
            if (resultCode == RESULT_OK) {
                Toast.makeText(this, "Bienvenido...", Toast.LENGTH_SHORT).show()
            } else {
                Toast.makeText(this, "Algo fallo, intente de nuevo.", Toast.LENGTH_SHORT).show()
            }
        } else if (requestCode == RC_FROM_GALLERY && resultCode == RESULT_OK) {

            if (true) {
                uploadImageTask(data?.data)
            } else {
                uploadImageFile(data?.data)
            }
        }
    }

    private fun uploadImageTask(selectedImageUri: Uri?) {
        val storage: FirebaseStorage = FirebaseStorage.getInstance()
        val reference: StorageReference = storage.reference.child(PATH_PROFILE).child(MY_PHOTO_AUTH)

        var bitmap: Bitmap

        try {
            if(Build.VERSION.SDK_INT < 28) {
                bitmap = MediaStore.Images.Media.getBitmap(this.contentResolver, selectedImageUri)
                //binding.imageViewPhoto.setImageBitmap(bitmap)
            } else {
                val source = ImageDecoder.createSource(this.contentResolver, selectedImageUri!!)
                bitmap = ImageDecoder.decodeBitmap(source)
                //binding.imageViewPhoto.setImageBitmap(bitmap)
            }

            bitmap = getResizedBitmap(bitmap, 480)
            val baos = ByteArrayOutputStream()
            bitmap.compress(Bitmap.CompressFormat.JPEG, 100, baos) //comprimos la imagen a un formato jpeg
            val data: ByteArray = baos.toByteArray()

            val uploadTask: UploadTask = reference.putBytes(data)

            uploadTask.addOnSuccessListener {
                reference.downloadUrl.addOnSuccessListener { uri ->
                    val user: FirebaseUser? = FirebaseAuth.getInstance().currentUser
                    if (user != null) {
                        val request: UserProfileChangeRequest = UserProfileChangeRequest.Builder()
                            .setPhotoUri(uri)
                            .build()
                        user.updateProfile(request).addOnCompleteListener {task ->
                            if (task.isSuccessful) {
                                loadImage(user.photoUrl)
                            }
                        }
                    }
                }
            }.addOnFailureListener {
                Toast.makeText(this, "Error...", Toast.LENGTH_SHORT).show()
            }
        } catch (e: Exception) {
        }
    }

    private fun getResizedBitmap(bitmap: Bitmap, maxSize: Int): Bitmap {
        var width: Int = bitmap.width
        var height: Int = bitmap.height

        if (width <= maxSize && height <=  maxSize) {
            return bitmap
        }

        val bitmapRatio: Float = (width / height).toFloat()
        if (bitmapRatio > 1) {
            width = maxSize
            height = (width / bitmapRatio).toInt()
        } else {
            height = maxSize
            width = (height * bitmapRatio).toInt()
        }

        return Bitmap.createScaledBitmap(bitmap, width, height, true)
    }

    private fun uploadImageFile(selectedImageUri: Uri?) {
        val storage: FirebaseStorage = FirebaseStorage.getInstance()
        val reference: StorageReference = storage.reference.child(PATH_PROFILE).child(MY_PHOTO_AUTH)

        if (selectedImageUri != null) {
            reference.putFile(selectedImageUri).addOnSuccessListener {
                reference.downloadUrl.addOnSuccessListener { uri ->
                    val user: FirebaseUser? = FirebaseAuth.getInstance().currentUser
                    if (user != null) {
                        val request: UserProfileChangeRequest = UserProfileChangeRequest.Builder()
                            .setPhotoUri(uri)
                            .build()
                        user.updateProfile(request).addOnCompleteListener {task ->
                            if (task.isSuccessful) {
                                loadImage(user.photoUrl)
                            }
                        }
                    }
                }
            }.addOnFailureListener {
                Toast.makeText(this, "Error...", Toast.LENGTH_SHORT).show()
            }
        }
    }

    private fun loadImage(photoUrl: Uri?) {
        val options: RequestOptions = RequestOptions()
            .diskCacheStrategy(DiskCacheStrategy.ALL)
            .centerCrop()

        Glide.with(this)
            .load(photoUrl)
            .apply(options)
            .into(binding.imageViewPhotoProfile)
    }

    override fun onResume() {
        super.onResume()
        mFirebaseAuth?.addAuthStateListener(mAuthStateListener!!)
    }

    override fun onPause() {
        super.onPause()
        if (mAuthStateListener != null) {
            mFirebaseAuth?.removeAuthStateListener(mAuthStateListener!!)
        }
    }

    override fun onCreateOptionsMenu(menu: Menu?): Boolean {
        val inflater: MenuInflater = menuInflater
        inflater.inflate(R.menu.main_menu, menu)
        return super.onCreateOptionsMenu(menu)
    }

    override fun onOptionsItemSelected(item: MenuItem): Boolean {
        when(item.itemId) {
            R.id.action_sign_out -> {
                AuthUI.getInstance().signOut(this)
                return true
            } else -> {
            return super.onOptionsItemSelected(item)
        }
        }
    }

    //Función cambiar foto de perfil
    private val selectPhoto = View.OnClickListener {
        val intent = Intent(Intent.ACTION_PICK, MediaStore.Images.Media.EXTERNAL_CONTENT_URI)
        startActivityForResult(intent, RC_FROM_GALLERY)
    }

    companion object {
        private const val RC_SIGN_IN = 123
        private const val PROVEEDOR_DESCONOCIDO = "Proveedor desconocido"
        private const val PASSWORD_FIREBASE = "password"
        private const val FACEBOOK = "facebook.com"
        private const val GOOGLE = "google.com"
        private const val RC_FROM_GALLERY = 124
        private const val PATH_PROFILE = "profile"
        private const val MY_PHOTO_AUTH = "my_photo_auth"

    }

}