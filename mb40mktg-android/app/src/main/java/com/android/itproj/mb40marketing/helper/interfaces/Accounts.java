package com.android.itproj.mb40marketing.helper.interfaces;

import com.android.itproj.mb40marketing.model.AccountModel;
import com.android.itproj.mb40marketing.model.LoanItemSummaryModel;
import com.android.itproj.mb40marketing.model.LoanModel;

import java.util.List;

public class Accounts {

    public interface UserAccountCallback {
        void onAccountRequest(AccountModel accountModel);

        void onError(Throwable throwable, int code);
    }

    public interface UserLoanCallback {
        void onLoanListRequest(List<LoanModel> loanModelList);

        void onError(Throwable throwable, int code);
    }

    public interface UserLoanItemsCallback {
        void onLoanItemListRequest(List<LoanItemSummaryModel> loanItemSummaryModels);

        void onError(Throwable throwable, int code);
    }
}
