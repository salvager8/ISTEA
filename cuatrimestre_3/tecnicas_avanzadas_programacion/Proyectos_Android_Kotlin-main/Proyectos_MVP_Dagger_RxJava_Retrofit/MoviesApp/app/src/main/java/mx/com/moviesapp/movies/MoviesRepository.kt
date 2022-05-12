package mx.com.moviesapp.movies

import io.reactivex.rxjava3.core.Observable
import io.reactivex.rxjava3.functions.Consumer
import mx.com.moviesapp.http.MoviesAPIService
import mx.com.moviesapp.http.MoviesExtraInfoAPIsService
import mx.com.moviesapp.http.apimodel.Results
import mx.com.moviesapp.http.apimodel.TopMoviesRated

class MoviesRepository(private val mService: MoviesAPIService, private val eService: MoviesExtraInfoAPIsService): Repository {

    private var countries: MutableList<String> = ArrayList()
    private var results: MutableList<Results> = ArrayList()
    private var lastTimestamp: Long = System.currentTimeMillis()

    private fun isUpdated(): Boolean {
        return (System.currentTimeMillis() - lastTimestamp) < CACHE_LIFETIME
    }

    override fun getResultFromNetwork(): Observable<Results> {
        val topMoviesRatedObservable : Observable<TopMoviesRated> = mService.getTopMoviesRated(1)
            .concatWith(mService.getTopMoviesRated(2))
            .concatWith(mService.getTopMoviesRated(3))

        return topMoviesRatedObservable.concatMap {
            return@concatMap Observable.fromIterable(it.results)

        }.doOnNext {
            results.add(it)
        }
    }

    override fun getResultFromCache(): Observable<Results> {
        return if (isUpdated()) {
            Observable.fromIterable(results)
        } else {
            lastTimestamp = System.currentTimeMillis()
            results.clear()
            Observable.empty()
        }
    }

    override fun getResultData(): Observable<Results> {
        return getResultFromCache().switchIfEmpty(getResultFromNetwork())
    }

    override fun getCountryFromNetwork(): Observable<String> {
        return getResultFromNetwork().concatMap {
            return@concatMap eService.getExtraInfoMovie(it.title!!)
        }.concatMap {
            if (it == null || it.country == null) {
                return@concatMap Observable.just("Desconocido")
            } else {
                return@concatMap Observable.just(it.country)
            }
        }.doOnNext(Consumer {
            countries.add(it)
        })
    }

    override fun getCountryFromCache(): Observable<String> {
        return if (isUpdated()) {
            Observable.fromIterable(countries)
        } else {
            lastTimestamp = System.currentTimeMillis()
            countries.clear()
            Observable.empty()
        }
    }

    override fun getMovieCountryData(): Observable<String> {
        return getCountryFromCache().switchIfEmpty(getCountryFromNetwork())
    }

    companion object {
        private const val CACHE_LIFETIME: Long = 20 * 1000 //Caché que durara 20 segundos
    }

}