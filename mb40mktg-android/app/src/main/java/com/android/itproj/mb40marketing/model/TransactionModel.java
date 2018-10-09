package com.android.itproj.mb40marketing.model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

@ToString
public class TransactionModel {

    @Expose(deserialize = false)
    @Getter
    private int id;

    @SerializedName("profile_id")
    @Getter
    @Setter
    private int profile_id;

    @SerializedName("loan_id")
    @Getter
    @Setter
    private int loan_id;

    @SerializedName("collector_id")
    @Getter
    @Setter
    private int collector_id;

    @Expose(deserialize = false)
    @SerializedName("c_fname")
    @Getter
    private String collectorFname;

    @Expose(deserialize = false)
    @SerializedName("c_mname")
    @Getter
    private String collectorMname;

    @Expose(deserialize = false)
    @SerializedName("c_lname")
    @Getter
    private String collectorLname;

    @SerializedName("payment")
    @Getter
    @Setter
    private double payment;

    @SerializedName("payment_total")
    @Getter
    @Setter
    private double payment_total;

    @SerializedName("remaining_balance")
    @Getter
    @Setter
    private double remaining_balance;

    @SerializedName("created_at")
    @Getter @Setter
    private String created_at;

    @SerializedName("updated_at")
    @Getter @Setter
    private String updated_at;

}
