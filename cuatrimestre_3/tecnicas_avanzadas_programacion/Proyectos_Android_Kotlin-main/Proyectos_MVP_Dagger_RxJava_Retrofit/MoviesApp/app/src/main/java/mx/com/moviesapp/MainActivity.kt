package mx.com.moviesapp

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.ViewGroup
import androidx.recyclerview.widget.DefaultItemAnimator
import androidx.recyclerview.widget.DividerItemDecoration
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import butterknife.BindView
import butterknife.ButterKnife
import com.google.android.material.snackbar.Snackbar
import mx.com.moviesapp.movies.recyclerView.ListAdapter
import mx.com.moviesapp.movies.MoviesMVP
import mx.com.moviesapp.movies.ViewModel
import mx.com.moviesapp.root.App
import javax.inject.Inject

class MainActivity : AppCompatActivity(), MoviesMVP.View {

    //@BindView(R.id.activity_root_view)
    //val rootView: ViewGroup? = null

    //@BindView(R.id.recyclerViewMovies)
    var recyclerView:RecyclerView? = null

    @set:Inject
    var presenter: MoviesMVP.Presenter? = null
    //RecyclerView
    private lateinit var listAdapter: ListAdapter
    private lateinit var resultList: MutableList<ViewModel>

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        //ButterKnife.bind(this)

        (application as App).getComponent().inject(this)

        recyclerView = findViewById(R.id.recyclerViewMovies)

        resultList = ArrayList()
        val layoutManager = LinearLayoutManager(this)

        listAdapter = ListAdapter(resultList)

        recyclerView?.adapter = listAdapter
        recyclerView?.addItemDecoration(DividerItemDecoration(this, DividerItemDecoration.HORIZONTAL))
        recyclerView?.itemAnimator = DefaultItemAnimator()
        recyclerView?.setHasFixedSize(true)
        layoutManager.orientation = LinearLayoutManager.VERTICAL

        recyclerView?.layoutManager = layoutManager

    }

    override fun onResume() {
        super.onResume()
        presenter?.setView(this)
        //updateData(ViewModel("Juegos del hambre", "USA"))
        presenter?.loadData()
    }

    override fun onStop() {
        super.onStop()
        presenter?.rxJavaUnsuscribe()
        resultList.clear()
        listAdapter.notifyDataSetChanged()
    }

    override fun updateData(viewModel: ViewModel) {
        resultList.add(viewModel)
        listAdapter.notifyItemInserted(resultList.size - 1)
        Log.d(TAG, "Información nueva: ${viewModel.movieTitle}")
    }

    override fun showSnackBar(s: String) {
        val rootView: ViewGroup = findViewById(R.id.activity_root_view)
        Snackbar.make(rootView, s, Snackbar.LENGTH_SHORT).show()
    }

    companion object {
        val TAG:String = MainActivity::class.java.name
    }
}