package mx.com.daggerloginexample.http.apiTwitch

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName

class Token (

    @SerializedName("access_token")
    @Expose
    val accessToken : String,
    @SerializedName("expires_in")
    @Expose
    val expiresIn : Int,
    @SerializedName("token_type")
    @Expose
    val tokenType : String

)