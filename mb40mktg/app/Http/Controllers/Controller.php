<?php

namespace App\Http\Controllers;

use App\Config\AccountStatus;
use App\Config\LoanStatus;
use App\Config\UserType;
use App\Config\StatusEnum;
use App\User;
use Illuminate\Foundation\Bus\DispatchesJobs;
use Illuminate\Routing\Controller as BaseController;
use Illuminate\Foundation\Validation\ValidatesRequests;
use Illuminate\Foundation\Auth\Access\AuthorizesRequests;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;

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

    //GET loanstatus
    public function getLoanStatus() {
        return LoanStatus::getLoanStatus();
    }

    //GET account status
    public function getAccountStatus() {
        return AccountStatus::getAccountStatus();
    }

    public function getUserInfo(Request $request){
        $user = User::where('api_token', $request->header('token'));
        return response()->json($user, 200);
    }

    public function getAllUsers()
    {
        $users = User::all();
        foreach ($users as $user) {
            $user->usertype_str = UserType::getUserTypeStr($user->user_type);
        }
        return $users;
    }

    public function update(Request $data, $id)
    {
        $updatedUser = Array(
            "username"=>$data["username"],
            "password" => Hash::make($data["password"])
        );
        $current = User::where("id", $id);
        $current->update($updatedUser);
        return response()->json($current->get(), 200);
    }
}
