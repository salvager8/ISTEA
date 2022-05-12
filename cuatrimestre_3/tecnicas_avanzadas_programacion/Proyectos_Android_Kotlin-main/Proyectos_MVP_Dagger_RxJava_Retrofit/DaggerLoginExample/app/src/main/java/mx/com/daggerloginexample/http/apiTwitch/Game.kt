package mx.com.daggerloginexample.http.apiTwitch

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName

class Game (

	@SerializedName("id")
	@Expose
	val id : Int,
	@SerializedName("name")
	@Expose
	val name : String,
	@SerializedName("box_art_url")
	@Expose
	val box_art_url : String
)