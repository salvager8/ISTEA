package mx.com.moviesapp.root

import android.app.Application
import mx.com.moviesapp.http.MovieExtraInfoAPIModule
import mx.com.moviesapp.http.MovieTitleAPIModule
import mx.com.moviesapp.movies.MoviesModule

class App: Application() {

    private lateinit var component: ApplicationComponent

    override fun onCreate() {
        super.onCreate()

        component = DaggerApplicationComponent.builder()
            .applicationModule(ApplicationModule(this))
            .moviesModule(MoviesModule())
            .movieTitleAPIModule(MovieTitleAPIModule())
            .movieExtraInfoAPIModule(MovieExtraInfoAPIModule())
            .build()
    }

    fun getComponent(): ApplicationComponent {
        return component
    }

}