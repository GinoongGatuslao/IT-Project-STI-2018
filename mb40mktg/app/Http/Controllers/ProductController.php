<?php

namespace App\Http\Controllers;

use App\ProductBatch;
use App\ProductBatchItem;
use App\ProductItem;
use Illuminate\Http\Request;
use App\Config\StatusEnum;

class ProductController extends Controller
{

    //GET product/item
    public function getAllProductItem() {
        return ProductItem::all();
    }

    //GET product/batch
    public function getAllProductBatch() {
        return ProductBatch::all();
    }

    //GET product/itembyid/{id}
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

    //GET product/itembybatch/{batch_number}
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

    //GET product/batch/{batch_number}
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

    //GET product/status/{status}
    public function filterStatus($status) {
        $productResult = ProductItem::where('status', $status);

        if ($productResult->count() == 0) {
            return $this->returnEmpty();
        }
        return response()
            ->json(ProductItem::where('status', $status)
                ->get());
    }

    //POST product/item
    public function storeItem(Request $request) {
        $newProduct = ProductItem::create($request->all());
        return response()->json($newProduct, 201);
    }

    //POST product/batch
    public function storeBatch(Request $request) {
        $productBatch = ProductBatch::create([
            "batch_name" => $request->get("batch_name"),
            "batch_number" => $request->get("batch_number"),
            "date_rcv"=>$request->get("date_rcv")]);

        $updatedProducts = Array();
        foreach ($request->get("items_rcv") as $item) {
            ProductBatchItem::create([
                "product_item_id" => $item["item_id"],
                "batch_id" => $productBatch->id,
                "quantity" => $item["quantity"]
            ]);

            $prodItem = ProductItem::findOrFail($item["item_id"]);
            $prodItem->stock_count = ($prodItem->stock_count + intval($item["quantity"]));
            $prodItem->update();

            $updatedProducts[] = $prodItem;
        }

        return response()->json($updatedProducts, 201);

        //old code
        /*$newProduct = ProductBatch::create($request->all());
        return response()->json($newProduct, 201);*/
    }

    //PUT product/item
    public function updateItem(Request $request, $id) {
        $product = ProductItem::findOrFail($id);
        $product->update($request->all());
        return response()->json($product, 200);
    }

    //PUT product/batch
    public function updateBatch(Request $request, $id) {
        $product = ProductBatch::findOrFail($id);
        $product->update($request->all());
        return response()->json($product, 200);
    }

    //DELETE product/item/{id}
    public function deleteItem($id) {
        ProductItem::where('id', $id)->delete();
        return $this->returnSuccess();
    }

    //DELETE product/batch/{id}
    public function deleteBatch($id) {
        ProductBatch::where('id', $id)->delete();
        return $this->returnSuccess();
    }
}
