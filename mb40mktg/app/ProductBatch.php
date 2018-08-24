<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class ProductBatch extends Model {

    protected $table = "tbl_product_batch";

    protected $fillable = [
        'batch_name',
        'batch_number'
    ];
}
