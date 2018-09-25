package com.android.itproj.mb40marketing.controller.modules;

import android.content.SharedPreferences;

import com.android.itproj.mb40marketing.Constants;
import com.android.itproj.mb40marketing.controller.scope.UserComponentScope;
import com.android.itproj.mb40marketing.model.ProfileModel;
import com.android.itproj.mb40marketing.model.UserModel;
import com.google.gson.Gson;

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
                sharedPreferences.getString(Constants.SHARED_PREFS_KEY_TOKEN, ""),
                sharedPreferences.getInt(Constants.SHARED_PREFS_KEY_USER_TYPE, 0));
    }

    public class AuthState {

        @Getter @Setter
        boolean isAuthenticated = false;

        @Getter @Setter
        String authString = "";

        @Getter @Setter
        int userType;

        public AuthState(boolean isAuthenticated, String authString, int userType) {
            setAuthenticated(isAuthenticated);
            setAuthString(authString);
            setUserType(userType);
        }

        public void saveKeyAndType(String key, int userType) {
            isAuthenticated = true;
            setAuthString(key);
            setUserType(userType);
            sharedPreferences
                    .edit()
                    .putString(Constants.SHARED_PREFS_KEY_TOKEN, key)
                    .putInt(Constants.SHARED_PREFS_KEY_USER_TYPE, userType)
                    .apply();
        }

        public void destroyKey() {
            isAuthenticated = false;
            setAuthString("");
            setUserType(0);
            sharedPreferences
                    .edit()
                    .remove(Constants.SHARED_PREFS_KEY_TOKEN)
                    .remove(Constants.SHARED_PREFS_KEY_USER_TYPE)
                    .apply();
        }
    }
}
