package mx.com.moviesapp.movies

import io.reactivex.rxjava3.android.schedulers.AndroidSchedulers
import io.reactivex.rxjava3.disposables.Disposable
import io.reactivex.rxjava3.observers.DisposableObserver
import io.reactivex.rxjava3.schedulers.Schedulers

class MoviesPresenter(private val model: MoviesMVP.Model): MoviesMVP.Presenter {

    private lateinit var view: MoviesMVP.View

    private var disposable: Disposable? = null

    override fun loadData() {
        disposable = model.result()
            .subscribeOn(Schedulers.io())
            .observeOn(AndroidSchedulers.mainThread())
            .subscribe(
                {
                    //OnNext
                    view.updateData(it)
                }, {
                    //OnError
                    it.printStackTrace()
                    view.showSnackBar("Error al descargar las películas...")
                }, {
                    //OnComplete
                    view.showSnackBar("Información descargada con éxito")
                }
            )
    }

    override fun rxJavaUnsuscribe() {
        if (disposable != null && disposable!!.isDisposed!!) {
            disposable!!.dispose()
        }
    }

    override fun setView(view: MoviesMVP.View) {
        this.view = view
    }

}