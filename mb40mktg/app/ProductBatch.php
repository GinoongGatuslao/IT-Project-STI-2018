<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class ProductBatch extends Model
{
    protected $fillable = [
        'batch_name',
        'batch_number'
    ];
}
