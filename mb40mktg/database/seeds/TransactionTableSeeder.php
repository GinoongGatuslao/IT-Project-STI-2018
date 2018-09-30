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
            $accntId = $faker->numberBetween(1, 10);
            $loanId = $faker->unique()->numberBetween(1, 20);
            $fakeLoanPayments = $faker->numberBetween(1, 10);
            $paymentMade = $faker->randomElement(array(1399, 1299, 2500, 995));
            for ($x = 0; $x < $fakeLoanPayments; $x++) {
                Transaction::create([
                    'profile_id' => $accntId,
                    'loan_id' => $loanId,
                    'collector_id' => $faker->numberBetween(1, 10),
                    'payment' => $paymentMade
                ]);
            }
        }
    }
}
