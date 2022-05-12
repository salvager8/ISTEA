package mx.com.storagemisfotografias

import android.Manifest
import android.content.Intent
import android.content.pm.PackageManager
import android.graphics.Bitmap
import android.graphics.ImageDecoder
import android.media.MediaScannerConnection
import android.net.Uri
import android.os.Build
import android.os.Bundle
import android.os.Environment
import android.provider.MediaStore
import android.view.View
import com.google.android.material.bottomnavigation.BottomNavigationView
import androidx.appcompat.app.AppCompatActivity
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import androidx.core.content.FileProvider
import com.bumptech.glide.Glide
import com.bumptech.glide.load.engine.DiskCacheStrategy
import com.bumptech.glide.request.RequestOptions
import com.google.android.material.snackbar.Snackbar
import com.google.firebase.database.*
import com.google.firebase.storage.FirebaseStorage
import com.google.firebase.storage.StorageReference
import mx.com.storagemisfotografias.databinding.ActivityMainBinding
import java.io.File
import java.io.IOException
import java.text.SimpleDateFormat
import java.util.*

class MainActivity : AppCompatActivity() {

    private lateinit var binding: ActivityMainBinding

    private var mStorageReference: StorageReference? = null
    private var mDatabaseReference: DatabaseReference? = null

    private var mCurrentPhotoPath: String? = null
    private var mPhotoSelectedUri: Uri? = null

    private val mOnNavigationItemSelectedListener = BottomNavigationView.OnNavigationItemSelectedListener {
        when(it.itemId) {
            R.id.navigation_gallery -> {
                binding.texViewMessage.setText(R.string.title_galley)

                //fromGallery()
                checkPermissionToApp(Manifest.permission.READ_EXTERNAL_STORAGE, RP_STORAGE)

                return@OnNavigationItemSelectedListener true
            }
            R.id.navigation_camera -> {
                binding.texViewMessage.setText(R.string.title_camera)
                //fromCamera()
                //dispatchTakePictureIntent()
                checkPermissionToApp(Manifest.permission.CAMERA, RP_CAMERA)
                return@OnNavigationItemSelectedListener true
            }
            else -> return@OnNavigationItemSelectedListener false
        }
    }

    private fun checkPermissionToApp(permissionStr: String, requestPermission: Int) {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {
            if (ContextCompat.checkSelfPermission(this, permissionStr) != PackageManager.PERMISSION_GRANTED) {
                    ActivityCompat.requestPermissions(this, arrayOf(permissionStr), requestPermission)
                return
            }
        }

        when(requestPermission) {
            RP_STORAGE -> {
                fromGallery()
            }
            RP_CAMERA -> {
                dispatchTakePictureIntent()
            }
        }
    }

    override fun onRequestPermissionsResult(
        requestCode: Int,
        permissions: Array<out String>,
        grantResults: IntArray
    ) {
        if (grantResults.isNotEmpty() && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
            when(requestCode) {
                RP_STORAGE -> {
                    fromGallery()
                }
                RP_CAMERA -> {
                    dispatchTakePictureIntent()
                }
            }
        }
        super.onRequestPermissionsResult(requestCode, permissions, grantResults)
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val navView: BottomNavigationView = findViewById(R.id.nav_view)
        navView.setOnNavigationItemSelectedListener(mOnNavigationItemSelectedListener)

        configFirebase()

        configPhotoProfile()
    }

    private fun fromGallery() {
        val intent = Intent(Intent.ACTION_PICK, MediaStore.Images.Media.EXTERNAL_CONTENT_URI)
        startActivityForResult(intent, RC_GALLERY)
    }

    private fun fromCamera() {
        val intent = Intent(MediaStore.ACTION_IMAGE_CAPTURE)
        startActivityForResult(intent, RC_CAMERA)
    }

    private fun dispatchTakePictureIntent() {
        val takePictureIntent = Intent(MediaStore.ACTION_IMAGE_CAPTURE)
        if (takePictureIntent.resolveActivity(packageManager) != null) {
            val photoFile: File = createImageFile()

            if (photoFile != null) {
                val photoUri: Uri = FileProvider.getUriForFile(this, "mx.com.storagemisfotografias", photoFile)
                takePictureIntent.putExtra(MediaStore.EXTRA_OUTPUT, photoUri)
                startActivityForResult(takePictureIntent, RC_CAMERA)
            }
        }
    }

    private fun createImageFile(): File {
        val timeStamp: String = SimpleDateFormat("dd-MM-yyyy_HHmmss", Locale.ROOT).format(Date())
        val imageFileName: String = MY_PHOTO + timeStamp + "_"
        val storageDir: File = getExternalFilesDir(Environment.DIRECTORY_PICTURES)!! //Solo la imagen estara guardada en la aplicacion

        var image: File? = null
        try {
            image = File.createTempFile(imageFileName, ".jpg", storageDir)
            mCurrentPhotoPath = image.absolutePath
        } catch (e: IOException) {
            e.printStackTrace()
        }

        return image!!
    }

    private fun configFirebase() {
        mStorageReference = FirebaseStorage.getInstance().reference

        val database: FirebaseDatabase = FirebaseDatabase.getInstance()

        mDatabaseReference = database.reference.child(PATH_PROFILE).child(PATH_PHOTO_URL)
    }

    private fun configPhotoProfile() {
        //CARGAR LA IMAGEN POR STORAGE(Conviene en casos más privados como la foto de perfil)
        mStorageReference?.child(PATH_PROFILE)?.child(MY_PHOTO)?.downloadUrl?.addOnSuccessListener { uri ->
            val options : RequestOptions = RequestOptions()
            .centerCrop()
            .diskCacheStrategy(DiskCacheStrategy.ALL)

        Glide.with(this).load(uri)
            .apply(options)
            .into(binding.imageViewPhoto)
        binding.btnDelete.visibility = View.VISIBLE

        binding.btnDelete.visibility = View.VISIBLE
        }?.addOnFailureListener { e ->
            binding.btnDelete.visibility = View.GONE
            Snackbar.make(binding.container, R.string.main_message_error_notFound, Snackbar.LENGTH_LONG).show()
        }

        //CARGAR LA IMAGEN POR DATABASE(Carga de una URL, conviene en casos que se cargue de internet o de algo no privado)
        /*mDatabaseReference?.addListenerForSingleValueEvent(object : ValueEventListener {
            override fun onDataChange(snapshot: DataSnapshot) {
                val options : RequestOptions = RequestOptions()
                    .centerCrop()
                    .diskCacheStrategy(DiskCacheStrategy.ALL)

                Glide.with(applicationContext).load(snapshot.value)
                    .apply(options)
                    .into(binding.imageViewPhoto)
                binding.btnDelete.visibility = View.VISIBLE
            }

            override fun onCancelled(error: DatabaseError) {
                binding.btnDelete.visibility = View.GONE
                Snackbar.make(binding.container, R.string.main_message_error_notFound, Snackbar.LENGTH_LONG).show()
            }
        })*/
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)

        if (resultCode == RESULT_OK) {
            when(requestCode) {
                RC_GALLERY -> {
                    if (data != null) {
                        mPhotoSelectedUri = data.data
                        try {
                            mPhotoSelectedUri?.let {
                                if(Build.VERSION.SDK_INT < 28) {
                                    val bitmap: Bitmap = MediaStore.Images.Media.getBitmap(this.contentResolver, mPhotoSelectedUri)
                                    binding.imageViewPhoto.setImageBitmap(bitmap)
                                } else {
                                    val source = ImageDecoder.createSource(this.contentResolver, mPhotoSelectedUri!!)
                                    val bitmap: Bitmap = ImageDecoder.decodeBitmap(source)
                                    binding.imageViewPhoto.setImageBitmap(bitmap)
                                }
                                binding.btnDelete.visibility = View.GONE
                                binding.texViewMessage.setText(R.string.main_message_question_upload)
                            }
                        } catch (e: Exception) {
                            e.printStackTrace()
                        }
                    }
                }
                RC_CAMERA -> {
                    //Miniatura de una foto tomada(mala calidad)
                    /*val extras= data?.extras
                    val bitmap = extras!!.get("data") as Bitmap*/

                    mPhotoSelectedUri = addPicGallery()

                    try {
                        mPhotoSelectedUri?.let {
                            if(Build.VERSION.SDK_INT < 28) {
                                val bitmap: Bitmap = MediaStore.Images.Media.getBitmap(this.contentResolver, mPhotoSelectedUri)
                                binding.imageViewPhoto.setImageBitmap(bitmap)
                            } else {
                                val source = ImageDecoder.createSource(this.contentResolver, mPhotoSelectedUri!!)
                                val bitmap: Bitmap = ImageDecoder.decodeBitmap(source)
                                binding.imageViewPhoto.setImageBitmap(bitmap)
                            }
                            binding.btnDelete.visibility = View.GONE
                            binding.texViewMessage.setText(R.string.main_message_question_upload)
                        }
                    } catch (e: Exception) {
                        e.printStackTrace()
                    }
                }
            }
        }

        binding.btnUpload.setOnClickListener {
            btnUploadPhoto()
        }

        binding.btnDelete.setOnClickListener {
            btnDeletePhoto()
        }
    }

    private fun addPicGallery(): Uri? {
        /*val file = File(MY_PHOTO)
        MediaScannerConnection.scanFile(this, arrayOf(file.toString()),
            arrayOf(file.name), null)*/
        val mediaScanIntent = Intent(Intent.ACTION_MEDIA_SCANNER_SCAN_FILE)
        val file = File(mCurrentPhotoPath!!)
        val contentUri = Uri.fromFile(file)
        mediaScanIntent.data = contentUri
        this.sendBroadcast(mediaScanIntent)
        mCurrentPhotoPath = null
        return contentUri
    }

    private fun btnUploadPhoto() {
        val profileReference: StorageReference = mStorageReference!!.child(PATH_PROFILE)

        val photoReference: StorageReference = profileReference.child(MY_PHOTO)

        photoReference.putFile(mPhotoSelectedUri!!).addOnSuccessListener { taskSnapshot ->
            Snackbar.make(binding.container, R.string.main_message_upload_success, Snackbar.LENGTH_LONG).show()
            taskSnapshot.storage.downloadUrl.addOnSuccessListener {
                savePhotoUri(it)
                binding.btnDelete.visibility = View.VISIBLE
                binding.texViewMessage.setText(R.string.main_message_done)
            }
        }.addOnFailureListener { e ->
            Snackbar.make(binding.container, R.string.main_message_upload_error, Snackbar.LENGTH_LONG).show()
        }
    }

    private fun btnDeletePhoto() {
        mStorageReference?.child(PATH_PROFILE)?.child(MY_PHOTO)?.delete()?.addOnSuccessListener {
            mDatabaseReference?.removeValue()
            binding.imageViewPhoto.setImageBitmap(null)
            binding.btnDelete.visibility = View.GONE
            Snackbar.make(binding.container, R.string.main_message_delete_success, Snackbar.LENGTH_LONG).show()
        }?.addOnFailureListener {e ->
            Snackbar.make(binding.container, R.string.main_message_delete_error, Snackbar.LENGTH_LONG).show()
        }
    }

    private fun savePhotoUri(downloadUri: Uri?) {
        mDatabaseReference?.setValue(downloadUri.toString())
    }


    companion object {
        private const val RC_GALLERY = 21
        private const val RC_CAMERA = 22

        private const val RP_CAMERA = 121
        private const val RP_STORAGE = 122

        private const val IMAGE_DIRECTORY = "/MyPhotoApp"
        private const val MY_PHOTO = "my_photo"

        private const val PATH_PROFILE = "profile"
        private const val PATH_PHOTO_URL = "photoUrl"
    }
}