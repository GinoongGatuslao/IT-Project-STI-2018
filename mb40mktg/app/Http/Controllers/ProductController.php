<?php

namespace App\Http\Controllers;

use App\ProductBatch;
use App\ProductItem;
use App\Price;
use function Couchbase\defaultDecoder;
use Illuminate\Http\Request;

class ProductController extends Controller
{
    //product/item
    //DONE
    public function getAllProductItem() {
        return ProductItem::all();
    }

    //product/batch
    //DONE
    public function getAllProductBatch() {
        return ProductBatch::all();
    }

    //product/itemById/{id}
    //DONE
    public function filterItemId($id) {
        $productResult = ProductItem::where('id', $id);

        if ($productResult->count() == 0) {
            return $this->returnEmpty();
        } else {
            return response()
                ->json($productResult
                    ->first(), 200);
        }
    }

    //product/item/{batch_number}
    //DONE
    public function filterItemBatchNumber($batch_number) {
        $productResult = ProductItem::select(
            'tbl_product_item.id as product_id', 'item_name', 'stock_count',
            'reorder_point', 'batch_name', 'batch_number')
            ->join('tbl_product_batch', 'tbl_product_item.batch_number_id', '=', 'tbl_product_batch.id')
            ->where('tbl_product_batch.batch_number', $batch_number);
        /*$productResult = ProductItem::whereHas('productBatch', function ($query) use ($batch_number){
            $query->where('batch_number', $batch_number);
        });*/

        if ($productResult->count() == 0) {

            return $this->returnEmpty();
        } else {
            return response()
                ->json($productResult
                    ->get(), 200);
        }
    }

    //product/batch/{batch_number}
    //DONE
    public function filterBatchNumber($batch_number) {
        $productResult = ProductBatch::where('batch_number', $batch_number);

        if ($productResult->count() == 0) {
            return $this->returnEmpty();
        } else {
            return response()
                ->json($productResult
                    ->get(), 200);
        }
    }

    //product/status/{status}
    //DONE
    public function filterStatus($status) {
        $productResult = ProductItem::where('status', $status);

        if ($productResult->count() == 0) {
            return $this->returnEmpty();
        }
        return response()
            ->json(ProductItem::where('status', $status)
                ->get());
    }

    //product/item
    //DONE
    public function storeItem(Request $request) {
        $newProduct = ProductItem::create($request->all());
        return response()->json($newProduct, 201);
    }

    //product/batch
    //DONE
    public function storeBatch(Request $request) {
        $newProduct = ProductBatch::create($request->all());
        return response()->json($newProduct, 201);
    }

    //product/price
    //DONE
    public function storePrice(Request $request) {
        $newProduct = Price::create($request->all());
        return response()->json($newProduct, 201);
    }

    //todo pending
    public function update(Request $request, $id) {
        $product = Product::findOrFail($id);
        $product->update($request->all());
        return response()->json($product, 200);
    }

    //todo pending
    public function deleteItem($id) {
        Product::where('id', $id)->delete();
        return $this->returnSuccess();
    }

    //todo pending
    public function deleteBatch($id) {
        Product::where('id', $id)->delete();
        return $this->returnSuccess();
    }

    //todo pending
    private function returnEmpty() {
        return response()->json([], 204, ["reason"=>"No result"]);
    }

    //todo pending
    private function returnSuccess() {
        return response()->json(["success"=>true], 200);
    }
}
