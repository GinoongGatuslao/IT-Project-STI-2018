package com.android.itproj.mb40marketing;

import android.app.Application;

import com.android.itproj.mb40marketing.controller.AuthenticationController;
import com.android.itproj.mb40marketing.controller.component.DaggerUserComponent;
import com.android.itproj.mb40marketing.controller.component.UserComponent;
import com.android.itproj.mb40marketing.controller.modules.AuthStateModule;
import com.android.itproj.mb40marketing.controller.modules.ContextModule;
import com.android.itproj.mb40marketing.controller.modules.PreferenceModule;
import com.android.itproj.mb40marketing.controller.modules.RestAPIModule;
import com.android.itproj.mb40marketing.helper.restservice.RestAPI;

import javax.inject.Inject;

import lombok.Getter;

public class CoreApp extends Application {

    @Inject
    @Getter
    RestAPI restAPI;

    @Inject
    @Getter
    AuthStateModule.AuthState authState;

    @Getter
    AuthenticationController authenticationController;

    @Override
    public void onCreate() {
        super.onCreate();

        UserComponent userComponent = DaggerUserComponent
                .builder()
                .authStateModule(new AuthStateModule())
                .contextModule(new ContextModule(this))
                .preferenceModule(new PreferenceModule())
                .restAPIModule(new RestAPIModule(this.getCacheDir(), BuildConfig.host))
                .build();

        authenticationController = new AuthenticationController(this);
        userComponent.inject(authenticationController);

        restAPI = userComponent.getRestAPICall();
        authState = userComponent.getAuthState();
    }


}
