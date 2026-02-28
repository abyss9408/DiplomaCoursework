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
		<title>Home</title>
		<meta charset="utf-8" />
		<meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
		<link rel="stylesheet" href="assets/css/main.css" />
		<noscript><link rel="stylesheet" href="assets/css/noscript.css" /></noscript>
        <link rel="stylesheet" href="assets/css/slideshow.css" />
	</head>
	<body class="is-preload landing">
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

			<!-- Banner -->
				<section id="banner">
					<div class="content">
						<header>
							<h2>Hexhosting</h2>
							<p style="text-align: center;">Our packages</p>
						</header>
						<!--<span class="image"><img class="mySlides" src="img_snowtops.jpg">
                            <img class="mySlides" src="img_lights.jpg">
                            <img class="mySlides" src="img_mountains.jpg">
                            <img class="mySlides" src="img_forest.jpg">
                        </span>-->
                        <div class="slideshow-container">

  <!-- Full-width images with number and caption text -->
  <div class="mySlides fade">
  <?php
  $sql = "SELECT * FROM carosel WHERE Id=1";
  $query_run = mysqli_query($connection, $sql); 
  $row = mysqli_fetch_array($query_run);

  echo '<img src="data:image/jpeg;base64,'.base64_encode($row['image']).'" style="width:100%">';
  ?>
  </div>

  <div class="mySlides fade">
    <?php
  $sql = "SELECT * FROM carosel WHERE Id=2";
  $query_run = mysqli_query($connection, $sql); 
  $row = mysqli_fetch_array($query_run);

  echo '<img src="data:image/jpeg;base64,'.base64_encode($row['image']).'" style="width:100%">';
  ?>
  </div>

  <div class="mySlides fade">
    <?php
  $sql = "SELECT * FROM carosel WHERE Id=3";
  $query_run = mysqli_query($connection, $sql); 
  $row = mysqli_fetch_array($query_run);

  echo '<img src="data:image/jpeg;base64,'.base64_encode($row['image']).'" style="width:100%">';
  ?>
  </div>

  <div class="mySlides fade">
    <img src="images/imagen4.jpg" style="width:100%">
  </div>

  <!-- Next and previous buttons -->
  <a class="prev" onclick="plusSlides(-1)">&#10094;</a>
  <a class="next" onclick="plusSlides(1)">&#10095;</a>
</div>
<br>

<!-- The dots/circles -->
<div style="text-align:center">
  <span class="dot" onclick="currentSlide(1)"></span>
  <span class="dot" onclick="currentSlide(2)"></span>
  <span class="dot" onclick="currentSlide(3)"></span>
  <span class="dot" onclick="currentSlide(4)"></span>
</div>
                    </div>
					<!--<a href="#one" class="goto-next scrolly">Next</a>-->
                </section>

			<!-- One -->
				<!--<section id="one" class="spotlight style1 bottom">
                    
					<span class="image fit main">
                    <img src="images/pic02.jpg" alt="" />
                    </span>
                    
					<div class="content">
						<div class="container">                        
							<div class="row">
								<div class="col-4 col-12-medium">
									<header>
										<h2>Odio faucibus ipsum integer consequat</h2>
										<p>Nascetur eu nibh vestibulum amet gravida nascetur praesent</p>
									</header>
								</div>
								<div class="col-4 col-12-medium">
									<p>Feugiat accumsan lorem eu ac lorem amet sed accumsan donec.
									Blandit orci porttitor semper. Arcu phasellus tortor enim mi
									nisi praesent dolor adipiscing. Integer mi sed nascetur cep aliquet
									augue varius tempus lobortis porttitor accumsan consequat
									adipiscing lorem dolor.</p>
								</div>
								<div class="col-4 col-12-medium">
									<p>Morbi enim nascetur et placerat lorem sed iaculis neque ante
									adipiscing adipiscing metus massa. Blandit orci porttitor semper.
									Arcu phasellus tortor enim mi mi nisi praesent adipiscing. Integer
									mi sed nascetur cep aliquet augue varius tempus. Feugiat lorem
									ipsum dolor nullam.</p>
								</div>
							</div>
						</div>
					</div>
					<a href="#two" class="goto-next scrolly">Next</a>
				</section>-->

			<!-- Two -->
				<!--<section id="two" class="spotlight style2 right">
					<span class="image fit main"><img src="images/pic03.jpg" alt="" /></span>
					<div class="content">
						<header>
							<h2>Interdum amet non magna accumsan</h2>
							<p>Nunc commodo accumsan eget id nisi eu col volutpat magna</p>
						</header>
						<p>Feugiat accumsan lorem eu ac lorem amet ac arcu phasellus tortor enim mi mi nisi praesent adipiscing. Integer mi sed nascetur cep aliquet augue varius tempus lobortis porttitor lorem et accumsan consequat adipiscing lorem.</p>
						<ul class="actions">
							<li><a href="#" class="button">Learn More</a></li>
						</ul>
					</div>
					<a href="#three" class="goto-next scrolly">Next</a>
				</section>-->

			<!-- Three -->
				<!--<section id="three" class="spotlight style3 left">
					<span class="image fit main bottom"><img src="images/pic04.jpg" alt="" /></span>
					<div class="content">
						<header>
							<h2>Interdum felis blandit praesent sed augue</h2>
							<p>Accumsan integer ultricies aliquam vel massa sapien phasellus</p>
						</header>
						<p>Feugiat accumsan lorem eu ac lorem amet ac arcu phasellus tortor enim mi mi nisi praesent adipiscing. Integer mi sed nascetur cep aliquet augue varius tempus lobortis porttitor lorem et accumsan consequat adipiscing lorem.</p>
						<ul class="actions">
							<li><a href="#" class="button">Learn More</a></li>
						</ul>
					</div>
					<a href="#four" class="goto-next scrolly">Next</a>
				</section>-->

			<!-- Four -->
				<!--<section id="four" class="wrapper style1 special fade-up">
					<div class="container">
						<header class="major">
							<h2>Accumsan sed tempus adipiscing blandit</h2>
							<p>Iaculis ac volutpat vis non enim gravida nisi faucibus posuere arcu consequat</p>
						</header>
						<div class="box alt">
							<div class="row gtr-uniform">
								<section class="col-4 col-6-medium col-12-xsmall">
									<span class="icon solid alt major fa-chart-area"></span>
									<h3>Ipsum sed commodo</h3>
									<p>Feugiat accumsan lorem eu ac lorem amet accumsan donec. Blandit orci porttitor.</p>
								</section>
								<section class="col-4 col-6-medium col-12-xsmall">
									<span class="icon solid alt major fa-comment"></span>
									<h3>Eleifend lorem ornare</h3>
									<p>Feugiat accumsan lorem eu ac lorem amet accumsan donec. Blandit orci porttitor.</p>
								</section>
								<section class="col-4 col-6-medium col-12-xsmall">
									<span class="icon solid alt major fa-flask"></span>
									<h3>Cubilia cep lobortis</h3>
									<p>Feugiat accumsan lorem eu ac lorem amet accumsan donec. Blandit orci porttitor.</p>
								</section>
								<section class="col-4 col-6-medium col-12-xsmall">
									<span class="icon solid alt major fa-paper-plane"></span>
									<h3>Non semper interdum</h3>
									<p>Feugiat accumsan lorem eu ac lorem amet accumsan donec. Blandit orci porttitor.</p>
								</section>
								<section class="col-4 col-6-medium col-12-xsmall">
									<span class="icon solid alt major fa-file"></span>
									<h3>Odio laoreet accumsan</h3>
									<p>Feugiat accumsan lorem eu ac lorem amet accumsan donec. Blandit orci porttitor.</p>
								</section>
								<section class="col-4 col-6-medium col-12-xsmall">
									<span class="icon solid alt major fa-lock"></span>
									<h3>Massa arcu accumsan</h3>
									<p>Feugiat accumsan lorem eu ac lorem amet accumsan donec. Blandit orci porttitor.</p>
								</section>
							</div>
						</div>
						<footer class="major">
							<ul class="actions special">
								<li><a href="#" class="button">Magna sed feugiat</a></li>
							</ul>
						</footer>
					</div>
				</section>-->

			<!-- Five -->
				<!--<section id="five" class="wrapper style2 special fade">
					<div class="container">
						<header>
							<h2>Magna faucibus lorem diam</h2>
							<p>Ante metus praesent faucibus ante integer id accumsan eleifend</p>
						</header>
						<form method="post" action="#" class="cta">
							<div class="row gtr-uniform gtr-50">
								<div class="col-8 col-12-xsmall"><input type="email" name="email" id="email" placeholder="Your Email Address" /></div>
								<div class="col-4 col-12-xsmall"><input type="submit" value="Get Started" class="fit primary" /></div>
							</div>
						</form>
					</div>
				</section>-->

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
            <script>
            var slideIndex = 1;
showSlides(slideIndex);

// Next/previous controls
function plusSlides(n) {
  showSlides(slideIndex += n);
}

// Thumbnail image controls
function currentSlide(n) {
  showSlides(slideIndex = n);
}

function showSlides(n) {
  var i;
  var slides = document.getElementsByClassName("mySlides");
  var dots = document.getElementsByClassName("dot");
  if (n > slides.length) {slideIndex = 1}
  if (n < 1) {slideIndex = slides.length}
  for (i = 0; i < slides.length; i++) {
      slides[i].style.display = "none";
  }
  for (i = 0; i < dots.length; i++) {
      dots[i].className = dots[i].className.replace(" active", "");
  }
  slides[slideIndex-1].style.display = "block";
  dots[slideIndex-1].className += " active";
}
            </script>
            <script>
        var slideIndex = 0;
showSlides();

function showSlides() {
  var i;
  var slides = document.getElementsByClassName("mySlides");
  for (i = 0; i < slides.length; i++) {
    slides[i].style.display = "none";
  }
  slideIndex++;
  if (slideIndex > slides.length) {slideIndex = 1}
  slides[slideIndex-1].style.display = "block";
  setTimeout(showSlides, 3000); // Change image every 3 seconds
}
            </script>
	</body>
</html>