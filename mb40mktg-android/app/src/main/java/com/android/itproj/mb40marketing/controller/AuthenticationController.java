package com.android.itproj.mb40marketing.controller;

import android.content.Context;
import android.content.SharedPreferences;
import android.util.Log;

import com.android.itproj.mb40marketing.BuildConfig;
import com.android.itproj.mb40marketing.Constants;
import com.android.itproj.mb40marketing.controller.component.DaggerDependencyComponent;
import com.android.itproj.mb40marketing.controller.modules.RestAPIModule;
import com.android.itproj.mb40marketing.helper.interfaces.AuthenticationCallback;
import com.android.itproj.mb40marketing.helper.restservice.RestAPICalls;
import com.android.itproj.mb40marketing.model.UserLogin;
import com.android.itproj.mb40marketing.model.UserModel;

import org.apache.http.HttpStatus;

import java.io.IOException;

import javax.inject.Inject;

import okhttp3.ResponseBody;
import retrofit2.Response;
import rx.Observer;
import rx.subscriptions.CompositeSubscription;

public class AuthenticationController {


    private CompositeSubscription compositeSubscription;

    @Inject
    public RestAPICalls restAPICalls;
    private SharedPreferences preferences;
    private AuthenticationCallback authCallback;

    public AuthenticationController(Context context) {
        this.preferences = context.getSharedPreferences(Constants.SHARED_PREFS_TABLE, Context.MODE_PRIVATE);
        this.compositeSubscription = new CompositeSubscription();
        this.authCallback = authCallback;

        DaggerDependencyComponent
                .builder()
                .restAPIModule(new RestAPIModule(preferences, context.getCacheDir(), BuildConfig.host))
                .build()
                .inject(this);
    }

    ///////////////////////////////////////////////////////////////////////////
    // PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////
    public void login(final String username, final String password, final AuthenticationCallback.AuthLoginCallback authCallback) {
        compositeSubscription.add(
                restAPICalls
                        .login(
                                UserLogin.initialize(username, password))
                        .subscribe(new Observer<UserModel>() {
                            @Override
                            public void onCompleted() {

                            }

                            @Override
                            public void onError(Throwable e) {
                                authCallback.onLoginFailed(e);
                            }

                            @Override
                            public void onNext(UserModel userModel) {
                                if (preferences != null) {
                                    preferences
                                            .edit().putString(Constants.SHARED_PREFS_KEY_TOKEN, userModel.getApi_token())
                                            .apply();
                                    authCallback.onLoginSuccess(userModel);
                                } else {
                                    authCallback.onLoginFailed(new Throwable("App Error! Unable to save on preference."));
                                }
                            }
                        })
        );
    }

    public void logout(final AuthenticationCallback.AuthLogoutCallback authCallback) {
        compositeSubscription.add(restAPICalls
                .logout()
                .subscribe(new Observer<Response<ResponseBody>>() {
                    @Override
                    public void onCompleted() {

                    }

                    @Override
                    public void onError(Throwable e) {
                        authCallback.onLogoutFailed(e);
                    }

                    @Override
                    public void onNext(Response<ResponseBody> response) {
                        if (response.code() == HttpStatus.SC_OK) {
                            if (preferences != null) {
                                preferences
                                        .edit().remove(Constants.SHARED_PREFS_KEY_TOKEN)
                                        .apply();
                                authCallback.onLogoutSuccess();
                            } else {
                                authCallback.onLogoutFailed(new Throwable("Warning! Unable to clear preference."));
                            }
                        } else {
                            try {
                                authCallback.onLogoutFailed(new Throwable(response.body().string()));
                            } catch (IOException e) {
                                authCallback.onLogoutFailed(new Throwable("Generic Failure"));
                                e.printStackTrace();
                            }
                        }
                    }
                })
        );
    }
}
