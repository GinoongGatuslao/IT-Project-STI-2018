package com.android.itproj.mb40marketing.helper.interfaces;

import com.android.itproj.mb40marketing.model.UserModel;

public class AuthenticationCallback {
    public interface AuthLoginCallback {
        void onLoginSuccess(UserModel model);

        void onLoginFailed(Throwable e);
    }

    public interface AuthLogoutCallback {
        void onLogoutSuccess();

        void onLogoutFailed(Throwable e);
    }

    public interface AuthRegisterCallback {
        void onRegisterSuccess(UserModel model);

        void onRegisterFailed(Throwable e);
    }
}
