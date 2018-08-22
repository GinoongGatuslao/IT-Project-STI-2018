<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateTableForProductBatch extends Migration
{
    protected $tableName = "tbl_product_batch";
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        if(!Schema::hasTable($this->tableName)){
            Schema::create($this->tableName, function (Blueprint $table) {
                $table->increments('id');
                $table->text('batch_name');
                $table->text('batch_number');
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
