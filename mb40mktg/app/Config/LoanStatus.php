<?php
/**
 * Created by PhpStorm.
 * Date: 8/27/18
 * Time: 9:41 PM
 */

namespace App\Config;


class LoanStatus
{
    static function getLoanStatus()
    {
        return [
            'types' => [
                '0' => "Inactive",
                '1' => "Active",
                '2' => "Completed"
            ]
        ];
    }
}