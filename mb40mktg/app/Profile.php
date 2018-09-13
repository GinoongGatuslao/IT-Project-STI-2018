<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Profile extends Model
{
    protected $table = "tbl_profiles";

    protected $fillable = [
        'user_id',
        'first_name',
        'middle_name',
        'last_name',
        'address',
        'contact_num',
        'bday',
        'occupation',
        'mo_income',
        'mo_expense',
        'path_id_pic',
        'path_house_sketch_pic'
    ];
}
