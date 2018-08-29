<?php

namespace App\Http\Controllers;

use App\Loan;
use Illuminate\Http\Request;

class LoanController extends Controller
{

    public function getAllLoan() {
        return Loan::all();
    }
    //POST loan/loan
    public function storeLoan(Request $request) {
        $newLoan = Loan::create($request->all());
        return response()->json($newLoan, 201);
    }

    //PUT loan/updateloan
    public function updateloan(Request $request, $id) {
        $product = Loan::findOrFail($id);
        $product->update($request->all());
        return response()->json($product, 200);
    }

    public function deleteLoan($id) {
        Loan::where('id', $id)->delete();
        return $this->returnSuccess();
    }
}
