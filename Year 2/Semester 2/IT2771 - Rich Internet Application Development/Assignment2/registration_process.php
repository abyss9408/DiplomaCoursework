<?php require_once("includes/connection.php"); ?>
<?php require_once("includes/functions.php"); ?>
<?php
$username = $_POST["username"];
$password_string = $_POST["password"];
$password_hash = password_hash($password_string, PASSWORD_BCRYPT);
$vkey = md5(time() . $username);
$email = $_POST["pEmail"];
$sql="INSERT INTO accounts_registered (Username, Password, vkey, Name, Age, Contact, Email, Gender, Country, Address, City, ZIP_Code)
VALUES ('$username','$password_hash','$vkey','$_POST[pName]','$_POST[pAge]','$_POST[pPhone]','$email','$_POST[pGender]', '$_POST[pCountry]', '$_POST[pAddress]', '$_POST[pCity]', '$_POST[pZip]')";

//executes query
if (!mysqli_query($connection, $sql))
{
  die('Error: ' . mysqli_error($connection));
}
else
{
      
    $to = $email;
    $subject = "Email Verification";
    $message = "Activate Account: http://abyss.epizy.com/verify.php?vkey=$vkey";
    $headers = "From: noreply@abyss.epizy.com \r\n";
    $headers .= "MIME-Version: 1.0" . "\r\n";
    $headers .= "Context-type:text/html;charset=UTF-8" . "\r\n";

    mail($to, $subject, $message, $headers);

    $location = "registration_successful.php";
    header("Location: {$location}");
    echo "1 record added" . $sql;
}  

//close connection
mysqli_close($connection);
?>