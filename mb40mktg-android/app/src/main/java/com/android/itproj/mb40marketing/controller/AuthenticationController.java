package com.android.itproj.mb40marketing.controller;

import android.content.Context;
import android.content.SharedPreferences;

import com.android.itproj.mb40marketing.Constants;
import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.controller.modules.AuthStateModule;
import com.android.itproj.mb40marketing.helper.interfaces.AuthenticationCallback;
import com.android.itproj.mb40marketing.model.UserLogin;
import com.android.itproj.mb40marketing.model.UserModel;

import org.apache.http.HttpStatus;

import java.io.IOException;

import javax.inject.Inject;

import lombok.Getter;
import lombok.Setter;
import okhttp3.ResponseBody;
import retrofit2.Response;
import rx.Observer;
import rx.subscriptions.CompositeSubscription;

public class AuthenticationController {

    private CompositeSubscription compositeSubscription;
    private Context context;

    @Inject
    @Getter
    @Setter
    AuthStateModule.AuthState authState;

    public AuthenticationController(Context context) {
        this.context = context;
        this.compositeSubscription = new CompositeSubscription();
    }

    ///////////////////////////////////////////////////////////////////////////
    // PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////

    public void login(final String username, final String password, final AuthenticationCallback.AuthLoginCallback authCallback) {
        compositeSubscription.add(
                ((CoreApp) context).getRestAPI()
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
                                if (authState != null) {
                                    authState.saveKey(userModel.getApi_token());
                                    authCallback.onLoginSuccess(userModel);
                                } else {
                                    authCallback.onLoginFailed(new Throwable("Error! Unable to save on preference."));
                                }
                            }
                        })
        );
    }

    public void logout(final AuthenticationCallback.AuthLogoutCallback authCallback) {
        compositeSubscription.add(
                ((CoreApp) context).getRestAPI()
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
                                    if (authState != null) {
                                        authState.destroyKey();
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
