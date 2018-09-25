package com.android.itproj.mb40marketing.controller;

import android.content.Context;

import com.android.itproj.mb40marketing.Constants;
import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.controller.modules.AuthStateModule;
import com.android.itproj.mb40marketing.helper.interfaces.AuthenticationCallback;
import com.android.itproj.mb40marketing.model.UserLogin;
import com.android.itproj.mb40marketing.model.UserModel;
import com.google.gson.Gson;
import com.google.gson.JsonObject;

import org.apache.http.HttpStatus;

import java.io.IOException;

import javax.inject.Inject;

import lombok.Getter;
import lombok.Setter;
import okhttp3.ResponseBody;
import retrofit2.HttpException;
import retrofit2.Response;
import rx.Observer;
import rx.Subscription;
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
                        .login(UserLogin.initialize(username, password))
                        .subscribe(new Observer<UserModel>() {
                            @Override
                            public void onCompleted() {

                            }

                            @Override
                            public void onError(Throwable e) {
                                authCallback.onLoginFailed(e, ((HttpException)e).code());
                            }

                            @Override
                            public void onNext(UserModel userModel) {
                                if (authState != null) {
                                    authState.saveKeyAndType(userModel.getApi_token(), userModel.getUser_type());
                                    Constants.API_TOKEN = userModel.getApi_token();
                                    authCallback.onLoginSuccess(userModel);
                                } else {
                                    authCallback.onLoginFailed(new Throwable("Error! Unable to save on preference."), HttpStatus.SC_METHOD_FAILURE);
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
                                authCallback.onLogoutFailed(e, ((HttpException)e).code());
                            }

                            @Override
                            public void onNext(Response<ResponseBody> response) {
                                if (response.code() == HttpStatus.SC_OK) {
                                    if (authState != null) {
                                        authState.destroyKey();
                                        Constants.API_TOKEN = "";
                                        authCallback.onLogoutSuccess();
                                    } else {
                                        authCallback.onLogoutFailed(new Throwable("Warning! Unable to clear preference."), HttpStatus.SC_METHOD_FAILURE);
                                    }
                                } else {
                                    try {
                                        authCallback.onLogoutFailed(new Throwable(response.body().string()), HttpStatus.SC_METHOD_FAILURE);
                                    } catch (IOException e) {
                                        authCallback.onLogoutFailed(new Throwable("Generic Failure"), HttpStatus.SC_METHOD_FAILURE);
                                        e.printStackTrace();
                                    }
                                }
                            }
                        })
        );
    }

    public void forceLogout(final AuthenticationCallback.AuthLogoutCallback authLogoutCallback) {
        if (authState != null) {
            authState.destroyKey();
            Constants.API_TOKEN = "";
        }
        authLogoutCallback.onLogoutSuccess();
    }
    public void registerUser(final JsonObject userModel, final AuthenticationCallback.AuthRegisterCallback registerCallback) {
        compositeSubscription.add(
                ((CoreApp)context).getRestAPI()
                .registerUser(userModel)
                .subscribe(new Observer<JsonObject>() {
                    @Override
                    public void onCompleted() {

                    }

                    @Override
                    public void onError(Throwable e) {
                        registerCallback.onRegisterFailed(e, ((HttpException)e).code());
                    }

                    @Override
                    public void onNext(JsonObject model) {
                        UserModel userModel = new Gson().fromJson(model, UserModel.class);
                        if (userModel != null) {
                            authState.saveKeyAndType(userModel.getApi_token(), userModel.getUser_type());
                            Constants.API_TOKEN = userModel.getApi_token();
                            registerCallback.onRegisterSuccess(userModel);
                        } else {
                            registerCallback.onRegisterFailed(new Throwable("Failed to register user!"), HttpStatus.SC_METHOD_FAILURE);
                        }
                    }
                })
        );
    }
}
