package mx.com.daggerloginexample.root

import dagger.Component
import mx.com.daggerloginexample.http.TwitchModule
import mx.com.daggerloginexample.login.LoginActivity
import mx.com.daggerloginexample.login.LoginModule
import javax.inject.Singleton

@Singleton
@Component(modules = arrayOf(ApplicationModule::class, LoginModule::class, TwitchModule::class))
interface ApplicationComponent {

    fun inject(tarjet: LoginActivity)

}