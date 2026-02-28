<?php require_once("includes/connection.php"); ?>
<?php require_once("includes/functions.php"); ?>
<?php 
if ($_SERVER['REQUEST_METHOD'] == 'POST') {
$uname = $_POST['username']; //retrieve input from previous pagelogin.php store in $uname
$pword = $_POST['password']; //retrieve input from previous pagelogin.php store in $pword
 
}

$SQL = "SELECT * FROM accounts_registered WHERE Username='".$uname."'"; //construct sql query see (b)
echo $SQL;
$result = mysqli_query($connection, $SQL);
$row = mysqli_fetch_array($result);
$num_rows = mysqli_num_rows($result);
echo "number of rows".$num_rows;

if ($num_rows > 0 && password_verify($pword, $row['Password']) && $row['Active'] == '1') {
session_start();            //start a new session
$_SESSION['login'] = $uname;  //create a new session variable named: login,  and pass the username to it

$roletype = $row['Account_Type']; //extract out the usertype for the found record

$_SESSION['role'] = $roletype; //create a new session variable: role,  and pass the roletype to it
 
if($roletype=="admin")             // check if $roletype == "admin"  
{header ("Location: admin.php");} //redirect to admin.php
else
{ header ("Location: index.php");}     //else non administrators redirect to index.php
}   //end if
else {
 // user not found or invalid login
header ("Location: login_error.php"); // user not found, redirect to login_error.php
}

mysqli_close($connection);

?>