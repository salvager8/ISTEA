package mx.com.daggerloginexample.http

import dagger.Module
import dagger.Provides
import okhttp3.OkHttpClient
import okhttp3.logging.HttpLoggingInterceptor
import retrofit2.Retrofit
import retrofit2.adapter.rxjava3.RxJava3CallAdapterFactory
import retrofit2.converter.gson.GsonConverterFactory
import java.util.concurrent.TimeUnit

@Module
class TwitchModule {

    val BASE_URL: String = "https://api.twitch.tv/helix/"
    val AUTH_URL: String = "https://id.twitch.tv/"

    @Provides
    fun provideHttpClient(): OkHttpClient {
        val interceptor = HttpLoggingInterceptor()
        interceptor.setLevel(HttpLoggingInterceptor.Level.HEADERS)

        return OkHttpClient()
            .newBuilder()
            .connectTimeout(10, TimeUnit.MINUTES)
            .readTimeout(10, TimeUnit.MINUTES)
            .addInterceptor(interceptor)
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
    fun provideTwitchService(): TwitchAPI {
        return provideRetrofit(BASE_URL, provideHttpClient()).create(TwitchAPI::class.java)
    }

    @Provides
    fun provideTwitchAuthService(): TwitchAuth {
        return provideRetrofit(AUTH_URL, provideHttpClient()).create(TwitchAuth::class.java)
    }

}