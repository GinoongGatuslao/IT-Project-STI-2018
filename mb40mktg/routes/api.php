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

/**
GET
*/
Route::get('product', function() {
	return Product::all();
});

Route::get('product/id/{id}', 'ProductController@filterId');

Route::get('product/bn/{batch_number}', 'ProductController@filterBatchNumber');

Route::get('product/status/{status}', 'ProductController@filterStatus');

/**
POST
*/

Route::post('product', function(Request $request) {
    return Product::create($request->all);
});

/**
UPDATE
*/
Route::put('product/{id}', function(Request $request, $id) {
    $product = Product::findOrFail($id);
    $product->update($request->all());

    return $product;
});

/**
DELETE
*/
Route::delete('product/{id}', function($id) {
    Product::find($id)->delete();

    return 204;
});