package com.android.itproj.mb40marketing.helper.interfaces;

import com.android.itproj.mb40marketing.model.UserModel;

public interface AuthenticationCallback {
    void onRegisterSuccess(UserModel model);

    void onRegisterFailed(Throwable e);

    void onLoginSuccess(UserModel model);

    void onLoginFailed(Throwable e);

    void onLogoutSuccess();

    void onLogoutFailed(Throwable e);
}
