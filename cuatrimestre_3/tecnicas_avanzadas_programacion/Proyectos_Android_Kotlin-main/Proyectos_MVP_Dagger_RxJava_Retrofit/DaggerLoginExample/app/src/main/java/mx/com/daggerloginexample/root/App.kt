package mx.com.daggerloginexample.root

import android.app.Application
import mx.com.daggerloginexample.http.TwitchModule
import mx.com.daggerloginexample.login.LoginModule

class App:Application() {

    private var component: ApplicationComponent? = null

    override fun onCreate() {
        super.onCreate()
        component = DaggerApplicationComponent.builder()
            .applicationModule(ApplicationModule(this))
            .loginModule(LoginModule())
            .twitchModule(TwitchModule())
            .build()
    }

    fun getComponent(): ApplicationComponent? {
        return component
    }

}