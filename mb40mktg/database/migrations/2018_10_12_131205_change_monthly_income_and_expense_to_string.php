<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class ChangeMonthlyIncomeAndExpenseToString extends Migration
{
    protected $tableName = "tbl_profiles";
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        if (Schema::hasTable($this->tableName)) {
            Schema::table($this->tableName, function (Blueprint $table) {
                $table->dropColumn("mo_income");
                $table->dropColumn("mo_expense");
            });
            Schema::table($this->tableName, function (Blueprint $table) {
                $table->text("mo_income")->nullable();
                $table->text("mo_expense")->nullable();
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
                $table->double("mo_income")->default(0)->change();
//                $table->dropColumn("mo_income");
                $table->double("mo_expense")->default(0)->change();
//                $table->dropColumn("mo_expense");
            });
        }
    }
}
