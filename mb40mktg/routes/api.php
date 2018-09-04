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

Route::get('statuslist', 'ProductController@getStatusList');

Route::post('login',                            'Auth\LoginController@login');
Route::post('logout',                           'Auth\LoginController@logout');
Route::post('register',                         'Auth\RegisterController@register');

Route::group(['middleware' => 'auth:api'], function() {
    Route::get('product/item','ProductController@getAllProductItem');
    Route::get('product/batch','ProductController@getAllProductBatch');
    Route::get('product/itembyid/{id}','ProductController@filterItemId');
    Route::get('product/itemsbybatch/{batch_number}','ProductController@filterItemBatchNumber');
    Route::get('product/batch/{batch_number}','ProductController@filterBatchNumber');
    Route::get('product/status/{status}','ProductController@filterStatus');

    Route::post('product/item','ProductController@storeItem');
    Route::post('product/batch','ProductController@storeBatch');

    Route::put('product/updateitem/{id}', 'ProductController@updateItem');
    Route::put('product/updatebatch/{id}', 'ProductController@updateBatch');

    Route::delete('product/item/{id}','ProductController@deleteItem');
    Route::delete('product/batch/{id}','ProductController@deleteBatch');

    /**
     * PRICE
     */
    Route::post('price/price','PriceController@storePrice');
    Route::put('price/updateprice/{id}', 'PriceController@updatePrice');
    Route::delete('price/price/{id}','PriceController@deletePrice');

    /**
     * LOAN
     */
    Route::post('loan/loan', 'LoanController@storeLoan');
    Route::put('loan/updateloan', 'LoanController@updateLoan');
    Route::delete('loan/deleteloan', 'LoanController@deleteLoan');

    /**
     * TRANSACTION
     */
    Route::get('transaction', 'TransactionController@getAll');
    Route::get('transaction/id/{id}', 'TransactionController@findId');
    Route::get('transaction/account/{id}', 'TransactionController@getTransactionByAccount');
    Route::get('transaction/collector/{id}', 'TransactionController@getTransactionByCollector');
    Route::get('transaction/loan/{id}', 'TransactionController@getTransactionByLoan');
    Route::get('transaction/pay_range', 'TransactionController@getTransactionByPaymentRange');
});
