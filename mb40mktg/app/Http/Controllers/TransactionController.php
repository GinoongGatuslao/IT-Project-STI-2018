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
        return Transaction::all();
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
                "SELECT t.*, p.first_name AS 'c_fname', p.middle_name AS 'c_mname', p.last_name AS 'c_lname'
                        FROM tbl_transactions t
                        INNER JOIN tbl_profiles p ON t.collector_id = p.id
                        WHERE t.profile_id = :profileId AND t.loan_id = :loanId"), array(
            'profileId' => $request->header("profile_id"),
            'loanId' => $request->header("loan_id")));
        return response()->json($transaction, 200);
    }

    public function getTransactionByCollector($id)
    {
        $transaction = Transaction::where('collector_id', $id);
        return response()->json($transaction->get(), 200);
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

        $transaction = Transaction::whereBetween('payment', [$min, $max]);
        return response()->json($transaction->get(), 200);
    }

    private function updatePaymentBalance($newTransaction)
    {
        $loan = Loan::findOrFail($newTransaction->loan_id);
        $deltaTotal = $loan->running_balance + $newTransaction->payment;
        if ($deltaTotal >= $loan->loan_value) {
            $loanstatus = 2;
        } else {
            $loanstatus = 1;
        }
        $loan->update([
            "status" => $loanstatus,
            "running_balance" => $deltaTotal
        ]);
    }
}
