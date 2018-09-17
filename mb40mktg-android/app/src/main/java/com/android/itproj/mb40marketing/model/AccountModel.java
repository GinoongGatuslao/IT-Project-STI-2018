package com.android.itproj.mb40marketing.model;

import android.accounts.Account;

import com.google.gson.annotations.SerializedName;

import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

@ToString
public class AccountModel {

    public AccountModel(int profile_id) {
        setProfile_id(profile_id);
    }

    @SerializedName("profile_id")
    @Setter
    @Getter
    public int profile_id;

    @SerializedName("credit_limit")
    @Getter
    public int credit_limit;

    @SerializedName("status")
    @Getter
    public String status;


}
