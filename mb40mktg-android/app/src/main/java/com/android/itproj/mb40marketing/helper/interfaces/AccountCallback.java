package com.android.itproj.mb40marketing.helper.interfaces;

import com.android.itproj.mb40marketing.model.AccountModel;
import com.android.itproj.mb40marketing.model.LoanModel;

import java.util.List;

public interface AccountCallback {

    void onAccountRequest(AccountModel accountModel);

    void onLoanListRequest(List<LoanModel> loanModelList);

    void onError(Throwable throwable);
}
