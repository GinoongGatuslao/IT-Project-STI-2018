package com.android.itproj.mb40marketing.adapter;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.android.itproj.mb40marketing.CoreApp;
import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.helper.interfaces.ProfileCallbacks;
import com.android.itproj.mb40marketing.model.LoanItemSummaryModel;
import com.android.itproj.mb40marketing.model.LoanModel;

import java.util.List;

import lombok.Getter;

public class LoanListAdapter extends BaseAdapter {

    private Context context;
    private List<LoanModel> loanModelList;

    public LoanListAdapter(Context context, List<LoanModel> loanModelList) {
        this.context = context;
        this.loanModelList = loanModelList;
    }

    @Override
    public int getCount() {
        return loanModelList.size();
    }

    @Override
    public LoanModel getItem(int i) {
        return this.loanModelList.get(i);
    }

    @Override
    public long getItemId(int i) {
        return loanModelList.get(i).getId();
    }

    @Override
    public View getView(final int i, View view, ViewGroup viewGroup) {
        LoanModel loanModel = this.loanModelList.get(i);
        final ViewHolder viewHolder;
        if (view == null) {
            viewHolder = new ViewHolder();
            view = LayoutInflater.from(this.context).inflate(R.layout.listview_loan_item, null);
            viewHolder.loanTitle = (TextView) view.findViewById(R.id.loanTitle);
            viewHolder.loanStatusValue = (TextView) view.findViewById(R.id.loanStatusValue);
            viewHolder.valueTerm = (TextView) view.findViewById(R.id.valueTerm);
            viewHolder.valueAmount = (TextView) view.findViewById(R.id.valueAmount);
            view.setTag(viewHolder);
        } else {
            viewHolder = (ViewHolder) view.getTag();
        }
        viewHolder.loanTitle.setText(loanModel.getCreated_at());
        viewHolder.loanStatusValue.setText((loanModel.getStatus() == 1 ? "ACTIVE" : "INACTIVE"));
        viewHolder.valueTerm.setText(loanModel.getTerm_length() + " @ " + loanModel.getAmortization());
        viewHolder.valueAmount.setText(String.valueOf(loanModel.getLoan_value()));

        ((CoreApp) this.context)
                .getProfileController()
                .getLoanItems(loanModel.getId(), new ProfileCallbacks.UserLoanItemsCallback() {
                    @Override
                    public void onLoanItemListRequest(List<LoanItemSummaryModel> loanItemSummaryModels) {
                        loanModelList.get(i).setLoanItemSummary(loanItemSummaryModels);
                    }

                    @Override
                    public void onError(Throwable throwable, int code) {
                        Log.e("LoanItemsErr", "onError: " + throwable.getMessage(), throwable);
                    }
                });
        return view;
    }

    public class ViewHolder {
        private TextView loanTitle;
        private TextView loanStatusValue;
        private TextView valueTerm;
        private TextView valueAmount;

        @Getter
        private List<LoanItemSummaryModel> loanItemSummaryModelList;
    }
}
