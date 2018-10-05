package com.android.itproj.mb40marketing.adapter;

import android.content.Context;
import android.text.format.Formatter;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.model.TransactionModel;

import java.text.Format;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.util.Locale;

import lombok.Getter;
import lombok.Setter;

public class TransactionListAdapter extends BaseAdapter {

    @Getter
    @Setter
    private Context context;

    @Getter
    @Setter
    private List<TransactionModel> transactionModels;

    public TransactionListAdapter(Context context, List<TransactionModel> transactionModels) {
        setContext(context);
        setTransactionModels(transactionModels);
    }

    @Override
    public int getCount() {
        return transactionModels.size();
    }

    @Override
    public TransactionModel getItem(int i) {
        return getTransactionModels().get(i);
    }

    @Override
    public long getItemId(int i) {
        return 0;
    }

    @Override
    public View getView(int i, View view, ViewGroup viewGroup) {
        ViewHolder holder;
        if (view == null) {
            holder = new ViewHolder();
            view = LayoutInflater.from(getContext()).inflate(R.layout.listview_transaction_item, null);
            holder.count = (TextView) view.findViewById(R.id.count);
            holder.paymentAmount = (TextView) view.findViewById(R.id.paymentAmount);
            holder.paymentDate = (TextView) view.findViewById(R.id.paymentDate);
            holder.collectorName = (TextView) view.findViewById(R.id.collectorName);
            view.setTag(holder);
        } else {
            holder = (ViewHolder) view.getTag();
        }

        holder.count.setText(String.valueOf(i + 1));
        holder.paymentAmount.setText(String.format(Locale.getDefault(), "%1$.2f", getTransactionModels().get(i).getPayment()));
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss", Locale.getDefault());
        Date date = null;
        try {
            date = sdf.parse(getTransactionModels().get(i).getUpdated_at());
            Format formatter = new SimpleDateFormat("MMMM dd,\nyyyy", Locale.getDefault());
            holder.paymentDate.setText(formatter.format(date));
        } catch (ParseException e) {
            e.printStackTrace();
        }

        holder.collectorName.setText(
                String.format(getContext().getString(R.string.text_payment_collector),
                        getTransactionModels().get(i).getCollectorFname(),
                        getTransactionModels().get(i).getCollectorLname()));

        return view;
    }

    public class ViewHolder {
        public TextView count;
        public TextView paymentAmount;
        public TextView paymentDate;
        public TextView collectorName;
    }
}
