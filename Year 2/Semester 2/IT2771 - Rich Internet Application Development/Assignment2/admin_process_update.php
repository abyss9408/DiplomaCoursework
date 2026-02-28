<?php require_once("includes/connection.php"); ?>
<?php require_once("includes/functions.php"); ?>
<?php
$action = $_POST['fAction'];

if ($action == 'Update')
{
    $sql="UPDATE accounts_registered SET
    Name='".$_POST[uName]."', Age='".$_POST[uAge]."', Contact='".$_POST[uContact]."', Email='".$_POST[uEmail]."', Gender='".$_POST[uGender]."', Country='".$_POST[uCountry]."',Address='".$_POST[uAddress]."', City='".$_POST[uCity]."', Zip_Code='".$_POST[uZip]."' WHERE 
    Id='".$_POST[uId]."'";
}
else
{
    $sql="DELETE FROM accounts_registered WHERE 
    Id='".$_POST[uId]."'";
}
 echo $sql;
if (!mysqli_query($connection,$sql))
  {
  die('Error: ' . mysqli_error());
  }
  else
  {
   $location = "admin.php";
  header("Location: {$location}");
  echo "1 record ".$action;
  }
mysqli_close($connection);


?>