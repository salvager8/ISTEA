package mx.com.rlr.solicitudeshttp

import android.content.Context
import android.net.ConnectivityManager
import android.net.NetworkCapabilities
import androidx.appcompat.app.AppCompatActivity

class Network {

    companion object {
        fun hayRed(activity: AppCompatActivity):Boolean {
            val connectivityManager = activity.getSystemService(Context.CONNECTIVITY_SERVICE) as ConnectivityManager
            val networkInfo = connectivityManager.getNetworkCapabilities(connectivityManager.activeNetwork)
            val estaConectado:Boolean? = networkInfo?.hasCapability(NetworkCapabilities.NET_CAPABILITY_INTERNET)
            return networkInfo != null && estaConectado == true
        }
    }

}