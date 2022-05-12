package mx.com.ejemplocamara

import android.app.Activity
import android.content.ContentResolver
import android.content.ContentValues
import android.content.Intent
import android.content.pm.PackageManager
import android.graphics.Bitmap
import android.graphics.BitmapFactory
import android.media.MediaScannerConnection
import android.net.Uri
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.Environment
import android.provider.MediaStore
import android.widget.Button
import android.widget.ImageView
import android.widget.Toast
import androidx.core.app.ActivityCompat
import androidx.core.content.FileProvider
import java.io.File
import java.io.IOException
import java.io.OutputStream
import java.text.SimpleDateFormat
import java.util.*

class MainActivity : AppCompatActivity() {

    val SOLICITUD_TOMAR_FOTO = 1

    val permisoCamera = android.Manifest.permission.CAMERA
    val permisoWriteStorage = android.Manifest.permission.WRITE_EXTERNAL_STORAGE
    val permisoReadStorage = android.Manifest.permission.READ_EXTERNAL_STORAGE

    var imageViewFoto:ImageView? = null

    var urlFotoActual = ""

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val btnTomarFoto = findViewById<Button>(R.id.btnTomarFoto)

        imageViewFoto = findViewById(R.id.imageViewFoto)

        btnTomarFoto.setOnClickListener {
            //dispararIntentTomarFoto()
            //PEDIR PERMISOS PARA ABRIR LA CAMARA
            pedirPermisos()
        }
    }

    fun pedirPermisos() {
        val deboProveeContexto = ActivityCompat.shouldShowRequestPermissionRationale(this, permisoCamera)

        if (deboProveeContexto) {
            solicitudPermisos()
        } else {
            solicitudPermisos()
        }
    }

    fun solicitudPermisos() {
        requestPermissions(arrayOf(permisoCamera, permisoWriteStorage, permisoReadStorage), SOLICITUD_TOMAR_FOTO)
    }

    override fun onRequestPermissionsResult(
        requestCode: Int,
        permissions: Array<out String>,
        grantResults: IntArray
    ) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults)

        when(requestCode) {
            SOLICITUD_TOMAR_FOTO -> {
                if (grantResults.size > 0 &&
                    grantResults[0] == PackageManager.PERMISSION_GRANTED &&
                    grantResults[1] == PackageManager.PERMISSION_GRANTED &&
                    grantResults[2] == PackageManager.PERMISSION_GRANTED) {
                    //Tenemos permiso
                    dispararIntentTomarFoto()
                } else {
                    //No tenemos permiso
                    Toast.makeText(this, "No diste permiso para acceder a la cámara y almacenamiento", Toast.LENGTH_SHORT).show()
                }
            }
        }
    }

    fun dispararIntentTomarFoto() {
        val intent = Intent(MediaStore.ACTION_IMAGE_CAPTURE)

        if (intent.resolveActivity(packageManager) !=  null) {

            var archivoFoto:File? = try {
                crearArchivoImagen()
            } catch (ex:IOException) {
                //Error
                null
            }

            if (archivoFoto != null) {
                var urlFoto: Uri = FileProvider.getUriForFile(this, "mx.com.ejemplocamara.android.fileprovider", archivoFoto)

                intent.putExtra(MediaStore.EXTRA_OUTPUT, urlFoto)
                startActivityForResult(intent, SOLICITUD_TOMAR_FOTO)
            }
        }
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)

        when(requestCode) {
            SOLICITUD_TOMAR_FOTO -> {
                if (resultCode == Activity.RESULT_OK) {
                    //Obtener imagen
                    //Log.d("ACTIVITY RESULT", "Obtener imagen")

                    //Mostrar la imagen en el activity
                    /*
                    val extras = data?.extras
                    val imageBitmap = extras?.get("data") as Bitmap
                    */
                    mostrarBitmap(urlFotoActual)

                    anadirImagenGaleria()

                } else {
                    //Se cancelo la foto
                }
            }
        }
    }

    private fun mostrarBitmap(url:String){
        val uri = Uri.parse(url)
        val stream = contentResolver.openInputStream(uri)
        val imageBitmap = BitmapFactory.decodeStream(stream)
        imageViewFoto?.setImageBitmap(imageBitmap)
    }

    fun crearArchivoImagen():File {
        //Create an image file name
        val timeStamp = SimpleDateFormat("yyyyMMdd_HHmmss").format(Date())
        val nombreArchivoImagen = "JPEG_${timeStamp}_" //prefix

        val directorio = getExternalFilesDir(Environment.DIRECTORY_PICTURES)

        //val directorio = Environment.getExternalStorageDirectory()
        //val directorioPictures: File = File(directorio.absolutePath + "/Pictures/")


        return File.createTempFile(nombreArchivoImagen,".jpg", directorio).apply {
            //Guardar archivo
            urlFotoActual = "file://" + absolutePath
        }

        //urlFotoActual = "file://" + imagen.absolutePath
    }

    fun anadirImagenGaleria() {
        //val file = File(urlFotoActual)
        //MediaScannerConnection.scanFile(this, arrayOf(file.toString()), arrayOf(file.name), null)
        val intent = Intent(Intent.ACTION_MEDIA_SCANNER_SCAN_FILE)
        val file = File(urlFotoActual)
        val uri = Uri.fromFile(file)
        intent.setData(uri)
        sendBroadcast(intent)
    }

}