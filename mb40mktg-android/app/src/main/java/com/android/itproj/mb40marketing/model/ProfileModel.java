package com.android.itproj.mb40marketing.model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

@ToString
public class ProfileModel implements Serializable{

    @Expose(deserialize = false)
    @Getter
    @Setter
    private int id;

    @SerializedName("user_id")
    @Getter
    @Setter
    private int user_id;

    @SerializedName("first_name")
    @Getter
    @Setter
    private String first_name;

    @SerializedName("middle_name")
    @Getter
    @Setter
    private String middle_name;

    @SerializedName("last_name")
    @Getter
    @Setter
    private String last_name;

    @SerializedName("gender")
    @Getter
    @Setter
    private String gender;

    @SerializedName("address")
    @Getter
    @Setter
    private String address;

    @SerializedName("contact_num")
    @Getter
    @Setter
    private String contact;

    @SerializedName("bday")
    @Getter
    @Setter
    private String birth;

    @SerializedName("occupation")
    @Getter
    @Setter
    private String occupation;

    @SerializedName("mo_income")
    @Getter
    @Setter
    private String income;

    @SerializedName("mo_expense")
    @Getter
    @Setter
    private String est_monthly_expenses;

    @SerializedName("verified")
    @Getter
    public int verified;

    @SerializedName("credit_limit")
    @Getter
    public int credit_limit;

    @SerializedName("account_status")
    @Getter
    public int account_status;
}
