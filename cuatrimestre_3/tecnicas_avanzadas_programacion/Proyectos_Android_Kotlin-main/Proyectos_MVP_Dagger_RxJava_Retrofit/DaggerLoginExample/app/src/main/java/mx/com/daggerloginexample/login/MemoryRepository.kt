package mx.com.daggerloginexample.login

class MemoryRepository:LoginRepository {

    private var user: User? = null

    override fun saveUser(user: User) {
        if (user == null) {
            this.user = getUser()
        }
        this.user = user
    }

    override fun getUser(): User {
        if (user == null) {
            user = User("Pepe", "Frog")
            user?.setId(0)
            return user!!
        } else {
            return user!!
        }

    }
}