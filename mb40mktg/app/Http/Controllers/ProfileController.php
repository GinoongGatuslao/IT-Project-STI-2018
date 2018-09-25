<?php

namespace App\Http\Controllers;

use App\Profile;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;

class ProfileController extends Controller
{
    public function getAllProfiles() {
        return Profile::select("*")
            ->join('users', 'tbl_profiles.user_id', '=', 'users.id')->get();
    }

    public function getProfile($id)
    {
        $profile = Profile::where('id', $id);
        return response()->json($profile->get(), 200);
    }

    public function getUserProfile($id)
    {
        $profile = Profile::where('user_id', $id);
        return response()->json($profile->first(), 200);
    }

    public function createProfile(Request $request)
    {
        $newProfile = Profile::create($request->all());
        return response()->json($newProfile, 200);
    }

    public function updateProfile(Request $request, $id)
    {
        $profile = Profile::findOrFail($id);
        $profile->update($request->all());
        return response()->json($profile, 200);
    }

    public function getAccountProfileByName(Request $request)
    {
        $fname = $request->header("fname");
        $lname = $request->header("lname");

        $profileQuery = DB::select(DB::raw(
            "SELECT *
            FROM tbl_profiles profile
            WHERE profile.last_name LIKE \"%" . $lname . "%\"
            AND profile.first_name LIKE \"%" . $fname . "%\""));

        return response()->json($profileQuery, 200);
    }
}
