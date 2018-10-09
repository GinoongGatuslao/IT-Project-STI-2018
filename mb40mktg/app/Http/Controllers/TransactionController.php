<?php

namespace App\Http\Controllers;

use App\Transaction;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;
use App\Loan;
use App\Http\Controllers\LoanController;

class TransactionController extends Controller
{
    public function getAll()
    {
        $transactions = DB::select(DB::raw("
        SELECT
          trans.*,
          loan.remaining_balance AS 'loan_rem',
          loan.loan_value,
          p.first_name,
          p.middle_name,
          p.last_name,
          c.first_name AS 'c_fname',
          c.middle_name AS 'c_mname',
          c.last_name AS 'c_lname'
        FROM tbl_transactions trans
        INNER JOIN tbl_loans loan ON loan.id = trans.loan_id
        INNER JOIN tbl_profiles p ON p.id = trans.profile_id
        INNER JOIN (SELECT * FROM tbl_profiles) c ON trans.collector_id = c.user_id"));
        return response()->json($transactions, 200);
    }

    public function storeTransaction(Request $request)
    {
        $newTransaction = Transaction::create($request->all());
        $this->updatePaymentBalance($newTransaction);
        return response()->json($newTransaction, 200);
    }

    public function findId($id)
    {
        $transaction = Transaction::where('id', $id);
        return response()->json($transaction->get(), 200);
    }

    public function getTransactionRecordsByProfileAndLoan(Request $request)
    {
        $transaction = DB::select(
            DB::raw(
                "
                        SELECT
                          trans.*,
                          loan.remaining_balance AS 'loan_rem',
                          loan.loan_value,
                          p.first_name,
                          p.middle_name,
                          p.last_name,
                          c.first_name AS 'c_fname',
                          c.middle_name AS 'c_mname',
                          c.last_name AS 'c_lname'
                        FROM tbl_transactions trans
                        INNER JOIN tbl_loans loan ON loan.id = trans.loan_id
                        INNER JOIN tbl_profiles p ON p.id = trans.profile_id
                        INNER JOIN (SELECT * FROM tbl_profiles) c ON trans.collector_id = c.user_id
                        WHERE trans.profile_id = :profileId AND trans.loan_id = :loanId"), array(
            'profileId' => $request->header("profile_id"),
            'loanId' => $request->header("loan_id")));
        return response()->json($transaction, 200);
    }

    public function getTransactionByCollector($id)
    {
        $transaction = DB::select(DB::raw("
        SELECT
          trans.*,
          loan.remaining_balance AS 'loan_rem',
          loan.loan_value,
          p.first_name,
          p.middle_name,
          p.last_name,
          c.first_name AS 'c_fname',
          c.middle_name AS 'c_mname',
          c.last_name AS 'c_lname'
        FROM tbl_transactions trans
        INNER JOIN tbl_loans loan ON loan.id = trans.loan_id
        INNER JOIN tbl_profiles p ON p.id = trans.profile_id
        INNER JOIN (SELECT * FROM tbl_profiles) c ON trans.collector_id = c.user_id
        WHERE trans.collector_id = " . $id));
        return response()->json($transaction, 200);
    }

    public function getTransactionByLoan($id)
    {
        $transaction = Transaction::where('loan_id', $id);
        return response()->json($transaction->get(), 200);
    }

    public function getTransactionByPaymentRange(Request $request)
    {
        $min = $request->header('min');
        $max = $request->header('max');

//        $transaction = Transaction::whereBetween('payment', [$min, $max]);
        $transaction = DB::select(DB::raw("
        SELECT
          trans.*,
          loan.remaining_balance AS 'loan_rem',
          loan.loan_value,
          p.first_name,
          p.middle_name,
          p.last_name,
          c.first_name AS 'c_fname',
          c.middle_name AS 'c_mname',
          c.last_name AS 'c_lname'
        FROM tbl_transactions trans
        INNER JOIN tbl_loans loan ON loan.id = trans.loan_id
        INNER JOIN tbl_profiles p ON p.id = trans.profile_id
        INNER JOIN (SELECT * FROM tbl_profiles) c ON trans.collector_id = c.user_id
        WHERE trans.payment BETWEEN " . $min . " AND " . $max . "
        "));
        return response()->json($transaction, 200);
    }

    private function updatePaymentBalance($newTransaction)
    {
        $transaction = Transaction::findOrFail($newTransaction->id);
        $loan = Loan::findOrFail($newTransaction->loan_id);
        $payRemain = $loan->remaining_balance - $transaction->payment;
        if ($payRemain <= 0) {
            $loanstatus = 2;
        } else {
            $loanstatus = 1;
        }
        $loan->update([
            "status" => $loanstatus,
            "remaining_balance" => $payRemain
        ]);
        $transaction->update([
            "remaining_balance" => $payRemain
        ]);
    }
}
