package mx.com.moviesapp.movies.recyclerView

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import butterknife.BindView
import butterknife.ButterKnife
import mx.com.moviesapp.R
import mx.com.moviesapp.movies.ViewModel

class ListAdapter(private val items: List<ViewModel>): RecyclerView.Adapter<ListAdapter.ViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val view: View = LayoutInflater.from(parent.context).inflate(R.layout.movie_list_item, parent, false)

        return ViewHolder(view)
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        val item = items[position]
        holder.itemMovieTitle?.text = item.movieTitle
        holder.countryMovie?.text = item.movieCountry
    }


    override fun getItemCount(): Int {
        return items.size
    }

    class ViewHolder(itemView: View): RecyclerView.ViewHolder(itemView) {
        //@BindView(R.id.textViewFragmentMovieTitle)
        //val itemMovieTitle: TextView? = null
        val itemMovieTitle: TextView? = itemView.findViewById(R.id.textViewFragmentMovieTitle)
        //@BindView(R.id.textViewFragmentMovieCountry)
        //val countryMovie: TextView? = null
        val countryMovie: TextView? = itemView.findViewById(R.id.textViewFragmentMovieCountry)
        /*
        init {
            ButterKnife.bind(this, itemView)
        }
         */

    }

}
