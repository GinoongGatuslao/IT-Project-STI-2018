<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateTableForProfile extends Migration
{
    protected $tableName = "tbl_profiles";

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
                $table->integer('user_id');
                $table->text('first_name');
                $table->text('middle_name');
                $table->text('last_name');
                $table->text('address');
                $table->text('contact_num');
                $table->text('bday');
                $table->text('occupation');
                $table->integer('mo_income');
                $table->integer('mo_expense');
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
