package com.android.itproj.mb40marketing.helper.interfaces;

import com.android.itproj.mb40marketing.model.TransactionModel;

import java.util.List;

public interface TransactionCallback {

    void onTransactionRecordRequest(List<TransactionModel> transactionModels);

    void onTransactionRecord(TransactionModel transactionModel);

    void onError(Throwable throwable, int code);
}
