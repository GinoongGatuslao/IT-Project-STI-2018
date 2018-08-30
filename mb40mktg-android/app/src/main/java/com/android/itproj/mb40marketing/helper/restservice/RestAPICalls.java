package com.android.itproj.mb40marketing.helper.restservice;

import com.android.itproj.mb40marketing.model.UserLogin;
import com.android.itproj.mb40marketing.model.UserModel;

import okhttp3.Response;
import rx.Observable;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

public class RestAPICalls {

    private final RestAPIService restService;
    private static final String TAG = "RestAPICalls";

    public RestAPICalls(RestAPIService restService) {
        this.restService = restService;
    }

    public Observable<UserModel> login(UserLogin userLogin) {
        return restService
                .doLogin(userLogin)
                .observeOn(Schedulers.io())
                .subscribeOn(AndroidSchedulers.mainThread());
    }

    public Observable<Response> logout() {
        return restService
                .doLogout()
                .observeOn(Schedulers.io())
                .subscribeOn(AndroidSchedulers.mainThread());
    }
}
