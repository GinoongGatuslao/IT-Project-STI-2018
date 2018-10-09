package com.android.itproj.mb40marketing.view.activities;

import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.helper.interfaces.ProfileCallback;
import com.android.itproj.mb40marketing.model.ProfileModel;
import com.android.itproj.mb40marketing.model.UserModel;
import com.mobsandgeeks.saripaar.ValidationError;
import com.mobsandgeeks.saripaar.Validator;
import com.mobsandgeeks.saripaar.annotation.ConfirmPassword;
import com.mobsandgeeks.saripaar.annotation.NotEmpty;
import com.mobsandgeeks.saripaar.annotation.Password;
import com.mobsandgeeks.saripaar.annotation.Pattern;

import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;

public class ProfileActivity extends AppCompatActivity implements
        SwipeRefreshLayout.OnRefreshListener,
        ProfileCallback.ProfileRequest,
        Validator.ValidationListener {

    @BindView(R.id.mainView)
    public View mainProfileView;

    //main views
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

    //edit views
    @BindView(R.id.editView)
    public View editProfileView;

    @NotEmpty
    @BindView(R.id.givenName)
    public EditText givenNameEditText;

    @NotEmpty
    @BindView(R.id.middleName)
    public EditText middleNameEditText;

    @NotEmpty
    @BindView(R.id.familyName)
    public EditText familyNameEditText;

    @NotEmpty
    @BindView(R.id.address)
    public EditText addressEditText;

    @NotEmpty
    @BindView(R.id.contact)
    public EditText contactEditText;

    @NotEmpty
    @BindView(R.id.birth)
    @Pattern(regex = "^(0[1-9]|1[0-2])\\/(0[1-9]|1\\d|2\\d|3[01])\\/(19|20)\\d{2}$")
    public EditText birthEditText;

    @NotEmpty
    @BindView(R.id.occupation)
    public EditText occupationEditText;

    @NotEmpty
    @BindView(R.id.income)
    public EditText incomeEditText;

    @NotEmpty
    @BindView(R.id.est_monthly_expenses)
    public EditText est_monthly_expensesEditText;

    @NotEmpty
    @BindView(R.id.username)
    public EditText usernameEditText;

    @NotEmpty
    @Password(scheme = Password.Scheme.ALPHA_NUMERIC,
            message = "Must contain alphanumeric characters")
    @BindView(R.id.password)
    public EditText passwordEditText;

    @NotEmpty
    @ConfirmPassword
    @BindView(R.id.confirm_password)
    public EditText confirm_passwordEditText;

    @BindView(R.id.registerBtn)
    public Button registerBtn;

    private Validator validator;

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

        validator = new Validator(this);
        validator.setValidationListener(this);
    }

    @Override
    public void onRefresh() {

    }

    @Override
    public void onProfileFetch(ProfileModel model) {

    }

    @Override
    public void onProfileFetch(List<ProfileModel> models) {

    }

    @Override
    public void onProfileFetchFailed(Throwable throwable, int code) {

    }

    @Override
    public void onValidationSucceeded() {

    }

    @Override
    public void onValidationFailed(List<ValidationError> errors) {
        for (ValidationError error : errors) {
            View view = error.getView();
            String message = error.getCollatedErrorMessage(this);

            // Display error messages ;)
            if (view instanceof EditText) {
                ((EditText) view).setError(message);
            } else {
                Toast.makeText(this, message, Toast.LENGTH_LONG).show();
            }
        }
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