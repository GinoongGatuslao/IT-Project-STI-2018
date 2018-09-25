package com.android.itproj.mb40marketing.view.activities;

import android.os.Handler;
import android.support.constraint.ConstraintLayout;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.View;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.ListView;

import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.helper.interfaces.ProfileCallbacks;
import com.android.itproj.mb40marketing.model.ProfileModel;

import java.util.Arrays;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnTextChanged;

public class CollectorActivity extends AppCompatActivity implements
        SwipeRefreshLayout.OnRefreshListener,
        TextWatcher,
        ProfileCallbacks.ProfileRequest{

    private static final String TAG = "CollectorActivity";

    @BindView(R.id.swipe_container)
    public SwipeRefreshLayout swipeRefreshLayout;

    @BindView(R.id.searchFname)
    public EditText searchFname;

    @BindView(R.id.searchLname)
    public EditText searchLname;

    @BindView(R.id.searchLoanId)
    public EditText searchLoanId;

    @BindView(R.id.searchResultList)
    public ListView searchResultList;

    @BindView(R.id.searchFormFrame)
    public ConstraintLayout searchFormFrame;

    @BindView(R.id.noticeFrame)
    public ConstraintLayout noticeFrame;

    private Runnable oldRunnable;
    private Handler handler = new Handler();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_collector);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        toolbar.setTitle(getString(R.string.app_page_collection));
        setSupportActionBar(toolbar);

        ButterKnife.bind(this);

        swipeRefreshLayout.setOnRefreshListener(this);
        searchFname.addTextChangedListener(this);
        searchLname.addTextChangedListener(this);
    }

    @Override
    public void onProfileFetch(ProfileModel model) {
        swipeRefreshLayout.setRefreshing(false);
        Log.d(TAG, "onProfileFetch: " + model.toString());
        checkIfProfileVerified();
    }

    @Override
    public void onProfileFetch(List<ProfileModel> models) {
        Log.d(TAG, "onProfileFetch: " + Arrays.deepToString(models.toArray()));

    }

    @Override
    public void onProfileFetchFailed(Throwable throwable, int code) {
        swipeRefreshLayout.setRefreshing(false);

    }

    @Override
    public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {
        Log.d(TAG, "beforeTextChanged: " + charSequence);
        if (oldRunnable != null) {
            handler.removeCallbacks(oldRunnable);
        }
    }

    @Override
    public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {
        Log.d(TAG, "onTextChanged: " + charSequence);
    }

    @Override
    public void afterTextChanged(Editable editable) {
        oldRunnable = new Runnable() {
            @Override
            public void run() {
                //request to API here
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        Log.d(TAG, "run: call to API");
                        ((CoreApp) getApplication())
                                .getProfileController()
                                .getUserProfileByName(
                                        searchFname.getText().toString(),
                                        searchLname.getText().toString(),
                                        CollectorActivity.this);
                    }
                });
            }
        };
        handler.postDelayed(oldRunnable, 2000);
    }

    @Override
    public void onRefresh() {
        Log.d(TAG, "refreshing...");
        ((CoreApp) getApplication())
                .getProfileController()
                .getUserProfile(
                        ((CoreApp) getApplication())
                                .getProfileController()
                                .getProfile()
                                .getUser_id()
                        , this);
    }

    private void checkIfProfileVerified() {
        if (((CoreApp) getApplication()).getProfileController().getProfile().getVerified() != 1) {
            swipeRefreshLayout.setRefreshing(false);
            showVerificationNotice();
        } else {
            setVisibility(View.GONE, noticeFrame);
            noticeFrame.invalidate();
            setVisibility(View.VISIBLE, searchFormFrame);
            searchFormFrame.invalidate();
        }
    }

    private void showVerificationNotice() {
        setVisibility(View.GONE, searchFormFrame);
        setVisibility(View.VISIBLE, noticeFrame, noticeFrame);
    }

    private void setVisibility(int visibility, View... views) {
        for (View view : views) {
            view.setVisibility(visibility);
            view.invalidate();
        }
    }

}
