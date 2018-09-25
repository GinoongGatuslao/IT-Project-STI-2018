<?php
/**
 * Created by PhpStorm.
 * Date: 8/27/18
 * Time: 9:41 PM
 */

namespace App\Config;


class AccountStatus
{
    static function getAccountStatus()
    {
        return [
            'types' => [
                '0' => "Pending",
                '1' => "Confirmed"
            ]
        ];
    }
}