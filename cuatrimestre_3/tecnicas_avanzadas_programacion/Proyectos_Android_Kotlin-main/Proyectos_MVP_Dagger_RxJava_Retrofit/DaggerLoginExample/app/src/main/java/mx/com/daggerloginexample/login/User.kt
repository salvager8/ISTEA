package mx.com.daggerloginexample.login

class User(firstName: String, lastName: String) {

    private var id:Int? = null
    var firstName:String? = null
    var lastName:String? = null

    init {
        this.firstName = firstName
        this.lastName = lastName
    }

    fun getId(): Int {
        return id!!
    }

    fun setId(id: Int) {
        this.id = id
    }

}