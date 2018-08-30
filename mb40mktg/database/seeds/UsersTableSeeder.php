<?php

use App\User;
use Illuminate\Database\Seeder;

class UsersTableSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        // Let's truncate our existing records to start from scratch.
        User::truncate();

        $faker = \Faker\Factory::create();

		$password = Hash::make('test1234');

        //custom user account
		User::create([
            // 'title' => $faker->sentence,
            // 'body' => $faker->paragraph,
			'first_name' => "John",
			'middle_name' => "Freakin",
			'last_name' => "doe",
			'username' => "root@root.com",
			'password' => $password
        ]);

        // And now, let's create a few users in our database:
        for ($i = 0; $i < 10; $i++) {
            User::create([
                // 'title' => $faker->sentence,
                // 'body' => $faker->paragraph,
                'first_name' => $faker->firstName($gender = 'male'|'female'),
                'middle_name' =>  $faker->lastName,
                'last_name' => $faker->lastName,
				'username' => $faker->email,
				'password' => $password
            ]);
        }
    }
}
