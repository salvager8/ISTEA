package mx.com.moviesapp.movies

import io.reactivex.rxjava3.core.Observable
import mx.com.moviesapp.http.apimodel.Results

interface Repository {

    fun getResultFromNetwork(): Observable<Results>
    fun getResultFromCache(): Observable<Results>
    fun getResultData(): Observable<Results>

    fun getCountryFromNetwork(): Observable<String>
    fun getCountryFromCache(): Observable<String>
    fun getMovieCountryData(): Observable<String>

}