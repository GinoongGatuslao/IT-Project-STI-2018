package com.android.itproj.mb40marketing.view.activities;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.DialogFragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.helper.interfaces.TransactionCallback;
import com.android.itproj.mb40marketing.model.TransactionModel;

import java.util.Arrays;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import lombok.Getter;
import lombok.Setter;

public class AddPaymentDialogFragment extends DialogFragment {

    private static final String TAG = "AddPaymentDialogFragmen";

    private static final String BUNDLE_KEY_LOAN_TITLE = "loan_title";
    private static final String BUNDLE_KEY_PROFILE_ID = "profile_id";
    private static final String BUNDLE_KEY_LOAN_ID = "loan_id";
    private static final String BUNDLE_KEY_COLLECTOR_ID = "collector_id";
    private static final String BUNDLE_KEY_PAYEE_NAME = "payee_name";
    private static final String BUNDLE_KEY_COLLECTOR_NAME = "collector_name";

    @BindView(R.id.loanTitle)
    public TextView loanTitleText;

    @BindView(R.id.payeeName)
    public TextView payeeNameText;

    @BindView(R.id.collectorName)
    public TextView collectorNameText;

    @BindView(R.id.paymentEditText)
    public EditText paymentEditText;

    @BindView(R.id.transactionSubmit)
    public Button transactionSubmitBtn;

    @Getter
    private String loanTitle = "";

    @Getter
    private int profileId = 0;

    @Getter
    private int loanId = 0;

    @Getter
    private int collectorId = 0;

    @Getter
    private String payeeName;

    @Getter
    private String collectorName;

    @Getter
    private TransactionCallback transactionCallback;

    public static AddPaymentDialogFragment newInstance(String loanTitle, int profileId, int loanId, int collectorId, String payee, String collector) {

        Bundle args = new Bundle();
        args.putString(BUNDLE_KEY_LOAN_TITLE, loanTitle);
        args.putInt(BUNDLE_KEY_PROFILE_ID, profileId);
        args.putInt(BUNDLE_KEY_LOAN_ID, loanId);
        args.putInt(BUNDLE_KEY_COLLECTOR_ID, collectorId);
        args.putString(BUNDLE_KEY_PAYEE_NAME, payee);
        args.putString(BUNDLE_KEY_COLLECTOR_NAME, collector);

        AddPaymentDialogFragment fragment = new AddPaymentDialogFragment();
        fragment.setArguments(args);
        return fragment;
    }

    public AddPaymentDialogFragment() {

    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            loanTitle = getArguments().getString(BUNDLE_KEY_LOAN_TITLE);
            profileId = getArguments().getInt(BUNDLE_KEY_PROFILE_ID);
            loanId = getArguments().getInt(BUNDLE_KEY_LOAN_ID);
            collectorId = getArguments().getInt(BUNDLE_KEY_COLLECTOR_ID);
            payeeName = getArguments().getString(BUNDLE_KEY_PAYEE_NAME);
            collectorName = getArguments().getString(BUNDLE_KEY_COLLECTOR_NAME);
        }
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_add_payment, container);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        ButterKnife.bind(this, view);
        loanTitleText.setText(getLoanTitle());
        payeeNameText.setText(getPayeeName());
        collectorNameText.setText(getCollectorName());
    }

    @OnClick(R.id.transactionSubmit)
    public void submitTransaction() {
        TransactionModel model = new TransactionModel();
        model.setProfile_id(profileId);
        model.setLoan_id(loanId);
        model.setCollector_id(collectorId);
        model.setPayment(Integer.parseInt(paymentEditText.getText().toString()));
        ((CoreApp) getActivity().getApplication())
                .getTransactionController()
                .createTransaction(model, getTransactionCallback());
        dismiss();
    }

    public void setOnNewTransactionCallback(TransactionCallback callback) {
        transactionCallback = callback;
    }
}
