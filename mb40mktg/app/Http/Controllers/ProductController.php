<?php

namespace App\Http\Controllers;

use App\ProductBatch;
use App\ProductBatchItem;
use App\ProductItem;
use Illuminate\Http\Request;
use App\Config\StatusEnum;
use Illuminate\Support\Facades\DB;

class ProductController extends Controller
{

    //GET product/item
    public function getAllProductItem()
    {
        return DB::select(DB::raw("
          SELECT tbl_product_item.*, price.price
          FROM tbl_product_item
          INNER JOIN tbl_product_price price ON price.id = tbl_product_item.price_id;"));
    }

    //GET product/batch
    public function getAllProductBatch()
    {
        return ProductBatch::all();
    }

    //GET product/itembyid/{id}
    public function filterItemId($id)
    {
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
    public function filterItemBatchNumber($batch_number)
    {
        $productResult = ProductBatchItem::select('tbl_product_item.*')
            ->join('tbl_product_batch', 'tbl_product_batch_item.batch_id', '=', 'tbl_product_batch.id')
            ->join('tbl_product_item', 'tbl_product_batch_item.product_item_id', '=', 'tbl_product_item.id')
            ->where('tbl_product_batch.batch_number', $batch_number);

        if ($productResult->count() == 0) {

            return $this->returnEmpty();
        } else {
            return response()
                ->json($productResult
                    ->get(), 200);
        }
    }

    //GET product/batch/{batch_number}
    public function filterBatchNumber($batch_number)
    {
        $productResult = ProductBatch::where('batch_number', $batch_number);

        if ($productResult->count() == 0) {
            return $this->returnEmpty();
        } else {
            return response()
                ->json($productResult
                    ->get(), 200);
        }
    }

    //POST product/item
    public function storeItem(Request $request)
    {
        $newProduct = ProductItem::create($request->all());
        return response()->json($newProduct, 201);
    }

    //POST product/batch
    public function storeBatch(Request $request)
    {
        $productBatch = ProductBatch::create([
            "batch_name" => $request->get("batch_name"),
            "batch_number" => $request->get("batch_number"),
            "date_rcv" => $request->get("date_rcv")]);

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
    public function updateItem(Request $request, $id)
    {
        $product = ProductItem::findOrFail($id);
        $product->update($request->all());
        return response()->json($product, 200);
    }

    //PUT product/batch
    public function updateBatch(Request $request, $id)
    {
        $product = ProductBatch::findOrFail($id);
        $product->update($request->all());
        return response()->json($product, 200);
    }

    //DELETE product/item/{id}
    public function deleteItem($id)
    {
        ProductItem::where('id', $id)->delete();
        return $this->returnSuccess();
    }

    //DELETE product/batch/{id}
    public function deleteBatch($id)
    {
        ProductBatch::where('id', $id)->delete();
        return $this->returnSuccess();
    }
}
