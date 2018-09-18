<?php

namespace App\Http\Controllers;

use App\Loan;
use App\Price;
use Illuminate\Http\Request;
use App\LoanItem;
use App\ProductItem;

class LoanController extends Controller
{

    public function getAllLoan()
    {
        return Loan::all();
    }

    public function getLoan($account_id)
    {
        $loan = Loan::where('account_id', $account_id);
        return response()->json($loan->get(), 200);
    }

    //POST loan/loan
    public function storeLoan(Request $request)
    {
        //preloop for amortization computation
        $totalValue = 0;
        $amortztn = 0;
        foreach ($request->get("loan_items") as $item) {
            $pI = ProductItem::where('id', $item["item_id"])->first();
            $pR = Price::where('id', $pI->price_id)->first();
            $totalValue += ($pR->price * (1 + $item["interest"]));
        }

        $amortztn = $totalValue / $request["term_length"];
        /*var_dump([
            "t_value"=>$totalValue,
            "amortization" => $amortztn
        ]);*/

        $loanItems = Array();
        $newLoan = Loan::create([
            "account_id" => $request->get("account_id"),
            "term_length" => $request->get("term_length"),
            "loan_value" => $totalValue,
            "amortization" => $amortztn,
            "status" => $request->get("status")]);

        $loanItems["account"] = $newLoan;

        foreach ($request->get("loan_items") as $item) {
            $loanItems["items"][] = LoanItem::create([
                "loan_id" => $newLoan->id,
                "item_id" => $item["item_id"],
                "item_status" => $item["item_status"],
                "interest" => $item["interest"]
            ]);
        }

        return response()->json($loanItems, 201);

        //old code
        /*$newProduct = ProductBatch::create($request->all());
        return response()->json($newProduct, 201);*/
    }

    //PUT loan/updateloan
    public function updateloan(Request $request, $id)
    {
        $product = Loan::findOrFail($id);
        $product->update($request->all());
        return response()->json($product, 200);
    }

    public function deleteLoan($id)
    {
        Loan::where('id', $id)->delete();
        return $this->returnSuccess();
    }
}
