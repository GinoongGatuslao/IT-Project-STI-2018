<?php

namespace App\Http\Controllers;

use App\Account;
use Illuminate\Http\Request;
use Illuminate\Http\Response;

class AccountController extends Controller
{
    public function getAccount($id)
    {
        $account = Account::findOrfail($id);
        return response()->json($account, 200);
    }

    public function getAccountByProfile($profileId)
    {
        $account = Account::where('profile_id', $profileId);
        return response()->json($account->first(), 200);
    }

    public function newAccount(Request $request)
    {
        $checkAccount = Account::where('profile_id', $request->get("profile_id"));
        if ($checkAccount->first() != null) {
            return response()->json(["reason"=>"Existing account"], Response::HTTP_CONFLICT);
        } else {
            $account = Account::create($request->all());
            return response($account, 200);
        }
    }
}
