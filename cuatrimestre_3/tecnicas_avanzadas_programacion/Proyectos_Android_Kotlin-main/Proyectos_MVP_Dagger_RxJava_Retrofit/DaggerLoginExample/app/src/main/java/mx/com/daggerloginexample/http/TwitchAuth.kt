package mx.com.daggerloginexample.http

import mx.com.daggerloginexample.http.apiTwitch.Token
import retrofit2.Call
import retrofit2.http.POST
import retrofit2.http.Query

interface TwitchAuth {

    @POST("oauth2/token")
    fun authentication(@Query("client_id") clientId: String, @Query("client_secret") clientSecret: String, @Query("grant_type") grantType: String): Call<Token>
    /*
    @POST("oauth2/token")
    fun authenticationObservable(@Query("client_id") clientId: String, @Query("client_secret") clientSecret: String, @Query("grant_type") grantType: String): Observable<Token>
     */
}