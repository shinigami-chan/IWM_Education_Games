<?php
	// Load configuration as an array. Use the actual location of your
	// configuration file
	$config = parse_ini_file('../../iwm_ulg_dbconfig.ini');

	// Try and connect to the database
	$connection = mysqli_connect(
			'localhost',$config['username'],
			$config['password'], $config['dbname']);
	
	// If connection was not successful, handle the error
	if($connection === false) {
		// Handle error - notify administrator, log to a file, show an error
		// screen etc.. 
		echo "An error occurred when connecting to the database.<br>";
	} else echo "Connection was successful!"
?>