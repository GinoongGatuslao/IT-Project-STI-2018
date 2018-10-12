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
use Illuminate\Foundation\Auth\AuthenticatesUsers;
use App\Loan;

class Controller extends BaseController
{
    use AuthorizesRequests, DispatchesJobs, ValidatesRequests, AuthenticatesUsers;

    public function returnEmpty()
    {
        return response()->json([], 204, ["reason" => "No result"]);
    }

    public function returnSuccess()
    {
        return response()->json(["success" => true], 200);
    }

    //GET statuslist
    public function getStatusList()
    {
        return StatusEnum::getStatus();
    }

    //GET usertypes
    public function getUserTypes()
    {
        return UserType::getUserTypes();
    }

    //GET loanstatus
    public function getLoanStatus()
    {
        return LoanStatus::getLoanStatus();
    }

    //GET account status
    public function getAccountStatus()
    {
        return AccountStatus::getAccountStatus();
    }

    public function getUserInfo(Request $request)
    {
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
        $updatedUser = Array();
        if (!strlen($data["username"]) == 0) {
            $updatedUser["username"] = $data["username"];
        }
//        "password" => Hash::make($data["password"])
        if (!strlen($data["password"]) == 0) {
            $updatedUser["password"] = Hash::make($data["password"]);
        }
        $current = User::where("id", $id);
        $current->update($updatedUser);
        return response()->json($current->first(), 200);
    }


    public function getTotalExistingLoanAmortization($profile_id)
    {
        $loans = Loan::where("profile_id", $profile_id)->get();
        $t_val = 0;
        foreach ($loans as $loan) {
            if ($loan["status"] == 1 || $loan["status"] == 0) {
                $t_val += $loan["amortization"];
            } else {
                continue;
            }
        }
        return $t_val;
    }
}
