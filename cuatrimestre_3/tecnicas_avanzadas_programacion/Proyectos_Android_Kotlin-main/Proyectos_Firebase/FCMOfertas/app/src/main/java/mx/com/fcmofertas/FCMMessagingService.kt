package mx.com.fcmofertas

import android.app.NotificationChannel
import android.app.NotificationManager
import android.app.PendingIntent
import android.content.Context
import android.content.Intent
import android.media.RingtoneManager
import android.os.Build
import android.util.Log
import androidx.core.app.NotificationCompat
import androidx.core.content.ContextCompat
import com.google.firebase.messaging.FirebaseMessagingService
import com.google.firebase.messaging.RemoteMessage

class FCMMessagingService : FirebaseMessagingService() {

    override fun onMessageReceived(remoteMessage: RemoteMessage) {
        super.onMessageReceived(remoteMessage)

        if (remoteMessage.data.size > 0 && remoteMessage.notification != null ) {
            sendNotification(remoteMessage)
        }
    }

    private fun sendNotification(remoteMessage: RemoteMessage) {
        val desc: Float = (remoteMessage.data.get(DESCUENTO))!!.toFloat()
        //val desc = java.lang.Float.valueOf(remoteMessage.data[DESCUENTO]!!)

        //SEND NOTIFICATION
        val intent = Intent(this, MainActivity::class.java)
        intent.putExtra(DESCUENTO, desc)
        intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP)

        val pendingIntent = PendingIntent.getActivity(
            this,
            0 /* Request code */,
            intent,
            PendingIntent.FLAG_ONE_SHOT
        )

        val notificationManager = getSystemService(Context.NOTIFICATION_SERVICE) as NotificationManager

        val defaultSoundUri = RingtoneManager.getDefaultUri(RingtoneManager.TYPE_NOTIFICATION)

        //val channelId = getString(R.string.normal_channel_id)
        //val channelName = getString(R.string.normal_channel_name)
        val channelId: String
        val channelName: String

        if (desc < .10) { //Esto no funciona
            channelId = getString(R.string.low_channel_id)
            channelName = getString(R.string.low_channel_name)
        } else {
            channelId = getString(R.string.normal_channel_id)
            channelName = getString(R.string.normal_channel_name)
        }

        val notificationBuilder = NotificationCompat.Builder(this, channelId)
            .setSmallIcon(R.drawable.ic_stat_name)
            .setContentTitle(remoteMessage.notification?.title)
            .setContentText(remoteMessage.notification?.body)
            .setAutoCancel(true)
            .setSound(defaultSoundUri)
            .setContentIntent(pendingIntent)

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.LOLLIPOP_MR1) {
            if (desc > .4) {
                notificationBuilder.color = ContextCompat.getColor(
                    applicationContext,
                    R.color.colorPrimary
                )
                notificationBuilder.color = ContextCompat.getColor(
                    applicationContext,
                    R.color.colorAccent
                )
            }
        }

        // Since android Oreo notification channel is needed.
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
            val channel = NotificationChannel(
                channelId,
                channelName,
                NotificationManager.IMPORTANCE_DEFAULT
            )
            channel.enableVibration(true)
            channel.vibrationPattern = LongArray(4) {100; 200; 200; 50}
            if (notificationManager != null) {
                notificationManager.createNotificationChannel(channel)
            }
            notificationBuilder.setChannelId(channelId)
        }

        if (notificationManager != null) {
            notificationManager.notify(0 /* ID of notification */, notificationBuilder.build())
        }
    }

    // [START on_new_token]
    override fun onNewToken(token: String) {
        //Log.d("FCMMessagingService", "Refreshed token: $token")
        super.onNewToken(token)
        sendRegistrationToServer(token)
    }

    private fun sendRegistrationToServer(token: String?) {
        Log.d("newToken", token)
    }

    companion object {
        private const val DESCUENTO = "descuento"
    }
}
