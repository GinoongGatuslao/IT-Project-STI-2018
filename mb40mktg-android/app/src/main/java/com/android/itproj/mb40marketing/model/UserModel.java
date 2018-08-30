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

    @SerializedName("email")
    @Getter
    private String email;

    @SerializedName("password")
    @Getter
    private String password;

    @SerializedName("api_token")
    @Getter
    private String api_token;
}
