<?php
	include 'postgres_connect.php';
	
	function replace_umlaut($string) {
		$string = str_replace('ae', '\' || U&\'\00E4\' || \'', $string);
		$string = str_replace('oe', '\' || U&\'\00F6\' || \'', $string);
		$string = str_replace('ue', '\' || U&\'\00FC\' || \'', $string);
		return $string;
	}
	
	function register_user($username, $password, $email, $sex, $age, $school, $state, $native_language) {
		// Connect to the database
		$connection = db_connect();
		
		$state = replace_umlaut($state);
		$school = replace_umlaut($school);
		
		echo 'school: '.$school.'<br>';
		
		$state_query = 'SELECT state_id FROM mathgames.state WHERE name = \''.$state.'\';';
		$school_query = 'SELECT school_type_id FROM mathgames.school_type WHERE name = \''.$school.'\';';
		echo $school_query.'<br>';
		
		$state_id = $connection->query($state_query)->fetchAll()[0][0];
		$school_id = $connection->query($school_query)->fetchAll()[0][0];
		echo 'school_id: '.$school_id.'<br>';
		
		$insert = 'INSERT INTO mathgames.user ';
		
		$fields = '(user_name, user_password';
		$values = 'VALUES (';
		if ($password == '') return false;
		elseif ($password == '') return false;
		else {
			$values .= '\''.$username.'\', \''.$password.'\'';
			if ($email != '') {
				$fields .= ',user_email';
				$values .= ', \''.$email.'\'';
			}
			if ($sex != '') {
				$fields .= ', sex';
				$values .= ', \''.$sex.'\'';
			}
			if ($age != '') {
				$fields .= ', age';
				$values .= ', '.$age.'';
			}
			if ($state != '') {
				$fields .= ', state_id';
				$values .= ', '.$state_id.'';
			}
			if ($school != '') {
				$fields .= ', school_type_id';
				$values .= ', '.$school_id.'';
			}
			if ($native_language != '') {
				$fields .= ', native_language_id';
				$values .= ', '.$native_language.'';
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
		$email = $_GET['email'];
		$sex = $_GET['sex'];
		$age = $_GET['age'];
		$school = $_GET['school'];
		$state = $_GET['state'];
		$mothertongue = $_GET['native_language'];
	} else {
		$username = $argv[1];
		$password = $argv[2];
		$email = $argv[3];
		$sex = $argv[3];
		$age = $argv[4];
		$school = $argv[5];
		$state = $argv[6];
		$mothertongue = $argv[7];
	}
	
	$result = register_user($username, $password, $email, $sex, $age, $school, $state, $mothertongue);

	if (!$result) {
		echo 'INSERT failed!';
		return '0';
	} 
	
	return "success";
?>