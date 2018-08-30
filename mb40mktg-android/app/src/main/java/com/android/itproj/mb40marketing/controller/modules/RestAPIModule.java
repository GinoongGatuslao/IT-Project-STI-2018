package com.android.itproj.mb40marketing.controller.modules;

import android.content.SharedPreferences;
import android.util.Log;

import com.android.itproj.mb40marketing.Constants;
import com.android.itproj.mb40marketing.helper.restservice.RestAPICalls;
import com.android.itproj.mb40marketing.helper.restservice.RestAPIService;
import com.facebook.stetho.okhttp3.StethoInterceptor;

import java.io.File;
import java.io.IOException;
import java.util.concurrent.TimeUnit;

import javax.inject.Singleton;

import dagger.Module;
import dagger.Provides;
import okhttp3.Cache;
import okhttp3.Dispatcher;
import okhttp3.Interceptor;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;
import retrofit2.Retrofit;
import retrofit2.adapter.rxjava.RxJavaCallAdapterFactory;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.converter.scalars.ScalarsConverterFactory;

@Module
public class RestAPIModule {

    File cacheFile;
    SharedPreferences sharedPreferences;
    String baseUrl;

    public RestAPIModule(SharedPreferences sharedPreferences, File cacheFile, String baseUrl) {
        this.cacheFile = cacheFile;
        this.sharedPreferences = sharedPreferences;
        this.baseUrl = baseUrl;
    }

    @Provides
    @Singleton
    Retrofit providesRetrofitCall() {
        Interceptor interceptor = new Interceptor() {
            @Override
            public Response intercept(Chain chain) throws IOException {
                Request authenticatedRequest =
                        chain.request()
                                .newBuilder()
                                .header("Authorization", "Bearer " + sharedPreferences.getString(Constants.SHARED_PREFS_KEY_TOKEN, "").trim())
                                .build();
                Response originalResponse = chain.proceed(authenticatedRequest);
                int trycount = 0;

                while (!originalResponse.isSuccessful() && trycount < 3) {
                    trycount++;
                    Log.d("retroFitCall", "intercept[attempt " + trycount + "/ " + 3 + "]: " + authenticatedRequest.url());
                    originalResponse = chain.proceed(authenticatedRequest);
                }
                Log.d("call", "intercept: " + originalResponse.request().url());
                return originalResponse;
            }
        };

        Cache cache = new Cache(cacheFile, 10*1024*1024);
        Dispatcher dispatcher = new Dispatcher();
        dispatcher.setMaxRequests(3);
        OkHttpClient okHttpClient = new OkHttpClient.Builder()
                .connectTimeout(30, TimeUnit.SECONDS )
                .readTimeout(30, TimeUnit.SECONDS )
                .writeTimeout(30, TimeUnit.SECONDS )
                .addInterceptor(interceptor)
                .addNetworkInterceptor(new StethoInterceptor())
                .cache(cache)
                .dispatcher(dispatcher)
                .build();

        return new Retrofit.Builder()
                .baseUrl(this.baseUrl)
                .addConverterFactory(GsonConverterFactory.create())
                .addConverterFactory(ScalarsConverterFactory.create())
                .addCallAdapterFactory(RxJavaCallAdapterFactory.create())
                .client(okHttpClient)
                .build();
    }

    @Provides
    @Singleton
    @SuppressWarnings("unused")
    public RestAPIService providesRESTService(Retrofit retrofit) {
        return retrofit.create(RestAPIService.class);
    }

    @Provides
    @Singleton
    @SuppressWarnings("unused")
    public RestAPICalls providesRESTCalls(RestAPIService service) {
        return new RestAPICalls(service);
    }
}
