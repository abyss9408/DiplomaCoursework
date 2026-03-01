<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <!-- Page Content -->

    <!-- Heading Starts Here -->
    <div class="page-heading header-text">
      <div class="container">
        <div class="row">
            <div class="col-md-4">
                </div>
          <div class="col-md-4">
            <h1>Smart Home</h1>
          </div>
            <div class="col-md-4">
                <img src="gif/homepage.gif" alt="" style="width:75%;height:75%;"/>
                </div>
        </div>
      </div>
    </div>
    <!-- Heading Ends Here -->


    <!-- Services Starts Here -->
    <div class="services-section">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="section-heading">
              <span>Smart home services</span>
              <h2>Services we provide</h2>
              <p>Our company have different services that we can provide for your smart home. We will go to your house and set everything up to your convenience.</p>
            </div>
          </div>
          <div class="col-md-4 col-sm-6 col-xs-12">
            <div class="service-item">
              <i class="fa fa-database"></i>
              <h4>Automatic window</h4>
              <p>We provide service to set up your window to close automtically whenever it is raining or open when it is to  hot in the house.</p>
            </div>
          </div>
          <div class="col-md-4 col-sm-6 col-xs-12">
            <div class="service-item">
              <i class="fa fa-database"></i>
              <h4>Automatic light</h4>
              <p>We can set your lights to automatically off when you leave the house or it sense that certain part of the house is not in use. The lights will on when it senses someone coming.</p>
            </div>
          </div>
          <div class="col-md-4 col-sm-6 col-xs-12">
            <div class="service-item">
              <i class="fa fa-database"></i>
              <h4>RFID door locker</h4>
              <p>We aim to have a convenient lifestyle for all our customers. With the RFID door locker you do not need to carry a bunch of heavy keys whenever you leave the house. You will just need to bring a card. </p>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- Services Ends Here -->


    <!-- Pricing Starts Here -->
    <div class="pricing-section">
      <div class="background-image-pricing">
      </div>
      <div class="container">
        <div class="row">
          <div class="col-md-8 offset-md-2">
            <div class="section-heading">
              <h2>Service Charges</h2>
              <p>The stated packages is for each service.</p>
            </div>
          </div>
          <div class="col-md-4 col-sm-6 col-xs-12">
            <div class="pricing-item">
              <h4>Automatic Windowx</h4>
              <div class="price">
                <h6>$150</h6>
                <span></span>
              </div>
              <p>This service will allow you to open and close the windows by using an app or programming a sensor.</p>
              <div class="dev"></div>
              <ul>
                <li><i class="fa fa-check"></i>The windows will close if the humidity is too high.</li>
                <li><i class="fa fa-check"></i>You can control the windows using an app.</li>
                  <!--
                <li><i class="fa fa-check"></i></li>
                <li><i class="fa fa-check"></i></li>
                <li><i class="fa fa-check"></i></li>
                <li><i class="fa fa-check"></i></li>-->
              </ul>
              <!--<a href="#" class="main-button">Select Service</a>-->
            </div>
          </div>
          <div class="col-md-4 col-sm-6 col-xs-12">
            <div class="pricing-item">
              <h4>Automatic Light</h4>
              <div class="price price-gradient">
                <h6>$150</h6>
                <span></span>
              </div>
              <p>This service will set the lights in your house to on when you reach home and close if the sensors detects that no one is at home.</p>
              <div class="dev"></div>
              <ul>
              <li><i class="fa fa-check"></i>Will turn on lights automatically when it detects a person</li>
                <li><i class="fa fa-check"></i>Will off when no one is at home for 15mins.</li>
                  <!--
                <li><i class="fa fa-check"></i>Fully Managed Panel</li>
                <li><i class="fa fa-check"></i>15-minute Quick Support</li>
                <li><i class="fa fa-check"></i>Unlimted Web Addons</li>
                <li><i class="fa fa-check"></i>Cancel or Upgrade Anytime</li>-->
              </ul>
              <!--<a href="#" class="gradient-button">Select Service</a>-->
            </div>
          </div>
          <div class="col-md-4 col-sm-6 col-xs-12">
            <div class="pricing-item">
              <h4>RFID Door Locker</h4>
              <div class="price">
                <h6>$150</h6>
                <span></span>
              </div>
              <p>This service allows you to unlock the door using a RFID card instead of using keys. It will lighten your pockets.</p>
              <div class="dev"></div>
              <ul>
                <li><i class="fa fa-check"></i>Unlock the door using a key card.</li>
                <li><i class="fa fa-check"></i>Alarm will go off if wrong or unknown card is read.</li>
                  <!--
                <li><i class="fa fa-check"></i>Fully Managed Panel</li>
                <li><i class="fa fa-check"></i>15-minute Quick Support</li>
                <li><i class="fa fa-check"></i>Top Notch Web Apps</li>
                <li><i class="fa fa-check"></i>Advanced Scalable</li>-->
              </ul>
              <!--<a href="#" class="main-button">Select Service</a>-->
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- Pricing Ends Here -->


    <!-- Features Starts Here -->
    <div class="features-section">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="section-heading">
              <span>Best Quality for you</span>
              <h2>Sensors we provide</h2>
            </div>
          </div>
          <div class="col-md-6">
            <div class="feature-item">
              <div class="icon">
                <img src="assets/images/light sensor.jpg" alt=""/>
              </div>
              <h4>Light Sensor</h4>
              <p>The light sensor is a passive devices that convert this “light energy” whether visible or in the infra-red parts of the spectrum into an electrical signal output. </p>
            </div>
          </div>
          <div class="col-md-6">
            <div class="feature-item">
              <div class="icon">
                <img src="assets/images/humid.jpg" alt=""/>
              </div>
              <h4>Humidity Sensor</h4>
              <p>A humidity sensor (or hygrometer) senses, measures and reports both moisture and air temperature. The ratio of moisture in the air to the highest amount of moisture at a particular air temperature is called relative humidity.</p>
            </div>
          </div>
          <div class="col-md-6">
            <div class="feature-item">
              <div class="icon">
                <img src="assets/images/rfid.jpg" alt=""/>
              </div>
              <h4>RFID Sensor</h4>
              <p>The RFID reader is a network-connected device that can be portable or permanently attached. It uses radio frequency waves to transmit signals that activate the tag. Once activated, the tag sends a wave back to the antenna, where it is translated into data. The transponder is located in the RFID tag itself.</p>
            </div>
          </div>
          <div class="col-md-6">
            <div class="feature-item">
              <div class="icon">
                <img src="assets/images/motion.jpg" alt=""/>
              </div>
              <h4>Motion Sensor</h4>
              <p>A motion sensor (or motion detector) is the linchpin of your security system, because it's the main device that detects when someone is in your home when they shouldn't be. A motion sensor uses one or multiple technologies to detect movement in an area.</p>
            </div>
          </div>
          <div class="col-md-6">
            <div class="feature-item">
              <div class="icon">
                <img src="assets/images/fingerprint.jpeg" alt=""/>
              </div>
              <h4>Fingerprint sensor</h4>
              <p>A fingerprint scanner is a type of technology that identifies and authenticates the fingerprints of an individual in order to grant or deny access to a computer system or a physical facility.</p>
            </div>
          </div>
          <div class="col-md-6">
            <div class="feature-item">
              <div class="icon">
                <img src="assets/images/water detector.jpg" alt=""/>
              </div>
              <h4>Water Detector</h4>
              <p>A water sensor is a device used in the detection of the water level for various applications. </p>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- Features Ends Here -->

</asp:Content>

