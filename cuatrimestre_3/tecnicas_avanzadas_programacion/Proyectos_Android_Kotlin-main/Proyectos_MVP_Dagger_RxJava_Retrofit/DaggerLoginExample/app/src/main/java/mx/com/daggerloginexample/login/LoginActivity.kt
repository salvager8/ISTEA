package mx.com.daggerloginexample.login

import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import butterknife.BindView
import butterknife.ButterKnife
import io.reactivex.rxjava3.android.schedulers.AndroidSchedulers
import io.reactivex.rxjava3.core.Observable
import io.reactivex.rxjava3.core.Observer
import io.reactivex.rxjava3.disposables.Disposable
import io.reactivex.rxjava3.functions.Function
import io.reactivex.rxjava3.schedulers.Schedulers
import mx.com.daggerloginexample.R
import mx.com.daggerloginexample.http.TwitchAPI
import mx.com.daggerloginexample.http.TwitchAuth
import mx.com.daggerloginexample.http.apiTwitch.Game
import mx.com.daggerloginexample.http.apiTwitch.Token
import mx.com.daggerloginexample.http.apiTwitch.Twitch
import mx.com.daggerloginexample.root.App
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import javax.inject.Inject


class LoginActivity : AppCompatActivity(), LoginActivityMVP.View {

    @Inject
    lateinit var presenter: LoginActivityMVP.Presenter

    @Inject
    lateinit var twitchAPI: TwitchAPI
    @Inject
    lateinit var twitchAuth: TwitchAuth

    @BindView(R.id.editTextFirstName)
    var firstName:EditText? = null
    @BindView(R.id.editTextLastName)
    var lastName:EditText? = null
    @BindView(R.id.btnLogin)
    var loginButton:Button? = null

    var token: String? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)

        ButterKnife.bind(this)

        (application as App).getComponent()!!.inject(this)

        loginButton?.setOnClickListener {
            presenter.loginButtonClicked()
        }

        authenticate()

    }
    private fun authenticate() {
        val call: Call<Token> = twitchAuth.authentication(
            "ohece9hhjpojkm6fb995xcmq23io4k",
            "1ymgbc5rh1lmtaerag2kjg5zizcdbk",
            "client_credentials"
        )

        call.enqueue(object : Callback<Token> {
            override fun onResponse(call: Call<Token>, response: Response<Token>) {
                token = response.body()?.accessToken
                Log.d("TOKEN", token!!)
                //gameList()
                gameListObservable()
            }

            override fun onFailure(call: Call<Token>, t: Throwable) {
                t.printStackTrace()
            }

        })
    }

    fun gameList() {
        val call: Call<Twitch> = twitchAPI.getTopGames(
            "Bearer $token",
            "ohece9hhjpojkm6fb995xcmq23io4k"
        )
        call.enqueue(object : Callback<Twitch> {
            override fun onResponse(call: Call<Twitch>, response: Response<Twitch>) {
                if (response.isSuccessful()) {
                    val topGames: List<Game> = response.body()?.game!!
                    for (game in topGames) {
                    Log.d("GAME", game.name)
                        //println(game.name)
                    }
                }
            }

            override fun onFailure(call: Call<Twitch>, t: Throwable) {
                t.printStackTrace()
            }

        })
    }

    fun gameListObservable() {
        twitchAPI.getTopGamesObservable("Bearer $token", "ohece9hhjpojkm6fb995xcmq23io4k").flatMap {
            return@flatMap Observable.fromIterable(it.game)
        }.flatMap {
            return@flatMap Observable.just(it.name)
        }.filter {
            return@filter it.contains("w") || it.contains("W")
        }
            .subscribeOn(Schedulers.io())
            .observeOn(AndroidSchedulers.mainThread())
            .subscribe({
                //OnNext
                Log.d("RxJava", it)
            }, {
                //OnError
                it.printStackTrace()
            }, {
                //OnComplete
            })
    }

    override fun onResume() {
        super.onResume()
        presenter.setView(this)
        presenter.getCurrentUser()
    }

    override fun getFirstName(): String {
        return this.firstName?.text.toString()
    }

    override fun getLastName(): String {
        return this.lastName?.text.toString()
    }

    override fun showUserNotAvailable() {
        Toast.makeText(this, "Error, el usuario no está disponible", Toast.LENGTH_SHORT).show()
    }

    override fun showInputError() {
        Toast.makeText(
            this,
            "Error, el nombre ni el apellido pueden estar vacios",
            Toast.LENGTH_SHORT
        ).show()
    }

    override fun showUserSaved() {
        Toast.makeText(this, "Usuario guardado correctamente!", Toast.LENGTH_SHORT).show()
    }

    override fun setFirstName(firstName: String) {
        this.firstName?.setText(firstName)
    }

    override fun setLastName(lastName: String) {
        this.lastName?.setText(lastName)
    }
}