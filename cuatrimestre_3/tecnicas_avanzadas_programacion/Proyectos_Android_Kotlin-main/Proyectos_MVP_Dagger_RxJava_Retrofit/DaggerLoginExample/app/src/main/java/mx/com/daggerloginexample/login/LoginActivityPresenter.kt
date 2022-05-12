package mx.com.daggerloginexample.login

import androidx.annotation.Nullable

class LoginActivityPresenter(model: LoginActivityMVP.Model): LoginActivityMVP.Presenter {

    @Nullable
    private lateinit var view: LoginActivityMVP.View
    private var model: LoginActivityMVP.Model? = null

    init {
        this.model = model
    }

    override fun setView(view: LoginActivityMVP.View) {
        this.view = view
    }

    override fun loginButtonClicked() {
        if (view != null) {
            if (view.getFirstName().trim().equals("") || view.getLastName().trim().equals("")) {
                view.showInputError()
            } else {
                model?.createUser(view.getFirstName().trim(), view.getLastName().trim())
                view.showUserSaved()
            }
        }
    }

    override fun getCurrentUser() {
        val user:User = model!!.getUser()

        if (user == null) {
            if (view != null) {
                view.showUserNotAvailable()
            }
        } else {
            if (view != null) {
                view.setFirstName(user.firstName.toString())
                view.setLastName(user.lastName.toString())
            }
        }
    }
}