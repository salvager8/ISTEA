package mx.com.moviesapp.http

import io.reactivex.rxjava3.core.Observable
import mx.com.moviesapp.http.apimodel.TopMoviesRated
import retrofit2.http.GET
import retrofit2.http.Query

interface MoviesAPIService {

    @GET("movie/top_rated")
    fun getTopMoviesRated(@Query("page") page: Int): Observable<TopMoviesRated>

}