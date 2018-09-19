package com.android.itproj.mb40marketing.model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.List;

import lombok.Getter;
import lombok.Setter;

public class LoanModel {

    @Expose(deserialize = false)
    @Getter
    private int id;

    @SerializedName("account_id")
    @Getter
    private int account_id;

    @SerializedName("term_length")
    @Getter
    private int term_length;

    @SerializedName("loan_value")
    @Getter
    private float loan_value;

    @SerializedName("amortization")
    @Getter
    private float amortization;

    @Expose(deserialize = false, serialize = false)
    @Getter
    @Setter
    private List<LoanItemSummaryModel> loanItemSummary;

    @SerializedName("status")
    @Getter
    private int status;

    @SerializedName("created_at")
    @Getter
    private String created_at;
}
