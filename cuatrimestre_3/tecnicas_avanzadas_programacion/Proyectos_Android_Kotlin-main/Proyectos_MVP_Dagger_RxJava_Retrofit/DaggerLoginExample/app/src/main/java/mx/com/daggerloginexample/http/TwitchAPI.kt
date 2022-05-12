package mx.com.daggerloginexample.http

import io.reactivex.rxjava3.core.Observable
import mx.com.daggerloginexample.http.apiTwitch.Twitch
import retrofit2.Call
import retrofit2.http.GET
import retrofit2.http.Header

interface TwitchAPI {

    @GET("games/top")
    fun getTopGames(@Header("Authorization") authorization: String, @Header("Client-Id") clientId: String): Call<Twitch>

    @GET("games/top")
    fun getTopGamesObservable(@Header("Authorization") authorization: String, @Header("Client-Id") clientId: String): Observable<Twitch>
}