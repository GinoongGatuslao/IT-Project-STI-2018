package com.android.itproj.mb40marketing.view.activities;

import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.Toast;

import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.helper.interfaces.AccountCallback;
import com.android.itproj.mb40marketing.helper.interfaces.AuthenticationCallback;
import com.android.itproj.mb40marketing.model.AccountModel;

public class HomeActivity extends AppCompatActivity implements
        AuthenticationCallback.AuthLogoutCallback,
        AccountCallback{

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        toolbar.setTitle("");
        setSupportActionBar(toolbar);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.menu_profile:
                break;
            case R.id.menu_logout:
                ((CoreApp)getApplication())
                        .getAuthenticationController()
                        .logout(this);
                break;
        }
        return true;
    }

    @Override
    public void onLogoutSuccess() {
        Intent intent = new Intent(this, LoginActivity.class);
        startActivity(intent);
        finish();
    }

    @Override
    public void onLogoutFailed(Throwable e) {
        Toast.makeText(this, "Failed to logout", Toast.LENGTH_LONG).show();
    }

    @Override
    public void onAccountRequest(AccountModel accountModel) {
        checkIfProfileVerified();
    }

    @Override
    public void onError(Throwable throwable) {
        Toast.makeText(this, throwable.getMessage(), Toast.LENGTH_LONG).show();
    }

    private void getAccountDetail() {
        ((CoreApp)getApplication())
                .getAccountController()
                .getAccountDetail(((CoreApp) getApplication()).getProfileController().getProfile(), this);

    }

    private void checkIfProfileVerified() {
        if (!((CoreApp) getApplication()).getProfileController().getProfile().isVerified()) {
            showVerifictionNotice();
        } else {

        }
    }

    private void showVerifictionNotice() {
    }
}
