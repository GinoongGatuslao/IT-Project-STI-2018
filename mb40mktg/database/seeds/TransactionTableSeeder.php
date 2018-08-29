<?php

use Illuminate\Database\Seeder;
use App\Transaction;

class TransactionTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        Transaction::truncate();

        $faker = \Faker\Factory::create();

        for ($i = 0; $i < 20; $i++) {
            Transaction::create([
                'account_id' => $faker->numberBetween(1, 10),
                'loan_id' => $faker->numberBetween(1, 10),
                'collector_id' => $faker->numberBetween(1, 10),
                'payment' => $faker->randomElement(array(1399, 1299, 2500, 995))
            ]);
        }
    }
}
