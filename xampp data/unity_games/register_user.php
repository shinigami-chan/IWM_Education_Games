<?php
	include 'postgres_connect.php';
	
	function register_user($username, $password, $sex, $age, $school, $state, $mothertongue) {
		// Connect to the database
		$connection = db_connect();
		
		$insert = 'INSERT INTO user_table ';
		
		$fields = '(username, password';
		$values = 'VALUES (';
		if ($password == '') return false;
		elseif ($password == '') return false;
		else {
			$values .= '\''.$username.'\', \''.$password.'\'';
			if ($sex != '') {
				$fields .= ', sex';
				$values .= ', \''.$sex.'\'';
			}
			if ($age != '') {
				$fields .= ', age';
				$values .= ', '.$age.'';
			}
			if ($school != '') {
				$fields .= ', school';
				$values .= ', \''.$school.'\'';
			}
			if ($state != '') { 
				$fields .= ', state';
				$values .= ', \''.$state.'\'';
			}
			if ($mothertongue != '') {
				$fields .= ', mothertongue';
				$values .= ', \''.$mothertongue.'\'';
			}
			$fields .= ') ';
			$values .= ');';
			
			$query = $insert.$fields.$values;
			
			echo $query."<br>";
		}
		return $connection->query($query);
	}
	
			
	if ($_GET) {
		$username = $_GET['username'];
		$password = $_GET['password'];
		$sex = $_GET['sex'];
		$age = $_GET['age'];
		$school = $_GET['school'];
		$state = $_GET['state'];
		$mothertongue = $_GET['mothertongue'];
	} else {
		$username = $argv[1];
		$password = $argv[2];
		$sex = $argv[3];
		$age = $argv[4];
		$school = $argv[5];
		$state = $argv[6];
		$mothertongue = $argv[7];
	}
	
	$result = register_user($username, $password, $sex, $age, $school, $state, $mothertongue);

	if (!$result) {
		echo "INSERT failed!";
		return "";
	} 
	
	return "success";
?>