<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class AlterProfileTableMergeAccountsTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        if (Schema::hasTable("tbl_profiles")) {
            Schema::table("tbl_profiles", function (Blueprint $table) {
                $table->integer("credit_limit")->after("verified");
                $table->integer("account_status")->after("credit_limit");
            });
        }

        if (Schema::hasTable("tbl_accounts")) {
            Schema::dropIfExists("tbl_accounts");
        }
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        if (Schema::hasTable("tbl_profiles")) {
            Schema::table("tbl_profiles", function (Blueprint $table) {
                $table->dropColumn("credit_limit");
                $table->dropColumn("account_status");
            });
        }

        Schema::create("tbl_accounts", function (Blueprint $table) {
            $table->increments('id');
            $table->integer('profile_id');
            $table->integer('credit_limit')->default('0');
            $table->integer('status')->default('0'); //pending / confirmed
            $table->timestamps();
        });
    }
}
