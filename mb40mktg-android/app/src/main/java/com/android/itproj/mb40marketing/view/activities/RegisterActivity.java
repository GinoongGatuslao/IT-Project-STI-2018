package com.android.itproj.mb40marketing.view.activities;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

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

import java.util.List;
import java.util.Random;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class RegisterActivity extends AppCompatActivity implements
        Validator.ValidationListener,
        AuthenticationCallback.AuthRegisterCallback,
        ProfileCallbacks.ProfileRegister {

    private static final String TAG = "RegisterActivity";

    private int USER_TYPE = 3;

    private UserModel userModel;

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

    private ProgressDialog alertDialog;

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

        //CREATING DUMMY VALUES
        Random rand = new Random();
        int rInt = rand.nextInt(10000);

        givenNameEditText.setText("Collector" + rInt);
        middleNameEditText.setText("Collector" + rInt);
        familyNameEditText.setText("Collector" + rInt);
        addressEditText.setText("Collector" + rInt);
        contactEditText.setText("Collector" + rInt);
        birthEditText.setText("01/03/1990");
        occupationEditText.setText("occupation" + rInt);
        incomeEditText.setText(String.valueOf(rInt * 10));
        est_monthly_expensesEditText.setText(String.valueOf(rInt));
        usernameEditText.setText("/c" + rInt);
        passwordEditText.setText("pass" + rInt);
        confirm_passwordEditText.setText("pass" + rInt);

        //initialize alert dialog
        alertDialog = new ProgressDialog(this, ProgressDialog.STYLE_SPINNER);
        alertDialog.setCancelable(false);
        alertDialog.setCanceledOnTouchOutside(false);
    }

    @OnClick(R.id.registerBtn)
    public void registerNewAccount() {
        //in-line hack for registering user as Collector
        String username = usernameEditText.getText().toString();
        if (username.charAt(0) == '/') {
            usernameEditText.setText(
                    username.substring(1, username.length()));
            USER_TYPE = 2;
        } else {
            USER_TYPE = 3;
        }
        validator.validate();
    }

    @Override
    public void onValidationSucceeded() {
        registerUserDetails();
    }

    @Override
    public void onValidationFailed(List<ValidationError> errors) {
        updateAlertDialog(false, "");
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
        userModel = model;
        updateAlertDialog(true, this.getString(R.string.progress_create_profile));
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
    public void onRegisterFailed(Throwable e, int code) {
        updateAlertDialog(false, "");
        Log.e(TAG, "onRegisterFailed: ", e);
    }

    @Override
    public void onProfileRegisterSuccess(ProfileModel model) {
        updateAlertDialog(false, "");
        Log.d(TAG, "onProfileRegisterSuccess: " + model.toString());
        ((CoreApp) getApplication()).getProfileController().setProfile(model);

        Intent intent = new Intent(this, CollectorActivity.class);
        intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TASK | Intent.FLAG_ACTIVITY_NEW_TASK);
        startActivity(intent);
    }

    @Override
    public void onProfileRegisterFailed(Throwable throwable, int code) {
        updateAlertDialog(false, "");
        Log.e(TAG, "onProfileRegisterFailed: ", throwable);
    }

    private void registerUserDetails() {
        updateAlertDialog(true, this.getString(R.string.progress_register));

        UserModel model = new UserModel();
        model.setUsername(usernameEditText.getText().toString());
        model.setPassword(passwordEditText.getText().toString());
        model.setPassword_confirmation(confirm_passwordEditText.getText().toString());
        model.setUser_type(2);//as ONLY CLIENT WILL BE USING THIS REGISTRATION FORMS, COLLECTOR'S PROFILE ARE CREATED VIA THE DESKTOP APP

        JsonObject newUser = new JsonObject();
        newUser.addProperty("username", usernameEditText.getText().toString());
        newUser.addProperty("user_type", USER_TYPE);//as ONLY CLIENT WILL BE USING THIS REGISTRATION FORMS, COLLECTOR'S PROFILE ARE CREATED VIA THE DESKTOP APP
        newUser.addProperty("password", passwordEditText.getText().toString());
        newUser.addProperty("password_confirmation", confirm_passwordEditText.getText().toString());

        ((CoreApp) getApplication())
                .getAuthenticationController()
                .registerUser(newUser, this);
    }

    private void updateAlertDialog(boolean showDialog, String message) {
        if (alertDialog != null) {
            if (showDialog) {
                if (!alertDialog.isShowing()) {
                    alertDialog.show();
                }
                alertDialog.setMessage(message);
            } else {
                alertDialog.dismiss();
            }
        }
    }
}
