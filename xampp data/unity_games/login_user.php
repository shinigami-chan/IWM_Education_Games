<?php
	include 'postgres_connect.php';
	
	function login_user($username, $password) {
		// Connect to the database
		$connection = db_connect();
		
		if ($username == "") return false;
		if ($password == "") return false;
		
		$query = "SELECT password FROM user_table WHERE username='$username';";
			
			echo $query."<br>";

		$user_password = $connection->query($query);
		$user_password = $user_password->fetch(PDO::FETCH_NUM)[0];
		
		echo $user_password."<br>";
		
		return $password == $user_password;
	}
	
			
	if ($_GET) {
		$username = $_GET['username'];
		$password = $_GET['password'];
	} else {
		$username = $argv[1];
		$password = $argv[2];
	}
	
	$result = login_user($username, $password);

	if (!$result) {
		echo "Verification failed! <br>";
		echo "RETURN:0";
		return "";
	} 
	echo "RETURN:1";
	return "success";
?>