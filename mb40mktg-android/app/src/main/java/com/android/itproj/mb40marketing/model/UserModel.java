package com.android.itproj.mb40marketing.model;

import com.google.gson.annotations.SerializedName;

import lombok.Getter;
import lombok.ToString;

@ToString
public class UserModel {

    @SerializedName("id")
    @Getter
    private int id;

    @SerializedName("first_name")
    @Getter
    private String first_name;

    @SerializedName("middle_name")
    @Getter
    private String middle_name;

    @SerializedName("last_name")
    @Getter
    private String last_name;

    @SerializedName("address")
    @Getter
    private String address;

    @SerializedName("contact")
    @Getter
    private String contact;

    @SerializedName("birth")
    @Getter
    private String birth;

    @SerializedName("occupation")
    @Getter
    private String occupation;

    @SerializedName("income")
    @Getter
    private String income;

    @SerializedName("est_monthly_expenses")
    @Getter
    private String est_monthly_expenses;

    @SerializedName("username")
    @Getter
    private String username;

    @SerializedName("api_token")
    @Getter
    private String api_token;
}
