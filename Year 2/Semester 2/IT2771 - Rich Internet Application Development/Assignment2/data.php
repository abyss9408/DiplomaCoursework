<?php require_once("includes/connection.php"); ?>
<?php require_once("includes/functions.php"); ?>
<?php

if (!$connection) {
  die('Could not connect: ' . mysqli_error());
}


$query = mysqli_query($connection, "SELECT plan_name, plan_users FROM hosting_plans");

$category = array();
$category['name'] = 'Plan Name';

$series1 = array();
$series1['name'] = 'Users';


while($r = mysqli_fetch_array($query)) {
    $category['data'][] = $r['plan_name'];
    $series1['data'][] = $r['plan_users'];
}

$result = array();
array_push($result,$category);
array_push($result,$series1);

print json_encode($result, JSON_NUMERIC_CHECK);

mysqli_close($connection);
?>
