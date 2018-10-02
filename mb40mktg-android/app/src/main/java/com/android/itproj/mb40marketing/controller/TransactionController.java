package com.android.itproj.mb40marketing.controller;

import android.content.Context;

import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.helper.interfaces.TransactionCallback;
import com.android.itproj.mb40marketing.model.TransactionModel;

import java.util.List;

import retrofit2.HttpException;
import rx.Observer;
import rx.subscriptions.CompositeSubscription;

public class TransactionController {

    private CompositeSubscription compositeSubscription;
    private Context context;

    public TransactionController(Context context) {
        this.context = context;
        this.compositeSubscription = new CompositeSubscription();
    }

    public void getTransactionRecords(int loanId, int profileId, final TransactionCallback callback) {
        compositeSubscription.add(
                ((CoreApp) context)
                        .getRestAPI()
                        .getTransactionRecords(loanId, profileId)
                        .subscribe(new Observer<List<TransactionModel>>() {
                            @Override
                            public void onCompleted() {

                            }

                            @Override
                            public void onError(Throwable e) {
                                callback.onError(e, ((HttpException)e).code());
                            }

                            @Override
                            public void onNext(List<TransactionModel> transactionModels) {
                                callback.onTransactionRecordRequest(transactionModels);
                            }
                        })
        );
    }

    public void createTransaction(TransactionModel transactionModel, final TransactionCallback callback) {
        compositeSubscription.add(
                ((CoreApp)context)
                .getRestAPI()
                .recordTransaction(transactionModel)
                .subscribe(new Observer<TransactionModel>() {
                    @Override
                    public void onCompleted() {

                    }

                    @Override
                    public void onError(Throwable e) {
                        callback.onError(e, ((HttpException)e).code());
                    }

                    @Override
                    public void onNext(TransactionModel transactionModel) {
                        callback.onTransactionRecord(transactionModel);
                    }
                })
        );
    }
}
