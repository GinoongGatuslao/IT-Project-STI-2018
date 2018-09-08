package com.android.itproj.mb40marketing.helper.restservice;

import com.android.itproj.mb40marketing.model.ProfileModel;
import com.android.itproj.mb40marketing.model.UserLogin;
import com.android.itproj.mb40marketing.model.UserModel;
import com.google.gson.JsonObject;

import org.json.JSONObject;

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

    ///////////////////////////////////////////////////////////////////////////
    // Login
    ///////////////////////////////////////////////////////////////////////////
    public Observable<UserModel> login(UserLogin userLogin) {
        return restService
                .doLogin(userLogin)
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());
    }

    ///////////////////////////////////////////////////////////////////////////
    // Logout
    ///////////////////////////////////////////////////////////////////////////
    public Observable<Response<ResponseBody>> logout() {
        return restService
                .doLogout()
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());
    }

    ///////////////////////////////////////////////////////////////////////////
    // Register User
    ///////////////////////////////////////////////////////////////////////////
    public Observable<JsonObject> registerUser(JsonObject userModel) {
        return restService
                .registerUser(userModel)
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());
    }

    ///////////////////////////////////////////////////////////////////////////
    // Create User Profile
    ///////////////////////////////////////////////////////////////////////////
    public Observable<ProfileModel> createProfile(ProfileModel model) {
        return restService
                .createProfile(model)
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());

    }

    ///////////////////////////////////////////////////////////////////////////
    // Get User Profile
    ///////////////////////////////////////////////////////////////////////////
    public Observable<ProfileModel> getUserProfile(int userId) {
        return restService
                .getUserProfile(userId)
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());

    }
}
