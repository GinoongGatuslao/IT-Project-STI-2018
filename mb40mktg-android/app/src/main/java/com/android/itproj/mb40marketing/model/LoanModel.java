package com.android.itproj.mb40marketing.model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.List;

import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

@ToString
public class LoanModel {

    @Expose(deserialize = false)
    @Getter
    private int id;

    @SerializedName("profile_id")
    @Getter
    private int profile_id;

    @SerializedName("term_length")
    @Getter
    private int term_length;

    @SerializedName("loan_value")
    @Getter
    private float loan_value;

    @SerializedName("amortization")
    @Getter
    private float amortization;

    @Expose(deserialize = false)
    @SerializedName("running_balance")
    @Getter
    private float running_balance;

    @Expose(deserialize = false, serialize = false)
    @Getter
    @Setter
    private List<LoanItemSummaryModel> loanItemSummary;

    @Expose(deserialize = false)
    @SerializedName("status_str")
    @Getter
    private String status_str;

    @SerializedName("status")
    @Getter
    private int status;

    @SerializedName("created_at")
    @Getter
    private String created_at;
}
