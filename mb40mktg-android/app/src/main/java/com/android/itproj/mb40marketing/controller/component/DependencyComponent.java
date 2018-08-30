package com.android.itproj.mb40marketing.controller.component;

import com.android.itproj.mb40marketing.controller.AuthenticationController;
import com.android.itproj.mb40marketing.controller.modules.RestAPIModule;

import javax.inject.Singleton;

import dagger.Component;

@Singleton
@Component(modules = {
        RestAPIModule.class})
public interface DependencyComponent {

    void inject(AuthenticationController authenticationController);

}
