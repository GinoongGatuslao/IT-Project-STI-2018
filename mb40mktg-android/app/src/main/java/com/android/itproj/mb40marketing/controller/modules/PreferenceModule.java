package com.android.itproj.mb40marketing.controller.modules;

import android.content.Context;
import android.content.SharedPreferences;

import com.android.itproj.mb40marketing.Constants;
import com.android.itproj.mb40marketing.controller.scope.UserComponentScope;

import dagger.Module;
import dagger.Provides;

@Module(includes = ContextModule.class)
public class PreferenceModule {

    @UserComponentScope
    @Provides
    public SharedPreferences providesSharedPreference(Context context) {
        return context.getSharedPreferences(Constants.SHARED_PREFS_TABLE, Context.MODE_PRIVATE);
    }

}
