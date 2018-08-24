<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Price extends Model {
    protected $table = "tbl_product_price";

    protected $fillable = [
        'price'
    ];
}
