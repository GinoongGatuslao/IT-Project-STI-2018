<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class LoanItem extends Model
{
    protected $table = "tbl_loan_items";

    protected $fillable = [
        'loan_id',
        'item_id',
        'item_status',
        'interest'
    ];
}
