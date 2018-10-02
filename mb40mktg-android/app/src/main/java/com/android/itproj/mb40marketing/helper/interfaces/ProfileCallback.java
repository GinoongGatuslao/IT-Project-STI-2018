package com.android.itproj.mb40marketing.helper.interfaces;

import com.android.itproj.mb40marketing.model.AccountModel;
import com.android.itproj.mb40marketing.model.LoanItemSummaryModel;
import com.android.itproj.mb40marketing.model.LoanModel;
import com.android.itproj.mb40marketing.model.ProfileModel;

import java.util.List;

public class ProfileCallback {

    public interface ProfileRegister {
        void onProfileRegisterSuccess(ProfileModel model);

        void onProfileRegisterFailed(Throwable throwable, int code);
    }

    public interface ProfileRequest {
        void onProfileFetch(ProfileModel model);

        void onProfileFetch(List<ProfileModel> models);

        void onProfileFetchFailed(Throwable throwable, int code);
    }

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
