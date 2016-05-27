<?php
	function db_connect() {
		static $dbh;
		
		// Try and connect to the database, if a connection has not been
		// established yet
		
		if(!isset($dbh)) {
			// Load configuration as an array. Use the actual location of your
			// configuration file
			$config = parse_ini_file('../../iwm_ulg_pgconfig.ini');
			$dbh = new PDO('pgsql:host=localhost user='.$config['username'].' dbname='.$config['dbname'].' password='.$config['password']);
		}
		
		// If connection was not successful, handle the error
		if($dbh === false) {
			// Handle error - notify administrator, log to a file, show an error
			// screen etc.. 
			echo "An error occurred when connecting to the database.<br>";
		} else echo "Connection was successful!<br>";

		return $dbh;
	}
?>