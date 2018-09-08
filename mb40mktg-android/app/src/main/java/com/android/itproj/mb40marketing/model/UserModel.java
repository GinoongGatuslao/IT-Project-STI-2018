package com.android.itproj.mb40marketing.model;

import com.google.gson.annotations.SerializedName;

import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

@ToString
public class UserModel {

    @SerializedName("id")
    @Getter @Setter
    private int id;

    @SerializedName("username")
    @Getter @Setter
    private String username;

    @SerializedName("api_token")
    @Getter @Setter
    private String api_token;

    @SerializedName("user_type")
    @Getter @Setter
    private int user_type;

    @Setter
    private String password;

    @Setter
    private String password_confirmation;
}
