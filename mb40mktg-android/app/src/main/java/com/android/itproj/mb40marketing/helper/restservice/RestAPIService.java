package com.android.itproj.mb40marketing.helper.restservice;

import com.android.itproj.mb40marketing.model.AccountModel;
import com.android.itproj.mb40marketing.model.LoanItemSummaryModel;
import com.android.itproj.mb40marketing.model.LoanModel;
import com.android.itproj.mb40marketing.model.PriceModel;
import com.android.itproj.mb40marketing.model.ProductBatchModel;
import com.android.itproj.mb40marketing.model.ProductItemModel;
import com.android.itproj.mb40marketing.model.ProfileModel;
import com.android.itproj.mb40marketing.model.TransactionModel;
import com.android.itproj.mb40marketing.model.UserLogin;
import com.android.itproj.mb40marketing.model.UserModel;
import com.google.gson.JsonObject;

import java.util.List;
import java.util.Map;

import okhttp3.ResponseBody;
import retrofit2.Response;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.HeaderMap;
import retrofit2.http.Headers;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;
import rx.Observable;

public interface RestAPIService {

    ///////////////////////////////////////////////////////////////////////////
    // AUTH
    ///////////////////////////////////////////////////////////////////////////
    @POST("login")
    Observable<UserModel> doLogin(@Body UserLogin userLogin);

    @POST("logout")
    Observable<Response<ResponseBody>> doLogout();

    @Headers("Content-Type: application/json")
    @POST("register")
    Observable<JsonObject> registerUser(@Body JsonObject newUserModel);

    ///////////////////////////////////////////////////////////////////////////
    // PROFILE
    ///////////////////////////////////////////////////////////////////////////

    @GET("profile/get/{id}")
    Observable<ProfileModel> getProfile(@Path("id") int id);

    @GET("profile/user/{id}")
    Observable<ProfileModel> getUserProfile(@Path("id") int id);

    @GET("profile/get")
    Observable<List<ProfileModel>> getUserProfileByName(@HeaderMap Map<String, String> headers);

    @POST("profile/createprofile")
    Observable<ProfileModel> createProfile(@Body ProfileModel profileModel);

    @PUT("profile/updateprofile")
    Observable<ProfileModel> updateProfile(@Body ProfileModel profileModel);

    ///////////////////////////////////////////////////////////////////////////
    // PRODUCT
    ///////////////////////////////////////////////////////////////////////////

    @GET("product/item")
    Observable<List<ProductItemModel>> getAllItems();

    @GET("product/itembyid/{id}")
    Observable<ProductItemModel> getItemById(@Path("id") String itemId);

    @GET("product/itemsbybatch/{batch_number}")
    Observable<List<ProductItemModel>> getItemsByBatchNumber(@Path("batch_number") String batchNumber);

    @GET("product/status/{status}")
    Observable<List<ProductItemModel>> getItemsByStatus(@Path("status") String status);

    @Headers("Content-Type:application/json")
    @POST("product/item")
    Observable<ProductItemModel> storeItem(@Body JsonObject jsonRequest);

    ///////////////////////////////////////////////////////////////////////////
    // BATCH
    ///////////////////////////////////////////////////////////////////////////

    @GET("product/batch")
    Observable<List<ProductBatchModel>> getAllBatch();

    @GET("product/batch/{batch_number}")
    Observable<ProductBatchModel> getBatchNumber();

    @Headers("Content-Type:application/json")
    @POST("product/batch")
    Observable<ProductBatchModel> storeBatch(@Body JsonObject jsonRequest);

    ///////////////////////////////////////////////////////////////////////////
    // PRICE
    ///////////////////////////////////////////////////////////////////////////
    @Headers("Content-Type:application/json")
    @POST("price/price")
    Observable<PriceModel> storePrice(@Body JsonObject jsonRequest);

    ///////////////////////////////////////////////////////////////////////////
    // LOAN
    ///////////////////////////////////////////////////////////////////////////
    @Headers("Content-Type:application/json")
    @POST("loan/addloan")
    Observable<LoanModel> storeLoan(@Body JsonObject jsonRequest);

    @GET("loan/getloan/{account_id}")
    Observable<List<LoanModel>> getLoans(@Path("account_id") int accountId);

    @GET("loan/getloanitems/{loan_id}")
    Observable<List<LoanItemSummaryModel>> getLoanItems(@Path("loan_id") int loan_id);

    ///////////////////////////////////////////////////////////////////////////
    // TRANSACTION
    ///////////////////////////////////////////////////////////////////////////
    @GET("transaction")
    Observable<List<TransactionModel>> getAllTransactions();

    @GET("transaction/id/{trans_id}")
    Observable<TransactionModel> getTransactionById(@Path("trans_id") String trans_id);

    @GET("transaction/account/{accnt_id}")
    Observable<List<TransactionModel>> getTransactionsByAccountId(@Path("accnt_id") String accnt_id);

    @GET("transaction/collector/{collector_id}")
    Observable<List<TransactionModel>> getTransactionsByCollector(@Path("collector_id") String collector_id);

    @GET("transaction/loan/{loan_id}")
    Observable<List<TransactionModel>> getTransactionsByLoan(@Path("loan_id") String loan_id);

    @GET("transaction/pay_range")
    Observable<List<TransactionModel>> getTransactionsWithinRange(@HeaderMap Map<String, String> headers);
}
