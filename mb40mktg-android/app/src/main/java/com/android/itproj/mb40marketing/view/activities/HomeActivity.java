package com.android.itproj.mb40marketing.view.activities;

import android.content.Intent;
import android.support.v4.app.FragmentActivity;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;

import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.controller.AuthenticationController;
import com.android.itproj.mb40marketing.helper.interfaces.AuthenticationCallback;
import com.android.itproj.mb40marketing.model.UserModel;

public class HomeActivity extends FragmentActivity implements
        AuthenticationCallback.AuthLogoutCallback,
        View.OnClickListener{

    private static final String TAG = "HomeActivity";

    private Button logoutBtn;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);

        logoutBtn = (Button) findViewById(R.id.logoutBtn);
        logoutBtn.setOnClickListener(this);
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

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.logoutBtn:
                ((CoreApp)getApplication())
                        .getAuthenticationController()
                        .logout(this);
                break;
        }
    }
}
