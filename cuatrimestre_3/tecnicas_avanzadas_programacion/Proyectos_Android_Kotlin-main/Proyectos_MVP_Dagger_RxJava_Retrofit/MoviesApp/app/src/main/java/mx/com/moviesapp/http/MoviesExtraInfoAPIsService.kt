package mx.com.moviesapp.http

import io.reactivex.rxjava3.core.Observable
import mx.com.moviesapp.http.apimodel.OmdbAPI
import retrofit2.http.GET
import retrofit2.http.Query

interface MoviesExtraInfoAPIsService {

    @GET("/")
    fun getExtraInfoMovie(@Query("t") title: String): Observable<OmdbAPI>

}