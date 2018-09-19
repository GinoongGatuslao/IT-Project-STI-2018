package com.android.itproj.mb40marketing.helper.interfaces;

import com.android.itproj.mb40marketing.model.ProfileModel;

public class ProfileCallbacks {

    public interface ProfileRegister {
        void onProfileRegisterSuccess(ProfileModel model);

        void onProfileRegisterFailed(Throwable throwable, int code);
    }

    public interface ProfileRequest {
        void onProfileFetch(ProfileModel model);

        void onProfileFetchFailed(Throwable throwable, int code);
    }

}
