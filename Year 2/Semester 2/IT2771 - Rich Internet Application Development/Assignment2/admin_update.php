<?php require_once("includes/connection.php"); ?>
<?php require_once("includes/functions.php"); ?>
<?php
session_start();

if (!(isset($_SESSION['login']) && $_SESSION['role'] != ''))
{
	header ("Location: login.php");
}
elseif (isset($_SESSION['login']) && ($_SESSION['role']!="admin"))
{
	header ("Location: access_denied.php");
}
?>
<!DOCTYPE HTML>
<!--
	Landed by HTML5 UP
	html5up.net | @ajlkn
	Free for personal and commercial use under the CCA 3.0 license (html5up.net/license)
-->
<html>
	<head>
		<title>Administration</title>
		<meta charset="utf-8" />
		<meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
		<link rel="stylesheet" href="assets/css/main.css" />
		<noscript><link rel="stylesheet" href="assets/css/noscript.css" /></noscript>
	</head>
	<body class="is-preload">
		<div id="page-wrapper">

			<!-- Header -->
				<header id="header">
					<h1 id="logo"><a href="index.php">Hexhosting</a></h1>
					<nav id="nav">
						<ul>
							<li><a href="index.php">Home</a></li>
                            <li><a href="admin.php">Administration</a></li>
                            <li><a href="hosting.php">Hosting</a></li>
                            <li><a href="stats.php">Statistics</a></li>
                            <li>
								<?php
									if(isset($_SESSION['login']) && $_SESSION['role']!="")
									{
										// do not display
									}
									else
									{
										echo("<a href='registration.php' class='button primary'>Sign Up</a>");
									}
								?>
							</li>
                            <li>
							    <?php
									if(isset($_SESSION['login']) && $_SESSION['role']!="")
									{
										echo"<a href='logout.php' class='button primary'>Logout ".$_SESSION['login']."</a>";
									}
									else
									{
										echo("<a href='login.php' class='button primary'>Login</a>");
									}
								?>
							</li>
						</ul>
					</nav>
				</header>

			<!-- Main -->
				<div id="main" class="wrapper style1">
					<div class="container">
						<header class="major">
							<h2>Administration</h2>
							<p>Accounts Management</p>
						</header>

						<!-- Content -->
							<section id="content">
            
            <form action="admin_process_update.php" method="post" style="margin: auto; width: 70%;">
                <h2>Update account details</h2>
                <?php
                $selid = $_POST['fselect'];

                $result = mysqli_query($connection, "SELECT * FROM accounts_registered WHERE Id = '".$selid."'");

                echo "<table border='1'><tr><th>Field</th><th>Value</th></tr>";

while($row = mysqli_fetch_array($result))
 {
echo "<tr><td> Id</td><td><input type='text' name='uId' readonly='readonly' value='" . $row['Id'] . "'></td></tr>";
echo "<tr><td> Name </td><td><input type='text' name='uName' value='". $row['Name'] . "'></td></tr>";
echo "<tr><td> Age </td><td><input type='text' name='uAge' pattern='[0-9]*' value='". $row['Age'] . "'></td></tr>";
echo "<tr><td> Contact </td><td><input type='text' name='uContact' pattern='[0-9]*' value='". $row['Contact'] . "'></td></tr>";
echo "<tr><td> Email </td><td><input type='email' name='uEmail' value='". $row['Email'] . "'></td></tr>";
		
$fgender = $row['Gender'];
 if ($fgender == "Male") 
{
echo '<tr><td> Gender </td><td><select name="uGender" required>';
echo '<option value="Male" selected>Male</option>';
echo '<option value="Female">Female</option></select></td></tr>';
 }
 else
{
echo '<tr><td> Gender </td><td><select name="uGender" required>';
echo '<option value="Male">Male</option>';
echo '<option value="Female" selected>Female</option></select></td></tr>';
 }
		  
echo "<tr><td> Country </td><td><input type='text' name='uCountry' value='". $row['Country'] . "'></td></tr>";
echo "<tr><td> Address </td><td><input type='text' name='uAddress' value='". $row['Address'] . "'></td></tr>";
echo "<tr><td> City </td><td><input type='text' name='uCity' value='". $row['City'] . "'></td></tr>";
echo "<tr><td> Postal Code </td><td><input type='text' name='uZip' pattern='[0-9]*' value='". $row['Zip_Code'] . "'></td></tr>";
  }
echo "</table>";
mysqli_close($connection);
                ?>
                <br>
              	<select name="fAction" required>
                    <option value="" selected disabled hidden>Select</option>
                    <option value="Update">Update</option>
                    <option value="Delete">Delete</option>
                </select>
            <br>
            <input name="submit" id="submit" value="Update" type="submit">   
            </form>
							</section>

					</div>
				</div>

			<!-- Footer -->
				<footer id="footer">
					<ul class="icons">
						<li><a href="#" class="icon brands alt fa-twitter"><span class="label">Twitter</span></a></li>
						<li><a href="#" class="icon brands alt fa-facebook-f"><span class="label">Facebook</span></a></li>
						<li><a href="#" class="icon brands alt fa-linkedin-in"><span class="label">LinkedIn</span></a></li>
						<li><a href="#" class="icon brands alt fa-instagram"><span class="label">Instagram</span></a></li>
						<li><a href="#" class="icon brands alt fa-github"><span class="label">GitHub</span></a></li>
						<li><a href="#" class="icon solid alt fa-envelope"><span class="label">Email</span></a></li>
					</ul>
					<ul class="copyright">
						<li>&copy; Hexhosting. All rights reserved.</li>
					</ul>
				</footer>

		</div>

		<!-- Scripts -->
			<script src="assets/js/jquery.min.js"></script>
			<script src="assets/js/jquery.scrolly.min.js"></script>
			<script src="assets/js/jquery.dropotron.min.js"></script>
			<script src="assets/js/jquery.scrollex.min.js"></script>
			<script src="assets/js/browser.min.js"></script>
			<script src="assets/js/breakpoints.min.js"></script>
			<script src="assets/js/util.js"></script>
			<script src="assets/js/main.js"></script>

	</body>
</html>