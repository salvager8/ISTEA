package mx.com.daggerloginexample

import mx.com.daggerloginexample.login.LoginActivityMVP
import mx.com.daggerloginexample.login.LoginActivityPresenter
import mx.com.daggerloginexample.login.User
import org.junit.Before
import org.junit.Test
import org.mockito.Mockito.*

class PresenterUnitTest {

    var presenter: LoginActivityPresenter? = null
    var user: User? = null

    var mockedModel: LoginActivityMVP.Model? = null
    var mockedView: LoginActivityMVP.View? = null

    @Before
    fun initialization() {
        mockedModel = mock(LoginActivityMVP.Model::class.java)
        mockedView = mock(LoginActivityMVP.View::class.java)

        user = User("Lionel", "Messi")

        `when`(mockedModel?.getUser()).thenReturn(user)

        //`when`(mockedView?.getFirstName()).thenReturn("Pepe")
        //`when`(mockedView?.getLastName()).thenReturn("Frog")

        presenter = LoginActivityPresenter(mockedModel!!)
        presenter?.setView(mockedView!!)
    }

    @Test
    fun noExistsInteractionWithView() {
        presenter?.getCurrentUser()

        verify(mockedView, never())?.showUserNotAvailable()
    }

    @Test
    fun loadUserFromTheRepoWhenValidUserIsPresent() {
        `when`(mockedModel?.getUser()).thenReturn(user)

        presenter?.getCurrentUser()

        //Comprobamos la  interactuacion con el modelo de datos
        verify(mockedModel, times(1))?.getUser()

        //Comprobamos la  interactuacion con la vista
        verify(mockedView, times(1))?.setFirstName("Lionel")
        verify(mockedView, times(1))?.setLastName("Messi")
        verify(mockedView, never())?.showUserNotAvailable()
    }

    @Test
    fun showErrorMessageWhenUserIsNull() {
        `when`(mockedModel?.getUser()).thenReturn(null)

        presenter?.getCurrentUser()

        //Comprobamos la  interactuacion con el modelo de datos
        verify(mockedModel, times(1))?.getUser()

        //Comprobamos la  interactuacion con la vista
        verify(mockedView, never())?.setFirstName("Lionel")
        verify(mockedView, never())?.setLastName("Messi")
        verify(mockedView, times(1))?.showUserNotAvailable()
    }

    @Test
    fun createErrorMessageIfAnyFieldIsEmpty() {
        //Primera prueba poniendo el campo FIRSTNAME vacio
        `when`(mockedView?.getFirstName()).thenReturn("")

        presenter?.loginButtonClicked()

        verify(mockedView, times(1))?.getFirstName()
        verify(mockedView, never())?.getLastName()
        verify(mockedView, times(1))?.showInputError()

        //Segunda prueba poniendo un valor en el campo FIRSTNAME y dejando el LASTNAME vacio
        `when`(mockedView?.getFirstName()).thenReturn("Pepe")
        `when`(mockedView?.getLastName()).thenReturn("")

        presenter?.loginButtonClicked()

        verify(mockedView, times(2))?.getFirstName() //El metodo TIMES se llama dos veces, una en la prueba anterior y una en la actual!
        verify(mockedView, times(1))?.getLastName() //Este metodo es la primera vez que se llama, ya que antes era NEVER
        verify(mockedView, times(2))?.showInputError() //El metodo se llamo antes y de nuevo ahora, en total dos veces
    }

    @Test
    fun saveValidUser() {
        `when`(mockedView?.getFirstName()).thenReturn("Pepe")
        `when`(mockedView?.getLastName()).thenReturn("Frog")

        presenter?.loginButtonClicked()

        //Se comprueban las llamadas deben ser dobles, una en el "if" y otra cuando se crea el "usuario"
        verify(mockedView, times(2))?.getFirstName()
        verify(mockedView, times(2))?.getLastName()

        //Miramos que el modelo persista en el repositorio
        verify(mockedModel, times(1))?.createUser("Pepe", "Frog")

        //Miramos que se muestre el mensaje de exito al usuario
        verify(mockedView, times(1))?.showUserSaved()
    }
}