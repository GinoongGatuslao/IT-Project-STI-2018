package com.android.itproj.mb40marketing.helper.restservice;

import com.android.itproj.mb40marketing.model.LoanItemSummaryModel;
import com.android.itproj.mb40marketing.model.LoanModel;
import com.android.itproj.mb40marketing.model.ProfileModel;
import com.android.itproj.mb40marketing.model.TransactionModel;
import com.android.itproj.mb40marketing.model.UserLogin;
import com.android.itproj.mb40marketing.model.UserModel;
import com.google.gson.JsonObject;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import okhttp3.ResponseBody;
import retrofit2.Response;
import rx.Observable;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

public class RestAPI {

    private final RestAPIService restService;
    private static final String TAG = "RestAPI";

    public RestAPI(RestAPIService restService) {
        this.restService = restService;
    }

    ///////////////////////////////////////////////////////////////////////////
    // Login
    ///////////////////////////////////////////////////////////////////////////
    public Observable<UserModel> login(UserLogin userLogin) {
        return restService
                .doLogin(userLogin)
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());
    }

    ///////////////////////////////////////////////////////////////////////////
    // Logout
    ///////////////////////////////////////////////////////////////////////////
    public Observable<Response<ResponseBody>> logout() {
        return restService
                .doLogout()
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());
    }

    ///////////////////////////////////////////////////////////////////////////
    // Register User
    ///////////////////////////////////////////////////////////////////////////
    public Observable<JsonObject> registerUser(JsonObject userModel) {
        return restService
                .registerUser(userModel)
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());
    }

    ///////////////////////////////////////////////////////////////////////////
    // Create User Profile
    ///////////////////////////////////////////////////////////////////////////
    public Observable<ProfileModel> createProfile(ProfileModel model) {
        return restService
                .createProfile(model)
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());

    }

    public Observable<ProfileModel> updateProfile(int profileId, ProfileModel updatedProfile) {
        return restService
                .updateProfile(profileId, updatedProfile)
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());
    }

    ///////////////////////////////////////////////////////////////////////////
    // Get User Profile
    ///////////////////////////////////////////////////////////////////////////
    public Observable<ProfileModel> getUserProfile(int userId) {
        return restService
                .getUserProfile(userId)
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());

    }

    ///////////////////////////////////////////////////////////////////////////
    // Update user account
    ///////////////////////////////////////////////////////////////////////////
    public Observable<UserModel> updateUserAccount(int userId, JsonObject jsonObject) {
        return restService
                .updateUserAccount(userId, jsonObject)
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());
    }

    ///////////////////////////////////////////////////////////////////////////
    // Get User Profile By Name
    ///////////////////////////////////////////////////////////////////////////
    public Observable<List<ProfileModel>> getUserProfileByName(Map<String, String> headers) {
        return restService
                .getUserProfileByName(headers)
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());
    }

    public Observable<List<ProfileModel>> getAllProfiles() {
        return restService
                .getAllProfiles()
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());
    }

    ///////////////////////////////////////////////////////////////////////////
    // Get Loans by account id
    ///////////////////////////////////////////////////////////////////////////
    public Observable<List<LoanModel>> getLoanList(int accountId) {
        return restService
                .getLoans(accountId)
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());

    }

    ///////////////////////////////////////////////////////////////////////////
    // Get Loaned items by loan id
    ///////////////////////////////////////////////////////////////////////////
    public Observable<List<LoanItemSummaryModel>> getLoanItems(int loan_id) {
        return restService
                .getLoanItems(loan_id)
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());
    }

    ///////////////////////////////////////////////////////////////////////////
    // Get transaction records
    ///////////////////////////////////////////////////////////////////////////
    public Observable<List<TransactionModel>> getTransactionRecords(int loan_id, int profile_id) {
        Map<String, Integer> header = new HashMap<>();
        header.put("loan_id", loan_id);
        header.put("profile_id", profile_id);
        return restService
                .getTransactionRecords(header)
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());
    }

    ///////////////////////////////////////////////////////////////////////////
    // Record transaction
    ///////////////////////////////////////////////////////////////////////////
    public Observable<TransactionModel> recordTransaction(TransactionModel model) {
        return restService
                .recordTransaction(model)
                .observeOn(AndroidSchedulers.mainThread())
                .subscribeOn(Schedulers.io());
    }
}
