package com.android.itproj.mb40marketing.controller.component;

import com.android.itproj.mb40marketing.controller.AuthenticationController;
import com.android.itproj.mb40marketing.controller.modules.AuthStateModule;
import com.android.itproj.mb40marketing.controller.modules.PreferenceModule;
import com.android.itproj.mb40marketing.controller.modules.RestAPIModule;
import com.android.itproj.mb40marketing.controller.scope.UserComponentScope;
import com.android.itproj.mb40marketing.helper.restservice.RestAPI;

import dagger.Component;

@UserComponentScope
@Component(modules = {AuthStateModule.class, RestAPIModule.class})
public interface UserComponent {

    RestAPI getRestAPICall();

    AuthStateModule.AuthState getAuthState();

    void inject(AuthenticationController authenticationController);
}
