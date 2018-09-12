<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class EditOnProductItemTable extends Migration
{
    protected $tableName = "tbl_product_item";
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        if (Schema::hasTable($this->tableName)) {
            Schema::table($this->tableName, function (Blueprint $table) {
                $table->dropColumn("status");
                $table->dropColumn("batch_number_id");
                $table->dropColumn("created_at");
                $table->dropColumn("updated_at");
                $table->text("description");
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
                $table->text("status");
                $table->integer("batch_number_id");
                $table->timestamps();
                $table->dropColumn("description");
            });
        }
    }
}
