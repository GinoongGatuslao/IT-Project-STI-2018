<?php

use App\Product;
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

Route::post('login',                            'Auth\LoginController@login');
Route::post('logout',                           'Auth\LoginController@logout');
Route::post('register',                         'Auth\RegisterController@register');

Route::group(['middleware' => 'auth:api'], function() {
    Route::get('product/item','ProductController@getAllProductItem');
    Route::get('product/batch','ProductController@getAllProductBatch');
    Route::get('product/itemById/{id}','ProductController@filterItemId');
    Route::get('product/itemByBatch/{batch_number}','ProductController@filterItemBatchNumber');
    Route::get('product/batch/{batch_number}','ProductController@filterBatchNumber');
    Route::get('product/status/{status}','ProductController@filterStatus');
    Route::post('product/price','ProductController@storePrice');
    Route::post('product/item','ProductController@storeItem');
    Route::post('product/batch','ProductController@storeBatch');
    Route::put('product/updateItem/{id}', 'ProductController@updateItem');
    Route::put('product/updateBatch/{id}', 'ProductController@updateBatch');
    Route::delete('product/item/{id}','ProductController@deleteItem');
    Route::delete('product/batch/{id}','ProductController@deleteBatch');
});
