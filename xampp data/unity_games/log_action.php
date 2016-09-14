<?php
	include 'postgres_connect.php';
	
	function log_game_selection($session_id, $game_id, $time_stamp) {
		// Connect to the database
		$connection = db_connect();
		
		$query = "INSERT INTO game_log (start_time, game_id, system_log_id) VALUES ('"$time_stamp"', '"$game_id"', '"$session_id"');";
			
			//echo $query."<br>";

		$log_game_selection = $connection->query($query);
		
		return $log_game_selection;
	}
	
			
	if ($_GET) {
		$session_id = $_GET['session_id'];
		$game_id = $_GET['game_id'];
		$time_stamp = $_GET['time_stamp'];
	} else {
		$session_id = $argv[1];
		$game_id = $argv[2];
		$time_stamp = $argv[3];
	}
	
	$result = log_game_selection($session_id, $game_id, $time_stamp);

	if (!$result) {
		echo "LOG:0";
		return "";
	} 
	echo "LOG:1";
	return "success";
?>