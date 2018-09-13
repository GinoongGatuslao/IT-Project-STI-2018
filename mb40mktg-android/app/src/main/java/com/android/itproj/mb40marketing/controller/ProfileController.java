package com.android.itproj.mb40marketing.controller;

import android.content.Context;
import android.content.SharedPreferences;
import android.util.Log;

import com.android.itproj.mb40marketing.Constants;
import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.helper.interfaces.ProfileCallbacks;
import com.android.itproj.mb40marketing.model.ProfileModel;
import com.google.gson.Gson;

import javax.inject.Inject;

import lombok.Getter;
import lombok.Setter;
import rx.Observer;
import rx.subscriptions.CompositeSubscription;

public class ProfileController {

    @Getter
    @Setter
    private ProfileModel profile;

    @Inject
    @Getter
    SharedPreferences preferences;

    private CompositeSubscription compositeSubscription;
    private Context context;

    public ProfileController(Context context) {
        this.context = context;
        this.compositeSubscription = new CompositeSubscription();
    }

    public void registerProfile(final ProfileModel profileModel, final ProfileCallbacks.ProfileRegister profileRegister) {
        compositeSubscription.add(
                ((CoreApp) context)
                        .getRestAPI()
                        .createProfile(profileModel)
                        .subscribe(new Observer<ProfileModel>() {
                            @Override
                            public void onCompleted() {

                            }

                            @Override
                            public void onError(Throwable e) {
                                profileRegister.onProfileRegisterFailed(e);
                            }

                            @Override
                            public void onNext(ProfileModel model) {
                                if (model != null) {
                                    saveProfileInfo(model);
                                    setProfile(model);
                                    profileRegister.onProfileRegisterSuccess(model);
                                } else {
                                    profileRegister.onProfileRegisterFailed(new Throwable("Failed to update profile!"));
                                }
                            }
                        })
        );
    }

    public void getUserProfile(final int userId, final ProfileCallbacks.ProfileRequest profileRequest) {
        compositeSubscription.add(
                ((CoreApp) context)
                        .getRestAPI()
                        .getUserProfile(userId)
                        .subscribe(new Observer<ProfileModel>() {
                            @Override
                            public void onCompleted() {

                            }

                            @Override
                            public void onError(Throwable e) {
                                profileRequest.onProfileFetchFailed(e);
                            }

                            @Override
                            public void onNext(ProfileModel model) {
                                if (model != null) {
                                    saveProfileInfo(model);
                                    setProfile(model);
                                    profileRequest.onProfileFetch(model);
                                } else {
                                    profileRequest.onProfileFetchFailed(new Throwable("Failed to fetch User profile"));
                                }
                            }
                        })
        );
    }

    public void getCachedProfile(ProfileCallbacks.ProfileRequest profileRequest) {
        String userProfile = preferences.getString(Constants.SHARED_PREFS_KEY_USER_INFO, "");
        Log.d("getCachedProfile", "getCachedProfile: " + userProfile);
        if (userProfile.isEmpty()) {
            profileRequest.onProfileFetchFailed(new Throwable("Empty user profile!"));
        } else {
            ProfileModel profileModel = new Gson().fromJson(userProfile, ProfileModel.class);
            setProfile(profileModel);
            profileRequest.onProfileFetch(profileModel);
        }
    }

    public void saveProfileInfo(ProfileModel model) {
        preferences
                .edit()
                .putString(Constants.SHARED_PREFS_KEY_USER_INFO, new Gson().toJson(model))
                .apply();
    }
}
