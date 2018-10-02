<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class AddImagePathsOnProfilesTable extends Migration
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
                $table->text("path_id_pic")->after("mo_expense");
                $table->text("path_house_sketch_pic")->after("path_id_pic");
            });
        }
    }

    /**
     * Reverse the migrations
     *
     * @return void
     */
    public function down()
    {
        if (Schema::hasTable($this->tableName)) {
            Schema::table($this->tableName, function (Blueprint $table) {
                $table->dropColumn("path_id_pic");
                $table->dropColumn("path_house_sketch_pic");
            });
        }
    }
}
