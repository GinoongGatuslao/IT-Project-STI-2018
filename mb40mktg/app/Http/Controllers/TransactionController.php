<?php

namespace App\Http\Controllers;

use App\Transaction;
use Illuminate\Http\Request;

class TransactionController extends Controller
{
    public function getAll()
    {
        return Transaction::all();
    }

    public function storeTransaction(Request $request) {
        $newTransaction = Transaction::create($request->all());
        return response()->json($newTransaction, 200);
    }

    public function findId($id)
    {
        $transaction = Transaction::where('id', $id);
        return response()->json($transaction->get(), 200);
    }

    public function getTransactionByAccount($id)
    {
        $transaction = Transaction::where('account_id', $id);
        return response()->json($transaction->get(), 200);
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
}
