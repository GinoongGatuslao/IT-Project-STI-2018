<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Price;

class PriceController extends Controller
{
    //POST product/price
    public function storePrice(Request $request) {
        $newProduct = Price::create($request->all());
        return response()->json($newProduct, 201);
    }

    //PUT product/price
    public function updatePrice(Request $request, $id) {
        $product = Price::findOrFail($id);
        $product->update($request->all());
        return response()->json($product, 200);
    }

    //DELETE product/price/{id}
    public function deletePrice($id) {
        Price::where('id', $id)->delete();
        return $this->returnSuccess();
    }
}
