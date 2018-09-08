package com.android.itproj.mb40marketing.view.activities;

import android.content.Intent;
import android.support.v4.app.FragmentActivity;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.controller.AuthenticationController;
import com.android.itproj.mb40marketing.helper.interfaces.AuthenticationCallback;
import com.android.itproj.mb40marketing.model.UserModel;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class HomeActivity extends FragmentActivity implements
        AuthenticationCallback.AuthLogoutCallback{

    private static final String TAG = "HomeActivity";

    @BindView(R.id.logoutBtn)
    public Button logoutBtn;

    @BindView(R.id.userInfo)
    public TextView userInfo;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);
        ButterKnife.bind(this);

        userInfo.setText(
                ((CoreApp)getApplication())
                        .getProfileController()
                        .getProfile()
                        .toString()
        );
    }

    @Override
    public void onLogoutSuccess() {
        Intent backToLogin = new Intent(this, LoginActivity.class);
        startActivity(backToLogin);
        finish();
    }

    @Override
    public void onLogoutFailed(Throwable e) {
        Log.e(TAG, "onLogoutFailed: ", e);
    }

    @OnClick(R.id.logoutBtn)
    public void logout() {
        ((CoreApp)getApplication())
                .getAuthenticationController()
                .logout(this);
    }
}
