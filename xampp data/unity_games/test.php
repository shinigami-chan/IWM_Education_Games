<?php
	//include 'register_user.php';

	function replace_umlaut($string) {
		$string = str_replace('ae', '\' || U&\'00E4\' || \'', $string);
		$string = str_replace('oe', '\' || U&\'00F6\' || \'', $string);
		$string = str_replace('ue', '\' || U&\'00FC\' || \'', $string);
		return $string;
	}
	
	$state = 'Baden-Wuerttemberg';

	$pos = strpos($state, 'ue');
	if (!($pos === false)) {			
		$state = replace_umlaut($state);
	}
	echo 'SELECT name FROM mathgames.state WHERE state = \''.$state.'\';';
?>