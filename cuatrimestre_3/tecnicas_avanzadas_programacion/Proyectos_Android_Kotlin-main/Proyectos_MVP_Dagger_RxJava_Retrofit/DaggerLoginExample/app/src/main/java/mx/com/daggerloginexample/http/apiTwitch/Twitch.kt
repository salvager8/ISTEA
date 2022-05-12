package mx.com.daggerloginexample.http.apiTwitch

import com.google.gson.annotations.SerializedName

data class Twitch (

	@SerializedName("data")
	val game : List<Game>? = null,
	@SerializedName("pagination")
	val pagination : Pagination
)