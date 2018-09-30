<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;


class CreateTableForLoans extends Migration
{
    protected $tableName = 'tbl_loans';
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        if (!Schema::hasTable($this->tableName)) {
            Schema::create($this->tableName, function (Blueprint $table) {
                // primary key
                $table->increments('id');
                //profile_id of the client
                $table->integer('profile_id');
                //length of amortization
                $table->integer('term_length'); // 30, 90, 180
                //loan value of current loan record
                $table->float('loan_value');
                //daily amortization value - this is calculated upon entry and approval
                $table->float('amortization');
                //if this loan is active OR inactive on client's account
                $table->boolean('status');//active / inactive
                $table->timestamps();
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
        Schema::dropIfExists($this->tableName);
    }
}
