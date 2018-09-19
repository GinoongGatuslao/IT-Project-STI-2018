package com.android.itproj.mb40marketing.view.activities;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.DialogFragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ListView;
import android.widget.TextView;

import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.model.LoanItemSummaryModel;

import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;

public class LoanItemDialogFragment extends DialogFragment {

    @BindView(R.id.itemsListView)
    public ListView itemsListView;

    private List<LoanItemSummaryModel> loanItemSummaryModels;

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

        LoanProductItemAdapter adapter = new LoanProductItemAdapter(
                getActivity(),
                this.loanItemSummaryModels);
        itemsListView.setAdapter(adapter);
    }

    public void setLoanItemSummaryModels(List<LoanItemSummaryModel> loanItemSummaryModels) {
        this.loanItemSummaryModels = loanItemSummaryModels;
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

}