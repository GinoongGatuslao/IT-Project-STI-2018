<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Transaction extends Model
{
    protected $table = "tbl_transactions";

    protected $fillable = [
        'account_id',
        'loan_id',
        'collector_id',
        'payment'
    ];
}
