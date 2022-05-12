package mx.com.moviesapp.root

import dagger.Component
import mx.com.moviesapp.MainActivity
import mx.com.moviesapp.http.MovieExtraInfoAPIModule
import mx.com.moviesapp.http.MovieTitleAPIModule
import mx.com.moviesapp.movies.MoviesModule
import javax.inject.Singleton

@Singleton
@Component(modules = [ApplicationModule::class, MoviesModule::class, MovieTitleAPIModule::class, MovieExtraInfoAPIModule::class])
interface ApplicationComponent {
    fun inject(target: MainActivity)
}