<?php require_once("includes/connection.php"); ?>
<?php require_once("includes/functions.php"); ?>
<?php
session_start();
?>
<!DOCTYPE HTML>
<!--
	Landed by HTML5 UP
	html5up.net | @ajlkn
	Free for personal and commercial use under the CCA 3.0 license (html5up.net/license)
-->
<html>
	<head>
		<title>Hosting Plans</title>
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
							<h2>Hosting Plans</h2>
							<p>Your website starts here.</p>
						</header>

						<!-- Content -->
							<section id="content">
                                <div class="row gtr-50 gtr-uniform">
								<div class="col-3 col-6-xsmall">                                    
                                    <?php
                                        $sql = "SELECT * FROM hosting_plans WHERE Id=1";
                                        $query_run = mysqli_query($connection, $sql); 
                                        $row = mysqli_fetch_array($query_run);
                                        
                                        echo '<span class="image fit">';
                                        echo '<img src="data:image/jpeg;base64,'.base64_encode($row['plan_image']).'" alt="" />';
                                        echo '</span>';
                                        echo '<p><strong>'.$row['plan_desc'].'</strong></p>';
                                    ?>
                                </div>
                                <div class="col-3 col-6-xsmall">
                                    <?php
                                        $sql = "SELECT * FROM hosting_plans WHERE Id=2";
                                        $query_run = mysqli_query($connection, $sql); 
                                        $row = mysqli_fetch_array($query_run);
                                        
                                        echo '<span class="image fit">';
                                        echo '<img src="data:image/jpeg;base64,'.base64_encode($row['plan_image']).'" alt="" />';
                                        echo '</span>';
                                        echo '<p><strong>'.$row['plan_desc'].'</strong></p>';
                                    ?>
                                </div>
								<div class="col-3 col-6-xsmall">
                                    <?php
                                        $sql = "SELECT * FROM hosting_plans WHERE Id=3";
                                        $query_run = mysqli_query($connection, $sql); 
                                        $row = mysqli_fetch_array($query_run);
                                        
                                        echo '<span class="image fit">';
                                        echo '<img src="data:image/jpeg;base64,'.base64_encode($row['plan_image']).'" alt="" />';
                                        echo '</span>';
                                        echo '<p><strong>'.$row['plan_desc'].'</strong></p>';
                                    ?>
                                </div>
                                <div class="col-3 col-6-xsmall">
                                <span class="image fit">
                                    <?php
                                        $sql = "SELECT * FROM hosting_plans WHERE Id=4";
                                        $query_run = mysqli_query($connection, $sql); 
                                        $row = mysqli_fetch_array($query_run);
                                        
                                        echo '<span class="image fit">';
                                        echo '<img src="data:image/jpeg;base64,'.base64_encode($row['plan_image']).'" alt="" />';
                                        echo '</span>';
                                        echo '<p><strong>'.$row['plan_desc'].'</strong></p>';
                                    ?>
                                </div>
                                </div>
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