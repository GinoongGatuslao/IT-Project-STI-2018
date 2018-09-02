package com.android.itproj.mb40marketing.controller.modules;

import android.content.SharedPreferences;

import com.android.itproj.mb40marketing.Constants;
import com.android.itproj.mb40marketing.controller.scope.UserComponentScope;

import dagger.Module;
import dagger.Provides;
import lombok.Getter;
import lombok.Setter;

@Module(includes = PreferenceModule.class)
public class AuthStateModule {

    private SharedPreferences sharedPreferences;

    @UserComponentScope
    @Provides
    public AuthState providesAuthState(SharedPreferences sharedPreferences) {
        this.sharedPreferences = sharedPreferences;
        return new AuthState(
                !sharedPreferences.getString(Constants.SHARED_PREFS_KEY_TOKEN, "").isEmpty(),
                sharedPreferences.getString(Constants.SHARED_PREFS_KEY_TOKEN, ""));
    }

    public class AuthState {

        @Getter @Setter
        boolean isAuthenticated = false;

        @Getter @Setter
        String authString = "";

        public AuthState(boolean isAuthenticated, String authString) {
            setAuthenticated(isAuthenticated);
            setAuthString(authString);
        }

        public void saveKey(String key) {
            sharedPreferences
                    .edit().putString(Constants.SHARED_PREFS_KEY_TOKEN, key)
                    .apply();
        }

        public void destroyKey() {
            sharedPreferences
                    .edit().remove(Constants.SHARED_PREFS_KEY_TOKEN)
                    .apply();
        }
    }
}
