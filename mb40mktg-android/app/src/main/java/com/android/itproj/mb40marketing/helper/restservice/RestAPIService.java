package com.android.itproj.mb40marketing.helper.restservice;

import com.android.itproj.mb40marketing.model.UserLogin;
import com.android.itproj.mb40marketing.model.UserModel;

import okhttp3.Response;
import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;
import rx.Observable;

public interface RestAPIService {

    @POST("login")
    Observable<UserModel> doLogin(@Body UserLogin userLogin);

    @POST("login")
    Observable<Response> doLogout();


}
