<?php

namespace App\Http\Controllers;

use App\Product;
use Illuminate\Http\Request;

class ProductController extends Controller
{

    public function index() {
        return Product::all();
    }

    public function filterId($id) {
        $productResult = Product::where('id', $id);
        if ($productResult->count() == 0) {
            return $this->returnEmpty();
        } else {
            return response()
                ->json($productResult
                    ->first(), 200);
        }
    }

    public function filterBatchNumber($batch_number) {
        $productResult = Product::where('batch_number', $batch_number);
        if ($productResult->count() == 0) {
            return $this->returnEmpty();
        } else {
            return response()
                ->json($productResult
                    ->get(), 200);
        }
    }

    public function filterStatus($status) {
        $productResult = Product::where('status', $status);
        if ($productResult->count() == 0) {
            return $this->returnEmpty();
        }
        return response()
            ->json(Product::where('status', $status)
                ->get());
    }

    public function store(Request $request) {
        $newProduct = Product::create($request->all());
        return response()->json($newProduct, 201);
    }

    public function update(Request $request, $id) {
        $product = Product::findOrFail($id);
        $product->update($request->all());
        return response()->json($product, 200);
    }

    public function delete(Request $request, $id) {
        $request->delete();
        return response()->json(null, 204);
    }

    private function returnEmpty() {
        return response()->json([], 204, ["reason"=>"No result"]);
    }
}
