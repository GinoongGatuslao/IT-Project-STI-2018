package com.android.itproj.mb40marketing.view.activities;

import android.content.DialogInterface;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.helper.interfaces.AuthenticationCallback;
import com.android.itproj.mb40marketing.helper.interfaces.ProfileCallback;
import com.android.itproj.mb40marketing.model.ProfileModel;
import com.android.itproj.mb40marketing.model.UserModel;
import com.google.gson.JsonObject;
import com.mobsandgeeks.saripaar.ValidationError;
import com.mobsandgeeks.saripaar.Validator;
import com.mobsandgeeks.saripaar.annotation.ConfirmPassword;
import com.mobsandgeeks.saripaar.annotation.NotEmpty;
import com.mobsandgeeks.saripaar.annotation.Optional;
import com.mobsandgeeks.saripaar.annotation.Password;
import com.mobsandgeeks.saripaar.annotation.Pattern;

import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class ProfileActivity extends AppCompatActivity implements
        ProfileCallback.ProfileRequest,
        Validator.ValidationListener,
        AuthenticationCallback.UserAccountUpdate,
        AuthenticationCallback.AuthLoginCallback {

    private static final String TAG = "ProfileActivity";

    //main views
    @BindView(R.id.mainView)
    public View mainProfileView;

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

    @BindView(R.id.updateProfile)
    public TextView updateProfile;

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

    @BindView(R.id.gender)
    public Spinner genderSpinner;

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
    @BindView(R.id.username)
    public EditText usernameEditText;

    @BindView(R.id.password)
    public EditText passwordEditText;

    @BindView(R.id.confirm_password)
    public EditText confirm_passwordEditText;

    @BindView(R.id.registerBtn)
    public Button registerBtn;

    private final String[] genderSelection = new String[]{"Male", "Female"};

    private Validator validator;

    private ProfileModel profileModel, newProfile;

    private AlertDialog dialog;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_profile);
        ButterKnife.bind(this);

        profileModel = ((CoreApp) getApplication())
                .getProfileController()
                .getProfile();

        setProfile(profileModel);

        validator = new Validator(this);
        validator.setValidationListener(this);
    }

    @Override
    public void onProfileFetch(ProfileModel model) {
        swipeRefreshLayout.setRefreshing(false);
        Log.d(TAG, "onProfileFetch: " + model.toString());
        changeView(editProfileView, mainProfileView);
        profileModel = model;
        profileModel.setUsername(newProfile.getUsername());
        profileModel.setUsertype_str(newProfile.getUsertype_str());
        setProfile(profileModel);
    }

    @Override
    public void onAccountUpdate(UserModel model) {
        Log.d(TAG, "onAccountUpdate: " + model.toString());
        ((CoreApp) getApplication())
                .getProfileController()
                .updateUserProfile(profileModel.getId(), newProfile, this);
    }

    @Override
    public void onAccountUpdateError(Throwable e, int code) {
        swipeRefreshLayout.setRefreshing(false);
        Toast.makeText(this, getString(R.string.err_failed_to_update_user), Toast.LENGTH_LONG).show();
        changeView(editProfileView, mainProfileView);
        setProfile(profileModel);
    }

    @Override
    public void onProfileFetch(List<ProfileModel> models) {

    }

    @Override
    public void onProfileFetchFailed(Throwable throwable, int code) {
        swipeRefreshLayout.setRefreshing(false);
        Toast.makeText(this, getString(R.string.err_failed_to_update_profile), Toast.LENGTH_LONG).show();
        changeView(editProfileView, mainProfileView);
        setProfile(profileModel);
    }

    @Override
    public void onValidationSucceeded() {
        swipeRefreshLayout.setRefreshing(true);
        newProfile = profileModel;
        newProfile.setUser_id(profileModel.getUser_id());
        newProfile.setFirst_name(givenNameEditText.getText().toString());
        newProfile.setMiddle_name(middleNameEditText.getText().toString());
        newProfile.setLast_name(familyNameEditText.getText().toString());
        newProfile.setAddress(addressEditText.getText().toString());
        newProfile.setContact(contactEditText.getText().toString());
        newProfile.setBirth(birthEditText.getText().toString());
        newProfile.setOccupation(occupationEditText.getText().toString());
        newProfile.setGender(genderSpinner.getSelectedItem().toString());
        newProfile.setVerified(profileModel.getVerified());
        newProfile.setCredit_limit(profileModel.getCredit_limit());
        newProfile.setAccount_status(profileModel.getAccount_status());

        JsonObject jsonObject = new JsonObject();
        if (usernameEditText.getText().toString().equals(profileModel.getUsername())
                && passwordEditText.getText().toString().isEmpty()
                && confirm_passwordEditText.getText().toString().isEmpty()) {
            ((CoreApp) getApplication())
                    .getProfileController()
                    .updateUserProfile(profileModel.getId(), profileModel, this);
        } else {
            View v = LayoutInflater.from(this).inflate(R.layout.dialog_confirm_password, null);
            final EditText password = (EditText) v.findViewById(R.id.reconfirmPassword);
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.setView(v);
            builder.setPositiveButton(getString(R.string.prompt_btn_confirm), new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialogInterface, int i) {
                    ((CoreApp)ProfileActivity.this.getApplication())
                            .getAuthenticationController()
                            .login(usernameEditText.getText().toString(), password.getText().toString(), ProfileActivity.this);
                }
            });
            builder.setNegativeButton(getString(R.string.prompt_btn_cancel), new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialogInterface, int i) {
                    dialogInterface.dismiss();
                }
            });
            dialog = builder.create();
            dialog.show();
        }
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

    @Override
    public void onLoginSuccess(UserModel model) {
        if (dialog != null && dialog.isShowing()) {
            dialog.dismiss();
        } else {
            Log.d(TAG, "onLoginSuccess: Unable to dismiss but was confirmed.");
        }
        JsonObject jsonObject = new JsonObject();
        if (!usernameEditText.getText().toString().equals(profileModel.getUsername())) {
            jsonObject.addProperty("username", usernameEditText.getText().toString());
        }
        if (!passwordEditText.getText().toString().equals(profileModel.getUsername())) {
            jsonObject.addProperty("password", passwordEditText.getText().toString());
        }
        ((CoreApp) getApplication())
                .getAuthenticationController()
                .updateUserAccount(profileModel.getUser_id(), jsonObject, this);
    }

    @Override
    public void onLoginFailed(Throwable e, int code) {
        Log.d(TAG, "onLoginFailed: " + e.getMessage());
        Toast.makeText(this, "Wrong credentials!", Toast.LENGTH_SHORT).show();
    }

    @OnClick(R.id.updateProfile)
    public void editProfile() {
        changeView(mainProfileView, editProfileView);
        updateFieldsFromProfile(profileModel);
    }

    @OnClick(R.id.registerBtn)
    public void updateProfile() {
        validator.validate();
    }

    private void updateFieldsFromProfile(ProfileModel profileModel) {
        givenNameEditText.setText(profileModel.getFirst_name());
        middleNameEditText.setText(profileModel.getMiddle_name());
        familyNameEditText.setText(profileModel.getLast_name());
        genderSpinner.setAdapter(provideGenderSpinnerAdapter());
        genderSpinner.setSelection(getIndexOfGender(profileModel.getGender()));
        addressEditText.setText(profileModel.getAddress());
        contactEditText.setText(profileModel.getContact());
        birthEditText.setText(profileModel.getBirth());
        occupationEditText.setText(profileModel.getOccupation());
        usernameEditText.setText(profileModel.getUsername());
    }

    private ArrayAdapter<String> provideGenderSpinnerAdapter() {
        return new ArrayAdapter<>(this, R.layout.listview_gender_item, genderSelection);
    }

    private int getIndexOfGender(String gender) {
        for (int i = 0; i < genderSelection.length; i++) {
            if (genderSelection[i].equals(gender)) {
                return i;
            }
        }
        return 0;
    }

    private void changeView(View toHide, View toShow) {
        toHide.setVisibility(View.INVISIBLE);
        toShow.setVisibility(View.VISIBLE);
        toHide.invalidate();
        toShow.invalidate();
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