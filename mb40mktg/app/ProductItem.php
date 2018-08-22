<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class ProductItem extends Model
{
    protected $fillable = [
        'item_name',
        'price_id',
        'status',
        'stock_count',
        'reorder_point',
        'batch_number_id'
    ];
}
