using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Movie_Ticket_Booking_System
{
    public partial class Form1 : Form
    {
        // create arrays
        int[] record_no = new int[10];
        string[] name = new string[10];
        int[] contact_no = new int[10];
        string[] email = new string[10];
        string[] cinema_details = new string[10];
        string[] movie = new string[10];
        string[] date = new string[10];
        string[] time = new string[10];
        int[] ticket = new int[10];
        float[] total = new float[10];
        string[] payment = new string[10];
        float[] total_sales = new float[10];
        string[] seats = new string[10];
        string[] movies_available = { "Me Before You", "Face and Body Swap", "Call Me by Your Name", "One Day", "A Quiet Place", "Insidious", "The Witch", "The Ritual", "The Conjuring", "Halloween" };
        string[] dates_available = { "02-01-2019", "04-01-2019", "05-01-2019", "07-01-2019", "09-01-2019", "12-01-2019" };
        string[] times_available = { "12:00 PM", "2:00 PM", "3:00 PM", "2:30 PM", "6:00 PM" };

        // drag window
        private bool _dragging = false;
        private Point _offset;
        private Point _start_point = new Point(0, 0);

        // class variables
        Cinema C1, C2, C3, C4, C5, C6, C7, C8, C9, C10;
        int no_of_seats_selected = 0;
        string seats_selected = "";

        // email address format
        string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-z]{2,9})$";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            // clear rtbList and label
            rtbList.Clear();
            lblTotalPrice.Text = "__________";
            // list all records
            string heading;
            string record = "";
            float total_price = 0f;

            heading = "Record no.".PadRight(20) + "Name".PadRight(20) + "Contact no.".PadRight(20) + "Email".PadRight(20) + "Cinema ID".PadRight(20) + "Location".PadRight(20) + "Opening hours".PadRight(20) + "No. of screens".PadRight(20) + "Movie".PadRight(35) + "Date".PadRight(20) + "Time".PadRight(20) + "No. of tickets".PadRight(20) + "Seats".PadRight(60) + "Total".PadRight(20) + "Payment Method\n";

            for (int i = 0; i < record_no.Length; i++)
            {
                if (name[i] != null)
                {
                    record += record_no[i].ToString().PadRight(20) + name[i].ToString().PadRight(20) + contact_no[i].ToString().PadRight(20) + email[i].ToString().PadRight(20) + cinema_details[i].ToString().PadRight(80) + movie[i].ToString().PadRight(35) + date[i].ToString().PadRight(20) + time[i].ToString().PadRight(20) + ticket[i].ToString().PadRight(20) + seats[i].PadRight(60) + total[i].ToString("C").PadRight(20) + payment[i].ToString() + "\n";
                    total_price += total[i];
                }
                else
                {
                    record_no[i] = i + 1;
                    record += record_no[i].ToString() + "\n";
                }
            }

            if (record == "")
            {
                MessageBox.Show("No records found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                rtbList.AppendText(heading + record);
                lblTotalPrice.Text = total_price.ToString("C");
            }
        }

        private float CalculateTotalPerRecord(int no_of_tickets)
        {
            float total = 0;
            total = no_of_tickets * 9.5f;
            return total;
        }

        private bool DataValidation(int tickets)
        {
            // data validation
            if (txtName.Text == "" || txtContactNo.Text == "" || txtEmail.Text == "")
            {
                MessageBox.Show("Please complete your personal details");
                return false;
            }
            else if (!Regex.IsMatch(txtEmail.Text, pattern))
            {
                MessageBox.Show("Please enter a valid email address");
                return false;
            }
            else if (comCinema.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a cinema");
                return false;
            }
            else if (comMovie.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a movie");
                return false;
            }
            else if (comDate.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a date");
                return false;
            }
            else if (comTime.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a time");
                return false;
            }
            else if (txtTicket.Text == "")
            {
                MessageBox.Show("Please enter the no. of tickets");
                return false;
            }
            else if (tickets <= 0)
            {
                MessageBox.Show("No. of tickets must be > 0");
                return false;
            }
            else if (comPayment.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a payment method");
                return false;
            }
            else if ((comPayment.SelectedIndex == 2 || comPayment.SelectedIndex == 3) && (txtCardNum.Text == "" || txtSecurityCode.Text == ""))
            {
                MessageBox.Show("Financial information incomplete");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void DataProcessing()
        {
            bool data_valid;
            bool no_of_seats_selected_tallies;
            bool found = false;
            int no_of_tickets;
            float local_total;
            string new_seats_selected;

            try
            {
                no_of_tickets = int.Parse(txtTicket.Text);

                local_total = CalculateTotalPerRecord(no_of_tickets);

                data_valid = DataValidation(no_of_tickets);

                no_of_seats_selected_tallies = CheckNoOfSeatsSelected(no_of_tickets);

                if (data_valid && no_of_seats_selected_tallies)
                {
                    DialogResult result = MessageBox.Show("Name: " + txtName.Text + "\n" + "Contact No: " + txtContactNo.Text + "\n" + "Email: " + txtEmail.Text + "\n" + "Cinema: " + comCinema.SelectedItem.ToString() + "\n" + "Movie: " + comMovie.SelectedItem.ToString() + "\n" + "Date: " + comDate.SelectedItem.ToString() + "\n" + "Time: " + comTime.SelectedItem.ToString() + "\n" + "No. of tickets: " + txtTicket.Text + "\n" + "Seats: " + seats_selected + "\n" + "Total: " + local_total.ToString("C") + "\n" + "Payment Method: " + comPayment.SelectedItem.ToString() + "\n", "Confirm Booking", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    // new data saving method
                    if (result == DialogResult.Yes)
                    {
                        for (int i = 0; i < record_no.Length; i++)
                        {
                            if (name[i] == null)
                            {
                                found = true; // set found to true for saving data
                                record_no[i] = i + 1;
                                name[i] = txtName.Text;
                                contact_no[i] = int.Parse(txtContactNo.Text);
                                email[i] = txtEmail.Text;
                                // cinema_details[i] = comCinema.SelectedItem.ToString();
                                movie[i] = comMovie.SelectedItem.ToString();
                                date[i] = comDate.SelectedItem.ToString();
                                time[i] = comTime.SelectedItem.ToString();
                                ticket[i] = no_of_tickets;
                                total[i] = local_total;
                                payment[i] = comPayment.SelectedItem.ToString();

                                // save cinema details based on the cinema selected
                                if (comCinema.SelectedIndex == 1)
                                {
                                    cinema_details[i] = C1.DisplayCinemaInfo();
                                    C1.SetNoOfTicketsSold(C1.GetNoOfTicketsSold() + ticket[i]); // update no of tickets sold in c1
                                    C1.SetTotalSales(C1.GetNoOfTicketsSold() * 9.50f); // update total sales of c1
                                    total_sales[0] = C1.GetTotalSales();
                                }
                                else if (comCinema.SelectedIndex == 2)
                                {
                                    cinema_details[i] = C2.DisplayCinemaInfo();
                                    C2.SetNoOfTicketsSold(C2.GetNoOfTicketsSold() + ticket[i]); // update no of tickets sold in c2
                                    C2.SetTotalSales(C2.GetNoOfTicketsSold() * 9.50f); // update total sales of c2
                                    total_sales[1] = C2.GetTotalSales();
                                }
                                else if (comCinema.SelectedIndex == 3)
                                {
                                    cinema_details[i] = C3.DisplayCinemaInfo();
                                    C3.SetNoOfTicketsSold(C3.GetNoOfTicketsSold() + ticket[i]); // update no of tickets sold in c3
                                    C3.SetTotalSales(C3.GetNoOfTicketsSold() * 9.50f); // update total sales of c3
                                    total_sales[2] = C3.GetTotalSales();
                                }
                                else if (comCinema.SelectedIndex == 4)
                                {
                                    cinema_details[i] = C4.DisplayCinemaInfo();
                                    C4.SetNoOfTicketsSold(C4.GetNoOfTicketsSold() + ticket[i]); // update no of tickets sold in c4
                                    C4.SetTotalSales(C4.GetNoOfTicketsSold() * 9.50f); // update total sales of c4
                                    total_sales[3] = C4.GetTotalSales();
                                }
                                else if (comCinema.SelectedIndex == 5)
                                {
                                    cinema_details[i] = C5.DisplayCinemaInfo();
                                    C5.SetNoOfTicketsSold(C5.GetNoOfTicketsSold() + ticket[i]); // update no of tickets sold in c5
                                    C5.SetTotalSales(C5.GetNoOfTicketsSold() * 9.50f); // update total sales of c5
                                    total_sales[4] = C5.GetTotalSales();
                                }
                                else if (comCinema.SelectedIndex == 6)
                                {
                                    cinema_details[i] = C6.DisplayCinemaInfo();
                                    C6.SetNoOfTicketsSold(C6.GetNoOfTicketsSold() + ticket[i]); // update no of tickets sold in c6
                                    C6.SetTotalSales(C5.GetNoOfTicketsSold() * 9.50f); // update total sales of c6
                                    total_sales[5] = C6.GetTotalSales();
                                }
                                else if (comCinema.SelectedIndex == 7)
                                {
                                    cinema_details[i] = C7.DisplayCinemaInfo();
                                    C7.SetNoOfTicketsSold(C7.GetNoOfTicketsSold() + ticket[i]); // update no of tickets sold in c7
                                    C7.SetTotalSales(C6.GetNoOfTicketsSold() * 9.50f); // update total sales of c7
                                    total_sales[6] = C7.GetTotalSales();
                                }
                                else if (comCinema.SelectedIndex == 8)
                                {
                                    cinema_details[i] = C8.DisplayCinemaInfo();
                                    C8.SetNoOfTicketsSold(C8.GetNoOfTicketsSold() + ticket[i]); // update no of tickets sold in c8
                                    C8.SetTotalSales(C8.GetNoOfTicketsSold() * 9.50f); // update total sales of c8
                                    total_sales[7] = C8.GetTotalSales();
                                }
                                else if (comCinema.SelectedIndex == 9)
                                {
                                    cinema_details[i] = C9.DisplayCinemaInfo();
                                    C9.SetNoOfTicketsSold(C9.GetNoOfTicketsSold() + ticket[i]); // update no of tickets sold in c9
                                    C9.SetTotalSales(C9.GetNoOfTicketsSold() * 9.50f); // update total sales of c9
                                    total_sales[8] = C9.GetTotalSales();
                                }
                                else if (comCinema.SelectedIndex == 10)
                                {
                                    cinema_details[i] = C10.DisplayCinemaInfo();
                                    C10.SetNoOfTicketsSold(C10.GetNoOfTicketsSold() + ticket[i]); // update no of tickets sold in c10
                                    C10.SetTotalSales(C10.GetNoOfTicketsSold() * 9.50f); // update total sales of c10
                                    total_sales[9] = C10.GetTotalSales();
                                }

                                // remove last white space
                                new_seats_selected = seats_selected.Substring(0, seats_selected.Length - 1);
                                seats[i] = new_seats_selected;

                                // display message according to payment method
                                if (comPayment.SelectedIndex == 1)
                                {
                                    MessageBox.Show("Tickets sucessfully booked!! Send the payment to paypal.me/######.");
                                }
                                else
                                {
                                    MessageBox.Show("Tickets sucessfully booked!!");
                                }

                                break; // get out of loop after saving data
                            }
                        }

                        // if no more empty slots for data saving
                        if (!found)
                        {
                            MessageBox.Show("Maximum number(10) of bookings reached.");
                        }
                        else
                        {
                            // DV Suntec City, "Me Before You", "02-01-2019", "12:00 PM" selected
                            if (comCinema.SelectedIndex == 1 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                            {
                                foreach (var btn in gbSuntecCityScreen1.Controls.OfType<Button>())
                                {
                                    if (btn.BackColor == Color.DarkGray)
                                    {
                                        btn.Text = "";
                                        btn.BackColor = Color.DimGray;
                                        btn.Enabled = false;
                                    }
                                }

                                gbSuntecCityScreen1.Visible = false;
                            }
                            // DV Plaza, "Face and Body Swap", "04-01-2019", "3:00 PM" selected
                            else if (comCinema.SelectedIndex == 2 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                            {
                                foreach (var btn in gbPlazaScreen5.Controls.OfType<Button>())
                                {
                                    if (btn.BackColor == Color.DarkGray)
                                    {
                                        btn.Text = "";
                                        btn.BackColor = Color.DimGray;
                                        btn.Enabled = false;
                                    }
                                }

                                gbPlazaScreen5.Visible = false;
                            }
                            // DV City Square, "Call Me by Your Name", "05-01-2019", "2:00 PM" selected
                            else if (comCinema.SelectedIndex == 3 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                            {
                                foreach (var btn in gbCitySquareScreen2.Controls.OfType<Button>())
                                {
                                    if (btn.BackColor == Color.DarkGray)
                                    {
                                        btn.Text = "";
                                        btn.BackColor = Color.DimGray;
                                        btn.Enabled = false;
                                    }
                                }

                                gbCitySquareScreen2.Visible = false;
                            }
                            // DV Bishan, "One Day", "07-01-2019", "12:00 PM" selected
                            else if (comCinema.SelectedIndex == 4 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                            {
                                foreach (var btn in gbBishanScreen3.Controls.OfType<Button>())
                                {
                                    if (btn.BackColor == Color.DarkGray)
                                    {
                                        btn.Text = "";
                                        btn.BackColor = Color.DimGray;
                                        btn.Enabled = false;
                                    }
                                }

                                gbBishanScreen3.Visible = false;
                            }
                            // DV Yishun, "A Quiet Place", "02-01-2019", "12:00 PM" selected
                            else if (comCinema.SelectedIndex == 5 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                            {
                                foreach (var btn in gbYishunScreen7.Controls.OfType<Button>())
                                {
                                    if (btn.BackColor == Color.DarkGray)
                                    {
                                        btn.Text = "";
                                        btn.BackColor = Color.DimGray;
                                        btn.Enabled = false;
                                    }
                                }

                                gbYishunScreen7.Visible = false;
                            }
                            // DV nex, "Insidious", "09-01-2019", "2:30 PM" selected
                            else if (comCinema.SelectedIndex == 6 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                            {
                                foreach (var btn in gbnexScreen11.Controls.OfType<Button>())
                                {
                                    if (btn.BackColor == Color.DarkGray)
                                    {
                                        btn.Text = "";
                                        btn.BackColor = Color.DimGray;
                                        btn.Enabled = false;
                                    }
                                }

                                gbnexScreen11.Visible = false;
                            }
                            // DV JCube, "The Witch", "09-01-2019", "2:00 PM" selected
                            else if (comCinema.SelectedIndex == 7 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                            {
                                foreach (var btn in gbJCubeScreen4.Controls.OfType<Button>())
                                {
                                    if (btn.BackColor == Color.DarkGray)
                                    {
                                        btn.Text = "";
                                        btn.BackColor = Color.DimGray;
                                        btn.Enabled = false;
                                    }
                                }

                                gbJCubeScreen4.Visible = false;
                            }
                            // DV Seletar, "The Ritual", "12-01-2019", "6:00 PM" selected
                            else if (comCinema.SelectedIndex == 8 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                            {
                                foreach (var btn in gbSeletarScreen9.Controls.OfType<Button>())
                                {
                                    if (btn.BackColor == Color.DarkGray)
                                    {
                                        btn.Text = "";
                                        btn.BackColor = Color.DimGray;
                                        btn.Enabled = false;
                                    }
                                }

                                gbSeletarScreen9.Visible = false;
                            }
                            // DV Tampines, "The Conjuring", "12-01-2019", "2:30 PM" selected
                            else if (comCinema.SelectedIndex == 9 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                            {
                                foreach (var btn in gbTampinesScreen6.Controls.OfType<Button>())
                                {
                                    if (btn.BackColor == Color.DarkGray)
                                    {
                                        btn.Text = "";
                                        btn.BackColor = Color.DimGray;
                                        btn.Enabled = false;
                                    }
                                }

                                gbTampinesScreen6.Visible = false;
                            }
                            // DV Jurong Point, "Halloween", "09-01-2019", "6:00 PM" selected
                            else if (comCinema.SelectedIndex == 10 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                            {
                                foreach (var btn in gbJurongPointScreen10.Controls.OfType<Button>())
                                {
                                    if (btn.BackColor == Color.DarkGray)
                                    {
                                        btn.Text = "";
                                        btn.BackColor = Color.DimGray;
                                        btn.Enabled = false;
                                    }
                                }

                                gbJurongPointScreen10.Visible = false;
                            }

                            tpSeats.Enabled = false;
                            tpBooking.Enabled = true;
                            tabControl1.SelectTab(0);
                            seats_selected = "";
                            no_of_seats_selected = 0;
                        }
                    }
                    else
                    {
                        // do nothing
                    }
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("One of more of your inputs is invalid");
            }
                      
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            // clear all fields in booking tab
            txtName.Clear();
            txtContactNo.Clear();
            txtEmail.Clear();
            comCinema.SelectedIndex = 0;
            comMovie.SelectedIndex = 0;
            comDate.SelectedIndex = 0;
            comTime.SelectedIndex = 0;
            txtTicket.Clear();
            comPayment.SelectedIndex = 0;
            txtCardNum.Clear();
            txtSecurityCode.Clear();
            comMonth.SelectedIndex = 0;
            comYear.SelectedIndex = 0;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string[] usernames = { "admin", "admin2" };
            string[] passwords = { "password", "password2" };

            if (usernames.Contains(txtUser.Text) && passwords.Contains(txtPass.Text) && Array.IndexOf(usernames, txtUser.Text) == Array.IndexOf(passwords, txtPass.Text))
            {
                btnList.Enabled = true;
                btnDelete.Enabled = true;
                btnListAllCinemaSales.Enabled = true;
                btnListMax.Enabled = true;
                btnListMin.Enabled = true;
                btnUpdateCinema.Enabled = true;
                btnListCinemaSales.Enabled = true;
            }
            else
            {
                MessageBox.Show("Authentication failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // intialize dropdownlist options
            comCinema.SelectedIndex = 0;
            comMovie.SelectedIndex = 0;
            comDate.SelectedIndex = 0;
            comTime.SelectedIndex = 0;
            comPayment.SelectedIndex = 0;
            comMonth.SelectedIndex = 0;
            comYear.SelectedIndex = 0;

            tpSeats.Enabled = false;

            // create cinema objects
            C1 = new Cinema();
            C2 = new Cinema();
            C3 = new Cinema();
            C4 = new Cinema();
            C5 = new Cinema();
            C6 = new Cinema();
            C7 = new Cinema();
            C8 = new Cinema();
            C9 = new Cinema();
            C10 = new Cinema();

            // update cinema objects
            C1.Update(comCinema.Items[1].ToString(), "Suntec City", "0930-2200", 8);
            C2.Update(comCinema.Items[2].ToString(), "Orchard Road", "0900-2300", 11);
            C3.Update(comCinema.Items[3].ToString(), "City Square Mall", "0930-2200", 7);
            C4.Update(comCinema.Items[4].ToString(), "Junction 8", "1000-2230", 9);
            C5.Update(comCinema.Items[5].ToString(), "Yishun Central", "0930-2200", 7);
            C6.Update(comCinema.Items[6].ToString(), "Serangoon nex", "0930-2300", 12);
            C7.Update(comCinema.Items[7].ToString(), "JCube Shopping mall", "0900-2200", 8);
            C8.Update(comCinema.Items[8].ToString(), "The Seletar mall", "0930-2230", 10);
            C9.Update(comCinema.Items[9].ToString(), "Tampines mall", "1000-2230", 9);
            C10.Update(comCinema.Items[10].ToString(), "Jurong West Central", "0930-2200", 10);

            // set parents of all seating plans to tpSeats and position them accordingly
            gbPlazaScreen5.Parent = tpSeats;
            gbPlazaScreen5.Location = new Point(195, 90);
            gbCitySquareScreen2.Parent = tpSeats;
            gbCitySquareScreen2.Location = new Point(195, 110);
            gbBishanScreen3.Parent = tpSeats;
            gbBishanScreen3.Location = new Point(195, 110);
            gbYishunScreen7.Parent = tpSeats;
            gbYishunScreen7.Location = new Point(195, 110);
            gbnexScreen11.Parent = tpSeats;
            gbnexScreen11.Location = new Point(195, 110);
            gbJCubeScreen4.Parent = tpSeats;
            gbJCubeScreen4.Location = new Point(195, 110);
            gbSeletarScreen9.Parent = tpSeats;
            gbSeletarScreen9.Location = new Point(195, 110);
            gbTampinesScreen6.Parent = tpSeats;
            gbTampinesScreen6.Location = new Point(195, 110);
            gbJurongPointScreen10.Parent = tpSeats;
            gbJurongPointScreen10.Location = new Point(195, 110);

            // set Text for groupboxes
            gbSuntecCityScreen1.Text = "DV Suntec City Screen 1 Movie: " + movies_available[0] + " Date: " + dates_available[0] + " Time: " + times_available[0];
            gbPlazaScreen5.Text = "DV Plaza Screen 5 Movie: " + movies_available[1] + " Date: " + dates_available[1] + " Time: " + times_available[2];
            gbCitySquareScreen2.Text = "DV City Square Screen 2 Movie: " + movies_available[2] + " Date: " + dates_available[2] + " Time: " + times_available[1];
            gbBishanScreen3.Text = "DV Bishan Screen 3 Movie: " + movies_available[3] + " Date: " + dates_available[3] + " Time: " + times_available[0];
            gbYishunScreen7.Text = "DV Yishun Screen 7 Movie: " + movies_available[4] + " Date: " + dates_available[0] + " Time: " + times_available[0];
            gbnexScreen11.Text = "DV nex Screen 11 Movie: " + movies_available[5] + " Date: " + dates_available[4] + " Time: " + times_available[3];
            gbJCubeScreen4.Text = "DV JCube Screen 4 Movie: " + movies_available[6] + " Date: " + dates_available[4] + " Time: " + times_available[1];
            gbSeletarScreen9.Text = "DV Seletar Screen 9 Movie: " + movies_available[7] + " Date: " + dates_available[5] + " Time: " + times_available[4];
            gbTampinesScreen6.Text = "DV Tampines Screen 6 Movie: " + movies_available[8] + " Date: " + dates_available[5] + " Time: " + times_available[3];
            gbJurongPointScreen10.Text = "DV Jurong Point Screen 10 Movie: " + movies_available[9] + " Date: " + dates_available[4] + " Time: " + times_available[4];
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            // local variables
            string heading;
            string record = "";
            int search_mobile_no;
            string search_email;
            float total_cost = 0f;

            rtbSearch.Clear();

            heading = "Record no.".PadRight(20) + "Name".PadRight(20) + "Contact no.".PadRight(20) + "Email".PadRight(20) + "Cinema ID".PadRight(20) + "Location".PadRight(20) + "Opening hours".PadRight(20) + "No. of screens".PadRight(20) + "Movie".PadRight(35) + "Date".PadRight(20) + "Time".PadRight(20) + "No. of tickets".PadRight(20) + "Seats".PadRight(60) + "Total".PadRight(20) + "Payment Method\n";
            try
            {
                // check booking based on user option
                if (rdbMobile.Checked && txtSearchMobileNo.TextLength > 0)
                {
                    search_mobile_no = int.Parse(txtSearchMobileNo.Text);

                    for (int i = 0; i < record_no.Length; i++)
                    {
                        if (search_mobile_no == contact_no[i] && name[i] != null)
                        {
                            record += record_no[i].ToString().PadRight(20) + name[i].ToString().PadRight(20) + contact_no[i].ToString().PadRight(20) + email[i].ToString().PadRight(20) + cinema_details[i].ToString().PadRight(80) + movie[i].ToString().PadRight(35) + date[i].ToString().PadRight(20) + time[i].ToString().PadRight(20) + ticket[i].ToString().PadRight(20) + seats[i].PadRight(60) + total[i].ToString("C").PadRight(20) + payment[i].ToString() + "\n";
                            total_cost += total[i];
                        }
                    }

                    if (record == "")
                    {
                        MessageBox.Show("No bookings found!!");
                    }
                    else
                    {
                        rtbSearch.AppendText(heading + record);
                        lblTotalCost.Text = total_cost.ToString("C");
                    }

                }
                else if (rdbEmail.Checked && txtSearchEmail.TextLength > 0)
                {
                    search_email = txtSearchEmail.Text;

                    for (int i = 0; i < record_no.Length; i++)
                    {
                        if (search_email == email[i] && name[i] != null)
                        {
                            record += record_no[i].ToString().PadRight(20) + name[i].ToString().PadRight(20) + contact_no[i].ToString().PadRight(20) + email[i].ToString().PadRight(20) + cinema_details[i].ToString().PadRight(80) + movie[i].ToString().PadRight(35) + date[i].ToString().PadRight(20) + time[i].ToString().PadRight(20) + ticket[i].ToString().PadRight(20) + seats[i].PadRight(60) + total[i].ToString("C").PadRight(20) + payment[i].ToString() + "\n";
                            total_cost += total[i];
                        }
                    }

                    if (record == "")
                    {
                        MessageBox.Show("No bookings found!!");
                    }
                    else
                    {
                        rtbSearch.AppendText(heading + record);
                        lblTotalCost.Text = total_cost.ToString("C");
                    }
                }
                else
                {
                    MessageBox.Show("Please select an option or enter something into the respective textbox to your bookings");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Input");
            }
        }

        private void btnDisplayAllCinema_Click(object sender, EventArgs e)
        {
            string heading;
            string display;

            heading = "Cinema ID".PadRight(20) + "Location".PadRight(20) + "Opening Hours".PadRight(20) + "No of screens\n";
            display = C1.DisplayCinemaInfo() + "\n" + C2.DisplayCinemaInfo() + "\n" + C3.DisplayCinemaInfo() + "\n" + C4.DisplayCinemaInfo() + "\n" + C5.DisplayCinemaInfo() + "\n" + C6.DisplayCinemaInfo() + "\n" + C7.DisplayCinemaInfo() + "\n" + C8.DisplayCinemaInfo() + "\n" + C9.DisplayCinemaInfo() + "\n" + C10.DisplayCinemaInfo() + "\n";
            rtbCinema.AppendText(heading + display);
        }

        private void btnListCinemaSales_Click(object sender, EventArgs e)
        {
            // clear rtbList and label
            rtbList.Clear();
            lblTotalPrice.Text = "__________";

            string heading2;
            string display2;

            heading2 = "Cinema ID".PadRight(20) + "No. of tickets sold".PadRight(20) + "Total sales\n";
            display2 = C1.DisplayCinemaSales() + C2.DisplayCinemaSales() + C3.DisplayCinemaSales() + C4.DisplayCinemaSales() + C5.DisplayCinemaSales() + C6.DisplayCinemaSales() + C7.DisplayCinemaSales() + C8.DisplayCinemaSales() + C9.DisplayCinemaSales() + C10.DisplayCinemaSales();
            rtbList.AppendText(heading2 + display2);
        }

        private void btnListMax_Click(object sender, EventArgs e)
        {
            rtbList.Clear();
            lblTotalPrice.Text = "__________";
            string heading_max;
            string display_max = "";
            float max;
            string max_index = "";

            // max = total_sales.Max();
            max = total_sales[0];

            for (int i = 1; i < total_sales.Length; i++)
            {
                if (total_sales[i] > max)
                {
                    max = total_sales[i];
                }
            }

            for (int i = 0; i < total_sales.Length; i++)
            {
                if (total_sales[i] == max)
                {
                    max_index += i.ToString();
                }
            }

            heading_max = "Cinema ID".PadRight(20) + "No. of tickets sold".PadRight(20) + "Total sales\n";

            if (max_index.Contains("0"))
            {
                display_max += C1.DisplayCinemaSales();
            }
            if (max_index.Contains("1"))
            {
                display_max += C2.DisplayCinemaSales();
            }
            if (max_index.Contains("2"))
            {
                display_max += C3.DisplayCinemaSales();
            }
            if (max_index.Contains("3"))
            {
                display_max += C4.DisplayCinemaSales();
            }
            if (max_index.Contains("4"))
            {
                display_max += C5.DisplayCinemaSales();
            }
            if (max_index.Contains("5"))
            {
                display_max += C6.DisplayCinemaSales();
            }
            if (max_index.Contains("6"))
            {
                display_max += C7.DisplayCinemaSales();
            }
            if (max_index.Contains("7"))
            {
                display_max += C8.DisplayCinemaSales();
            }
            if (max_index.Contains("8"))
            {
                display_max += C9.DisplayCinemaSales();
            }
            if (max_index.Contains("9"))
            {
                display_max += C10.DisplayCinemaSales();
            }

            rtbList.AppendText(heading_max + display_max);
        }

        private void btnListMin_Click(object sender, EventArgs e)
        {
            rtbList.Clear();
            lblTotalPrice.Text = "__________";
            string heading_min;
            string display_min = "";
            float min;
            string min_index = "";

            // min = total_sales.Min();
            min = total_sales[0];

            for (int i = 1; i < total_sales.Length; i++)
            {
                if (total_sales[i] < min)
                {
                    min = total_sales[i];
                }
            }

            for (int i = 0; i < total_sales.Length; i++)
            {
                if (total_sales[i] == min)
                {
                    min_index += i.ToString();
                }
            }

            heading_min = "Cinema ID".PadRight(20) + "No. of tickets sold".PadRight(20) + "Total sales\n";

            if (min_index.Contains("0"))
            {
                display_min += C1.DisplayCinemaSales();
            }
            if (min_index.Contains("1"))
            {
                display_min += C2.DisplayCinemaSales();
            }
            if (min_index.Contains("2"))
            {
                display_min += C3.DisplayCinemaSales();
            }
            if (min_index.Contains("3"))
            {
                display_min += C4.DisplayCinemaSales();
            }
            if (min_index.Contains("4"))
            {
                display_min += C5.DisplayCinemaSales();
            }
            if (min_index.Contains("5"))
            {
                display_min += C6.DisplayCinemaSales();
            }
            if (min_index.Contains("6"))
            {
                display_min += C7.DisplayCinemaSales();
            }
            if (min_index.Contains("7"))
            {
                display_min += C8.DisplayCinemaSales();
            }
            if (min_index.Contains("8"))
            {
                display_min += C9.DisplayCinemaSales();
            }
            if (min_index.Contains("9"))
            {
                display_min += C10.DisplayCinemaSales();
            }

            rtbList.AppendText(heading_min + display_min);
        }

        private void btnUpdateCinema_Click(object sender, EventArgs e)
        {
            string update_cinema_id;
            string update_location;
            string update_opening_hours;
            int update_no_of_screens;

            try
            {
                update_cinema_id = txtUpdateCinemaID.Text;
                update_location = txtUpdateLocation.Text;
                update_opening_hours = txtUpdateOpeningHours.Text;
                update_no_of_screens = int.Parse(txtUpdateNoOfScreens.Text);

                if (rdbC1.Checked)
                {
                    C1.Update(update_cinema_id, update_location, update_opening_hours, update_no_of_screens);
                    MessageBox.Show("Details of DV Suntec City is successfully updated!");
                }
                else if (rdbC2.Checked)
                {
                    C2.Update(update_cinema_id, update_location, update_opening_hours, update_no_of_screens);
                    MessageBox.Show("Details of DV Plaza is successfully updated!");
                }
                else if (rdbC3.Checked)
                {
                    C3.Update(update_cinema_id, update_location, update_opening_hours, update_no_of_screens);
                    MessageBox.Show("Details of DV City Square is successfully updated!");
                }
                else if (rdbC4.Checked)
                {
                    C4.Update(update_cinema_id, update_location, update_opening_hours, update_no_of_screens);
                    MessageBox.Show("Details of DV Bishan is successfully updated!");
                }
                else if (rdbC5.Checked)
                {
                    C5.Update(update_cinema_id, update_location, update_opening_hours, update_no_of_screens);
                    MessageBox.Show("Details of DV Yishun is successfully updated!");
                }
                else if (rdbC6.Checked)
                {
                    C6.Update(update_cinema_id, update_location, update_opening_hours, update_no_of_screens);
                    MessageBox.Show("Details of DV nex is successfully updated!");
                }
                else if (rdbC7.Checked)
                {
                    C7.Update(update_cinema_id, update_location, update_opening_hours, update_no_of_screens);
                    MessageBox.Show("Details of DV JCube is successfully updated!");
                }
                else if (rdbC8.Checked)
                {
                    C8.Update(update_cinema_id, update_location, update_opening_hours, update_no_of_screens);
                    MessageBox.Show("Details of DV Seletar is successfully updated!");
                }
                else if (rdbC9.Checked)
                {
                    C9.Update(update_cinema_id, update_location, update_opening_hours, update_no_of_screens);
                    MessageBox.Show("Details of DV Tampines is successfully updated!");
                }
                else if (rdbC10.Checked)
                {
                    C10.Update(update_cinema_id, update_location, update_opening_hours, update_no_of_screens);
                    MessageBox.Show("Details of DV Jurong Point is successfully updated!");
                }
                else
                {
                    MessageBox.Show("Please enter a cinema to update its details");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void rdbC1_CheckedChanged(object sender, EventArgs e)
        {
            if(((RadioButton)sender).Checked)
            {
                txtUpdateCinemaID.Text = comCinema.Items[1].ToString();
            }
        }

        private void rdbC2_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                txtUpdateCinemaID.Text = comCinema.Items[2].ToString();
            }
        }

        private void rdbC3_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                txtUpdateCinemaID.Text = comCinema.Items[3].ToString();
            }
        }

        private void rdbC4_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                txtUpdateCinemaID.Text = comCinema.Items[4].ToString();
            }
        }

        private void rdbC5_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                txtUpdateCinemaID.Text = comCinema.Items[5].ToString();
            }
        }

        private void rdbC6_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                txtUpdateCinemaID.Text = comCinema.Items[6].ToString();
            }
        }

        private void rdbC7_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                txtUpdateCinemaID.Text = comCinema.Items[7].ToString();
            }
        }

        private void rdbC8_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                txtUpdateCinemaID.Text = comCinema.Items[8].ToString();
            }
        }

        private void rdbC9_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                txtUpdateCinemaID.Text = comCinema.Items[9].ToString();
            }
        }

        private void rdbC10_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                txtUpdateCinemaID.Text = comCinema.Items[10].ToString();
            }
        }

        private void comPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comPayment.SelectedIndex == 2 || comPayment.SelectedIndex == 3)
            {
                txtCardNum.Enabled = true;
                txtSecurityCode.Enabled = true;
                comMonth.Enabled = true;
                comYear.Enabled = true;
            }
            else
            {
                txtCardNum.Enabled = false;
                txtSecurityCode.Enabled = false;
                comMonth.Enabled = false;
                comYear.Enabled = false;
            }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            DataProcessing();
        }

        private bool CheckNoOfSeatsSelected(int no_of_tickets_purchased)
        {
            if (no_of_seats_selected > no_of_tickets_purchased)
            {
                MessageBox.Show("The no. of seats selected is more than that you want to book");
                return false;
            }
            else if (no_of_seats_selected < no_of_tickets_purchased)
            {
                MessageBox.Show("The no. of seats selected is less than that you want to book");
                return false;
            }
            return true;
        }

        private void btnProceedBookSeats_Click(object sender, EventArgs e)
        {
            bool is_data_valid;

            try
            {
                is_data_valid = DataValidation(int.Parse(txtTicket.Text));

                if (is_data_valid)
                {
                    // DV Suntec City, "Me Before You", "02-01-2019" and "12:00 PM" selected
                    if (comCinema.SelectedIndex == 1 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                    {
                        gbSuntecCityScreen1.Visible = true;
                    }
                    // DV Plaza, "Face and Body Swap", "04-01-2019" and "3:00 PM" selected
                    else if (comCinema.SelectedIndex == 2 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                    {
                        gbPlazaScreen5.Visible = true;
                    }
                    // DV City Square, "Call Me by Your Name", "05-01-2019" and "2:00 PM" selected
                    else if (comCinema.SelectedIndex == 3 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                    {
                        gbCitySquareScreen2.Visible = true;
                    }
                    // DV Bishan, "One Day", "07-01-2019" and "12:00 PM" selected
                    else if (comCinema.SelectedIndex == 4 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                    {
                        gbBishanScreen3.Visible = true;
                    }
                    // DV Yishun, "A Quiet Place", "02-01-2019" and "12:00 PM" selected
                    else if (comCinema.SelectedIndex == 5 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                    {
                        gbYishunScreen7.Visible = true;
                    }
                    // DV nex, "Insidious", "09-01-2019" and "2:30 PM" selected
                    else if (comCinema.SelectedIndex == 6 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                    {
                        gbnexScreen11.Visible = true;
                    }
                    // DV JCube, "The Witch", "09-01-2019" and "2:00 PM" selected
                    else if (comCinema.SelectedIndex == 7 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                    {
                        gbJCubeScreen4.Visible = true;
                    }
                    // DV Seletar, "The Ritual", "12-01-2019" and "6:00 PM" selected
                    else if (comCinema.SelectedIndex == 8 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                    {
                        gbSeletarScreen9.Visible = true;
                    }
                    // DV Tampines, "The Conjuring", "12-01-2019" and "2:30 PM" selected
                    else if (comCinema.SelectedIndex == 9 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                    {
                        gbTampinesScreen6.Visible = true;
                    }
                    // DV Jurong Point, "Halloween", "09-01-2019" and "6:00 PM" selected
                    else if (comCinema.SelectedIndex == 10 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1 && comTime.SelectedIndex == 1)
                    {
                        gbJurongPointScreen10.Visible = true;
                    }

                    tpBooking.Enabled = false;
                    tpSeats.Enabled = true;
                    tabControl1.SelectTab(1);                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!e.TabPage.Enabled)
            {
                e.Cancel = true;
            }
        }

        private void btnReturnToBookingDetails_Click(object sender, EventArgs e)
        {
            // deselect any selected seats that are not booked and set the respecive groupbox to invisible
            if (gbSuntecCityScreen1.Visible)
            {
                foreach (var btn in gbSuntecCityScreen1.Controls.OfType<Button>())
                {
                    if (btn.BackColor == Color.DarkGray && btn.Enabled)
                    {
                        btn.BackColor = Color.Black;
                        if (btn.Name == "btnSuntecCityScreen1G04" || btn.Name == "btnSuntecCityScreen1G13")
                        {
                            btn.Text = "W";
                        }
                    }
                }
                gbSuntecCityScreen1.Visible = false;
            }
            else if (gbPlazaScreen5.Visible)
            {
                foreach (var btn in gbPlazaScreen5.Controls.OfType<Button>())
                {
                    if (btn.BackColor == Color.DarkGray && btn.Enabled)
                    {
                        btn.BackColor = Color.Black;
                    }
                }
                gbPlazaScreen5.Visible = false;
            }
            else if (gbCitySquareScreen2.Visible)
            {
                foreach (var btn in gbCitySquareScreen2.Controls.OfType<Button>())
                {
                    if (btn.BackColor == Color.DarkGray && btn.Enabled)
                    {
                        btn.BackColor = Color.Black;
                    }
                }
                gbCitySquareScreen2.Visible = false;
            }
            else if (gbBishanScreen3.Visible)
            {
                foreach (var btn in gbBishanScreen3.Controls.OfType<Button>())
                {
                    if (btn.BackColor == Color.DarkGray && btn.Enabled)
                    {
                        btn.BackColor = Color.Black;
                    }
                }
                gbBishanScreen3.Visible = false;
            }
            else if (gbYishunScreen7.Visible)
            {
                foreach (var btn in gbYishunScreen7.Controls.OfType<Button>())
                {
                    if (btn.BackColor == Color.DarkGray && btn.Enabled)
                    {
                        btn.BackColor = Color.Black;
                    }
                }
                gbYishunScreen7.Visible = false;
            }
            else if (gbnexScreen11.Visible)
            {
                foreach (var btn in gbnexScreen11.Controls.OfType<Button>())
                {
                    if (btn.BackColor == Color.DarkGray && btn.Enabled)
                    {
                        btn.BackColor = Color.Black;
                    }
                }
                gbnexScreen11.Visible = false;
            }
            else if (gbJCubeScreen4.Visible)
            {
                foreach (var btn in gbJCubeScreen4.Controls.OfType<Button>())
                {
                    if (btn.BackColor == Color.DarkGray && btn.Enabled)
                    {
                        btn.BackColor = Color.Black;
                    }
                }
                gbJCubeScreen4.Visible = false;
            }
            else if (gbSeletarScreen9.Visible)
            {
                foreach (var btn in gbSeletarScreen9.Controls.OfType<Button>())
                {
                    if (btn.BackColor == Color.DarkGray && btn.Enabled)
                    {
                        btn.BackColor = Color.Black;
                    }
                }
                gbSeletarScreen9.Visible = false;
            }
            else if (gbTampinesScreen6.Visible)
            {
                foreach (var btn in gbTampinesScreen6.Controls.OfType<Button>())
                {
                    if (btn.BackColor == Color.DarkGray && btn.Enabled)
                    {
                        btn.BackColor = Color.Black;
                    }
                }
                gbTampinesScreen6.Visible = false;
            }
            else if (gbJurongPointScreen10.Visible)
            {
                foreach (var btn in gbJurongPointScreen10.Controls.OfType<Button>())
                {
                    if (btn.BackColor == Color.DarkGray && btn.Enabled)
                    {
                        btn.BackColor = Color.Black;
                    }
                }
                gbJurongPointScreen10.Visible = false;
            }

            tpBooking.Enabled = true;
            tpSeats.Enabled = false;
            tabControl1.SelectTab(0);
            no_of_seats_selected = 0;
            seats_selected = "";
        }

        private void btnSuntecCityScreen1A01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSuntecCityScreen1A01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A01 ";
                btnSuntecCityScreen1A01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A01 ", "");
                btnSuntecCityScreen1A01.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1A02_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1A02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A02 ";
                btnSuntecCityScreen1A02.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A02 ", "");
                btnSuntecCityScreen1A02.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1A03_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1A03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A03 ";
                btnSuntecCityScreen1A03.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A03 ", "");
                btnSuntecCityScreen1A03.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1A04_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1A04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A04 ";
                btnSuntecCityScreen1A04.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A04 ", "");
                btnSuntecCityScreen1A04.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1A05_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1A05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A05 ";
                btnSuntecCityScreen1A05.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A05 ", "");
                btnSuntecCityScreen1A05.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1A06_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1A06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A06 ";
                btnSuntecCityScreen1A06.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A06 ", "");
                btnSuntecCityScreen1A06.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1A07_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1A07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A07 ";
                btnSuntecCityScreen1A07.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A07 ", "");
                btnSuntecCityScreen1A07.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1A08_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1A08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A08 ";
                btnSuntecCityScreen1A08.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A08 ", "");
                btnSuntecCityScreen1A08.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1A09_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1A09.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A09 ";
                btnSuntecCityScreen1A09.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A09 ", "");
                btnSuntecCityScreen1A09.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1A10_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1A10.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A10 ";
                btnSuntecCityScreen1A10.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A10 ", "");
                btnSuntecCityScreen1A10.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1A11_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1A11.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A11 ";
                btnSuntecCityScreen1A11.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A11 ", "");
                btnSuntecCityScreen1A11.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1A12_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1A12.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A12 ";
                btnSuntecCityScreen1A12.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A12 ", "");
                btnSuntecCityScreen1A12.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1A13_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1A13.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A13 ";
                btnSuntecCityScreen1A13.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A13 ", "");
                btnSuntecCityScreen1A13.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1A14_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1A14.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A14 ";
                btnSuntecCityScreen1A14.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A14 ", "");
                btnSuntecCityScreen1A14.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1A15_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1A15.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A15 ";
                btnSuntecCityScreen1A15.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A15 ", "");
                btnSuntecCityScreen1A15.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1A16_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1A16.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A16 ";
                btnSuntecCityScreen1A16.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A16 ", "");
                btnSuntecCityScreen1A16.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1A17_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1A17.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A17 ";
                btnSuntecCityScreen1A17.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A17 ", "");
                btnSuntecCityScreen1A17.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1B01_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1B01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B01 ";
                btnSuntecCityScreen1B01.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B01 ", "");
                btnSuntecCityScreen1B01.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1B02_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1B02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B02 ";
                btnSuntecCityScreen1B02.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B02 ", "");
                btnSuntecCityScreen1B02.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1B03_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1B03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B03 ";
                btnSuntecCityScreen1B03.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B03 ", "");
                btnSuntecCityScreen1B03.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1B04_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1B04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B04 ";
                btnSuntecCityScreen1B04.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B04 ", "");
                btnSuntecCityScreen1B04.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1B05_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1B05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B05 ";
                btnSuntecCityScreen1B05.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B05 ", "");
                btnSuntecCityScreen1B05.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1B06_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1B06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B06 ";
                btnSuntecCityScreen1B06.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B06 ", "");
                btnSuntecCityScreen1B06.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1B07_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1B07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B07 ";
                btnSuntecCityScreen1B07.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B07 ", "");
                btnSuntecCityScreen1B07.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1B08_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1B08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B08 ";
                btnSuntecCityScreen1B08.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B08 ", "");
                btnSuntecCityScreen1B08.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1B09_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1B09.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B09 ";
                btnSuntecCityScreen1B09.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B09 ", "");
                btnSuntecCityScreen1B09.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1B10_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1B10.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B10 ";
                btnSuntecCityScreen1B10.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B10 ", "");
                btnSuntecCityScreen1B10.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1B11_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1B11.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B11 ";
                btnSuntecCityScreen1B11.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B11 ", "");
                btnSuntecCityScreen1B11.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1B12_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1B12.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B12 ";
                btnSuntecCityScreen1B12.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B12 ", "");
                btnSuntecCityScreen1B12.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1B13_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1B13.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B13 ";
                btnSuntecCityScreen1B13.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B13 ", "");
                btnSuntecCityScreen1B13.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1B14_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1B14.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B14 ";
                btnSuntecCityScreen1B14.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B14 ", "");
                btnSuntecCityScreen1B14.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1B15_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1B15.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B15 ";
                btnSuntecCityScreen1B15.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B15 ", "");
                btnSuntecCityScreen1B15.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1B16_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1B16.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B16 ";
                btnSuntecCityScreen1B16.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B16 ", "");
                btnSuntecCityScreen1B16.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1B17_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1B17.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B17 ";
                btnSuntecCityScreen1B17.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B17 ", "");
                btnSuntecCityScreen1B17.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1C01_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1C01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C01 ";
                btnSuntecCityScreen1C01.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C01 ", "");
                btnSuntecCityScreen1C01.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1C02_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1C02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C02 ";
                btnSuntecCityScreen1C02.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C02 ", "");
                btnSuntecCityScreen1C02.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1C03_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1C03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C03 ";
                btnSuntecCityScreen1C03.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C03 ", "");
                btnSuntecCityScreen1C03.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1C04_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1C04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C04 ";
                btnSuntecCityScreen1C04.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C04 ", "");
                btnSuntecCityScreen1C04.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1C05_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1C05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C05 ";
                btnSuntecCityScreen1C05.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C05 ", "");
                btnSuntecCityScreen1C05.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1C06_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1C06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C06 ";
                btnSuntecCityScreen1C06.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C06 ", "");
                btnSuntecCityScreen1C06.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1C07_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1C07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C07 ";
                btnSuntecCityScreen1C07.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C07 ", "");
                btnSuntecCityScreen1C07.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1C08_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1C08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C08 ";
                btnSuntecCityScreen1C08.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C08 ", "");
                btnSuntecCityScreen1C08.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1C09_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1C09.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C09 ";
                btnSuntecCityScreen1C09.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C09 ", "");
                btnSuntecCityScreen1C09.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1C10_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1C10.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C10 ";
                btnSuntecCityScreen1C10.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C10 ", "");
                btnSuntecCityScreen1C10.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1C11_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1C11.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C11 ";
                btnSuntecCityScreen1C11.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C11 ", "");
                btnSuntecCityScreen1C11.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1C12_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1C12.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C12 ";
                btnSuntecCityScreen1C12.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C12 ", "");
                btnSuntecCityScreen1C12.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1C13_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1C13.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C13 ";
                btnSuntecCityScreen1C13.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C13 ", "");
                btnSuntecCityScreen1C13.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1C14_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1C14.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C14 ";
                btnSuntecCityScreen1C14.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C14 ", "");
                btnSuntecCityScreen1C14.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1C15_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1C15.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C15 ";
                btnSuntecCityScreen1C15.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C15 ", "");
                btnSuntecCityScreen1C15.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1C16_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1C16.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C16 ";
                btnSuntecCityScreen1C16.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C16 ", "");
                btnSuntecCityScreen1C16.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1C17_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1C17.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C17 ";
                btnSuntecCityScreen1C17.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C17 ", "");
                btnSuntecCityScreen1C17.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1D01_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1D01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D01 ";
                btnSuntecCityScreen1D01.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D01 ", "");
                btnSuntecCityScreen1D01.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1D02_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1D02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D02 ";
                btnSuntecCityScreen1D02.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D02 ", "");
                btnSuntecCityScreen1D02.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1D03_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1D03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D03 ";
                btnSuntecCityScreen1D03.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D03 ", "");
                btnSuntecCityScreen1D03.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1D04_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1D04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D04 ";
                btnSuntecCityScreen1D04.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D04 ", "");
                btnSuntecCityScreen1D04.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1D05_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1D05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D05 ";
                btnSuntecCityScreen1D05.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D05 ", "");
                btnSuntecCityScreen1D05.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1D06_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1D06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D06 ";
                btnSuntecCityScreen1D06.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D06 ", "");
                btnSuntecCityScreen1D06.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1D07_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1D07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D07 ";
                btnSuntecCityScreen1D07.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D07 ", "");
                btnSuntecCityScreen1D07.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1D08_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1D08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D08 ";
                btnSuntecCityScreen1D08.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D08 ", "");
                btnSuntecCityScreen1D08.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1D09_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1D09.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D09 ";
                btnSuntecCityScreen1D09.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D09 ", "");
                btnSuntecCityScreen1D09.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1D10_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1D10.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D10 ";
                btnSuntecCityScreen1D10.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D10 ", "");
                btnSuntecCityScreen1D10.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1D11_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1D11.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D11 ";
                btnSuntecCityScreen1D11.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D11 ", "");
                btnSuntecCityScreen1D11.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1D12_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1D12.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D12 ";
                btnSuntecCityScreen1D12.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D12 ", "");
                btnSuntecCityScreen1D12.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1D13_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1D13.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D13 ";
                btnSuntecCityScreen1D13.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D13 ", "");
                btnSuntecCityScreen1D13.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1D14_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1D14.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D14 ";
                btnSuntecCityScreen1D14.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D14 ", "");
                btnSuntecCityScreen1D14.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1D15_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1D15.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D15 ";
                btnSuntecCityScreen1D15.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D15 ", "");
                btnSuntecCityScreen1D15.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1D16_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1D16.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D16 ";
                btnSuntecCityScreen1D16.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D16 ", "");
                btnSuntecCityScreen1D16.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1D17_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1D17.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D17 ";
                btnSuntecCityScreen1D17.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D17 ", "");
                btnSuntecCityScreen1D17.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1E01_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1E01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E01 ";
                btnSuntecCityScreen1E01.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E01 ", "");
                btnSuntecCityScreen1E01.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1E02_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1E02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E02 ";
                btnSuntecCityScreen1E02.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E02 ", "");
                btnSuntecCityScreen1E02.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1E03_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1E03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E03 ";
                btnSuntecCityScreen1E03.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E03 ", "");
                btnSuntecCityScreen1E03.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1E04_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1E04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E04 ";
                btnSuntecCityScreen1E04.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E04 ", "");
                btnSuntecCityScreen1E04.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1E05_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1E05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E05 ";
                btnSuntecCityScreen1E05.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E05 ", "");
                btnSuntecCityScreen1E05.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1E06_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1E06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E06 ";
                btnSuntecCityScreen1E06.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E06 ", "");
                btnSuntecCityScreen1E06.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1E07_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1E07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E07 ";
                btnSuntecCityScreen1E07.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E07 ", "");
                btnSuntecCityScreen1E07.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1E08_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1E08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E08 ";
                btnSuntecCityScreen1E08.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E08 ", "");
                btnSuntecCityScreen1E08.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1E09_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1E09.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E09 ";
                btnSuntecCityScreen1E09.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E09 ", "");
                btnSuntecCityScreen1E09.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1E10_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1E10.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E10 ";
                btnSuntecCityScreen1E10.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E10 ", "");
                btnSuntecCityScreen1E10.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1E11_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1E11.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E11 ";
                btnSuntecCityScreen1E11.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E11 ", "");
                btnSuntecCityScreen1E11.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1E12_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1E12.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E12 ";
                btnSuntecCityScreen1E12.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E12 ", "");
                btnSuntecCityScreen1E12.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1E13_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1E13.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E13 ";
                btnSuntecCityScreen1E13.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E13 ", "");
                btnSuntecCityScreen1E13.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1E14_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1E14.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E14 ";
                btnSuntecCityScreen1E14.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E14 ", "");
                btnSuntecCityScreen1E14.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1E15_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1E15.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E15 ";
                btnSuntecCityScreen1E15.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E15 ", "");
                btnSuntecCityScreen1E15.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1E16_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1E16.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E16 ";
                btnSuntecCityScreen1E16.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E16 ", "");
                btnSuntecCityScreen1E16.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1E17_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1E17.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E17 ";
                btnSuntecCityScreen1E17.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E17 ", "");
                btnSuntecCityScreen1E17.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1F01_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1F01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "F01 ";
                btnSuntecCityScreen1F01.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("F01 ", "");
                btnSuntecCityScreen1F01.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1F02_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1F02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "F02 ";
                btnSuntecCityScreen1F02.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("F02 ", "");
                btnSuntecCityScreen1F02.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1F03_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1F03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "F03 ";
                btnSuntecCityScreen1F03.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("F03 ", "");
                btnSuntecCityScreen1F03.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1F04_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1F04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "F04 ";
                btnSuntecCityScreen1F04.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("F04 ", "");
                btnSuntecCityScreen1F04.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1F05_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1F05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "F05 ";
                btnSuntecCityScreen1F05.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("F05 ", "");
                btnSuntecCityScreen1F05.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1F06_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1F06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "F06 ";
                btnSuntecCityScreen1F06.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("F06 ", "");
                btnSuntecCityScreen1F06.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1F07_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1F07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "F07 ";
                btnSuntecCityScreen1F07.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("F07 ", "");
                btnSuntecCityScreen1F07.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1F08_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1F08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "F08 ";
                btnSuntecCityScreen1F08.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("F08 ", "");
                btnSuntecCityScreen1F08.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1F09_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1F09.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "F09 ";
                btnSuntecCityScreen1F09.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("F09 ", "");
                btnSuntecCityScreen1F09.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1F10_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1F10.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "F10 ";
                btnSuntecCityScreen1F10.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("F10 ", "");
                btnSuntecCityScreen1F10.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1F11_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1F11.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "F11 ";
                btnSuntecCityScreen1F11.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("F11 ", "");
                btnSuntecCityScreen1F11.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1F12_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1F12.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "F12 ";
                btnSuntecCityScreen1F12.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("F12 ", "");
                btnSuntecCityScreen1F12.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1F13_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1F13.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "F13 ";
                btnSuntecCityScreen1F13.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("F13 ", "");
                btnSuntecCityScreen1F13.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1F14_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1F14.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "F14 ";
                btnSuntecCityScreen1F14.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("F14 ", "");
                btnSuntecCityScreen1F14.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1F15_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1F15.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "F15 ";
                btnSuntecCityScreen1F15.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("F15 ", "");
                btnSuntecCityScreen1F15.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1F16_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1F16.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "F16 ";
                btnSuntecCityScreen1F16.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("F16 ", "");
                btnSuntecCityScreen1F16.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1F17_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1F17.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "F17 ";
                btnSuntecCityScreen1F17.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("F17 ", "");
                btnSuntecCityScreen1F17.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1G01_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1G01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "G01 ";
                btnSuntecCityScreen1G01.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("G01 ", "");
                btnSuntecCityScreen1G01.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1G02_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1G02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "G02 ";
                btnSuntecCityScreen1G02.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("G02 ", "");
                btnSuntecCityScreen1G02.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1G04_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1G04.BackColor == Color.Black)
            {
                btnSuntecCityScreen1G04.Text = "";
                no_of_seats_selected += 1;
                seats_selected += "G04 ";
                btnSuntecCityScreen1G04.BackColor = Color.DarkGray;
            }
            else
            {
                btnSuntecCityScreen1G04.Text = "W";
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("G04 ", "");
                btnSuntecCityScreen1G04.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1G06_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1G06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "G06 ";
                btnSuntecCityScreen1G06.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("G06 ", "");
                btnSuntecCityScreen1G06.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1G13_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1G13.BackColor == Color.Black)
            {
                btnSuntecCityScreen1G13.Text = "";
                no_of_seats_selected += 1;
                seats_selected += "G13 ";
                btnSuntecCityScreen1G13.BackColor = Color.DarkGray;
            }
            else
            {
                btnSuntecCityScreen1G13.Text = "W";
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("G13 ", "");
                btnSuntecCityScreen1G13.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1G15_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1G15.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "G15 ";
                btnSuntecCityScreen1G15.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("G15 ", "");
                btnSuntecCityScreen1G15.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1G16_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1G16.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "G16 ";
                btnSuntecCityScreen1G16.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("G16 ", "");
                btnSuntecCityScreen1G16.BackColor = Color.Black;
            }
        }

        private void btnSuntecCityScreen1G17_Click(object sender, EventArgs e)
        {
            if (btnSuntecCityScreen1G17.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "G17 ";
                btnSuntecCityScreen1G17.BackColor = Color.DarkGray;
            }
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("G17 ", "");
                btnSuntecCityScreen1G17.BackColor = Color.Black;
            }
        }

        private void txtUpdateNoOfScreens_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        private void comCinema_SelectedIndexChanged(object sender, EventArgs e)
        {
            comMovie.SelectedIndex = 0;
            // remove movies from other cinemas
            for (int i = 0; i < comMovie.Items.Count; i++)
            {
                string st = comMovie.Items[i].ToString();
                if (st != "--Movies--")
                {
                    comMovie.Items.RemoveAt(i);
                    i--;
                }
            }

            // add movies from the selected cinema
            if (comCinema.SelectedIndex == 1)
            {
                comMovie.Enabled = true;
                comMovie.Items.AddRange(new object[]
                {
                    // "Me Before You"
                    movies_available[0]
                });
            }
            else if (comCinema.SelectedIndex == 2)
            {
                comMovie.Enabled = true;
                comMovie.Items.AddRange(new object[]
                {
                    // "Face and Body Swap"
                    movies_available[1]
                });
            }
            else if (comCinema.SelectedIndex == 3)
            {
                comMovie.Enabled = true;
                comMovie.Items.AddRange(new object[]
                {
                    // "Call Me by Your Name"
                    movies_available[2]
                });
            }
            else if (comCinema.SelectedIndex == 4)
            {
                comMovie.Enabled = true;
                comMovie.Items.AddRange(new object[]
                {
                    // "One Day"
                    movies_available[3]
                });
            }
            else if (comCinema.SelectedIndex == 5)
            {
                comMovie.Enabled = true;
                comMovie.Items.AddRange(new object[]
                {
                    // "A Quiet Place"
                    movies_available[4]
                });
            }
            else if (comCinema.SelectedIndex == 6)
            {
                comMovie.Enabled = true;
                comMovie.Items.AddRange(new object[]
                {
                    // "Insidious"
                    movies_available[5]
                });
            }
            else if (comCinema.SelectedIndex == 7)
            {
                comMovie.Enabled = true;
                comMovie.Items.AddRange(new object[]
                {
                    // "The Witch"
                    movies_available[6]
                });
            }
            else if (comCinema.SelectedIndex == 8)
            {
                comMovie.Enabled = true;
                comMovie.Items.AddRange(new object[]
                {
                    // "The Ritual"
                    movies_available[7]
                });
            }
            else if (comCinema.SelectedIndex == 9)
            {
                comMovie.Enabled = true;
                comMovie.Items.AddRange(new object[]
                {
                    // "The Conjuring"
                    movies_available[8]
                });
            }
            else if (comCinema.SelectedIndex == 10)
            {
                comMovie.Enabled = true;
                comMovie.Items.AddRange(new object[]
                {
                    // "Halloween"
                    movies_available[9]
                });
            }
            else
            {
                comMovie.Enabled = false;
            }
        }

        private void comDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            comTime.SelectedIndex = 0;

            for (int i = 0; i < comTime.Items.Count; i++)
            {
                string st = comTime.Items[i].ToString();
                if (st != "--Time--")
                {
                    comTime.Items.RemoveAt(i);
                    i--;
                }
            }

            // DV Suntec City, "Me Before You" and "02-01-2019" selected
            if (comCinema.SelectedIndex == 1 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1)
            {
                comTime.Enabled = true;
                comTime.Items.AddRange(new object[]
                {
                    // "12:00 PM"
                    times_available[0]
                });
            }
            // DV Plaza, "Face and Body Swap" and "04-01-2019" selected
            else if (comCinema.SelectedIndex == 2 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1)
            {
                comTime.Enabled = true;
                comTime.Items.AddRange(new object[]
                {
                    // "3:00 PM"
                    times_available[2]
                });
            }
            // DV City Square, "Call Me by Your Name" and "05-01-2019" selected
            else if (comCinema.SelectedIndex == 3 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1)
            {
                comTime.Enabled = true;
                comTime.Items.AddRange(new object[]
                {
                    // "2:00 PM"
                    times_available[1]
                });
            }
            // DV Bishan, "One Day" and "07-01-2019" selected
            else if (comCinema.SelectedIndex == 4 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1)
            {
                comTime.Enabled = true;
                comTime.Items.AddRange(new object[]
                {
                    // "12:00 PM"
                    times_available[0]
                });
            }
            // DV Yishun, "A Quiet Place" and "02-01-2019" selected
            else if (comCinema.SelectedIndex == 5 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1)
            {
                comTime.Enabled = true;
                comTime.Items.AddRange(new object[]
                {
                    // "12:00 PM"
                    times_available[0]
                });
            }
            // DV nex, "Insidious" and "09-01-2019" selected
            else if (comCinema.SelectedIndex == 6 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1)
            {
                comTime.Enabled = true;
                comTime.Items.AddRange(new object[]
                {
                    // "2:30 PM"
                    times_available[3]
                });
            }
            // DV JCube, "The Witch" and "09-01-2019" selected
            else if (comCinema.SelectedIndex == 7 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1)
            {
                comTime.Enabled = true;
                comTime.Items.AddRange(new object[]
                {
                    // "2:00 PM"
                    times_available[1]
                });
            }
            // DV Seletar, "The Ritual" and "12-01-2019" selected
            else if (comCinema.SelectedIndex == 8 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1)
            {
                comTime.Enabled = true;
                comTime.Items.AddRange(new object[]
                {
                    // "6:00 PM"
                    times_available[4]
                });
            }
            // DV Tampines, "The Conjuring" and "12-01-2019" selected
            else if (comCinema.SelectedIndex == 9 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1)
            {
                comTime.Enabled = true;
                comTime.Items.AddRange(new object[]
                {
                    // "2:30 PM"
                    times_available[3]
                });
            }
            // DV Jurong Point, "Halloween" and "09-01-2019" selected
            else if (comCinema.SelectedIndex == 10 && comMovie.SelectedIndex == 1 && comDate.SelectedIndex == 1)
            {
                comTime.Enabled = true;
                comTime.Items.AddRange(new object[]
                {
                    // "6:00 PM"
                    times_available[4]
                });
            }
            else
            {
                comTime.Enabled = false;
            }
        }

        private void comMovie_SelectedIndexChanged(object sender, EventArgs e)
        {
            comDate.SelectedIndex = 0;

            for (int i = 0; i < comDate.Items.Count; i++)
            {
                string st = comDate.Items[i].ToString();
                if (st != "--Date--")
                {
                    comDate.Items.RemoveAt(i);
                    i--;
                }
            }

            // DV Suntec City and "Me Before You" selected
            if (comCinema.SelectedIndex == 1 && comMovie.SelectedIndex == 1)
            {
                comDate.Enabled = true;
                // add dates from the selected cinema and movie
                comDate.Items.AddRange(new object[]
                {
                    // "02-01-2019"
                    dates_available[0]
                });
            }
            // DV Plaza and "Face and Body Swap" selected
            else if (comCinema.SelectedIndex == 2 && comMovie.SelectedIndex == 1)
            {
                comDate.Enabled = true;
                // add dates from the selected cinema and movie
                comDate.Items.AddRange(new object[]
                {
                    // "04-01-2019"
                    dates_available[1]
                });
            }
            // DV City Square and "Call Me by Your Name" selected
            else if (comCinema.SelectedIndex == 3 && comMovie.SelectedIndex == 1)
            {
                comDate.Enabled = true;
                // add dates from the selected cinema and movie
                comDate.Items.AddRange(new object[]
                {
                    // "05-01-2019"
                    dates_available[2]
                });
            }
            // DV Bishan and "One Day" selected
            else if (comCinema.SelectedIndex == 4 && comMovie.SelectedIndex == 1)
            {
                comDate.Enabled = true;
                // add dates from the selected cinema and movie
                comDate.Items.AddRange(new object[]
                {
                    // "07-01-2019"
                    dates_available[3]
                });
            }
            // DV Yishun and "A Quiet Place" selected
            else if (comCinema.SelectedIndex == 5 && comMovie.SelectedIndex == 1)
            {
                comDate.Enabled = true;
                // add dates from the selected cinema and movie
                comDate.Items.AddRange(new object[]
                {
                    // "02-01-2019"
                    dates_available[0]
                });
            }
            // DV nex and "Insidious" selected
            else if (comCinema.SelectedIndex == 6 && comMovie.SelectedIndex == 1)
            {
                comDate.Enabled = true;
                // add dates from the selected cinema and movie
                comDate.Items.AddRange(new object[]
                {
                    // "09-01-2019"
                    dates_available[4]
                });
            }
            // DV JCube and "The Witch" selected
            else if (comCinema.SelectedIndex == 7 && comMovie.SelectedIndex == 1)
            {
                comDate.Enabled = true;
                // add dates from the selected cinema and movie
                comDate.Items.AddRange(new object[]
                {
                    // "09-01-2019"
                    dates_available[4]
                });
            }
            // DV JCube and "The Witch" selected
            else if (comCinema.SelectedIndex == 7 && comMovie.SelectedIndex == 1)
            {
                comDate.Enabled = true;
                // add dates from the selected cinema and movie
                comDate.Items.AddRange(new object[]
                {
                    // "09-01-2019"
                    dates_available[4]
                });
            }
            // DV Seletar and "The Ritual" selected
            else if (comCinema.SelectedIndex == 8 && comMovie.SelectedIndex == 1)
            {
                comDate.Enabled = true;
                // add dates from the selected cinema and movie
                comDate.Items.AddRange(new object[]
                {
                    // "12-01-2019"
                    dates_available[5]
                });                 
            }
            // DV Tampines and "The Conjuring" selected
            else if (comCinema.SelectedIndex == 9 && comMovie.SelectedIndex == 1)
            {
                comDate.Enabled = true;
                // add dates from the selected cinema and movie
                comDate.Items.AddRange(new object[]
                {
                    // "12-01-2019"
                    dates_available[5]
                });
            }
            // DV Jurong Point and "Halloween" selected
            else if (comCinema.SelectedIndex == 10 && comMovie.SelectedIndex == 1)
            {
                comDate.Enabled = true;
                // add dates from the selected cinema and movie
                comDate.Items.AddRange(new object[]
                {
                    // "09-01-2019"
                    dates_available[4]
                });
            }
            else
            {
                comDate.Enabled = false;
            }
        }

        private void txtCardNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        private void txtSecurityCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        private void btnPlazaScreen5A01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5A01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A01 ";
                btnPlazaScreen5A01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A01 ", "");
                btnPlazaScreen5A01.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5A02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5A02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A02 ";
                btnPlazaScreen5A02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A02 ", "");
                btnPlazaScreen5A02.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5A03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5A03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A03 ";
                btnPlazaScreen5A03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A03 ", "");
                btnPlazaScreen5A03.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5A04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5A04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A04 ";
                btnPlazaScreen5A04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A04 ", "");
                btnPlazaScreen5A04.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5A05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5A05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A05 ";
                btnPlazaScreen5A05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A05 ", "");
                btnPlazaScreen5A05.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5A06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5A06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A06 ";
                btnPlazaScreen5A06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A06 ", "");
                btnPlazaScreen5A06.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5A07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5A07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A07 ";
                btnPlazaScreen5A07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A07 ", "");
                btnPlazaScreen5A07.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5A08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5A08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A08 ";
                btnPlazaScreen5A08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A08 ", "");
                btnPlazaScreen5A08.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5B01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5B01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B01 ";
                btnPlazaScreen5B01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B01 ", "");
                btnPlazaScreen5B01.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5B02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5B02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B02 ";
                btnPlazaScreen5B02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B02 ", "");
                btnPlazaScreen5B02.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5B03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5B03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B03 ";
                btnPlazaScreen5B03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B03 ", "");
                btnPlazaScreen5B03.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5B04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5B04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B04 ";
                btnPlazaScreen5B04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B04 ", "");
                btnPlazaScreen5B04.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5B05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5B05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B05 ";
                btnPlazaScreen5B05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B05 ", "");
                btnPlazaScreen5B05.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5B06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5B06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B06 ";
                btnPlazaScreen5B06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B06 ", "");
                btnPlazaScreen5B06.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5B07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5B07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B07 ";
                btnPlazaScreen5B07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B07 ", "");
                btnPlazaScreen5B07.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5B08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5B08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B08 ";
                btnPlazaScreen5B08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B08 ", "");
                btnPlazaScreen5B08.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5C01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5C01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C01 ";
                btnPlazaScreen5C01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C01 ", "");
                btnPlazaScreen5C01.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5C02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5C02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C02 ";
                btnPlazaScreen5C02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C02 ", "");
                btnPlazaScreen5C02.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5C03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5C03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C03 ";
                btnPlazaScreen5C03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C03 ", "");
                btnPlazaScreen5C03.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5C04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5C04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C04 ";
                btnPlazaScreen5C04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C04 ", "");
                btnPlazaScreen5C04.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5C05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5C05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C05 ";
                btnPlazaScreen5C05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C05 ", "");
                btnPlazaScreen5C05.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5C06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5C06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C06 ";
                btnPlazaScreen5C06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C06 ", "");
                btnPlazaScreen5C06.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5C07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5C07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C07 ";
                btnPlazaScreen5C07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C07 ", "");
                btnPlazaScreen5C07.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5C08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5C08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C08 ";
                btnPlazaScreen5C08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C08 ", "");
                btnPlazaScreen5C08.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5D01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5D01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D01 ";
                btnPlazaScreen5D01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D01 ", "");
                btnPlazaScreen5D01.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5D02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5D02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D02 ";
                btnPlazaScreen5D02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D02 ", "");
                btnPlazaScreen5D02.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5D03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5D03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D03 ";
                btnPlazaScreen5D03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D03 ", "");
                btnPlazaScreen5D03.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5D04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5D04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D04 ";
                btnPlazaScreen5D04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D04 ", "");
                btnPlazaScreen5D04.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5D05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5D05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D05 ";
                btnPlazaScreen5D05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D05 ", "");
                btnPlazaScreen5D05.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5D06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5D06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D06 ";
                btnPlazaScreen5D06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D06 ", "");
                btnPlazaScreen5D06.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5D07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5D07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D07 ";
                btnPlazaScreen5D07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D07 ", "");
                btnPlazaScreen5D07.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5D08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5D08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D08 ";
                btnPlazaScreen5D08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D08 ", "");
                btnPlazaScreen5D08.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5E01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5E01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E01 ";
                btnPlazaScreen5E01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E01 ", "");
                btnPlazaScreen5E01.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5E02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5E02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E02 ";
                btnPlazaScreen5E02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E02 ", "");
                btnPlazaScreen5E02.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5E03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5E03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E03 ";
                btnPlazaScreen5E03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E03 ", "");
                btnPlazaScreen5E03.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5E04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5E04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E04 ";
                btnPlazaScreen5E04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E04 ", "");
                btnPlazaScreen5E04.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5E05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5E05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E05 ";
                btnPlazaScreen5E05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E05 ", "");
                btnPlazaScreen5E05.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5E06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5E06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E06 ";
                btnPlazaScreen5E06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E06 ", "");
                btnPlazaScreen5E06.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5E07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5E07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E07 ";
                btnPlazaScreen5E07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E07 ", "");
                btnPlazaScreen5E07.BackColor = Color.Black;
            }
        }

        private void btnPlazaScreen5E08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnPlazaScreen5E08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "E08 ";
                btnPlazaScreen5E08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("E08 ", "");
                btnPlazaScreen5E08.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2A01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2A01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A01 ";
                btnCitySquareScreen2A01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A01 ", "");
                btnCitySquareScreen2A01.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2A02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2A02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A02 ";
                btnCitySquareScreen2A02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A02 ", "");
                btnCitySquareScreen2A02.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2A03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2A03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A03 ";
                btnCitySquareScreen2A03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A03 ", "");
                btnCitySquareScreen2A03.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2A04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2A04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A04 ";
                btnCitySquareScreen2A04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A04 ", "");
                btnCitySquareScreen2A04.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2A05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2A05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A05 ";
                btnCitySquareScreen2A05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A05 ", "");
                btnCitySquareScreen2A05.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2A06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2A06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A06 ";
                btnCitySquareScreen2A06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A06 ", "");
                btnCitySquareScreen2A06.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2A07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2A07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A07 ";
                btnCitySquareScreen2A07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A07 ", "");
                btnCitySquareScreen2A07.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2A08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2A08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A08 ";
                btnCitySquareScreen2A08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A08 ", "");
                btnCitySquareScreen2A08.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2B01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2B01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B01 ";
                btnCitySquareScreen2B01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B01 ", "");
                btnCitySquareScreen2B01.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2B02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2B02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B02 ";
                btnCitySquareScreen2B02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B02 ", "");
                btnCitySquareScreen2B02.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2B03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2B03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B03 ";
                btnCitySquareScreen2B03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B03 ", "");
                btnCitySquareScreen2B03.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2B04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2B04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B04 ";
                btnCitySquareScreen2B04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B04 ", "");
                btnCitySquareScreen2B04.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2B05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2B05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B05 ";
                btnCitySquareScreen2B05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B05 ", "");
                btnCitySquareScreen2B05.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2B06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2B06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B06 ";
                btnCitySquareScreen2B06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B06 ", "");
                btnCitySquareScreen2B06.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2B07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2B07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B07 ";
                btnCitySquareScreen2B07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B07 ", "");
                btnCitySquareScreen2B07.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2B08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2B08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B08 ";
                btnCitySquareScreen2B08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B08 ", "");
                btnCitySquareScreen2B08.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2C01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2C01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C01 ";
                btnCitySquareScreen2C01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C01 ", "");
                btnCitySquareScreen2C01.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2C02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2C02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C02 ";
                btnCitySquareScreen2C02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C02 ", "");
                btnCitySquareScreen2C02.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2C03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2C03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C03 ";
                btnCitySquareScreen2C03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C03 ", "");
                btnCitySquareScreen2C03.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2C04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2C04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C04 ";
                btnCitySquareScreen2C04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C04 ", "");
                btnCitySquareScreen2C04.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2C05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2C05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C05 ";
                btnCitySquareScreen2C05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C05 ", "");
                btnCitySquareScreen2C05.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2C06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2C06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C06 ";
                btnCitySquareScreen2C06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C06 ", "");
                btnCitySquareScreen2C06.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2C07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2C07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C07 ";
                btnCitySquareScreen2C07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C07 ", "");
                btnCitySquareScreen2C07.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2C08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2C08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C08 ";
                btnCitySquareScreen2C08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C08 ", "");
                btnCitySquareScreen2C08.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2D01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2D01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D01 ";
                btnCitySquareScreen2D01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D01 ", "");
                btnCitySquareScreen2D01.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2D02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2D02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D02 ";
                btnCitySquareScreen2D02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D02 ", "");
                btnCitySquareScreen2D02.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2D03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2D03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D03 ";
                btnCitySquareScreen2D03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D03 ", "");
                btnCitySquareScreen2D03.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2D04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2D04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D04 ";
                btnCitySquareScreen2D04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D04 ", "");
                btnCitySquareScreen2D04.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2D05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2D05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D05 ";
                btnCitySquareScreen2D05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D05 ", "");
                btnCitySquareScreen2D05.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2D06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2D06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D06 ";
                btnCitySquareScreen2D06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D06 ", "");
                btnCitySquareScreen2D06.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2D07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2D07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D07 ";
                btnCitySquareScreen2D07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D07 ", "");
                btnCitySquareScreen2D07.BackColor = Color.Black;
            }
        }

        private void btnCitySquareScreen2D08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnCitySquareScreen2D08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D08 ";
                btnCitySquareScreen2D08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D08 ", "");
                btnCitySquareScreen2D08.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3A01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3A01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A01 ";
                btnBishanScreen3A01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A01 ", "");
                btnBishanScreen3A01.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3A02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3A02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A02 ";
                btnBishanScreen3A02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A02 ", "");
                btnBishanScreen3A02.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3A03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3A03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A03 ";
                btnBishanScreen3A03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A03 ", "");
                btnBishanScreen3A03.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3A04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3A04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A04 ";
                btnBishanScreen3A04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A04 ", "");
                btnBishanScreen3A04.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3A05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3A05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A05 ";
                btnBishanScreen3A05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A05 ", "");
                btnBishanScreen3A05.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3A06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3A06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A06 ";
                btnBishanScreen3A06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A06 ", "");
                btnBishanScreen3A06.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3A07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3A07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A07 ";
                btnBishanScreen3A07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A07 ", "");
                btnBishanScreen3A07.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3A08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3A08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A08 ";
                btnBishanScreen3A08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A08 ", "");
                btnBishanScreen3A08.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3B01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3B01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B01 ";
                btnBishanScreen3B01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B01 ", "");
                btnBishanScreen3B01.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3B02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3B02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B02 ";
                btnBishanScreen3B02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B02 ", "");
                btnBishanScreen3B02.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3B03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3B03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B03 ";
                btnBishanScreen3B03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B03 ", "");
                btnBishanScreen3B03.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3B04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3B04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B04 ";
                btnBishanScreen3B04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B04 ", "");
                btnBishanScreen3B04.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3B05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3B05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B05 ";
                btnBishanScreen3B05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B05 ", "");
                btnBishanScreen3B05.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3B06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3B06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B06 ";
                btnBishanScreen3B06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B06 ", "");
                btnBishanScreen3B06.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3B07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3B07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B07 ";
                btnBishanScreen3B07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B07 ", "");
                btnBishanScreen3B07.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3B08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3B08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B08 ";
                btnBishanScreen3B08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B08 ", "");
                btnBishanScreen3B08.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3C01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3C01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C01 ";
                btnBishanScreen3C01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C01 ", "");
                btnBishanScreen3C01.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3C02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3C02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C02 ";
                btnBishanScreen3C02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C02 ", "");
                btnBishanScreen3C02.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3C03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3C03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C03 ";
                btnBishanScreen3C03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C03 ", "");
                btnBishanScreen3C03.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3C04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3C04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C04 ";
                btnBishanScreen3C04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C04 ", "");
                btnBishanScreen3C04.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3C05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3C05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C05 ";
                btnBishanScreen3C05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C05 ", "");
                btnBishanScreen3C05.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3C06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3C06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C06 ";
                btnBishanScreen3C06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C06 ", "");
                btnBishanScreen3C06.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3C07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3C07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C07 ";
                btnBishanScreen3C07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C07 ", "");
                btnBishanScreen3C07.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3C08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3C08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C08 ";
                btnBishanScreen3C08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C08 ", "");
                btnBishanScreen3C08.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3D01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3D01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D01 ";
                btnBishanScreen3D01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D01 ", "");
                btnBishanScreen3D01.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3D02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3D02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D02 ";
                btnBishanScreen3D02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D02 ", "");
                btnBishanScreen3D02.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3D03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3D03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D03 ";
                btnBishanScreen3D03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D03 ", "");
                btnBishanScreen3D03.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3D04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3D04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D04 ";
                btnBishanScreen3D04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D04 ", "");
                btnBishanScreen3D04.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3D05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3D05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D05 ";
                btnBishanScreen3D05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D05 ", "");
                btnBishanScreen3D05.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3D06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3D06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D06 ";
                btnBishanScreen3D06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D06 ", "");
                btnBishanScreen3D06.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3D07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3D07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D07 ";
                btnBishanScreen3D07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D07 ", "");
                btnBishanScreen3D07.BackColor = Color.Black;
            }
        }

        private void btnBishanScreen3D08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnBishanScreen3D08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D08 ";
                btnBishanScreen3D08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D08 ", "");
                btnBishanScreen3D08.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7A01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7A01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A01 ";
                btnYishunScreen7A01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A01 ", "");
                btnYishunScreen7A01.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7A02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7A02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A02 ";
                btnYishunScreen7A02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A02 ", "");
                btnYishunScreen7A02.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7A03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7A03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A03 ";
                btnYishunScreen7A03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A03 ", "");
                btnYishunScreen7A03.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7A04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7A04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A04 ";
                btnYishunScreen7A04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A04 ", "");
                btnYishunScreen7A04.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7A05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7A05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A05 ";
                btnYishunScreen7A05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A05 ", "");
                btnYishunScreen7A05.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7A06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7A06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A06 ";
                btnYishunScreen7A06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A06 ", "");
                btnYishunScreen7A06.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7A07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7A07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A07 ";
                btnYishunScreen7A07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A07 ", "");
                btnYishunScreen7A07.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7A08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7A08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A08 ";
                btnYishunScreen7A08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A08 ", "");
                btnYishunScreen7A08.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7B01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7B01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B01 ";
                btnYishunScreen7B01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B01 ", "");
                btnYishunScreen7B01.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7B02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7B02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B02 ";
                btnYishunScreen7B02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B02 ", "");
                btnYishunScreen7B02.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7B03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7B03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B03 ";
                btnYishunScreen7B03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B03 ", "");
                btnYishunScreen7B03.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7B04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7B04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B04 ";
                btnYishunScreen7B04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B04 ", "");
                btnYishunScreen7B04.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7B05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7B05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B05 ";
                btnYishunScreen7B05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B05 ", "");
                btnYishunScreen7B05.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7B06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7B06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B06 ";
                btnYishunScreen7B06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B06 ", "");
                btnYishunScreen7B06.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7B07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7B07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B07 ";
                btnYishunScreen7B07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B07 ", "");
                btnYishunScreen7B07.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7B08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7B08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B08 ";
                btnYishunScreen7B08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B08 ", "");
                btnYishunScreen7B08.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7C01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7C01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C01 ";
                btnYishunScreen7C01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C01 ", "");
                btnYishunScreen7C01.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7C02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7C02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C02 ";
                btnYishunScreen7C02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C02 ", "");
                btnYishunScreen7C02.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7C03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7C03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C03 ";
                btnYishunScreen7C03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C03 ", "");
                btnYishunScreen7C03.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7C04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7C04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C04 ";
                btnYishunScreen7C04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C04 ", "");
                btnYishunScreen7C04.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7C05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7C05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C05 ";
                btnYishunScreen7C05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C05 ", "");
                btnYishunScreen7C05.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7C06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7C06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C06 ";
                btnYishunScreen7C06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C06 ", "");
                btnYishunScreen7C06.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7C07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7C07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C07 ";
                btnYishunScreen7C07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C07 ", "");
                btnYishunScreen7C07.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7C08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7C08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C08 ";
                btnYishunScreen7C08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C08 ", "");
                btnYishunScreen7C08.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7D01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7D01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D01 ";
                btnYishunScreen7D01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D01 ", "");
                btnYishunScreen7D01.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7D02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7D02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D02 ";
                btnYishunScreen7D02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D02 ", "");
                btnYishunScreen7D02.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7D03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7D03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D03 ";
                btnYishunScreen7D03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D03 ", "");
                btnYishunScreen7D03.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7D04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7D04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D04 ";
                btnYishunScreen7D04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D04 ", "");
                btnYishunScreen7D04.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7D05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7D05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D05 ";
                btnYishunScreen7D05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D05 ", "");
                btnYishunScreen7D05.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7D06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7D06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D06 ";
                btnYishunScreen7D06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D06 ", "");
                btnYishunScreen7D06.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7D07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7D07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D07 ";
                btnYishunScreen7D07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D07 ", "");
                btnYishunScreen7D07.BackColor = Color.Black;
            }
        }

        private void btnYishunScreen7D08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnYishunScreen7D08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D08 ";
                btnYishunScreen7D08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D08 ", "");
                btnYishunScreen7D08.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11A01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11A01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A01 ";
                btnNexScreen11A01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A01 ", "");
                btnNexScreen11A01.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11A02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11A02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A02 ";
                btnNexScreen11A02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A02 ", "");
                btnNexScreen11A02.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11A03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11A03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A03 ";
                btnNexScreen11A03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A03 ", "");
                btnNexScreen11A03.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11A04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11A04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A04 ";
                btnNexScreen11A04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A04 ", "");
                btnNexScreen11A04.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11A05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11A05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A05 ";
                btnNexScreen11A05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A05 ", "");
                btnNexScreen11A05.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11A06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11A06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A06 ";
                btnNexScreen11A06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A06 ", "");
                btnNexScreen11A06.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11A07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11A07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A07 ";
                btnNexScreen11A07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A07 ", "");
                btnNexScreen11A07.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11A08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11A08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A08 ";
                btnNexScreen11A08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A08 ", "");
                btnNexScreen11A08.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11B01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11B01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B01 ";
                btnNexScreen11B01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B01 ", "");
                btnNexScreen11B01.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11B02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11B02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B02 ";
                btnNexScreen11B02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B02 ", "");
                btnNexScreen11B02.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11B03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11B03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B03 ";
                btnNexScreen11B03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B03 ", "");
                btnNexScreen11B03.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11B04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11B04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B04 ";
                btnNexScreen11B04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B04 ", "");
                btnNexScreen11B04.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11B05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11B05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B05 ";
                btnNexScreen11B05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B05 ", "");
                btnNexScreen11B05.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11B06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11B06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B06 ";
                btnNexScreen11B06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B06 ", "");
                btnNexScreen11B06.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11B07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11B07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B07 ";
                btnNexScreen11B07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B07 ", "");
                btnNexScreen11B07.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11B08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11B08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B08 ";
                btnNexScreen11B08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B08 ", "");
                btnNexScreen11B08.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11C01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11C01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C01 ";
                btnNexScreen11C01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C01 ", "");
                btnNexScreen11C01.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11C02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11C02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C02 ";
                btnNexScreen11C02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C02 ", "");
                btnNexScreen11C02.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11C03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11C03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C03 ";
                btnNexScreen11C03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C03 ", "");
                btnNexScreen11C03.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11C04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11C04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C04 ";
                btnNexScreen11C04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C04 ", "");
                btnNexScreen11C04.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11C05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11C05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C05 ";
                btnNexScreen11C05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C05 ", "");
                btnNexScreen11C05.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11C06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11C06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C06 ";
                btnNexScreen11C06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C06 ", "");
                btnNexScreen11C06.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11C07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11C07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C07 ";
                btnNexScreen11C07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C07 ", "");
                btnNexScreen11C07.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11C08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11C08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C08 ";
                btnNexScreen11C08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C08 ", "");
                btnNexScreen11C08.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11D01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11D01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D01 ";
                btnNexScreen11D01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D01 ", "");
                btnNexScreen11D01.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11D02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11D02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D02 ";
                btnNexScreen11D02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D02 ", "");
                btnNexScreen11D02.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11D03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11D03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D03 ";
                btnNexScreen11D03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D03 ", "");
                btnNexScreen11D03.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11D04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11D04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D04 ";
                btnNexScreen11D04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D04 ", "");
                btnNexScreen11D04.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11D05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11D05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D05 ";
                btnNexScreen11D05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D05 ", "");
                btnNexScreen11D05.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11D06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11D06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D06 ";
                btnNexScreen11D06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D06 ", "");
                btnNexScreen11D06.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11D07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11D07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D07 ";
                btnNexScreen11D07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D07 ", "");
                btnNexScreen11D07.BackColor = Color.Black;
            }
        }

        private void btnNexScreen11D08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnNexScreen11D08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D08 ";
                btnNexScreen11D08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D08 ", "");
                btnNexScreen11D08.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4A01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4A01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A01 ";
                btnJCubeScreen4A01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A01 ", "");
                btnJCubeScreen4A01.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4A02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4A02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A02 ";
                btnJCubeScreen4A02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A02 ", "");
                btnJCubeScreen4A02.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4A03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4A03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A03 ";
                btnJCubeScreen4A03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A03 ", "");
                btnJCubeScreen4A03.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4A04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4A04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A04 ";
                btnJCubeScreen4A04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A04 ", "");
                btnJCubeScreen4A04.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4A05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4A05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A05 ";
                btnJCubeScreen4A05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A05 ", "");
                btnJCubeScreen4A05.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4A06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4A06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A06 ";
                btnJCubeScreen4A06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A06 ", "");
                btnJCubeScreen4A06.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4A07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4A07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A07 ";
                btnJCubeScreen4A07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A07 ", "");
                btnJCubeScreen4A07.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4A08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4A08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A08 ";
                btnJCubeScreen4A08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A08 ", "");
                btnJCubeScreen4A08.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4B01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4B01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B01 ";
                btnJCubeScreen4B01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B01 ", "");
                btnJCubeScreen4B01.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4B02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4B02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B02 ";
                btnJCubeScreen4B02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B02 ", "");
                btnJCubeScreen4B02.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4B03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4B03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B03 ";
                btnJCubeScreen4B03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B03 ", "");
                btnJCubeScreen4B03.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4B04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4B04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B04 ";
                btnJCubeScreen4B04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B04 ", "");
                btnJCubeScreen4B04.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4B05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4B05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B05 ";
                btnJCubeScreen4B05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B05 ", "");
                btnJCubeScreen4B05.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4B06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4B06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B06 ";
                btnJCubeScreen4B06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B06 ", "");
                btnJCubeScreen4B06.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4B07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4B07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B07 ";
                btnJCubeScreen4B07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B07 ", "");
                btnJCubeScreen4B07.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4B08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4B08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B08 ";
                btnJCubeScreen4B08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B08 ", "");
                btnJCubeScreen4B08.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4C01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4C01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C01 ";
                btnJCubeScreen4C01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C01 ", "");
                btnJCubeScreen4C01.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4C02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4C02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C02 ";
                btnJCubeScreen4C02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C02 ", "");
                btnJCubeScreen4C02.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4C03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4C03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C03 ";
                btnJCubeScreen4C03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C03 ", "");
                btnJCubeScreen4C03.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4C04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4C04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C04 ";
                btnJCubeScreen4C04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C04 ", "");
                btnJCubeScreen4C04.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4C05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4C05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C05 ";
                btnJCubeScreen4C05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C05 ", "");
                btnJCubeScreen4C05.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4C06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4C06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C06 ";
                btnJCubeScreen4C06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C06 ", "");
                btnJCubeScreen4C06.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4C07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4C07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C07 ";
                btnJCubeScreen4C07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C07 ", "");
                btnJCubeScreen4C07.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4C08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4C08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C08 ";
                btnJCubeScreen4C08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C08 ", "");
                btnJCubeScreen4C08.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4D01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4D01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D01 ";
                btnJCubeScreen4D01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D01 ", "");
                btnJCubeScreen4D01.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4D02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4D02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D02 ";
                btnJCubeScreen4D02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D02 ", "");
                btnJCubeScreen4D02.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4D03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4D03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D03 ";
                btnJCubeScreen4D03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D03 ", "");
                btnJCubeScreen4D03.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4D04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4D04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D04 ";
                btnJCubeScreen4D04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D04 ", "");
                btnJCubeScreen4D04.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4D05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4D05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D05 ";
                btnJCubeScreen4D05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D05 ", "");
                btnJCubeScreen4D05.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4D06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4D06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D06 ";
                btnJCubeScreen4D06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D06 ", "");
                btnJCubeScreen4D06.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4D07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4D07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D07 ";
                btnJCubeScreen4D07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D07 ", "");
                btnJCubeScreen4D07.BackColor = Color.Black;
            }
        }

        private void btnJCubeScreen4D08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJCubeScreen4D08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D08 ";
                btnJCubeScreen4D08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D08 ", "");
                btnJCubeScreen4D08.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9A01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9A01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A01 ";
                btnSeletarScreen9A01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A01 ", "");
                btnSeletarScreen9A01.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9A02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9A02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A02 ";
                btnSeletarScreen9A02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A02 ", "");
                btnSeletarScreen9A02.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9A03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9A03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A03 ";
                btnSeletarScreen9A03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A03 ", "");
                btnSeletarScreen9A03.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9A04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9A04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A04 ";
                btnSeletarScreen9A04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A04 ", "");
                btnSeletarScreen9A04.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9A05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9A05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A05 ";
                btnSeletarScreen9A05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A05 ", "");
                btnSeletarScreen9A05.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9A06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9A06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A06 ";
                btnSeletarScreen9A06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A06 ", "");
                btnSeletarScreen9A06.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9A07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9A07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A07 ";
                btnSeletarScreen9A07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A07 ", "");
                btnSeletarScreen9A07.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9A08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9A08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A08 ";
                btnSeletarScreen9A08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A08 ", "");
                btnSeletarScreen9A08.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9B01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9B01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B01 ";
                btnSeletarScreen9B01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B01 ", "");
                btnSeletarScreen9B01.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9B02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9B02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B02 ";
                btnSeletarScreen9B02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B02 ", "");
                btnSeletarScreen9B02.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9B03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9B03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B03 ";
                btnSeletarScreen9B03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B03 ", "");
                btnSeletarScreen9B03.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9B04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9B04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B04 ";
                btnSeletarScreen9B04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B04 ", "");
                btnSeletarScreen9B04.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9B05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9B05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B05 ";
                btnSeletarScreen9B05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B05 ", "");
                btnSeletarScreen9B05.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9B06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9B06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B06 ";
                btnSeletarScreen9B06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B06 ", "");
                btnSeletarScreen9B06.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9B07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9B07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B07 ";
                btnSeletarScreen9B07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B07 ", "");
                btnSeletarScreen9B07.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9B08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9B08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B08 ";
                btnSeletarScreen9B08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B08 ", "");
                btnSeletarScreen9B08.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9C01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9C01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C01 ";
                btnSeletarScreen9C01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C01 ", "");
                btnSeletarScreen9C01.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9C02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9C02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C02 ";
                btnSeletarScreen9C02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C02 ", "");
                btnSeletarScreen9C02.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9C03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9C03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C03 ";
                btnSeletarScreen9C03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C03 ", "");
                btnSeletarScreen9C03.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9C04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9C04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C04 ";
                btnSeletarScreen9C04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C04 ", "");
                btnSeletarScreen9C04.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9C05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9C05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C05 ";
                btnSeletarScreen9C05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C05 ", "");
                btnSeletarScreen9C05.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9C06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9C06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C06 ";
                btnSeletarScreen9C06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C06 ", "");
                btnSeletarScreen9C06.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9C07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9C07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C07 ";
                btnSeletarScreen9C07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C07 ", "");
                btnSeletarScreen9C07.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9C08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9C08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C08 ";
                btnSeletarScreen9C08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C08 ", "");
                btnSeletarScreen9C08.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9D01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9D01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D01 ";
                btnSeletarScreen9D01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D01 ", "");
                btnSeletarScreen9D01.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9D02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9D02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D02 ";
                btnSeletarScreen9D02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D02 ", "");
                btnSeletarScreen9D02.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9D03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9D03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D03 ";
                btnSeletarScreen9D03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D03 ", "");
                btnSeletarScreen9D03.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9D04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9D04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D04 ";
                btnSeletarScreen9D04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D04 ", "");
                btnSeletarScreen9D04.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9D05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9D05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D05 ";
                btnSeletarScreen9D05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D05 ", "");
                btnSeletarScreen9D05.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9D06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9D06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D06 ";
                btnSeletarScreen9D06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D06 ", "");
                btnSeletarScreen9D06.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9D07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9D07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D07 ";
                btnSeletarScreen9D07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D07 ", "");
                btnSeletarScreen9D07.BackColor = Color.Black;
            }
        }

        private void btnSeletarScreen9D08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnSeletarScreen9D08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D08 ";
                btnSeletarScreen9D08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D08 ", "");
                btnSeletarScreen9D08.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6A01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6A01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A01 ";
                btnTampinesScreen6A01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A01 ", "");
                btnTampinesScreen6A01.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6A02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6A02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A02 ";
                btnTampinesScreen6A02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A02 ", "");
                btnTampinesScreen6A02.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6A03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6A03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A03 ";
                btnTampinesScreen6A03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A03 ", "");
                btnTampinesScreen6A03.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6A04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6A04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A04 ";
                btnTampinesScreen6A04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A04 ", "");
                btnTampinesScreen6A04.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6A05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6A05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A05 ";
                btnTampinesScreen6A05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A05 ", "");
                btnTampinesScreen6A05.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6A06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6A06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A06 ";
                btnTampinesScreen6A06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A06 ", "");
                btnTampinesScreen6A06.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6A07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6A07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A07 ";
                btnTampinesScreen6A07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A07 ", "");
                btnTampinesScreen6A07.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6A08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6A08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A08 ";
                btnTampinesScreen6A08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A08 ", "");
                btnTampinesScreen6A08.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6B01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6B01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B01 ";
                btnTampinesScreen6B01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B01 ", "");
                btnTampinesScreen6B01.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6B02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6B02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B02 ";
                btnTampinesScreen6B02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B02 ", "");
                btnTampinesScreen6B02.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6B03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6B03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B03 ";
                btnTampinesScreen6B03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B03 ", "");
                btnTampinesScreen6B03.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6B04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6B04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B04 ";
                btnTampinesScreen6B04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B04 ", "");
                btnTampinesScreen6B04.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6B05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6B05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B05 ";
                btnTampinesScreen6B05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B05 ", "");
                btnTampinesScreen6B05.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6B06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6B06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B06 ";
                btnTampinesScreen6B06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B06 ", "");
                btnTampinesScreen6B06.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6B07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6B07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B07 ";
                btnTampinesScreen6B07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B07 ", "");
                btnTampinesScreen6B07.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6B08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6B08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B08 ";
                btnTampinesScreen6B08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B08 ", "");
                btnTampinesScreen6B08.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6C01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6C01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C01 ";
                btnTampinesScreen6C01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C01 ", "");
                btnTampinesScreen6C01.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6C02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6C02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C02 ";
                btnTampinesScreen6C02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C02 ", "");
                btnTampinesScreen6C02.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6C03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6C03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C03 ";
                btnTampinesScreen6C03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C03 ", "");
                btnTampinesScreen6C03.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6C04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6C04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C04 ";
                btnTampinesScreen6C04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C04 ", "");
                btnTampinesScreen6C04.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6C05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6C05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C05 ";
                btnTampinesScreen6C05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C05 ", "");
                btnTampinesScreen6C05.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6C06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6C06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C06 ";
                btnTampinesScreen6C06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C06 ", "");
                btnTampinesScreen6C06.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6C07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6C07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C07 ";
                btnTampinesScreen6C07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C07 ", "");
                btnTampinesScreen6C07.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6C08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6C08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C08 ";
                btnTampinesScreen6C08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C08 ", "");
                btnTampinesScreen6C08.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6D01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6D01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D01 ";
                btnTampinesScreen6D01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D01 ", "");
                btnTampinesScreen6D01.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6D02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6D02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D02 ";
                btnTampinesScreen6D02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D02 ", "");
                btnTampinesScreen6D02.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6D03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6D03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D03 ";
                btnTampinesScreen6D03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D03 ", "");
                btnTampinesScreen6D03.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6D04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6D04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D04 ";
                btnTampinesScreen6D04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D04 ", "");
                btnTampinesScreen6D04.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6D05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6D05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D05 ";
                btnTampinesScreen6D05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D05 ", "");
                btnTampinesScreen6D05.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6D06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6D06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D06 ";
                btnTampinesScreen6D06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D06 ", "");
                btnTampinesScreen6D06.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6D07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6D07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D07 ";
                btnTampinesScreen6D07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D07 ", "");
                btnTampinesScreen6D07.BackColor = Color.Black;
            }
        }

        private void btnTampinesScreen6D08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnTampinesScreen6D08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D08 ";
                btnTampinesScreen6D08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D08 ", "");
                btnTampinesScreen6D08.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10A01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10A01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A01 ";
                btnJurongPointScreen10A01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A01 ", "");
                btnJurongPointScreen10A01.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10A02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10A02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A02 ";
                btnJurongPointScreen10A02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A02 ", "");
                btnJurongPointScreen10A02.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10A03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10A03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A03 ";
                btnJurongPointScreen10A03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A03 ", "");
                btnJurongPointScreen10A03.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10A04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10A04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A04 ";
                btnJurongPointScreen10A04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A04 ", "");
                btnJurongPointScreen10A04.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10A05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10A05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A05 ";
                btnJurongPointScreen10A05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A05 ", "");
                btnJurongPointScreen10A05.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10A06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10A06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A06 ";
                btnJurongPointScreen10A06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A06 ", "");
                btnJurongPointScreen10A06.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10A07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10A07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A07 ";
                btnJurongPointScreen10A07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A07 ", "");
                btnJurongPointScreen10A07.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10A08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10A08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "A08 ";
                btnJurongPointScreen10A08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("A08 ", "");
                btnJurongPointScreen10A08.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10B01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10B01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B01 ";
                btnJurongPointScreen10B01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B01 ", "");
                btnJurongPointScreen10B01.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10B02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10B02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B02 ";
                btnJurongPointScreen10B02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B02 ", "");
                btnJurongPointScreen10B02.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10B03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10B03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B03 ";
                btnJurongPointScreen10B03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B03 ", "");
                btnJurongPointScreen10B03.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10B04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10B04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B04 ";
                btnJurongPointScreen10B04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B04 ", "");
                btnJurongPointScreen10B04.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10B05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10B05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B05 ";
                btnJurongPointScreen10B05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B05 ", "");
                btnJurongPointScreen10B05.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10B06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10B06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B06 ";
                btnJurongPointScreen10B06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B06 ", "");
                btnJurongPointScreen10B06.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10B07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10B07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B07 ";
                btnJurongPointScreen10B07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B07 ", "");
                btnJurongPointScreen10B07.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10B08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10B08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "B08 ";
                btnJurongPointScreen10B08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("B08 ", "");
                btnJurongPointScreen10B08.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10C01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10C01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C01 ";
                btnJurongPointScreen10C01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C01 ", "");
                btnJurongPointScreen10C01.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10C02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10C02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C02 ";
                btnJurongPointScreen10C02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C02 ", "");
                btnJurongPointScreen10C02.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10C03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10C03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C03 ";
                btnJurongPointScreen10C03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C03 ", "");
                btnJurongPointScreen10C03.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10C04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10C04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C04 ";
                btnJurongPointScreen10C04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C04 ", "");
                btnJurongPointScreen10C04.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10C05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10C05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C05 ";
                btnJurongPointScreen10C05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C05 ", "");
                btnJurongPointScreen10C05.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10C06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10C06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C06 ";
                btnJurongPointScreen10C06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C06 ", "");
                btnJurongPointScreen10C06.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10C07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10C07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C07 ";
                btnJurongPointScreen10C07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C07 ", "");
                btnJurongPointScreen10C07.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10C08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10C08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "C08 ";
                btnJurongPointScreen10C08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("C08 ", "");
                btnJurongPointScreen10C08.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10D01_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10D01.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D01 ";
                btnJurongPointScreen10D01.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D01 ", "");
                btnJurongPointScreen10D01.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10D02_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10D02.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D02 ";
                btnJurongPointScreen10D02.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D02 ", "");
                btnJurongPointScreen10D02.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10D03_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10D03.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D03 ";
                btnJurongPointScreen10D03.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D03 ", "");
                btnJurongPointScreen10D03.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10D04_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10D04.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D04 ";
                btnJurongPointScreen10D04.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D04 ", "");
                btnJurongPointScreen10D04.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10D05_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10D05.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D05 ";
                btnJurongPointScreen10D05.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D05 ", "");
                btnJurongPointScreen10D05.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10D06_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10D06.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D06 ";
                btnJurongPointScreen10D06.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D06 ", "");
                btnJurongPointScreen10D06.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10D07_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10D07.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D07 ";
                btnJurongPointScreen10D07.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D07 ", "");
                btnJurongPointScreen10D07.BackColor = Color.Black;
            }
        }

        private void btnJurongPointScreen10D08_Click(object sender, EventArgs e)
        {
            // if user wants to select the seat
            if (btnJurongPointScreen10D08.BackColor == Color.Black)
            {
                no_of_seats_selected += 1;
                seats_selected += "D08 ";
                btnJurongPointScreen10D08.BackColor = Color.DarkGray;
            }
            // if user wants to deselect the seat
            else
            {
                no_of_seats_selected -= 1;
                seats_selected = seats_selected.Replace("D08 ", "");
                btnJurongPointScreen10D08.BackColor = Color.Black;
            }
        }

        private void btnListCinemaSales_Click_1(object sender, EventArgs e)
        {
            rtbList.Clear();
            string heading;
            string display_sales = "";
            bool is_a_cinema_selected = false;

            heading = "Cinema ID".PadRight(20) + "No. of tickets sold".PadRight(20) + "Total sales\n";

            if (rdbC1.Checked)
            {
                is_a_cinema_selected = true;
                display_sales = C1.DisplayCinemaSales();
            }
            else if (rdbC2.Checked)
            {
                is_a_cinema_selected = true;
                display_sales = C2.DisplayCinemaSales();
            }
            else if (rdbC3.Checked)
            {
                is_a_cinema_selected = true;
                display_sales = C3.DisplayCinemaSales();
            }
            else if (rdbC4.Checked)
            {
                is_a_cinema_selected = true;
                display_sales = C4.DisplayCinemaSales();
            }
            else if (rdbC5.Checked)
            {
                is_a_cinema_selected = true;
                display_sales = C5.DisplayCinemaSales();
            }
            else if (rdbC6.Checked)
            {
                is_a_cinema_selected = true;
                display_sales = C6.DisplayCinemaSales();
            }
            else if (rdbC7.Checked)
            {
                is_a_cinema_selected = true;
                display_sales = C7.DisplayCinemaSales();
            }
            else if (rdbC8.Checked)
            {
                is_a_cinema_selected = true;
                display_sales = C8.DisplayCinemaSales();
            }
            else if (rdbC9.Checked)
            {
                is_a_cinema_selected = true;
                display_sales = C9.DisplayCinemaSales();
            }
            else if (rdbC10.Checked)
            {
                is_a_cinema_selected = true;
                display_sales = C10.DisplayCinemaSales();
            }
            else
            {
                MessageBox.Show("Please select a cinema to list its sales");
            }

            if (is_a_cinema_selected)
            {
                rtbList.AppendText(heading + display_sales);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _start_point = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            // clear all fields in check booking tab
            rtbSearch.Clear();
            txtSearchMobileNo.Clear();
            txtSearchEmail.Clear();
            lblTotalCost.Text = "__________";
        }

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // set txtContactNo to only accept numeric input
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        private void txtTicket_KeyPress(object sender, KeyPressEventArgs e)
        {
            // set txtTicket to only accept numeric input
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        private void txtSearchMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // set txtSearchMobileNo to only accept numeric input
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        private void rdbMobile_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                txtSearchMobileNo.Enabled = true;
                txtSearchMobileNo.Focus();
            }
            else
            {
                txtSearchMobileNo.Enabled = false;
                txtSearchMobileNo.Clear();
            }
        }

        private void rdbEmail_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                txtSearchEmail.Enabled = true;
                txtSearchEmail.Focus();
            }
            else
            {
                txtSearchEmail.Enabled = false;
                txtSearchEmail.Clear();
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // clear rtbList label
            rtbList.Clear();
            lblTotalPrice.Text = "__________";

            int find_record;
            bool record_found = false;
            string[] seperate_seats;

            try
            {
                find_record = int.Parse(txtRecordNo.Text);

                for (int i = 0; i < record_no.Length; i++)
                {
                    if (find_record == record_no[i] && name[i] != null)
                    {
                        record_found = true;

                        // split the respective seats element into elements of an array
                        seperate_seats = seats[i].Split(' ');

                        // update the number of tickets sold by the respective cinemas after clearing a record
                        if (cinema_details[i] == C1.DisplayCinemaInfo())
                        { 
                            C1.SetNoOfTicketsSold(C1.GetNoOfTicketsSold() - ticket[i]); // update no of tickets sold in c1 after clearing a record
                            C1.SetTotalSales(C1.GetNoOfTicketsSold() * 9.50f); // update total sales of c1 after clearing a record
                            total_sales[0] = C1.GetTotalSales();

                            // set seats for the respective cinema screen to available after clearing the respective record
                            if (movie[i] == movies_available[0] && date[i] == dates_available[0] && time[i] == times_available[0])
                            {
                                foreach (var btn in gbSuntecCityScreen1.Controls.OfType<Button>())
                                {
                                    foreach (string seat in seperate_seats)
                                    {
                                        if (btn.BackColor == Color.DimGray && !btn.Enabled && btn.Name.Contains(seat))
                                        {
                                            btn.Enabled = true;
                                            btn.BackColor = Color.Black;
                                        }
                                    }
                                }
                            }
                        }
                        else if (cinema_details[i] == C2.DisplayCinemaInfo())
                        {
                            C2.SetNoOfTicketsSold(C2.GetNoOfTicketsSold() - ticket[i]); // update no of tickets sold in c2 after clearing a record
                            C2.SetTotalSales(C2.GetNoOfTicketsSold() * 9.50f); // update total sales of c2 after clearing a record
                            total_sales[1] = C2.GetTotalSales();

                            // set seats for the respective cinema screen to available after clearing the respective record
                            if (movie[i] == movies_available[1] && date[i] == dates_available[1] && time[i] == times_available[2])
                            {
                                foreach (var btn in gbPlazaScreen5.Controls.OfType<Button>())
                                {
                                    foreach (string seat in seperate_seats)
                                    {
                                        if (btn.BackColor == Color.DimGray && !btn.Enabled && btn.Name.Contains(seat))
                                        {
                                            btn.Enabled = true;
                                            btn.BackColor = Color.Black;
                                        }
                                    }
                                }
                            }
                        }
                        else if (cinema_details[i] == C3.DisplayCinemaInfo())
                        {
                            C3.SetNoOfTicketsSold(C3.GetNoOfTicketsSold() - ticket[i]); // update no of tickets sold in c3 after clearing a record
                            C3.SetTotalSales(C3.GetNoOfTicketsSold() * 9.50f); // update total sales of c3 after clearing a record
                            total_sales[2] = C3.GetTotalSales();

                            // set seats for the respective cinema screen to available after clearing the respective record
                            if (movie[i] == movies_available[2] && date[i] == dates_available[2] && time[i] == times_available[1])
                            {
                                foreach (var btn in gbCitySquareScreen2.Controls.OfType<Button>())
                                {
                                    foreach (string seat in seperate_seats)
                                    {
                                        if (btn.BackColor == Color.DimGray && !btn.Enabled && btn.Name.Contains(seat))
                                        {
                                            btn.Enabled = true;
                                            btn.BackColor = Color.Black;
                                        }
                                    }
                                }
                            }                              
                        }
                        else if (cinema_details[i] == C4.DisplayCinemaInfo())
                        {
                            C4.SetNoOfTicketsSold(C4.GetNoOfTicketsSold() - ticket[i]); // update no of tickets sold in c4 after clearing a record
                            C4.SetTotalSales(C4.GetNoOfTicketsSold() * 9.50f); // update total sales of c4 after clearing a record
                            total_sales[3] = C4.GetTotalSales();

                            // set seats for the respective cinema screen to available after clearing the respective record
                            if (movie[i] == movies_available[3] && date[i] == dates_available[3] && time[i] == times_available[0])
                            {
                                foreach (var btn in gbBishanScreen3.Controls.OfType<Button>())
                                {
                                    foreach (string seat in seperate_seats)
                                    {
                                        if (btn.BackColor == Color.DimGray && !btn.Enabled && btn.Name.Contains(seat))
                                        {
                                            btn.Enabled = true;
                                            btn.BackColor = Color.Black;
                                        }
                                    }
                                }
                            }
                        }
                        else if (cinema_details[i] == C5.DisplayCinemaInfo())
                        {
                            C5.SetNoOfTicketsSold(C5.GetNoOfTicketsSold() - ticket[i]); // update no of tickets sold in c5 after clearing a record
                            C5.SetTotalSales(C5.GetNoOfTicketsSold() * 9.50f); // update total sales of c5 after clearing a record
                            total_sales[4] = C5.GetTotalSales();

                            // set seats for the respective cinema screen to available after clearing the respective record
                            if (movie[i] == movies_available[4] && date[i] == dates_available[0] && time[i] == times_available[0])
                            {
                                foreach (var btn in gbYishunScreen7.Controls.OfType<Button>())
                                {
                                    foreach (string seat in seperate_seats)
                                    {
                                        if (btn.BackColor == Color.DimGray && !btn.Enabled && btn.Name.Contains(seat))
                                        {
                                            btn.Enabled = true;
                                            btn.BackColor = Color.Black;
                                        }
                                    }
                                }
                            }
                        }
                        else if (cinema_details[i] == C6.DisplayCinemaInfo())
                        {
                            C6.SetNoOfTicketsSold(C6.GetNoOfTicketsSold() - ticket[i]); // update no of tickets sold in c6 after clearing a record
                            C6.SetTotalSales(C6.GetNoOfTicketsSold() * 9.50f); // update total sales of c6 after clearing a record
                            total_sales[5] = C6.GetTotalSales();

                            // set seats for the respective cinema screen to available after clearing the respective record
                            if (movie[i] == movies_available[5] && date[i] == dates_available[4] && time[i] == times_available[3])
                            {
                                foreach (var btn in gbnexScreen11.Controls.OfType<Button>())
                                {
                                    foreach (string seat in seperate_seats)
                                    {
                                        if (btn.BackColor == Color.DimGray && !btn.Enabled && btn.Name.Contains(seat))
                                        {
                                            btn.Enabled = true;
                                            btn.BackColor = Color.Black;
                                        }
                                    }
                                }
                            }
                        }
                        else if (cinema_details[i] == C7.DisplayCinemaInfo())
                        {
                            C7.SetNoOfTicketsSold(C7.GetNoOfTicketsSold() - ticket[i]); // update no of tickets sold in c7 after clearing a record
                            C7.SetTotalSales(C7.GetNoOfTicketsSold() * 9.50f); // update total sales of c7 after clearing a record
                            total_sales[6] = C7.GetTotalSales();

                            // set seats for the respective cinema screen to available after clearing the respective record
                            if (movie[i] == movies_available[6] && date[i] == dates_available[4] && time[i] == times_available[1])
                            {
                                foreach (var btn in gbJCubeScreen4.Controls.OfType<Button>())
                                {
                                    foreach (string seat in seperate_seats)
                                    {
                                        if (btn.BackColor == Color.DimGray && !btn.Enabled && btn.Name.Contains(seat))
                                        {
                                            btn.Enabled = true;
                                            btn.BackColor = Color.Black;
                                        }
                                    }
                                }
                            }
                        }
                        else if (cinema_details[i] == C8.DisplayCinemaInfo())
                        {
                            C8.SetNoOfTicketsSold(C8.GetNoOfTicketsSold() - ticket[i]); // update no of tickets sold in c8 after clearing a record
                            C8.SetTotalSales(C8.GetNoOfTicketsSold() * 9.50f); // update total sales of c8 after clearing a record
                            total_sales[7] = C8.GetTotalSales();

                            // set seats for the respective cinema screen to available after clearing the respective record
                            if (movie[i] == movies_available[7] && date[i] == dates_available[5] && time[i] == times_available[4])
                            {
                                foreach (var btn in gbSeletarScreen9.Controls.OfType<Button>())
                                {
                                    foreach (string seat in seperate_seats)
                                    {
                                        if (btn.BackColor == Color.DimGray && !btn.Enabled && btn.Name.Contains(seat))
                                        {
                                            btn.Enabled = true;
                                            btn.BackColor = Color.Black;
                                        }
                                    }
                                }
                            }
                        }
                        else if (cinema_details[i] == C9.DisplayCinemaInfo())
                        {
                            C9.SetNoOfTicketsSold(C9.GetNoOfTicketsSold() - ticket[i]); // update no of tickets sold in c9 after clearing a record
                            C9.SetTotalSales(C8.GetNoOfTicketsSold() * 9.50f); // update total sales of c9 after clearing a record
                            total_sales[8] = C9.GetTotalSales();

                            // set seats for the respective cinema screen to available after clearing the respective record
                            if (movie[i] == movies_available[8] && date[i] == dates_available[5] && time[i] == times_available[3])
                            {
                                foreach (var btn in gbTampinesScreen6.Controls.OfType<Button>())
                                {
                                    foreach (string seat in seperate_seats)
                                    {
                                        if (btn.BackColor == Color.DimGray && !btn.Enabled && btn.Name.Contains(seat))
                                        {
                                            btn.Enabled = true;
                                            btn.BackColor = Color.Black;
                                        }
                                    }
                                }
                            }
                        }
                        else if (cinema_details[i] == C10.DisplayCinemaInfo())
                        {
                            C10.SetNoOfTicketsSold(C10.GetNoOfTicketsSold() - ticket[i]); // update no of tickets sold in c10 after clearing a record
                            C10.SetTotalSales(C10.GetNoOfTicketsSold() * 9.50f); // update total sales of c10 after clearing a record
                            total_sales[9] = C10.GetTotalSales();

                            // set seats for the respective cinema screen to available after clearing the respective record
                            if (movie[i] == movies_available[9] && date[i] == dates_available[4] && time[i] == times_available[4])
                            {
                                foreach (var btn in gbJurongPointScreen10.Controls.OfType<Button>())
                                {
                                    foreach (string seat in seperate_seats)
                                    {
                                        if (btn.BackColor == Color.DimGray && !btn.Enabled && btn.Name.Contains(seat))
                                        {
                                            btn.Enabled = true;
                                            btn.BackColor = Color.Black;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            // do nothing
                        }
                  
                        name[i] = null;
                        contact_no[i] = 0;
                        email[i] = null;
                        cinema_details[i] = null;
                        movie[i] = null;
                        date[i] = null;
                        time[i] = null;
                        ticket[i] = 0;
                        seats[i] = null;
                        total[i] = 0;
                        payment[i] = null;
                        MessageBox.Show("Record successfully cleared!");
                        break;
                    }
                }

                if (record_found == false)
                {
                    MessageBox.Show("No such record found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void txtRecordNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }
    }
}
