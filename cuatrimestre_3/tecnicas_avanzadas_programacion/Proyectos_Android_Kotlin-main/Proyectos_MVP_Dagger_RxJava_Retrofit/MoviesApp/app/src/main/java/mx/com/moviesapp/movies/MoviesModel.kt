package mx.com.moviesapp.movies

import io.reactivex.rxjava3.core.Observable
import io.reactivex.rxjava3.functions.BiFunction

class MoviesModel(private var repository: Repository): MoviesMVP.Model {

    override fun result(): Observable<ViewModel> {
        return Observable.zip(repository.getResultData(), repository.getMovieCountryData(), BiFunction { result, country ->
            return@BiFunction ViewModel(result.title!!, country)
        })
    }
}