package mx.com.moviesapp.http.apimodel

import com.google.gson.annotations.SerializedName

//The Movie DataBase API
class TopMoviesRated {
    @SerializedName("page") val page : Int? = null
    @SerializedName("total_results") val total_results : Int? = null
    @SerializedName("total_pages") val total_pages : Int? = null
    @SerializedName("results") val results : List<Results>? = null
}