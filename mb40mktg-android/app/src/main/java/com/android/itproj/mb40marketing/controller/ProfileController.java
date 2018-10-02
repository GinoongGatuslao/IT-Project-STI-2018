package com.android.itproj.mb40marketing.controller;

import android.content.Context;
import android.content.SharedPreferences;
import android.util.Log;

import com.android.itproj.mb40marketing.Constants;
import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.helper.interfaces.ProfileCallback;
import com.android.itproj.mb40marketing.model.LoanItemSummaryModel;
import com.android.itproj.mb40marketing.model.LoanModel;
import com.android.itproj.mb40marketing.model.ProfileModel;
import com.google.gson.Gson;

import org.apache.http.HttpStatus;

import java.util.HashMap;
import java.util.List;

import javax.inject.Inject;

import lombok.Getter;
import lombok.Setter;
import retrofit2.HttpException;
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

    public void registerProfile(final ProfileModel profileModel, final ProfileCallback.ProfileRegister profileRegister) {
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
                                profileRegister.onProfileRegisterFailed(e, ((HttpException)e).code());
                            }

                            @Override
                            public void onNext(ProfileModel model) {
                                if (model != null) {
                                    saveProfileInfo(model);
                                    setProfile(model);
                                    profileRegister.onProfileRegisterSuccess(model);
                                } else {
                                    profileRegister.onProfileRegisterFailed(new Throwable("Failed to update profile!"), HttpStatus.SC_METHOD_FAILURE);
                                }
                            }
                        })
        );
    }

    public void getUserProfile(final int userId, final ProfileCallback.ProfileRequest profileRequest) {
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
                                profileRequest.onProfileFetchFailed(e, ((HttpException) e).code());
                            }

                            @Override
                            public void onNext(ProfileModel model) {
                                if (model != null) {
                                    saveProfileInfo(model);
                                    setProfile(model);
                                    profileRequest.onProfileFetch(model);
                                } else {
                                    profileRequest.onProfileFetchFailed(new Throwable("Failed to fetch User profile"), HttpStatus.SC_METHOD_FAILURE);
                                }
                            }
                        })
        );
    }

    public void getUserProfileByName(final String firstName, final String lastName, final String usertype, final ProfileCallback.ProfileRequest profileRequest) {
        compositeSubscription.add(
                ((CoreApp)context)
                .getRestAPI()
                .getUserProfileByName(new HashMap<String, String>() {{
                    put("fname", firstName);
                    put("lname", lastName);
                    put("usertype", usertype);
                }})
                .subscribe(new Observer<List<ProfileModel>>() {
                    @Override
                    public void onCompleted() {

                    }

                    @Override
                    public void onError(Throwable e) {
                        profileRequest.onProfileFetchFailed(e, ((HttpException)e).code());
                    }

                    @Override
                    public void onNext(List<ProfileModel> profileModels) {
                        profileRequest.onProfileFetch(profileModels);
                    }
                })
        );
    }

    public void getAllProfiles(final ProfileCallback.ProfileRequest profileRequest) {
        compositeSubscription.add(
                ((CoreApp)context)
                .getRestAPI()
                .getAllProfiles()
                .subscribe(new Observer<List<ProfileModel>>() {
                    @Override
                    public void onCompleted() {

                    }

                    @Override
                    public void onError(Throwable e) {
                        profileRequest.onProfileFetchFailed(e, ((HttpException)e).code());
                    }

                    @Override
                    public void onNext(List<ProfileModel> profileModels) {
                        profileRequest.onProfileFetch(profileModels);
                    }
                })
        );
    }

    public void getCachedProfile(ProfileCallback.ProfileRequest profileRequest) {
        String userProfile = preferences.getString(Constants.SHARED_PREFS_KEY_USER_INFO, "");
        Log.d("getCachedProfile", "getCachedProfile: " + userProfile);
        if (userProfile.isEmpty()) {
            profileRequest.onProfileFetchFailed(new Throwable("Empty user profile!"), HttpStatus.SC_METHOD_FAILURE);
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

    public void getLoans(int accountId, final ProfileCallback.UserLoanCallback callback) {
        compositeSubscription.add(
                ((CoreApp) context)
                        .getRestAPI()
                        .getLoanList(accountId)
                        .subscribe(new Observer<List<LoanModel>>() {
                            @Override
                            public void onCompleted() {

                            }

                            @Override
                            public void onError(Throwable e) {
                                callback.onError(e, ((HttpException)e).code());
                            }

                            @Override
                            public void onNext(List<LoanModel> loanModelList) {
                                callback.onLoanListRequest(loanModelList);
                            }
                        }));
    }

    public void getLoanItems(int loan_id, final ProfileCallback.UserLoanItemsCallback callback) {
        compositeSubscription.add(
                ((CoreApp)context)
                        .getRestAPI()
                        .getLoanItems(loan_id)
                        .subscribe(new Observer<List<LoanItemSummaryModel>>() {
                            @Override
                            public void onCompleted() {

                            }

                            @Override
                            public void onError(Throwable e) {
                                callback.onError(e, ((HttpException)e).code());
                            }

                            @Override
                            public void onNext(List<LoanItemSummaryModel> loanItemSummaryModels) {
                                callback.onLoanItemListRequest(loanItemSummaryModels);
                            }
                        })
        );
    }
}
