package com.android.itproj.mb40marketing.adapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import com.android.itproj.mb40marketing.R;
import com.android.itproj.mb40marketing.model.ProfileModel;

import java.util.List;
import java.util.Locale;

import lombok.Getter;
import lombok.Setter;

public class ProfileListViewAdapter extends BaseAdapter {

    @Getter
    @Setter
    private Context context;

    @Getter
    @Setter
    private List<ProfileModel> profileModels;

    public ProfileListViewAdapter(Context context, List<ProfileModel> profileModelList) {
        this.setContext(context);
        this.setProfileModels(profileModelList);
    }

    @Override
    public int getCount() {
        return getProfileModels().size();
    }

    @Override
    public ProfileModel getItem(int i) {
        return getProfileModels().get(i);
    }

    @Override
    public long getItemId(int i) {
        return getProfileModels().get(i).getId();
    }

    @Override
    public View getView(int i, View view, ViewGroup viewGroup) {
        ViewHolder holder;
        if (view == null) {
            holder = new ViewHolder();
            view = LayoutInflater.from(getContext()).inflate(R.layout.listview_profile_item, null);
            holder.customerName = (TextView) view.findViewById(R.id.customerName);
            holder.customerAddress = (TextView) view.findViewById(R.id.customerAddress);
            holder.customerLoanId = (TextView) view.findViewById(R.id.loanId);
            view.setTag(holder);
        } else {
            holder = (ViewHolder) view.getTag();
        }
        holder.customerName.setText(String.format(
                getContext().getString(R.string.text_customer_name_placeholder),
                getProfileModels().get(i).getLast_name().toUpperCase(Locale.getDefault()),
                getProfileModels().get(i).getFirst_name(),
                getProfileModels().get(i).getMiddle_name()));
        holder.customerLoanId.setText(
                String.format(
                        getContext().getString(R.string.text_customer_profile_id_placeholder),
                        getProfileModels().get(i).getId()));
        holder.customerAddress.setText(getProfileModels().get(i).getAddress());
        return view;
    }

    public class ViewHolder {
        public TextView customerName;
        public TextView customerAddress;
        public TextView customerLoanId;
    }
}
