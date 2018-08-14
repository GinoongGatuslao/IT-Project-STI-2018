<?php

use App\Product;
use Illuminate\Database\Seeder;

class ProductsTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        // Let's truncate our existing records to start from scratch.
        Product::truncate();

        $faker = \Faker\Factory::create();

        // And now, let's create a few articles in our database:
        for ($i = 0; $i < 50; $i++) {
            Product::create([
                // 'title' => $faker->sentence,
                // 'body' => $faker->paragraph,
				'name' => $faker->domainWord,
				'price' => $faker->numberBetween($min = 500, $max = 10000),
				'reorder_point' => $faker->randomElement($array = array('1','2','3')),
                'count' => $faker->numberBetween($min = 1, $max = 50),
				'status' => $faker->randomElement($array = array ('1','2','3')),
				'batch_number' => $faker->creditCardNumber, //this is just a dummy representation of random large number combination
				'returned' => $faker->boolean($chanceOfGettingTrue = 15),
				'defective' => $faker->boolean($chanceOfGettingTrue = 5)
            ]);
        }
    }
}
