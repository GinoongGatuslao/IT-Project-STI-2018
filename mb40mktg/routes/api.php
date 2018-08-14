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
    Route::get('product',                           'ProductController@index');
    Route::get('product/id/{id}',                   'ProductController@filterId');
    Route::get('product/bn/{batch_number}',         'ProductController@filterBatchNumber');
    Route::get('product/status/{status}',           'ProductController@filterStatus');
    Route::put('product/{id}',                      'ProductController@update');
    Route::post('product',                          'ProductController@store');
    Route::delete('product/{id}',                   'ProductController@delete');
});
