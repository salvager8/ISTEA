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
class MovieExtraInfoAPIModule {

    @Provides
    fun provideClient(): OkHttpClient {
        val interceptor = HttpLoggingInterceptor()
        interceptor.setLevel(HttpLoggingInterceptor.Level.BODY)

        return OkHttpClient()
            .newBuilder()
            .addInterceptor(interceptor)
            .addInterceptor(Interceptor {
                var request: Request = it.request()
                val url: HttpUrl = request.url.newBuilder().addQueryParameter("apikey", API_KEY).build()
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
    fun provideExtraInfoAPIService(): MoviesExtraInfoAPIsService {
        return provideRetrofit(BASE_URL, provideClient()).create(MoviesExtraInfoAPIsService::class.java)
    }

    companion object {
        val BASE_URL: String = "http://www.omdbapi.com/"
        val API_KEY: String = "e1dd9ae6"
    }

}