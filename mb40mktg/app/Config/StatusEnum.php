<?php

namespace App\Config;

/**
 * Created by PhpStorm.
 * Date: 8/27/18
 * Time: 9:06 PM
 */

class StatusEnum {

    static function getStatus() {
        return [
            'status' => [
                '0' => "In-Stock",
                '1' => "Re-possessed",
                '2' => "Defective",
            ]
        ];
    }
}