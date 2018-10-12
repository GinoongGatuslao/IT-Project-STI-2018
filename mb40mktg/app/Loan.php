<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Loan extends Model
{
    protected $table = "tbl_loans";

    protected $fillable = [
        'profile_id',
        'term_length',
        'loan_value',
        'running_balance',
        'amortization',
        'remaining_balance',
        'status'
    ];
}
