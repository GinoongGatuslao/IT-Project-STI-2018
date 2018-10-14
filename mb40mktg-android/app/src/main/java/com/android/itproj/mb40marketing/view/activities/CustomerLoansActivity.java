package com.android.itproj.mb40marketing.view.activities;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.constraint.ConstraintLayout;
import android.support.design.widget.BottomSheetBehavior;
import android.support.v4.content.ContextCompat;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.MenuItem;
import android.view.View;
import android.widget.AbsListView;
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
        TransactionCallback,
        LoanItemsFragment.OnItemsLoadedCallback{

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

    @BindView(R.id.bottom_sheet)
    public View bottomSheet;

    private LoanSpinnerAdapter loanSpinnerAdapter;

    private int lastSelectedIndex = 0;

    private boolean isFromHidden = true;

    private BottomSheetBehavior bottomSheetBehavior;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_customer_loans);
        ButterKnife.bind(this);

        swipeRefreshLayout.setColorSchemeColors(
                getResources().getColor(R.color.colorAccent),
                getResources().getColor(R.color.colorSkyBlueLink));
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

        //bottom sheet behavior
        bottomSheetBehavior = BottomSheetBehavior.from(bottomSheet);
        bottomSheetBehavior.setState(BottomSheetBehavior.STATE_HIDDEN);
        bottomSheetBehavior.setBottomSheetCallback(new BottomSheetBehavior.BottomSheetCallback() {
            @Override
            public void onStateChanged(@NonNull View bottomSheet, int newState) {
                if (newState == BottomSheetBehavior.STATE_COLLAPSED) {
                    Log.d(TAG, "onStateChanged: COLLAPSED! hide fragments?");
                    bottomSheetBehavior.setState(BottomSheetBehavior.STATE_HIDDEN);
                } else if (newState == BottomSheetBehavior.STATE_EXPANDED) {
                    Log.d(TAG, "onStateChanged: EXPANDED! show fragments?");
                } else if (newState == BottomSheetBehavior.STATE_HIDDEN) {
                    Log.d(TAG, "onStateChanged: HIDDEN! you can't see me!");
                    isFromHidden = true;
                }
            }

            @Override
            public void onSlide(@NonNull View bottomSheet, float slideOffset) {

            }
        });

        transactionList.setOnScrollListener(new AbsListView.OnScrollListener() {
            @Override
            public void onScrollStateChanged(AbsListView view, int scrollState) {

            }

            @Override
            public void onScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount) {
                if (transactionList.getChildCount() > 0) {
                    swipeRefreshLayout.setEnabled(firstVisibleItem == 0
                            && transactionList.getChildAt(0).getTop() == 0);
                }
            }
        });
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
        LoanModel loanItem = loanSpinnerAdapter.getItem(lastSelectedIndex);
        Log.d(TAG, "onItemSelected: " + loanItem.toString());
        swipeRefreshLayout.setRefreshing(true);
        addPaymentButton.setEnabled(false);
        viewLoansBtn.setEnabled(false);
        loanSpinner.setEnabled(false);
        bottomSheetBehavior.setState(BottomSheetBehavior.STATE_HIDDEN);
        ((CoreApp) getApplication())
                .getTransactionController()
                .getTransactionRecords(
                        loanItem.getId(),
                        loanItem.getProfile_id(),
                        this);
    }

    @Override
    public void onNothingSelected(AdapterView<?> adapterView) {

    }

    @Override
    public void onRefresh() {
        setVisibility(View.VISIBLE, loansFrame);
        setVisibility(View.GONE, noticeFrame);

        swipeRefreshLayout.setRefreshing(true);
        addPaymentButton.setEnabled(false);
        viewLoansBtn.setEnabled(false);
        loanSpinner.setEnabled(false);
        ((CoreApp) getApplication())
                .getProfileController()
                .getLoans(getCustomerProfile().getId(), this);
    }

    @Override
    public void onLoanListRequest(List<LoanModel> loanModelList) {
        if (loanModelList.size() > 0) {
            setVisibility(View.VISIBLE, loansFrame);
            setVisibility(View.GONE, noticeFrame);
            Log.d(TAG, "onLoanListRequest: " + Arrays.deepToString(loanModelList.toArray()));
            loanSpinnerAdapter = new LoanSpinnerAdapter(this, android.R.layout.simple_list_item_1, loanModelList);
            loanSpinner.setAdapter(loanSpinnerAdapter);
            loanSpinner.setOnItemSelectedListener(null);
            loanSpinner.setSelection(lastSelectedIndex);
            loanSpinner.setOnItemSelectedListener(this);
            loanSpinner.invalidate();
        } else {
            setErrorMessage("No loan records.", 0);
            swipeRefreshLayout.setRefreshing(false);
            loanSpinner.setEnabled(true);
        }
    }

    @Override
    public void onLoanItemListRequest(List<LoanItemSummaryModel> loanItemSummaryModels) {
        Log.d(TAG, "onLoanItemListRequest: " + Arrays.deepToString(loanItemSummaryModels.toArray()));
    }

    @Override
    public void onTransactionRecordRequest(List<TransactionModel> transactionModels) {
        Log.d(TAG, "onTransactionRecordRequest: " + Arrays.deepToString(transactionModels.toArray()));
        TransactionListAdapter transactionListAdapter = new TransactionListAdapter(this, transactionModels);
        transactionList.setAdapter(transactionListAdapter);
        LoanItemsFragment instance = LoanItemsFragment.newInstance(
                loanSpinnerAdapter.getItem(lastSelectedIndex).getId());
        getSupportFragmentManager()
                .beginTransaction()
                .replace(R.id.fragmentHolder, instance)
                .commit();
        instance.setOnLoanItemsLoaded(this);
    }

    @Override
    public void onTransactionRecord(TransactionModel transactionModel) {
        addPaymentButton.setEnabled(false);
        viewLoansBtn.setEnabled(false);
        onRefresh();
    }

    @Override
    public void onCancel() {
        addPaymentButton.setEnabled(true);
        viewLoansBtn.setEnabled(true);
    }

    @Override
    public void onError(Throwable throwable, int code) {
        swipeRefreshLayout.setRefreshing(false);
        addPaymentButton.setEnabled(true);
        viewLoansBtn.setEnabled(true);
        setErrorMessage(throwable.getMessage(), code);
    }

    @Override
    public void onItemsLoaded() {
        LoanModel loan = loanSpinnerAdapter.getItem(lastSelectedIndex);
        if (loan.getStatus() == 1) {
            addPaymentButton.setEnabled(true);
        } else {
            addPaymentButton.setEnabled(false);
        }
        swipeRefreshLayout.setRefreshing(false);
        viewLoansBtn.setEnabled(true);
        loanSpinner.setEnabled(true);
    }

    @OnClick(R.id.addPaymentButton)
    public void addPayment() {
        addPaymentButton.setEnabled(false);
        viewLoansBtn.setEnabled(false);
        LoanModel model = (LoanModel) loanSpinner.getSelectedItem();
        Log.d(TAG, "addPayment: " + model.toString());
        ProfileModel profile = ((CoreApp) getApplication()).getProfileController().getProfile();
        AddPaymentDialogFragment addPaymentDialogFragment = AddPaymentDialogFragment.newInstance(
                model.getCreated_at(),
                model.getProfile_id(),
                model.getId(),
                profile.getId(),
                model.getAmortization(),
                getCustomerProfile().getLast_name() + ", " + getCustomerProfile().getFirst_name() + " " + getCustomerProfile().getMiddle_name(),
                profile.getLast_name() + ", " + profile.getFirst_name());
        addPaymentDialogFragment.setOnNewTransactionCallback(this);
        addPaymentDialogFragment.show(getSupportFragmentManager(), "PaymentDialog");
        addPaymentDialogFragment.setCancelable(false);
    }

    @OnClick(R.id.viewLoansBtn)
    public void viewLoans() {
        LoanModel model = loanSpinnerAdapter.getItem(lastSelectedIndex);
        bottomSheetBehavior.setState(BottomSheetBehavior.STATE_EXPANDED);
        ((CoreApp)getApplication())
                .getProfileController()
                .getLoanItems(model.getId(), this);
    }

    private void setErrorMessage(String message, int code) {
        setVisibility(View.INVISIBLE, loansFrame);
        setVisibility(View.VISIBLE, noticeFrame);
        noticeText.setText(message);
        noticeText.setTextColor(ContextCompat.getColor(this, R.color.colorPalePink));
    }

    private void setVisibility(int visibility, View... views) {
        for (View view : views) {
            view.setVisibility(visibility);
            view.invalidate();
        }
    }
}
