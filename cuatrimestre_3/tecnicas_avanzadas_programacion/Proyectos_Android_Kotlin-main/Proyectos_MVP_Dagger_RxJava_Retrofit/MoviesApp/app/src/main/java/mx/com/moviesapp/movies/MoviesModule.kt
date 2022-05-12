package mx.com.moviesapp.movies

import dagger.Module
import dagger.Provides
import mx.com.moviesapp.http.MoviesAPIService
import mx.com.moviesapp.http.MoviesExtraInfoAPIsService
import javax.inject.Singleton

@Module
class MoviesModule {

    @Provides
    fun provideMoviesPresenter(moviesModel: MoviesMVP.Model): MoviesMVP.Presenter {
        return MoviesPresenter(moviesModel)
    }

    @Provides
    fun provideMovieModel(repository: Repository): MoviesMVP.Model {
        return MoviesModel(repository)
    }

    @Singleton
    @Provides
    fun provideMoviesRepository(moviesAPIService: MoviesAPIService, extraInfoAPIsService: MoviesExtraInfoAPIsService): Repository {
        return MoviesRepository(moviesAPIService, extraInfoAPIsService)
    }

}