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
            <form action="admin_display.php" method="post" style="margin: auto; width: 70%;">

                <label>Search by</label>
                <select name="SearchOption" required>
                    <option value="" selected disabled hidden>Select</option>
                    <option value="All">All</option>
                    <option value="Name">Name</option>
                    <option value="Account_Type">Account Type</option>
                    <option value="Contact">Contact</option>
                </select>
                <br>
                <input type="text" style="width: 100%;" name="SearchString" placeholder="Leave if if 'All' option is selected">				
            <br>
            <input name="submit" id="submit" value="Search" type="submit">   
            </form>
            <br>
            <form action="admin_update.php" method="post" style="margin: auto; width: 70%;">

                <?php
                $result = mysqli_query($connection, "SELECT * FROM accounts_registered");

                echo 'Select ID <select name="fselect" required>';

                while ($row=mysqli_fetch_array($result))
                {
                    echo "<option value'".$row['Id']."'>".$row['Id']."</option>";
                }
                echo "</select>";
                ?>
                	
            <br>
            <input name="submit" id="submit" value="Proceed to Update" type="submit">   
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