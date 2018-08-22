<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateTableForAccounts extends Migration
{
    protected $tableName = "tbl_accounts";
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        if (!Schema::hasTable($this->tableName)) {
            Schema::create($this->tableName, function (Blueprint $table) {
                $table->increments('id');
                $table->text('first_name');
                $table->text('middle_name');
                $table->text('last_name');
                $table->text('address');
                $table->text('contact_num');
                $table->text('bday');
                $table->integer('user_type');
                $table->text('occupation');
                $table->text('mo_income');
                $table->text('mo_expense');
                $table->integer('credit_limit')->default('0');
                $table->text('path_id_pic');
                $table->text('path_house_sketch_pic');
                $table->boolean('verified')->default(false); // true or false
                $table->integer('status')->default('0'); //pending / confirmed
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
