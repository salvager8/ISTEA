package mx.com.moviesapp.movies

import io.reactivex.rxjava3.core.Observable

interface MoviesMVP {

    interface View {
        fun updateData(viewModel: ViewModel)

        fun showSnackBar(s:String)
    }

    interface Presenter {
        fun loadData()

        fun rxJavaUnsuscribe()

        fun setView(view: View)
    }

    interface Model {
        fun result(): Observable<ViewModel>
    }

}