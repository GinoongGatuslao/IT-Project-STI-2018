<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Product extends Model
{
    //
    protected $fillable = ['id',
							'name',
							'price',
							'reorder_point',
							'status',
							'batch_number',
							'returned',
							'defective'];

	
}
