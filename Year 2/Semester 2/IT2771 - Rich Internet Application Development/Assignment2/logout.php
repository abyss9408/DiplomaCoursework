<?php
session_start();
if(isset($_SESSION['login']))
{
	unset($_SESSION['login']);
	unset($_SESSION['role']);
    header("Location: index.php");
}
?>
