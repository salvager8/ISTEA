package mx.com.daggerloginexample.login

interface LoginActivityMVP {

    interface View {
        fun getFirstName(): String
        fun getLastName(): String

        fun showUserNotAvailable()
        fun showInputError()
        fun showUserSaved()

        fun setFirstName(firstName: String)
        fun setLastName(lastName: String)

    }

    interface Presenter {
        fun setView(view: LoginActivityMVP.View)

        fun loginButtonClicked()

        fun getCurrentUser()
    }

    interface Model {
        fun createUser(firstName: String, lastName: String)

        fun getUser():User
    }

}