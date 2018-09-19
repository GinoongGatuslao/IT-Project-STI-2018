package com.android.itproj.mb40marketing.view.activities;

import android.content.Intent;
import android.os.Bundle;
import android.support.constraint.ConstraintLayout;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.adapter.LoanListAdapter;
import com.android.itproj.mb40marketing.helper.interfaces.AccountCallback;
import com.android.itproj.mb40marketing.helper.interfaces.AuthenticationCallback;
import com.android.itproj.mb40marketing.helper.interfaces.ProfileCallbacks;
import com.android.itproj.mb40marketing.model.AccountModel;
import com.android.itproj.mb40marketing.model.LoanModel;
import com.android.itproj.mb40marketing.model.ProfileModel;

import org.apache.http.HttpStatus;

import java.util.Arrays;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import retrofit2.HttpException;

public class HomeActivity extends AppCompatActivity implements
        AuthenticationCallback.AuthLogoutCallback,
        SwipeRefreshLayout.OnRefreshListener,
        ProfileCallbacks.ProfileRequest,
        AccountCallback {

    private static final String TAG = "HomeActivity";

    @BindView(R.id.accountRequestBtn)
    public Button accountRequestBtn;

    @BindView(R.id.loanListView)
    public ListView loanListView;

    @BindView(R.id.noticeFrame)
    public ConstraintLayout noticeFrame;

    @BindView(R.id.noticeText)
    public TextView noticeText;

    @BindView(R.id.pullDownRefresh)
    public TextView pullDownRefreshText;

    @BindView(R.id.swipe_container)
    public SwipeRefreshLayout swipeRefreshLayout;

    AccountModel accountModel;

    List<LoanModel> loanModelList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_home);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        toolbar.setTitle(getString(R.string.app_page_account));
        setSupportActionBar(toolbar);

        ButterKnife.bind(this);

        swipeRefreshLayout.setOnRefreshListener(this);
    }

    @Override
    protected void onResume() {
        super.onResume();
        swipeRefreshLayout.setRefreshing(true);
        onRefresh();
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
                ((CoreApp) getApplication())
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
    public void onLogoutFailed(Throwable e, int code) {
        Toast.makeText(this, "Failed to logout", Toast.LENGTH_LONG).show();
    }

    @Override
    public void onAccountRequest(AccountModel accountModel) {
        this.accountModel = accountModel;
        ((CoreApp) getApplication())
                .getAccountController()
                .getLoans(this.accountModel.getId(), this);
    }

    @Override
    public void onLoanListRequest(List<LoanModel> loanModelList) {
        Log.d(TAG, "onLoanListRequest: " + Arrays.deepToString(loanModelList.toArray()));
        if (loanModelList.size() > 0) {
            swipeRefreshLayout.setRefreshing(false);
            setVisibility(View.GONE, noticeFrame);
            setVisibility(View.VISIBLE, loanListView);
            setFieldDetails(loanModelList);
        }
    }

    @Override
    public void onError(Throwable throwable, int code) {
        Log.e(TAG, "onError: ", throwable);
        Toast.makeText(this, throwable.getMessage(), Toast.LENGTH_LONG).show();
    }

    @Override
    public void onRefresh() {
        Log.d(TAG, "refreshing...");
        ((CoreApp)getApplication())
                .getProfileController()
                .getUserProfile(
                        ((CoreApp)getApplication())
                                .getProfileController()
                                .getProfile()
                                .getUser_id()
                ,this);
    }

    @Override
    public void onProfileFetch(ProfileModel model) {
        Log.d(TAG, "onProfileFetch: " + model.toString());
        checkIfProfileVerified();
    }

    @Override
    public void onProfileFetchFailed(Throwable throwable, int code) {
        Toast.makeText(this, throwable.getMessage(), Toast.LENGTH_LONG).show();
        if (code == HttpStatus.SC_UNAUTHORIZED) {
            ((CoreApp)getApplication())
                    .getAuthenticationController()
                    .forceLogout(this);
        }
    }

    private void setFieldDetails(List<LoanModel> loanModelList) {
        LoanListAdapter loanListAdapter = new LoanListAdapter(getApplication(), loanModelList);
        loanListView.setAdapter(loanListAdapter);
    }

    private void getAccountDetail() {
        ((CoreApp) getApplication())
                .getAccountController()
                .getAccountDetail(((CoreApp) getApplication()).getProfileController().getProfile(), this);

    }

    private void checkIfProfileVerified() {
        if (((CoreApp) getApplication()).getProfileController().getProfile().getVerified() != 1) {
            swipeRefreshLayout.setRefreshing(false);
            showVerificationNotice();
        } else {
            getAccountDetail();
        }
    }

    private void showVerificationNotice() {
        setVisibility(View.GONE, loanListView, accountRequestBtn);
        loanListView.setVisibility(View.GONE);

        setVisibility(View.VISIBLE, noticeFrame, noticeText, pullDownRefreshText);
        noticeText.setText(this.getString(R.string.text_not_verified));
    }

    private void setVisibility(int visibility, View... views) {
        for (View view : views) {
            view.setVisibility(visibility);
            view.invalidate();
        }
    }
}
