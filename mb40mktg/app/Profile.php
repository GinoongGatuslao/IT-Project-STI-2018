<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Profile extends Model
{
    protected $table = "tbl_profiles";

    protected $hidden = [
        'password'
    ];

    protected $fillable = [
        'user_id',
        'first_name',
        'middle_name',
        'last_name',
        'gender',
        'address',
        'contact_num',
        'bday',
        'occupation',
        'mo_income',
        'mo_expense',
        'verified',
        'credit_limit',
        'account_status'
    ];
}
