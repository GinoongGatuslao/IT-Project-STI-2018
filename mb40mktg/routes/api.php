<?php

use Illuminate\Http\Request;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
*/

Route::middleware('auth:api')->get('/user', function (Request $request) {
    return $request->user();
});

Route::get('statuslist', 'Controller@getStatusList');
Route::get('usertypes', 'Controller@getUserTypes');

Route::post('login', 'Auth\LoginController@login');
Route::post('logout', 'Auth\LoginController@logout');
Route::post('register', 'Auth\RegisterController@register');

Route::group(['middleware' => 'auth:api'], function () {
    Route::get('userinfo', 'Controller@getUserInfo');

    Route::get('product/getitems', 'ProductController@getAllProductItem');
    Route::get('product/getbatches', 'ProductController@getAllProductBatch');
    Route::get('product/itembyid/{id}', 'ProductController@filterItemId');
    Route::get('product/itemsbybatch/{batch_number}', 'ProductController@filterItemBatchNumber');
    Route::get('product/batch/{batch_number}', 'ProductController@filterBatchNumber');
    Route::get('product/status/{status}', 'ProductController@filterStatus');

    Route::post('product/item', 'ProductController@storeItem');
    Route::post('product/batch', 'ProductController@storeBatch');

    Route::put('product/updateitem/{id}', 'ProductController@updateItem');
    Route::put('product/updatebatch/{id}', 'ProductController@updateBatch');

    Route::delete('product/item/{id}', 'ProductController@deleteItem');
    Route::delete('product/batch/{id}', 'ProductController@deleteBatch');

    /**
     * PROFILE
     */
    Route::get('profile/get/{id}', 'ProfileController@getProfile');
    Route::get('profile/user/{id}', 'ProfileController@getUserProfile');
    Route::post('profile/createprofile', 'ProfileController@createProfile');
    Route::put('profile/updateprofile', 'ProfileController@updateProfile');

    /**
     * PRICE
     */
    Route::post('price/addprice', 'PriceController@storePrice');
    Route::put('price/updateprice/{id}', 'PriceController@updatePrice');
    Route::delete('price/price/{id}', 'PriceController@deletePrice');

    /**
     * LOAN
     * todo: add specific filtering like transactions except by collector and by loan
     */
    Route::get('loan/getloan/{account_id}', 'LoanController@getLoan');
    Route::get('loan/getloanitems/{loan_id}', 'LoanController@getLoanItems');
    Route::post('loan/addloan', 'LoanController@storeLoan');
    Route::put('loan/updateloan', 'LoanController@updateLoan');
    Route::delete('loan/deleteloan', 'LoanController@deleteLoan');

    /**
     * TRANSACTION
     * lacking adding of transaction record
     */
    Route::get('transaction', 'TransactionController@getAll');
    Route::get('transaction/id/{id}', 'TransactionController@findId');
    Route::get('transaction/account/{id}', 'TransactionController@getTransactionByAccount');
    Route::get('transaction/collector/{id}', 'TransactionController@getTransactionByCollector');
    Route::get('transaction/loan/{id}', 'TransactionController@getTransactionByLoan');
    Route::get('transaction/pay_range', 'TransactionController@getTransactionByPaymentRange');

    /**
     * ACCOUNT
     */
    Route::post('account/newaccount', 'AccountController@newAccount');
    Route::get('account/getaccount/{id}', 'AccountController@getAccount');
    Route::get('account/getaccountbyprofile/{id}', 'AccountController@getAccountByProfile');
});
