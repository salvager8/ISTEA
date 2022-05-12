package mx.com.daggerloginexample.login

class LoginActivityModel(repository: LoginRepository): LoginActivityMVP.Model {

    private var repository: LoginRepository? = null

    init {
        this.repository = repository
    }

    override fun createUser(firstName: String, lastName: String) {
        //Logica de business(Que el usuario existe, que sea correcto, etc)
        repository?.saveUser(User(firstName, lastName))
    }

    override fun getUser(): User {
        //Logica de business(Que el usuario existe, que sea correcto, etc)
        return repository!!.getUser()
    }

}