package com.android.itproj.mb40marketing.view.activities;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.DialogFragment;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ListView;
import android.widget.TextView;

import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.helper.interfaces.ProfileCallback;
import com.android.itproj.mb40marketing.model.LoanItemSummaryModel;

import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;

public class LoanItemsFragment extends Fragment implements ProfileCallback.UserLoanItemsCallback{

    private static final String TAG = "LoanItemsFragment";

    private static final String BUNDLE_KEY_LOAN_ID = "loan_id";

    @BindView(R.id.itemsListView)
    public ListView itemsListView;

    private int loanId = -1;

    private List<LoanItemSummaryModel> loanItemSummaryModels;

    private OnItemsLoadedCallback callback;

    public static LoanItemsFragment newInstance(int loanId) {

        Bundle args = new Bundle();
        args.putInt(BUNDLE_KEY_LOAN_ID, loanId);

        LoanItemsFragment fragment = new LoanItemsFragment();
        fragment.setArguments(args);
        return fragment;
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            loanId = getArguments().getInt(BUNDLE_KEY_LOAN_ID);
        }
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.fragment_loan_product_items, null);
        return v;
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this, view);

        if (loanId != -1) {
            ((CoreApp)getActivity().getApplication())
                    .getProfileController()
                    .getLoanItems(loanId, this);
        }
    }

    @Override
    public void onLoanItemListRequest(List<LoanItemSummaryModel> loanItemSummaryModels) {
        this.loanItemSummaryModels = loanItemSummaryModels;
        LoanProductItemAdapter adapter = new LoanProductItemAdapter(getActivity().getApplication(), this.loanItemSummaryModels);
        itemsListView.setAdapter(adapter);
        this.callback.onItemsLoaded();
    }

    @Override
    public void onError(Throwable throwable, int code) {
        Log.d(TAG, "onError:[" + code + "] " + throwable.getMessage());
    }

    public void setOnLoanItemsLoaded(OnItemsLoadedCallback callback) {
        this.callback = callback;
    }

    public class LoanProductItemAdapter extends BaseAdapter {

        private Context context;
        private List<LoanItemSummaryModel> loanItemSummaryModelList;

        private LoanProductItemAdapter(Context context, List<LoanItemSummaryModel> loanItemSummaryModels) {
            this.context = context;
            this.loanItemSummaryModelList = loanItemSummaryModels;
        }

        @Override
        public int getCount() {
            return this.loanItemSummaryModelList.size();
        }

        @Override
        public LoanItemSummaryModel getItem(int i) {
            return this.loanItemSummaryModelList.get(i);
        }

        @Override
        public long getItemId(int i) {
            return 0;
        }

        @Override
        public View getView(int i, View view, ViewGroup viewGroup) {
            ViewHolder viewHolder;
            if (view == null) {
                viewHolder = new ViewHolder();
                view = LayoutInflater.from(this.context).inflate(R.layout.listview_product_item, null);
                viewHolder.productName = (TextView) view.findViewById(R.id.productName);
                viewHolder.priceValue = (TextView) view.findViewById(R.id.priceValue);
                viewHolder.interestValue = (TextView) view.findViewById(R.id.interestValue);
                view.setTag(viewHolder);
            } else {
                viewHolder = (ViewHolder) view.getTag();
            }
            viewHolder.productName.setText(this.loanItemSummaryModelList.get(i).getItem_name());
            viewHolder.priceValue.setText(this.loanItemSummaryModelList.get(i).getPrice());
            float interest = Float.valueOf(this.loanItemSummaryModelList.get(i).getInterest()) * 100;
            viewHolder.interestValue.setText(String.valueOf(interest) + "%");

            return view;
        }

        private class ViewHolder {
            private TextView productName;
            private TextView priceValue;
            private TextView interestValue;
        }
    }

    public interface OnItemsLoadedCallback {
        void onItemsLoaded();
    }

}