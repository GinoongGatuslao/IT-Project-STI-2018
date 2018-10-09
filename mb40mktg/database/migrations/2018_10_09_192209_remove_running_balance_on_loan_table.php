<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class RemoveRunningBalanceOnLoanTable extends Migration
{
    protected $tableName = "tbl_loans";
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        if (Schema::hasTable($this->tableName)) {
            Schema::table($this->tableName, function (Blueprint $table) {
                $table->dropColumn("running_balance");
                $table->double("remaining_balance")->default(0)->after("amortization_m");
            });
        }
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        if (Schema::hasTable($this->tableName)) {
            Schema::table($this->tableName, function (Blueprint $table) {
                $table->double("running_balance")->default(0)->after("amortization_m");
                $table->dropColumn("remaining_balance");
            });
        }
    }
}
