<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class ProductItem extends Model {

    protected $table = "tbl_product_item";
    public $timestamps = false;

    protected $fillable = [
        "item_name",
        "price_id",
        "stock_count",
        "repossessed",
        "damaged",
        "reorder_point",
        "description"
    ];
}
