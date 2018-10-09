package com.android.itproj.mb40marketing.adapter;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.content.ContextCompat;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.model.LoanModel;

import java.util.List;

import lombok.Getter;
import lombok.Setter;

public class LoanSpinnerAdapter extends ArrayAdapter<LoanModel> {

    @Getter
    @Setter
    private Context context;

    @Getter
    @Setter
    private List<LoanModel> loanModels;

    public LoanSpinnerAdapter(@NonNull Context context, int resource, @NonNull List<LoanModel> objects) {
        super(context, resource, objects);
        setContext(context);
        setLoanModels(objects);
    }

    @Override
    public int getCount() {
        return getLoanModels().size();
    }

    @Override
    public LoanModel getItem(int i) {
        return getLoanModels().get(i);
    }

    @Override
    public long getItemId(int i) {
        return 0;
    }

    @Override
    public View getDropDownView(int position, @Nullable View convertView, @NonNull ViewGroup parent) {
        return createLoanItemView(position, convertView, parent);
    }

    @Override
    public View getView(int i, View view, ViewGroup viewGroup) {
        return createLoanItemView(i, view, viewGroup);
    }

    private View createLoanItemView(int position, View view, ViewGroup viewGroup) {
        ViewHolder viewHolder;
        if (view == null) {
            viewHolder = new ViewHolder();
            view = LayoutInflater.from(getContext()).inflate(R.layout.listview_loan_item, null);
            viewHolder.loanTitle = (TextView) view.findViewById(R.id.loanTitle);
            viewHolder.loanStatusValue = (TextView) view.findViewById(R.id.loanStatusValue);
            viewHolder.valueTerm = (TextView) view.findViewById(R.id.valueTerm);
            viewHolder.valueAmount = (TextView) view.findViewById(R.id.valueAmount);
            view.setTag(viewHolder);
        } else {
            viewHolder = (ViewHolder) view.getTag();
        }

        viewHolder.loanTitle.setText(getLoanModels().get(position).getCreated_at());
        viewHolder.loanStatusValue.setText((getLoanModels().get(position).getStatus_str()));
        setStatusColor(viewHolder.loanStatusValue, getLoanModels().get(position).getStatus());
        viewHolder.valueTerm.setText(
                String.format(getContext().getString(R.string.loan_term_placeholder),
                        getLoanModels().get(position).getTerm_length(),
                        getLoanModels().get(position).getAmortization()));
        viewHolder.valueAmount.setText(
                String.format(context.getString(R.string.loan_amount_placeholder),
                getLoanModels().get(position).getRemaining_balance(),
                getLoanModels().get(position).getLoan_value()));

        return view;
    }

    private void setStatusColor(TextView statusText, int status) {
        switch (status) {
            case 0:
                statusText.setTextColor(ContextCompat.getColor(context, R.color.colorRed));
                break;
            case 1:
                statusText.setTextColor(ContextCompat.getColor(context, android.R.color.white));
                break;
            case 2:
                statusText.setTextColor(ContextCompat.getColor(context, R.color.colorLightGreen));
                break;
        }
    }

    public class ViewHolder {
        public TextView loanTitle;
        public TextView loanStatusValue;
        public TextView valueTerm;
        public TextView valueAmount;
    }
}
