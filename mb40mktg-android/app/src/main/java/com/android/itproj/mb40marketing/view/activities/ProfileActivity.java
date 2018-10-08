package com.android.itproj.mb40marketing.view.activities;

import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.TextView;

import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.model.ProfileModel;
import com.android.itproj.mb40marketing.model.UserModel;

import butterknife.BindView;
import butterknife.ButterKnife;

public class ProfileActivity extends AppCompatActivity implements SwipeRefreshLayout.OnRefreshListener {

    @BindView(R.id.swipeRefreshLayout)
    public SwipeRefreshLayout swipeRefreshLayout;

    @BindView(R.id.profileName)
    public TextView profileName;

    @BindView(R.id.profileLoginName)
    public TextView profileLoginName;

    @BindView(R.id.profileType)
    public TextView profileType;

    @BindView(R.id.profileAddress)
    public TextView profileAddress;

    @BindView(R.id.profileContact)
    public TextView profileContact;

    @BindView(R.id.profileBirth)
    public TextView profileBirth;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_profile);
        ButterKnife.bind(this);
        swipeRefreshLayout.setOnRefreshListener(this);

        ProfileModel profileModel = ((CoreApp) getApplication())
                .getProfileController()
                .getProfile();

        setProfile(profileModel);
    }

    @Override
    public void onRefresh() {

    }

    private void setProfile(ProfileModel profile) {
        profileName.setText(
                String.format(
                        getString(R.string.text_customer_name_placeholder),
                        profile.getLast_name(),
                        profile.getFirst_name(),
                        profile.getMiddle_name()));
        profileLoginName.setText(
                String.format(
                        getString(R.string.label_logged_as_placeholder),
                        profile.getUsername()));
        profileType.setText(profile.getUsertype_str());
        profileAddress.setText(profile.getAddress());
        profileContact.setText(profile.getContact());
        profileBirth.setText(profile.getBirth());
    }
}