package com.android.itproj.mb40marketing.view.activities;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.android.itproj.mb40marketing.Constants;
import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.helper.interfaces.AuthenticationCallback;
import com.android.itproj.mb40marketing.helper.interfaces.ProfileCallbacks;
import com.android.itproj.mb40marketing.model.ProfileModel;
import com.android.itproj.mb40marketing.model.UserModel;
import com.google.gson.JsonObject;
import com.mobsandgeeks.saripaar.ValidationError;
import com.mobsandgeeks.saripaar.Validator;
import com.mobsandgeeks.saripaar.annotation.ConfirmPassword;
import com.mobsandgeeks.saripaar.annotation.NotEmpty;
import com.mobsandgeeks.saripaar.annotation.Password;
import com.mobsandgeeks.saripaar.annotation.Pattern;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.List;
import java.util.Random;

import butterknife.BindView;
import butterknife.BindViews;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class RegisterActivity extends AppCompatActivity implements
        Validator.ValidationListener,
        AuthenticationCallback.AuthRegisterCallback,
        ProfileCallbacks.ProfileRegister {

    private static final String TAG = "RegisterActivity";

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
        setContentView(R.layout.activity_register);
        ButterKnife.bind(this);

        initialize();
    }

    private void initialize() {
        validator = new Validator(this);
        validator.setValidationListener(this);
    }

    @OnClick(R.id.registerBtn)
    public void registerNewAccount() {
        validator.validate();
    }

    @Override
    public void onValidationSucceeded() {
        Toast.makeText(this, "Success! Register action", Toast.LENGTH_SHORT).show();

        UserModel model = new UserModel();
        model.setUsername(usernameEditText.getText().toString());
        model.setPassword(passwordEditText.getText().toString());
        model.setPassword_confirmation(confirm_passwordEditText.getText().toString());
        model.setUser_type(2);//as ONLY CLIENT WILL BE USING THIS REGISTRATION FORMS, COLLECTOR'S PROFILE ARE CREATED VIA THE DESKTOP APP

        JsonObject newUser = new JsonObject();
        newUser.addProperty("username", usernameEditText.getText().toString());
        newUser.addProperty("user_type", 2);//as ONLY CLIENT WILL BE USING THIS REGISTRATION FORMS, COLLECTOR'S PROFILE ARE CREATED VIA THE DESKTOP APP
        newUser.addProperty("password", passwordEditText.getText().toString());
        newUser.addProperty("password_confirmation", confirm_passwordEditText.getText().toString());

        ((CoreApp) getApplication())
                .getAuthenticationController()
                .registerUser(newUser, this);
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
    public void onRegisterSuccess(UserModel model) {
        //now we create profile for the client as filled-in on forms

        //create the model for POST body
        ProfileModel profileModel = new ProfileModel();
        profileModel.setUser_id(model.getId());
        profileModel.setFirst_name(givenNameEditText.getText().toString());
        profileModel.setMiddle_name(middleNameEditText.getText().toString());
        profileModel.setLast_name(familyNameEditText.getText().toString());
        profileModel.setAddress(addressEditText.getText().toString());
        profileModel.setContact(contactEditText.getText().toString());
        profileModel.setBirth(birthEditText.getText().toString());
        profileModel.setOccupation(occupationEditText.getText().toString());
        profileModel.setIncome(incomeEditText.getText().toString());
        profileModel.setEst_monthly_expenses(est_monthly_expensesEditText.getText().toString());
        ((CoreApp) getApplication())
                .getProfileController()
                .registerProfile(profileModel, this);
    }

    @Override
    public void onRegisterFailed(Throwable e) {
        Log.e(TAG, "onRegisterFailed: ", e);
    }

    @Override
    public void onProfileRegisterSuccess(ProfileModel model) {
        Log.d(TAG, "onProfileRegisterSuccess: " + model.toString());
        ((CoreApp)getApplication()).getProfileController().setProfile(model);

        Intent intent = new Intent(this, HomeActivity.class);
        intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TASK|Intent.FLAG_ACTIVITY_NEW_TASK);
        startActivity(intent);
    }

    @Override
    public void onProfileRegisterFailed(Throwable throwable) {
        Log.e(TAG, "onProfileRegisterFailed: ", throwable);
    }
}
