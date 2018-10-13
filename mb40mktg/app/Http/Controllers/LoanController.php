<?php

namespace App\Http\Controllers;

use App\Config\LoanStatus;
use App\Config\StatusEnum;
use App\Loan;
use App\LoanItem;
use App\Price;
use App\ProductItem;
use App\Profile;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;

class LoanController extends Controller
{

    public function getAllLoan()
    {
        $loans = DB::select(DB::raw("
        SELECT loan.*, 
            profile.first_name, profile.middle_name, 
            profile.last_name, profile.contact_num, 
            profile.address, profile.credit_limit
        FROM tbl_loans loan
        INNER JOIN tbl_profiles profile ON loan.profile_id = profile.id"));
        //add String representation of 'status' id
        foreach ($loans as $loan) {
            $loan->status_str = LoanStatus::getStatusStr($loan->status);
        }
        return response()->json($loans, 200);
    }

    public function getLoan($profile_id)
    {
        $loan = DB::select(DB::raw("
        SELECT * FROM tbl_loans loan
        WHERE loan.profile_id = :profileId"), array("profileId" => $profile_id));
        foreach ($loan as $l) {
            $l->status_str = LoanStatus::getStatusStr($l->status);
        }
        return response()->json($loan, 200);
    }

    public function getLoanById($loan_id)
    {
        $loan = DB::select(DB::raw("
        SELECT loan.*, 
            profile.first_name, profile.middle_name, 
            profile.last_name, profile.contact_num, 
            profile.address, profile.credit_limit
        FROM tbl_loans loan
        INNER JOIN tbl_profiles profile ON loan.profile_id = profile.id
        WHERE loan.id = :loan_id"), array("loan_id" => $loan_id));
        foreach ($loan as $l) {
            $l->status_str = LoanStatus::getStatusStr($l->status);
        }
        return response()->json($loan[0], 200);
    }

    public function getLoanByStatus($status)
    {
        $loan = DB::select(DB::raw("
        SELECT loan.*, 
            profile.first_name, profile.middle_name, 
            profile.last_name, profile.contact_num, 
            profile.address, profile.credit_limit
        FROM tbl_loans loan
        INNER JOIN tbl_profiles profile ON loan.profile_id = profile.id
        WHERE loan.status = :loan_id"), array("loan_id" => $status));
        foreach ($loan as $l) {
            $l->status_str = LoanStatus::getStatusStr($l->status);
        }
        return response()->json($loan, 200);
    }

    public function getLoanItems($loan_id)
    {
        $loanitems = DB::select(DB::raw(
            "SELECT DISTINCT t2.item_name, t2.price, t1.interest, t2.id AS 'item_id', t2.description, t1.item_status
                    FROM tbl_loan_items t1
                           INNER JOIN
                             ( SELECT pr.price, pitm.item_name, pitm.id, pitm.description
                               FROM tbl_product_price pr
                                      INNER JOIN tbl_product_item pitm ON pr.id = pitm.price_id )
                                 t2 ON t1.item_id = t2.id
                    WHERE t1.loan_id = :loanid"), array('loanid' => $loan_id));
        foreach ($loanitems as $l) {
            $l->status_str = StatusEnum::getStatusStr($l->item_status);
        }
//        var_dump($loanitems);
        /*$categories = \DB::table('dental_support_categories AS c')
            ->select(\DB::raw('c.*, (SELECT COUNT(t.id) FROM dental_support_tickets t WHERE t.category_id=c.id AND t.status=0) AS num_tickets, (SELECT COUNT(a.id) FROM dental_support_category_admin a WHERE a.category_id=c.id) AS num_admins'))
            ->where('c.status', 0)
            ->orderBy('c.title')
            ->get();*/
        return response()->json($loanitems, 200);
    }

    //POST loan/loan
    public function storeLoan(Request $request)
    {
        //preloop for amortization computation
        /*$totalValue = 0;
        $amortztn = 0;
        foreach ($request->get("loan_items") as $item) {
            $pI = ProductItem::where('id', $item["item_id"])->first();
            $pR = Price::where('id', $pI->price_id)->first();
            $totalValue += ($pR->price * (1 + $item["interest"]));
        }*/

        /*$timeNow = time();
        $future = strtotime('+' . $request["term_length"] . ' months', time());

        $datediff = $future - $timeNow;
        $daysDiff = round($datediff / (60 * 60 * 24));

        $amortztn = $totalValue / $daysDiff;*/
//        $amortztn_m = $totalValue / $request["term_length"];

//        dd($amortztn, $totalValue, $daysDiff);

        //check first if still below credit limit
        $prevAmortizationTotal = $this->getTotalExistingLoanAmortization($request->get("profile_id"));
        $valueWithExisting = $prevAmortizationTotal + $request["amortization"];
        $account = Profile::where("id", $request->get("profile_id"))->first();
        if ($account->credit_limit < $valueWithExisting) {
            return response()->json([
                "requested_amortization" => $request["amortization"],
                "prev_total_amortization" => $prevAmortizationTotal,
                "credit_limit" => $account->credit_limit,
                "message" => "Exceeds Credit Limit"
            ], 406);
        }

        $loanItems = Array();
        $newLoan = Loan::create([
            "profile_id" => $request->get("profile_id"),
            "term_length" => $request["term_length"],
            "loan_value" => $request["loan_value"],
            "amortization" => round($request["amortization"], 2), //daily amortization
            "remaining_balance" => round($request["loan_value"], 2),
            "status" => $request->get("status")]);

        $loanItems["account"] = $newLoan;

        foreach ($request->get("loan_items") as $item) {
            $loanItems["items"][] = LoanItem::create([
                "loan_id" => $newLoan->id,
                "item_id" => $item["item_id"],
                "item_status" => $item["item_status"],
                "interest" => $item["interest"]
            ]);

            $productItem = ProductItem::where('id', $item["item_id"])->first();
            $productItem["stock_count"] = $productItem["stock_count"] - 1;
            $productItem->update();
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
        $request["term_length"] = $product->term_length;
        $request["amortization"] = $product->amortization;
        $request["running_balance"] = $product->running_balance;
        $product->update($request->all());
        return response()->json($product, 200);
    }

    public function deleteLoan($id)
    {
        Loan::where('id', $id)->delete();
        return $this->returnSuccess();
    }
}
