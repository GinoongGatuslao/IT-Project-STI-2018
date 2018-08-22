<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateTableForProductItem extends Migration
{
    protected $tableName = "tbl_product_item";
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
                $table->text('item_name');
                $table->integer('price_id');
                $table->text('status');
                $table->integer('stock_count');
                $table->integer('reorder_point');
                $table->integer('batch_number_id');
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
