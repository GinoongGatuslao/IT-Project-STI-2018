<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Loan extends Model
{
    protected $table = "tbl_loans";

    protected $fillable = [
        'account_id',
        'length	',
        'loan_value',
        'amortization',
        'interest_rt',
        'status'
    ];
}
