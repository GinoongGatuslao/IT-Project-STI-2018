package com.android.itproj.mb40marketing;

import android.app.Application;

import com.android.itproj.mb40marketing.controller.AuthenticationController;
import com.android.itproj.mb40marketing.controller.ProfileController;
import com.android.itproj.mb40marketing.controller.component.DaggerUserComponent;
import com.android.itproj.mb40marketing.controller.component.UserComponent;
import com.android.itproj.mb40marketing.controller.modules.AuthStateModule;
import com.android.itproj.mb40marketing.controller.modules.ContextModule;
import com.android.itproj.mb40marketing.controller.modules.PreferenceModule;
import com.android.itproj.mb40marketing.controller.modules.RestAPIModule;
import com.android.itproj.mb40marketing.helper.restservice.RestAPI;
import com.cloudinary.android.MediaManager;

import java.util.HashMap;
import java.util.Map;

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

    @Getter
    ProfileController profileController;

    @Getter
    RestAPIModule restAPIModule;

    @Override
    public void onCreate() {
        super.onCreate();

        restAPIModule = new RestAPIModule(this.getCacheDir(), BuildConfig.host);
        UserComponent userComponent = DaggerUserComponent
                .builder()
                .authStateModule(new AuthStateModule())
                .contextModule(new ContextModule(this))
                .preferenceModule(new PreferenceModule())
                .restAPIModule(restAPIModule)
                .build();

        authenticationController = new AuthenticationController(this);
        userComponent.inject(authenticationController);
        profileController = new ProfileController(this);
        userComponent.inject(profileController);

        restAPI = userComponent.getRestAPICall();
        authState = userComponent.getAuthState();

        Map<String, String> config = new HashMap<>();
        config.put("cloud_name", "horbordidorp");
        config.put("api_key", "872388742584913");
        config.put("api_secret", "hKJRyPNQUS7JnHDJ1TlAQzLFN7E");
        MediaManager.init(this, config);
    }

}
