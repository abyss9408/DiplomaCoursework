<?php require_once("includes/connection.php"); ?>
<?php require_once("includes/functions.php"); ?>
<!DOCTYPE HTML>
<!--
	Landed by HTML5 UP
	html5up.net | @ajlkn
	Free for personal and commercial use under the CCA 3.0 license (html5up.net/license)
-->
<html>
	<head>
		<title>Account Activation</title>
		<meta charset="utf-8" />
		<meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
		<link rel="stylesheet" href="assets/css/main.css" />
		<noscript><link rel="stylesheet" href="assets/css/noscript.css" /></noscript>
		<link rel="stylesheet" href="assets/css/registration.css" />
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
                            <li><a href="registration.php" class="button primary">Sign Up</a></li>
                            <li><a href="login.php" class="button primary">Login</a></li>
						</ul>
					</nav>
				</header>

			<!-- Main -->
				<div id="main" class="wrapper style1">
					<div class="container">
						<header class="major">
							<h2>Account Activation</h2>
							<?php
                            if (isset($_GET['vkey'])){
                                $vkey = $_GET['vkey'];
                                $result = mysqli_query($connection, "SELECT active, vkey FROM accounts_registered WHERE active=0 AND vkey='$vkey'");

                                if (mysqli_num_rows($result) == 1)
                                {
                                    $update = mysqli_query($connection, "UPDATE accounts_registered SET active = 1 WHERE vkey='$vkey'");

                                    if($update)
                                    {
                                        echo '<p>Your account has been activated. <br><a href="login.php">Proceed to Login</a></p>';
                                    }
                                    else{
                                        echo $mysqli->error;
                                    }
                                }else{
                                    echo '<p>This account is invalid or already activated</p>';
                                }
                            }
                            else
                            {
                                die("Something went wrong");
                            }
                            ?>                       
						</header>

						<!-- Content -->
							<section id="content">

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