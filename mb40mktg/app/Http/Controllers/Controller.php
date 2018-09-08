<?php

namespace App\Http\Controllers;

use App\Config\UserType;
use App\Config\StatusEnum;
use App\User;
use Illuminate\Foundation\Bus\DispatchesJobs;
use Illuminate\Routing\Controller as BaseController;
use Illuminate\Foundation\Validation\ValidatesRequests;
use Illuminate\Foundation\Auth\Access\AuthorizesRequests;

class Controller extends BaseController
{
    use AuthorizesRequests, DispatchesJobs, ValidatesRequests;

    public function returnEmpty() {
        return response()->json([], 204, ["reason"=>"No result"]);
    }

    public function returnSuccess() {
        return response()->json(["success"=>true], 200);
    }

    //GET statuslist
    public function getStatusList() {
        return StatusEnum::getStatus();
    }

    //GET usertypes
    public function getUserTypes() {
        return UserType::getUserTypes();
    }

    public function getUserInfo(Request $request){
        $user = User::where('api_token', $request->header('token'));
        return response()->json($user, 200);
    }
}
