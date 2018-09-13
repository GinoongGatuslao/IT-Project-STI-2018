package com.android.itproj.mb40marketing.model;

import com.google.gson.annotations.SerializedName;

import lombok.Getter;
import lombok.ToString;

@ToString
public class AccountModel {

    @SerializedName("profile_id")
    @Getter
    public int profile_id;

    @SerializedName("credit_limit")
    @Getter
    public int credit_limit;

    @SerializedName("path_id_pic")
    @Getter
    public String path_id_pic;

    @SerializedName("path_house_sketch_pic")
    @Getter
    public String path_house_sketch_pic;

    @SerializedName("verified")
    @Getter
    public boolean verified;

    @SerializedName("status")
    @Getter
    public String status;


}
