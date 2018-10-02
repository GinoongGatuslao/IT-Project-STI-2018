package com.android.itproj.mb40marketing.model;

import com.google.gson.annotations.SerializedName;

import lombok.Getter;
import lombok.ToString;

@ToString
public class LoanItemSummaryModel {

    @SerializedName("item_name")
    @Getter
    public String item_name;

    @SerializedName("price")
    @Getter
    public String price;

    @SerializedName("interest")
    @Getter
    public String interest;
}
