package com.android.itproj.mb40marketing.helper.interfaces;

import com.android.itproj.mb40marketing.model.AccountModel;

public interface AccountCallback {

    void onAccountRequest(AccountModel accountModel);

    void onError(Throwable throwable);
}
