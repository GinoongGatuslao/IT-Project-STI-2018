package com.android.itproj.mb40marketing.view.activities;

import android.content.DialogInterface;
import android.os.Bundle;
import android.support.constraint.ConstraintLayout;
import android.support.v4.content.ContextCompat;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.TextView;

import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.adapter.LoanSpinnerAdapter;
import com.android.itproj.mb40marketing.adapter.TransactionListAdapter;
import com.android.itproj.mb40marketing.helper.interfaces.ProfileCallback;
import com.android.itproj.mb40marketing.helper.interfaces.TransactionCallback;
import com.android.itproj.mb40marketing.model.LoanItemSummaryModel;
import com.android.itproj.mb40marketing.model.LoanModel;
import com.android.itproj.mb40marketing.model.ProfileModel;
import com.android.itproj.mb40marketing.model.TransactionModel;

import java.util.Arrays;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import lombok.Getter;
import lombok.Setter;

public class CustomerLoansActivity extends AppCompatActivity implements
        ProfileCallback.UserLoanCallback,
        ProfileCallback.UserLoanItemsCallback,
        SwipeRefreshLayout.OnRefreshListener,
        AdapterView.OnItemSelectedListener,
        TransactionCallback {

    private static final String TAG = "CustomerLoansActivity";

    @Getter
    @Setter
    private ProfileModel customerProfile;

    @BindView(R.id.loanSpinner)
    public Spinner loanSpinner;

    @BindView(R.id.swipe_container)
    public SwipeRefreshLayout swipeRefreshLayout;

    @BindView(R.id.noticeFrame)
    public ConstraintLayout noticeFrame;

    @BindView(R.id.noticeText)
    public TextView noticeText;

    @BindView(R.id.loansFrame)
    public ConstraintLayout loansFrame;

    @BindView(R.id.transactionList)
    public ListView transactionList;

    @BindView(R.id.addPaymentButton)
    public Button addPaymentButton;

    @BindView(R.id.viewLoansBtn)
    public Button viewLoansBtn;

    private LoanSpinnerAdapter loanSpinnerAdapter;

    private int lastSelectedIndex = 0;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_customer_loans);
        ButterKnife.bind(this);

        loanSpinner.setOnItemSelectedListener(this);
        swipeRefreshLayout.setOnRefreshListener(this);

        setCustomerProfile((ProfileModel) getIntent().getExtras().get("profile"));

        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        toolbar.setTitle(
                String.format(
                        getString(R.string.text_customer_name_placeholder),
                        getCustomerProfile().getLast_name(),
                        getCustomerProfile().getFirst_name(),
                        getCustomerProfile().getMiddle_name()));
        setSupportActionBar(toolbar);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        onRefresh();
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                onBackPressed();
                return true;
        }
        return super.onOptionsItemSelected(item);
    }

    @Override
    public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
        if (lastSelectedIndex != i) {
            lastSelectedIndex = i;
        }
        Log.d(TAG, "onItemSelected: " + loanSpinnerAdapter.getItem(lastSelectedIndex).toString());
        swipeRefreshLayout.setRefreshing(true);
        addPaymentButton.setEnabled(false);
        viewLoansBtn.setEnabled(false);
        ((CoreApp) getApplication())
                .getTransactionController()
                .getTransactionRecords(
                        loanSpinnerAdapter.getItem(lastSelectedIndex).getId(),
                        loanSpinnerAdapter.getItem(lastSelectedIndex).getProfile_id(),
                        this);
    }

    @Override
    public void onNothingSelected(AdapterView<?> adapterView) {

    }

    @Override
    public void onRefresh() {
        swipeRefreshLayout.setRefreshing(true);
        addPaymentButton.setEnabled(false);
        viewLoansBtn.setEnabled(false);
        ((CoreApp) getApplication())
                .getProfileController()
                .getLoans(getCustomerProfile().getId(), this);
    }

    @Override
    public void onLoanListRequest(List<LoanModel> loanModelList) {
        if (loanModelList.size() > 0) {
            Log.d(TAG, "onLoanListRequest: " + Arrays.deepToString(loanModelList.toArray()));
            loanSpinnerAdapter = new LoanSpinnerAdapter(this, android.R.layout.simple_list_item_1, loanModelList);
            loanSpinner.setAdapter(loanSpinnerAdapter);
            loanSpinner.invalidate();
        } else {
            swipeRefreshLayout.setRefreshing(false);
        }
    }

    @Override
    public void onLoanItemListRequest(List<LoanItemSummaryModel> loanItemSummaryModels) {
        Log.d(TAG, "onLoanItemListRequest: " + Arrays.deepToString(loanItemSummaryModels.toArray()));
    }

    @Override
    public void onTransactionRecordRequest(List<TransactionModel> transactionModels) {
        swipeRefreshLayout.setRefreshing(false);
        addPaymentButton.setEnabled(true);
        viewLoansBtn.setEnabled(true);
        Log.d(TAG, "onTransactionRecordRequest: " + Arrays.deepToString(transactionModels.toArray()));
        TransactionListAdapter transactionListAdapter = new TransactionListAdapter(this, transactionModels);
        transactionList.setAdapter(transactionListAdapter);
    }

    @Override
    public void onTransactionRecord(TransactionModel transactionModel) {
        addPaymentButton.setEnabled(false);
        viewLoansBtn.setEnabled(false);
        onRefresh();
    }

    @Override
    public void onError(Throwable throwable, int code) {
        swipeRefreshLayout.setRefreshing(false);
        addPaymentButton.setEnabled(true);
        viewLoansBtn.setEnabled(true);
        setErrorMessage(throwable.getMessage(), code);
    }

    @OnClick(R.id.addPaymentButton)
    public void addPayment() {
        LoanModel model = (LoanModel) loanSpinner.getSelectedItem();
        ProfileModel profile = ((CoreApp) getApplication()).getProfileController().getProfile();
        AddPaymentDialogFragment addPaymentDialogFragment = AddPaymentDialogFragment.newInstance(
                model.getCreated_at(),
                model.getProfile_id(),
                model.getId(),
                profile.getId(),
                getCustomerProfile().getLast_name() + ", " + getCustomerProfile().getFirst_name() + " " + getCustomerProfile().getMiddle_name(),
                profile.getLast_name() + ", " + profile.getFirst_name());
        addPaymentDialogFragment.setOnNewTransactionCallback(this);
        addPaymentDialogFragment.show(getSupportFragmentManager(), "PaymentDialog");
    }

    @OnClick(R.id.viewLoansBtn)
    public void viewLoans() {
        LoanModel model = loanSpinnerAdapter.getItem(lastSelectedIndex);
        ((CoreApp)getApplication())
                .getProfileController()
                .getLoanItems(model.getId(), this);
    }

    private void setErrorMessage(String message, int code) {
        setVisibility(View.INVISIBLE, loansFrame);
        setVisibility(View.VISIBLE, noticeFrame);
        noticeText.setText(message);
        noticeText.setTextColor(ContextCompat.getColor(this, R.color.colorLightRed));
    }

    private void setVisibility(int visibility, View... views) {
        for (View view : views) {
            view.setVisibility(visibility);
            view.invalidate();
        }
    }
}
