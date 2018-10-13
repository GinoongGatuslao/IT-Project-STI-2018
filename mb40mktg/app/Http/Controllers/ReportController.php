<?php

namespace App\Http\Controllers;

use App\Transaction;
use Illuminate\Http\Request;
use DateTime;

class ReportController extends Controller
{
    public function getTransactionReportWeekly(Request $request)
    {
        $transactions = Transaction::all();
        $filtered = Array();
        foreach ($transactions as $transaction) {
            $date = $transaction->created_at;
            $d = new DateTime($date);
            $week = $d->format("W");
            if ($request->header("week") == $week) {
                array_push($filtered, $transaction);
            }
        }

        return $filtered;
    }

    public function getTransactionReportDaily(Request $request)
    {
        $transactions = Transaction::all();
        $filtered = Array();
        foreach ($transactions as $transaction) {
            $date = $transaction->created_at;
            $d = new DateTime($date);
            $day = $d->format("m/d/Y");
            if ($request->header("day") == $day) {
                array_push($filtered, $transaction);
            }
        }

        return $filtered;
    }

    public function getTransactionReportMonthly(Request $request)
    {
        $transactions = Transaction::all();
        $filtered = Array();
        foreach ($transactions as $transaction) {
            $date = $transaction->created_at;
            $xdate = new DateTime(now());
            $c_year = $xdate->format("Y");
            $d = new DateTime($date);
            $month = $d->format("m");
            $requestMonth = $request->header("month");
            if ($requestMonth < 10) {
                $requestMonth = "0" . $requestMonth;
            }
//            echo($c_year . "/" . $requestMonth) . "\n";
//            echo($c_year . "/" . $month) . "\n";
            if (($c_year . "/" . $requestMonth) == ($c_year . "/" . $month)) {
                array_push($filtered, $transaction);
            }
//            echo $month . "\n";
        }

        return $filtered;
    }
}
