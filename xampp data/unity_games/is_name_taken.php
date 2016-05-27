<?php
	include 'postgres_connect.php';
	
	function is_name_free($username) {
		// Connect to the database
		$connection = db_connect();
		
		$query = 'SELECT * FROM user_table WHERE username = \''.$username.'\'';

		return $connection->query($query);
	}
	
			
	if ($_GET) {
		$username = $_GET['username'];
	} else {
		$username = $argv[1];
	}
	
	$result = is_name_free($username);

	if (!$result) echo "INSERT failed!";
	
	return $result;
?>