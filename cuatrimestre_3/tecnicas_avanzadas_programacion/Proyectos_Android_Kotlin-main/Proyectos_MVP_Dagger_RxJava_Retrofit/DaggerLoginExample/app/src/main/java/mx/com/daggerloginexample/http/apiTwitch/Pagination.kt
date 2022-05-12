package mx.com.daggerloginexample.http.apiTwitch

import com.google.gson.annotations.SerializedName

data class Pagination (

	@SerializedName("cursor")
	val cursor : String
)