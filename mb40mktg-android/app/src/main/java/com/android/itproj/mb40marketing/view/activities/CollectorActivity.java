package com.android.itproj.mb40marketing.view.activities;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Handler;
import android.os.PersistableBundle;
import android.support.constraint.ConstraintLayout;
import android.support.v4.content.ContextCompat;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.AdapterView;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.adapter.ProfileListViewAdapter;
import com.android.itproj.mb40marketing.helper.interfaces.AuthenticationCallback;
import com.android.itproj.mb40marketing.helper.interfaces.ProfileCallback;
import com.android.itproj.mb40marketing.model.ProfileModel;

import java.util.Arrays;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;

public class CollectorActivity extends AppCompatActivity implements
        SwipeRefreshLayout.OnRefreshListener,
        TextWatcher,
        ProfileCallback.ProfileRequest,
        AuthenticationCallback.AuthLogoutCallback, AdapterView.OnItemClickListener {

    private static final String TAG = "CollectorActivity";

    @BindView(R.id.swipe_container)
    public SwipeRefreshLayout swipeRefreshLayout;

    @BindView(R.id.searchFname)
    public EditText searchFname;

    @BindView(R.id.searchLname)
    public EditText searchLname;

    @BindView(R.id.searchResultList)
    public ListView searchResultList;

    @BindView(R.id.searchFormFrame)
    public ConstraintLayout searchFormFrame;

    @BindView(R.id.noticeFrame)
    public ConstraintLayout noticeFrame;

    @BindView(R.id.noticeText)
    public TextView noticeText;

    private Runnable oldRunnable;
    private Handler handler = new Handler();

    private ProfileListViewAdapter profileListViewAdapter;

    private List<ProfileModel> allProfileList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_collector);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        toolbar.setTitle(getString(R.string.label_search_customer));
        setSupportActionBar(toolbar);

        ButterKnife.bind(this);

        swipeRefreshLayout.setOnRefreshListener(this);
        searchFname.addTextChangedListener(this);
        searchLname.addTextChangedListener(this);

        if (savedInstanceState != null) {
            //lets try to recover recent query
            String fname = savedInstanceState.getString("fname");
            String lname = savedInstanceState.getString("lname");
            if (!fname.isEmpty()) {
                searchFname.setText(fname);
            }

            if (!lname.isEmpty()) {
                searchLname.setText(lname);
            }
        }

        setVisibility(View.GONE, searchFormFrame, noticeFrame);
        swipeRefreshLayout.setRefreshing(true);
        swipeRefreshLayout.setEnabled(true);
        swipeRefreshLayout.setColorSchemeColors(
                getResources().getColor(R.color.colorAccent),
                getResources().getColor(R.color.colorLink));

        searchResultList.setOnItemClickListener(this);

        onRefresh();
    }

    @Override
    public void onSaveInstanceState(Bundle outState, PersistableBundle outPersistentState) {
        super.onSaveInstanceState(outState, outPersistentState);
        outState.putString("fname", searchFname.getText().toString());
        outState.putString("lname", searchLname.getText().toString());
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
    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
        Intent intent = new Intent(this, CustomerLoansActivity.class);
        intent.putExtra("profile", profileListViewAdapter.getItem(i));
        startActivity(intent);
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
        setErrorMessage(getString(R.string.err_server_unavailable), code);
    }

    @Override
    public void onProfileFetch(ProfileModel model) {
        enableFormFields(true);
        swipeRefreshLayout.setRefreshing(false);
        Log.d(TAG, "onProfileFetch: " + model.toString());
        checkIfProfileVerified();
    }

    @Override
    public void onProfileFetch(List<ProfileModel> models) {
        enableFormFields(true);
        swipeRefreshLayout.setRefreshing(false);
        Log.d(TAG, "onProfileFetch: " + Arrays.deepToString(models.toArray()));
        updateCustomerList(models);
    }

    @Override
    public void onProfileFetchFailed(Throwable throwable, int code) {
        Toast.makeText(this, "Unable to profile(s). Try again.", Toast.LENGTH_LONG).show();
        swipeRefreshLayout.setRefreshing(false);
        hideKeyboardFrom(this, findViewById(android.R.id.content).getRootView());
    }

    @Override
    public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {
        if (oldRunnable != null) {
            handler.removeCallbacks(oldRunnable);
        }
    }

    @Override
    public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {
    }

    @Override
    public void afterTextChanged(Editable editable) {
        Log.d(TAG, "afterTextChanged: " + editable.toString());
        if (!editable.toString().isEmpty()) {
            oldRunnable = new Runnable() {
                @Override
                public void run() {
                    //request to API here
                    runOnUiThread(new Runnable() {
                        @Override
                        public void run() {
                            hideKeyboardFrom(CollectorActivity.this, findViewById(android.R.id.content).getRootView());
                            swipeRefreshLayout.setRefreshing(true);
                            ((CoreApp) getApplication())
                                    .getProfileController()
                                    .getUserProfileByName(
                                            searchFname.getText().toString(),
                                            searchLname.getText().toString(),
                                            "3",
                                            CollectorActivity.this);
                        }
                    });
                }
            };
            handler.postDelayed(oldRunnable, 1000);
        } else {
            updateCustomerList(allProfileList);
        }
    }

    @Override
    public void onRefresh() {
        enableFormFields(false);
        ((CoreApp) getApplication())
                .getProfileController()
                .getUserProfile(
                        ((CoreApp) getApplication())
                                .getProfileController()
                                .getProfile()
                                .getUser_id()
                        , this);

        ((CoreApp) getApplication())
                .getProfileController()
                .getUserProfileByName(
                        "",
                        "",
                        "3",
                        new ProfileCallback.ProfileRequest() {
                            @Override
                            public void onProfileFetch(ProfileModel model) {

                            }

                            @Override
                            public void onProfileFetch(List<ProfileModel> models) {
                                allProfileList = models;
                                updateCustomerList(allProfileList);
                            }

                            @Override
                            public void onProfileFetchFailed(Throwable throwable, int code) {
                                CollectorActivity.this.onProfileFetchFailed(throwable, code);
                            }
                        });
    }

    private void updateCustomerList(List<ProfileModel> profileModels) {
        profileListViewAdapter = new ProfileListViewAdapter(this, profileModels);
        searchResultList.setAdapter(profileListViewAdapter);
    }

    private void checkIfProfileVerified() {
        if (((CoreApp) getApplication()).getProfileController().getProfile().getVerified() != 1) {
            swipeRefreshLayout.setRefreshing(false);
            swipeRefreshLayout.setEnabled(true);
            showVerificationNotice();
        } else {
            swipeRefreshLayout.setEnabled(false);
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

    private void setErrorMessage(String message, int code) {
        setVisibility(View.VISIBLE, noticeFrame);
        noticeText.setText(message);
        noticeText.setTextColor(ContextCompat.getColor(this, R.color.colorPalePink));
        swipeRefreshLayout.setRefreshing(false);
    }

    public void hideKeyboardFrom(Context context, View view) {
        InputMethodManager imm = (InputMethodManager) context.getSystemService(Activity.INPUT_METHOD_SERVICE);
        imm.hideSoftInputFromWindow(view.getWindowToken(), 0);
    }

    public void enableFormFields(boolean enable) {
        searchFname.setEnabled(enable);
        searchLname.setEnabled(enable);
    }
}
