<?php
	include 'postgres_connect.php';
	
	function is_name_free($username) {
		// Connect to the database
		$connection = db_connect();
		
		$query = 'SELECT * FROM mathgames.user WHERE user_name = \''.$username.'\';';
		echo $query;
		
		return $connection->query($query);
	}
	
			
	if ($_GET) {
		$username = $_GET['username'];
	} else {
		$username = $argv[1];
	}
	
	$result = is_name_free($username);

	$array = $result->fetchAll();
	
	if (!empty($array)) {
		echo 'taken';
	} else
		echo 'not taken';
	
	
	return $result;
?>