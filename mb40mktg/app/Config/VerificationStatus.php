<?php
/**
 * Created by PhpStorm.
 * Date: 8/27/18
 * Time: 9:41 PM
 */

namespace App\Config;


class VerificationStatus
{
    static function getVerificationStatus()
    {
        return [
            'state' => [
                '0' => "Unconfirmed",
                '1' => "Confirmed"
            ]
        ];
    }

    static function getVerificationState($id)
    {
        return self::getVerificationStatus()["state"][$id];
    }
}