<?php
	$servername = "localhost";
	$username = "root";
	$password = "";
	$dbName = "users";
	
	if ($_GET) {
		$field = $_GET['field'];
	} else {
		$field = $argv[1];
	}
	
	echo $field;
	
	//Make Connection
	$conn = new mysqli($servername, $username, $password, $dbName);
	//Check Connection
	if (!$conn) {
		die("Connection Failed. ". mysqli_connect_error());
	}
	
	$sql = "SELECT * FROM userstable WHERE Username = '".$field."'";
	$result = mysqli_query($conn, $sql);
	
	if(mysqli_num_rows($result) > 0) {
		//show data for each row
		while ($row = mysqli_fetch_assoc($result)) {
			echo "ID: ".$row['ID'] . " Name & Password: |".$row['Username']. "|" .$row['Password'];
		}
	}
?>