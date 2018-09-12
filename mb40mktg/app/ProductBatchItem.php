<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class ProductBatchItem extends Model
{
    protected $table = "tbl_product_batch_item";

    protected $fillable = [
        "product_item_id",
        "batch_id",
        "quantity"
    ];
}
