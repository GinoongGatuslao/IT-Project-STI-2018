<?php

namespace App\Http\Controllers;

use App\Profile;
use Illuminate\Http\Request;

class ProfileController extends Controller
{
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
}
