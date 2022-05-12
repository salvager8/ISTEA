package mx.com.moviesapp.http

import dagger.Module
import dagger.Provides
import okhttp3.HttpUrl
import okhttp3.Interceptor
import okhttp3.OkHttpClient
import okhttp3.Request
import okhttp3.logging.HttpLoggingInterceptor
import retrofit2.Retrofit
import retrofit2.adapter.rxjava3.RxJava3CallAdapterFactory
import retrofit2.converter.gson.GsonConverterFactory

@Module
class MovieTitleAPIModule {

    @Provides
    fun provideClient(): OkHttpClient {
        val interceptor = HttpLoggingInterceptor()
        interceptor.setLevel(HttpLoggingInterceptor.Level.BASIC)

        return OkHttpClient()
            .newBuilder()
            .addInterceptor(interceptor)
            .addInterceptor(Interceptor {
                var request: Request = it.request()
                val url: HttpUrl = request.url.newBuilder().addQueryParameter("api_key", API_KEY).build()
                request = request.newBuilder().url(url).build()
                return@Interceptor it.proceed(request)
            })
            .build()
    }

    @Provides
    fun provideRetrofit(baseURL: String, client: OkHttpClient): Retrofit {
        return Retrofit.Builder()
            .baseUrl(baseURL)
            .client(client)
            .addConverterFactory(GsonConverterFactory.create())
            .addCallAdapterFactory(RxJava3CallAdapterFactory.create())
            .build()
    }

    @Provides
    fun provideMoviesAPIService(): MoviesAPIService {
        return provideRetrofit(BASE_URL, provideClient()).create(MoviesAPIService::class.java)
    }

    companion object {
        val BASE_URL: String = "https://api.themoviedb.org/3/"
        val API_KEY: String = "63c9b00a2df98eef93c8da94ef1de7f9"
    }

}