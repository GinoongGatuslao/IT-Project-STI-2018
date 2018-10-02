<?php
/**
 * Created by PhpStorm.
 * Date: 8/27/18
 * Time: 9:41 PM
 */

namespace App\Config;


class UserType
{
    static function getUserTypes()
    {
        return [
            'types' => [
                '0' => "Administrator",
                '1' => "Staff",
                '2' => "Collector",
                '3' => "Client"
            ]
        ];
    }

    static function getUserTypeStr($id)
    {
        return self::getUserTypes()["types"][$id];
    }
}