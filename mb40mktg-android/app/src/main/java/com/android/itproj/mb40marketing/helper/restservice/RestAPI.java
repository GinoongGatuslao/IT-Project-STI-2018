package com.android.itproj.mb40marketing.helper.restservice;

import com.android.itproj.mb40marketing.model.UserLogin;
import com.android.itproj.mb40marketing.model.UserModel;

import okhttp3.ResponseBody;
import retrofit2.Response;
import rx.Observable;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

public class RestAPI {

    private final RestAPIService restService;
    private static final String TAG = "RestAPI";

    public RestAPI(RestAPIService restService) {
        this.restService = restService;
    }

    public Observable<UserModel> login(UserLogin userLogin) {
        return restService
                .doLogin(userLogin)
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());
    }

    public Observable<Response<ResponseBody>> logout() {
        return restService
                .doLogout()
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());
    }
}
