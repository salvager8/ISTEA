package mx.com.daggerloginexample.login

interface LoginRepository {

    fun saveUser(user: User)

    fun getUser():User

}