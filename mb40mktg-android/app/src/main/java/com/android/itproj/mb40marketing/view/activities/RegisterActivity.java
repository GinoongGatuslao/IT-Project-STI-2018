package com.android.itproj.mb40marketing.view.activities;

import android.app.Activity;
import android.content.Intent;
import android.database.Cursor;
import android.net.Uri;
import android.provider.MediaStore;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
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
import com.cloudinary.android.MediaManager;
import com.cloudinary.android.UploadRequest;
import com.cloudinary.android.callback.ErrorInfo;
import com.cloudinary.android.callback.UploadCallback;
import com.cloudinary.android.preprocess.BitmapDecoder;
import com.cloudinary.android.preprocess.BitmapEncoder;
import com.cloudinary.android.preprocess.ImagePreprocessChain;
import com.cloudinary.android.preprocess.Limit;
import com.google.gson.JsonObject;
import com.mobsandgeeks.saripaar.ValidationError;
import com.mobsandgeeks.saripaar.Validator;
import com.mobsandgeeks.saripaar.annotation.ConfirmPassword;
import com.mobsandgeeks.saripaar.annotation.NotEmpty;
import com.mobsandgeeks.saripaar.annotation.Password;
import com.mobsandgeeks.saripaar.annotation.Pattern;

import java.util.List;
import java.util.Map;
import java.util.Random;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

public class RegisterActivity extends AppCompatActivity implements
        Validator.ValidationListener,
        AuthenticationCallback.AuthRegisterCallback,
        ProfileCallbacks.ProfileRegister,
        UploadCallback {

    private static final String TAG = "RegisterActivity";

    private final int REQUEST_CHOOSE_VALID_ID = 1000;

    private final int REQUEST_CHOOSE_HOME_SKETCH = 1001;

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

    @BindView(R.id.selectValidIdBtn)
    public Button selectValidIdBtn;

    @BindView(R.id.selectHomeSketch)
    public Button selectHomeSketch;

    private Validator validator;

    private UploadEntry validIdEntry;
    private UploadEntry homeSketchEntry;

    private boolean validIdUploaded = false;
    private boolean validIdSelected = false;
    private boolean homeSketchUploaded = false;
    private boolean homeSketchSelected = false;

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

        givenNameEditText.setText("givenName" + rInt);
        middleNameEditText.setText("middleName" + rInt);
        familyNameEditText.setText("familyName" + rInt);
        addressEditText.setText("address" + rInt);
        contactEditText.setText("contact" + rInt);
        birthEditText.setText("01/03/1990");
        occupationEditText.setText("occupation" + rInt);
        incomeEditText.setText(String.valueOf(rInt * 10));
        est_monthly_expensesEditText.setText(String.valueOf(rInt));
        usernameEditText.setText("username" + rInt);
        passwordEditText.setText("password" + rInt);
        confirm_passwordEditText.setText("password" + rInt);

    }

    @OnClick(R.id.registerBtn)
    public void registerNewAccount() {
        //VALIDATE FIRST THE FORM FIELDS
        validator.validate();
    }

    @OnClick(R.id.selectValidIdBtn)
    public void selectValidIdOnGallery() {
        Intent getIntent = new Intent(Intent.ACTION_GET_CONTENT);
        getIntent.setType("image/*");

        Intent pickIntent = new Intent(Intent.ACTION_PICK, android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI);
        pickIntent.setType("image/*");

        Intent chooserIntent = Intent.createChooser(getIntent, "Select Image");
        chooserIntent.putExtra(Intent.EXTRA_INITIAL_INTENTS, new Intent[]{pickIntent});

        startIntentForChooseImage(chooserIntent, REQUEST_CHOOSE_VALID_ID);
    }

    @OnClick(R.id.selectHomeSketch)
    public void selectHomeSketchOnGallery() {
        Intent getIntent = new Intent(Intent.ACTION_GET_CONTENT);
        getIntent.setType("image/*");

        Intent pickIntent = new Intent(Intent.ACTION_PICK, android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI);
        pickIntent.setType("image/*");

        Intent chooserIntent = Intent.createChooser(getIntent, "Select Image");
        chooserIntent.putExtra(Intent.EXTRA_INITIAL_INTENTS, new Intent[]{pickIntent});

        startIntentForChooseImage(chooserIntent, REQUEST_CHOOSE_HOME_SKETCH);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (resultCode != Activity.RESULT_CANCELED || data != null) {
            if (requestCode == REQUEST_CHOOSE_VALID_ID) {
                validIdSelected = true;
                validIdEntry = new UploadEntry(requestCode, MediaManager
                        .get()
                        .upload(data.getData())
                        .preprocess(new ImagePreprocessChain()
                                .loadWith(new BitmapDecoder(1024, 1024))
                                .addStep(new Limit(1024, 1024))
                                .saveWith(new BitmapEncoder(BitmapEncoder.Format.PNG, 100)))
                        .callback(this));
                Log.d(TAG, "onActivityResult: " + validIdEntry.toString());
            } else if (requestCode == REQUEST_CHOOSE_HOME_SKETCH) {
                homeSketchSelected = true;
                homeSketchEntry = new UploadEntry(requestCode, MediaManager
                        .get()
                        .upload(data.getData())
                        .preprocess(new ImagePreprocessChain()
                                .loadWith(new BitmapDecoder(1024, 1024))
                                .addStep(new Limit(1024, 1024))
                                .saveWith(new BitmapEncoder(BitmapEncoder.Format.PNG, 100)))
                        .callback(this));
                Log.d(TAG, "onActivityResult: " + homeSketchEntry.toString());
            }
        }
    }

    @Override
    public void onValidationSucceeded() {
        //AFTER VALIDATION, UPLOAD THE IMAGES
        if (!validIdSelected && !homeSketchSelected) {
            registerUserDetails();
        }

        if (validIdSelected) {
            validIdEntry.setRequestId(validIdEntry.uploadRequest.dispatch(this));
            Log.d(TAG, "uploadimage: " + validIdEntry.toString());
        }

        if (homeSketchSelected) {
            homeSketchEntry.setRequestId(homeSketchEntry.uploadRequest.dispatch(this));
            Log.d(TAG, "uploadimage: " + homeSketchEntry.toString());
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
        if (validIdUploaded) {
            profileModel.setPath_id_pic(validIdEntry.resultData.get("url").toString());
        }
        if (homeSketchUploaded) {
            profileModel.setPath_house_sketch_pic(homeSketchEntry.resultData.get("url").toString());
        }
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
        ((CoreApp) getApplication()).getProfileController().setProfile(model);

        Intent intent = new Intent(this, HomeActivity.class);
        intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TASK | Intent.FLAG_ACTIVITY_NEW_TASK);
        startActivity(intent);
    }

    @Override
    public void onProfileRegisterFailed(Throwable throwable) {
        Log.e(TAG, "onProfileRegisterFailed: ", throwable);
    }

    @Override
    public void onStart(String requestId) {

    }

    @Override
    public void onProgress(String requestId, long bytes, long totalBytes) {
        Log.d(TAG, "onProgress[" + bytes + "/" + totalBytes + "]: " + requestId);
    }

    @Override
    public void onSuccess(String requestId, Map resultData) {
        Log.d(TAG, "onSuccess: " + requestId + " " + resultData.toString());
        if (validIdEntry != null && validIdEntry.getRequestId().equals(requestId)) {
            validIdEntry.setResultData(resultData);
            validIdUploaded = true;
            Log.d(TAG, "uploadimage: " + validIdEntry.toString());
        } else if (homeSketchEntry != null && homeSketchEntry.getRequestId().equals(requestId)) {
            homeSketchEntry.setResultData(resultData);
            homeSketchUploaded = true;
            Log.d(TAG, "uploadimage: " + homeSketchEntry.toString());
        }

        if (validIdSelected && homeSketchSelected) {
            if (validIdUploaded && homeSketchUploaded) {
                registerUserDetails();
            } else {
                Log.d(TAG, "waiting for other upload entry...");
            }
        } else {
            registerUserDetails();
        }

    }

    @Override
    public void onError(String requestId, ErrorInfo error) {

    }

    @Override
    public void onReschedule(String requestId, ErrorInfo error) {

    }

    private void startIntentForChooseImage(Intent intent, int REQUEST_CODE) {
        startActivityForResult(intent, REQUEST_CODE);
    }

    private void registerUserDetails() {
        UserModel model = new UserModel();
        model.setUsername(usernameEditText.getText().toString());
        model.setPassword(passwordEditText.getText().toString());
        model.setPassword_confirmation(confirm_passwordEditText.getText().toString());
        model.setUser_type(2);//as ONLY CLIENT WILL BE USING THIS REGISTRATION FORMS, COLLECTOR'S PROFILE ARE CREATED VIA THE DESKTOP APP

        JsonObject newUser = new JsonObject();
        newUser.addProperty("username", usernameEditText.getText().toString());
        newUser.addProperty("user_type", 3);//as ONLY CLIENT WILL BE USING THIS REGISTRATION FORMS, COLLECTOR'S PROFILE ARE CREATED VIA THE DESKTOP APP
        newUser.addProperty("password", passwordEditText.getText().toString());
        newUser.addProperty("password_confirmation", confirm_passwordEditText.getText().toString());

        ((CoreApp) getApplication())
                .getAuthenticationController()
                .registerUser(newUser, this);
    }

    @ToString
    public class UploadEntry {

        @Getter
        @Setter
        String requestId;

        @Getter
        @Setter
        int requestCode;

        @Getter
        @Setter
        Map resultData;

        @Getter
        @Setter
        UploadRequest uploadRequest;

        public UploadEntry(int requestCode, UploadRequest uploadRequest) {
            this.requestCode = requestCode;
            this.uploadRequest = uploadRequest;
        }
    }
}
